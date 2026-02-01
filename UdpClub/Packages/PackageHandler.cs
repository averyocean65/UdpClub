using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using UdpClub.Utils;
using static UdpClub.Utils.DebugUtils;

namespace UdpClub.Packages {
	public static class PackageHandler {
		public static Action<BasePackage> OnPackageSend = OnPacketSend;
		public static Action<byte[], IPEndPoint> OnMessageReceived = ParsePackage;
		public static Action<BasePackage> OnPackageParsed;
		
		private static void OnPacketSend(BasePackage obj) {
			DebugPrintln($"Sending Package: {BitConverter.ToString(obj.ToBytes())}");
		}

		private static void ParsePackage(byte[] data, IPEndPoint ep) {
			byte[] decompressed = data.Decompress();
			
			if (decompressed.Length < 1) {
				throw new ArgumentException($"{nameof(decompressed)} is of size 0");
			}
			DebugPrintln($"Decompressed bytes: {BitConverter.ToString(decompressed)}");

			byte id = decompressed[0];
			Type packageType = PackageMap.GetPackageType(id);

			if (packageType == null) {
				throw new ApplicationException($"Failed to get package from id: {id}");
			}

			ConstructorInfo[] constructors = packageType.GetConstructors();
			ConstructorInfo constructor = constructors
				.Where(ByteArrayConstructorPredicate)
				.FirstOrDefault();
			DebugPrintln($"Constructor is null? {constructor == null}");
			
			if (constructor == null) {
				throw new ApplicationException($"Failed to find valid constructor for {packageType.FullName}");
			}

			DebugPrintln("Calling constructor!");
			BasePackage package = (BasePackage)constructor.Invoke(new object[] { decompressed, ep });
			DebugPrintln($"Parsed Package: {package.Id}");
			
			OnPackageParsed.Invoke(package);
		}

		private static bool ByteArrayConstructorPredicate(ConstructorInfo info) {
			ParameterInfo[] parameters = info.GetParameters();


			DebugPrintln($"-- DEBUGGING: {info.Name} --");
#if DEBUG
			foreach (ParameterInfo pInfo in parameters) {
				DebugPrintln($"\t{pInfo.ParameterType}");
			}
#endif
			
			bool parameterMatch = parameters.Length == 2 &&
			                      parameters[0].ParameterType == typeof(byte[]) &&
			                      parameters[1].ParameterType == typeof(IPEndPoint);
			
			bool constructorMatch = info.IsPublic && info.IsConstructor;
			
			DebugPrintln($"is a constructor: {constructorMatch}");
			DebugPrintln($"args match: {parameterMatch}");

			return parameterMatch && constructorMatch;	
		}

		// NOTE: Purposefully disregarding function overloads here
		
		/// <summary>
		/// Sends a package to a specific IPEndPoint
		/// </summary>
		/// <param name="client">The sending client</param>
		/// <param name="endPoint">The IP to send to (disregard if you're a client communicating to a server)</param>
		/// <param name="package">The data package to send</param>
		public static void SendPackage(UdpBase client, IPEndPoint endPoint, BasePackage package) {
			OnPackageSend.Invoke(package);
			byte[] data = package.ToBytes().Compress();
			client.Send(data, endPoint);
		}
		
		/// <summary>
		/// Sends a package to a specific set of IPEndPoints
		/// </summary>
		/// <param name="client">The sending client</param>
		/// <param name="endPoints">The list of IPs to send to</param>
		/// <param name="package">The data package to send</param>
		public static void SendPackageToAll(UdpBase client, IEnumerable<IPEndPoint> endPoints, BasePackage package) {
			foreach (IPEndPoint ep in endPoints) {
				SendPackage(client, ep, package);
			}
		}

		/// <summary>
		/// Sends a package to a specific set of IPEndPoints with an exception
		/// </summary>
		/// <param name="client">The sending client</param>
		/// <param name="endPoints">The list of IPs to send to</param>
		/// <param name="exception">The excluded IP endpoint</param>
		/// <param name="package">The data package to send</param>
		public static void SendPackageToAllExcept(UdpBase client, IEnumerable<IPEndPoint> endPoints, IPEndPoint exception,
			BasePackage package) {
			foreach (IPEndPoint ep in endPoints) {
				if (Equals(ep, exception)) continue;
				SendPackage(client, ep, package);
			}
		}
	}
}