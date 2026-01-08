using System;
using System.Linq;
using System.Net;
using System.Reflection;

namespace UdpClub.Packages {
	public static class PackageHandler {
		public static Action<BasePackage> OnPackageSend;
		public static Action<byte[]> OnMessageReceived = ParsePackage;
		public static Action<BasePackage> OnPackageParsed;

		private static void ParsePackage(byte[] data) {
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

			BasePackage package = (BasePackage)constructor.Invoke(new object[] { data });
			OnPackageParsed.Invoke(package);
		}

		private static bool ByteArrayConstructorPredicate(ConstructorInfo info) {
			ParameterInfo[] parameters = info.GetParameters();
			bool parameterMatch = parameters.Length == 1 && parameters[0].ParameterType == typeof(byte[]);
			bool constructorMath = info.IsPublic && info.IsConstructor;

			return parameterMatch && constructorMath;	
		}

		public static void SendPackage(UdpBase client, IPEndPoint endPoint, BasePackage package) {
			byte[] data = package.ToBytes();
			client.Send(data, endPoint);
		}
	}
}