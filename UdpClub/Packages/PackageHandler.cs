using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using UdpClub.RPCs;

namespace UdpClub.Packages {
	public static class PackageHandler {
		public static Action<BasePackage> OnPackageSend;
		public static Action<byte[], IPEndPoint> OnMessageReceived = ParsePackage;
		public static Action<BasePackage> OnPackageParsed;

		private static void ParsePackage(byte[] data, IPEndPoint ep) {
			if (data.Length < 1) {
				throw new ArgumentException($"{nameof(data)} is of size 0");
			}

			byte id = data[0];
			Type packageType = PackageMap.GetPackageType(id);

			if (packageType == null) {
				throw new ApplicationException($"Failed to get package from id: {id}");
			}

			ConstructorInfo[] constructors = packageType.GetConstructors();
			ConstructorInfo constructor = constructors
				.Where(ByteArrayConstructorPredicate)
				.FirstOrDefault();

			if (constructor == null) {
				throw new ApplicationException($"Failed to find valid constructor for {packageType.FullName}");
			}

			BasePackage package = (BasePackage)constructor.Invoke(new object[] { data, ep });
			
			// special case for RpcPackages
            if (id == PackageMap.GetPackageId(typeof(RpcPackage))) {
	            RpcPackage rpc = package as RpcPackage;
	            
#if _DEBUG
	            Console.WriteLine($"Executing RPC Package: {rpc.rpcId}");
#endif
	            
	            RpcManager.CallRpc(rpc.RpcId);
            }
			
			OnPackageParsed.Invoke(package);
		}

		private static bool ByteArrayConstructorPredicate(ConstructorInfo info) {
			ParameterInfo[] parameters = info.GetParameters();

#if _DEBUG
			Console.WriteLine($"-- DEBUGGING: {info.Name} --");
			foreach (ParameterInfo pInfo in parameters) {
				Console.WriteLine($"\t{pInfo.ParameterType}");
			}
#endif
			
			bool parameterMatch = parameters.Length == 2 &&
			                      parameters[0].ParameterType == typeof(byte[]) &&
			                      parameters[1].ParameterType == typeof(IPEndPoint);
			
			bool constructorMatch = info.IsPublic && info.IsConstructor;
			
#if _DEBUG
			Console.WriteLine($"is a constructor: {constructorMatch}");
			Console.WriteLine($"args match: {parameterMatch}");
#endif

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
			byte[] data = package.ToBytes();
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