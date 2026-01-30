using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;
using UdpClub.Packages;
using UdpClub.RPCs;

using static UdpClub.Utils.DebugUtils;

namespace UdpClub {
	public abstract class UdpBase {
		public readonly List<IPEndPoint> RegisteredIPs = new List<IPEndPoint>();
		
		/// <summary>
		/// The assembly of the calling program, required for RPCs.
		/// </summary>
		public static Assembly ProgramAssembly;
		
		/// <summary>
		/// The bare knuckles UDP client.
		/// </summary>
		protected UdpClient InnerClient;
		
		/// <summary>
		/// The hostname / IP address of the connection.
		/// </summary>
		public string Hostname { get; private set; }
		
		/// <summary>
		/// The port of the connection.
		/// </summary>
		public int Port { get; private set; }
		
		/// <summary>
		/// Decides whether to treat certain operations like a server or like a client.
		/// </summary>
		public bool IsServer { get; private set; }

		public Action OnConnected;
		public Action OnDisconnected;
		
		/// <summary>
		/// Creates a new instance of class UdpBase.
		/// </summary>
		/// <param name="programAssembly">The assembly of the program initializing a UdpBase instance, see <see cref="ProgramAssembly"/></param>
		/// <param name="hostname">The address for the connection. Assigned to <see cref="Hostname"/></param>
		/// <param name="port">The port that the client will be listening on / sending to. Assigned to <see cref="Port"/></param>
		/// <param name="isServer">Whether to internally treat the instance as a server or client. Assigned to <see cref="IsServer"/></param>
		protected UdpBase(Assembly programAssembly, string hostname, int port, bool isServer) {
			ProgramAssembly = programAssembly;
			Hostname = hostname;
			Port = port;
			IsServer = isServer;

			RegisteredIPs = new List<IPEndPoint>();
			
			InitializeRpc();
			PackageHandler.OnPackageParsed += HandleRpcPackage;
			
			if (IsServer) {
				// server-specific constructor
				InnerClient = new UdpClient(Port);
				return;
			}
			
			InnerClient = new UdpClient();
		}

		~UdpBase() {
			Disconnect();
			InnerClient.Dispose();
		}

		protected void InitializeRpc() {
			foreach (Type t in ProgramAssembly.GetTypes()) {
				DebugPrintln($"Scanning type: {t.Name}");
				foreach (MethodInfo m in t.GetMethods(BindingFlags.Static | BindingFlags.Public)) {
					DebugPrintln($"Scanning method: {m.Name}");
					if (!m.IsPublic || !m.IsStatic) {
						continue;
					}

					var rpc = m.GetCustomAttributes<RpcAttribute>().FirstOrDefault();
					if (rpc != null) {
						RpcManager.Subscribe(rpc);
					}
				}
			}
		}

		private void HandleRpcPackage(BasePackage obj) {
			DebugPrintln($"HandleRpcPackage: {obj.Id}");
			
			if (obj.Id == PackageMap.GetPackageId(typeof(RpcPackage))) {
				RpcPackage rpc = obj as RpcPackage;
				DebugPrintln($"Handling RPC Package: {rpc.RpcId}");
				
				// handle package distribution
				if (IsServer) {
					DebugPrintln($"Distributing RPC Package: {rpc.RpcId}");
					if (rpc.Loopback) {
						PackageHandler.SendPackageToAll(this, RegisteredIPs, rpc);
					}
					else {
						PackageHandler.SendPackageToAllExcept(this, RegisteredIPs, rpc.Sender, rpc);
					}
				}
				
				RpcManager.CallLocalRpc(rpc.RpcId, rpc.Parameter);
			}
		}

		/// <summary>
		/// Connects to a specified server.
		/// </summary>
		/// <returns>A boolean stating whether the connection attempt was successful or not.</returns>
		public virtual bool Connect() {
			if (InnerClient.Client.Connected) {
				Console.Error.WriteLine("You're already connected!");
				return false;
			}
			
			if (IsServer) {
				Task.Run(NetworkLoop);
				return true;
			}

			try {
				InnerClient.Connect(Hostname, Port);
				Task.Run(NetworkLoop);
			}
			catch(Exception ex) {
				Console.Error.WriteLine(ex);
				return false;
			}
			
			OnConnected.Invoke();
			return true;
		}
		
		public virtual void Disconnect() {
			if (InnerClient == null) {
				return;
			}
			
			if (!InnerClient.Client.Connected) {
				return;
			}
			InnerClient.Close();

			if (OnDisconnected != null) {
				OnDisconnected.Invoke();
			}
		}

		/// <summary>
		/// Handles running different functions such as <see cref="HandlePackage"/>
		/// </summary>
		private void NetworkLoop() {
			IPEndPoint ep = null;
			while (InnerClient.Client.Connected || IsServer) {
				Console.WriteLine("Waiting for new packages...");
				HandlePackage(ref ep);
			}
			
			Console.WriteLine("Connected ended!");
		}

		protected virtual void HandlePackage(ref IPEndPoint endPoint) {
			byte[] received = InnerClient.Receive(ref endPoint);
			
			DebugPrintln($"Bytes from {endPoint.Address}: {BitConverter.ToString(received)}");
			if (!RegisteredIPs.Contains(endPoint)) {
				RegisteredIPs.Add(endPoint);
			}
			
			PackageHandler.OnMessageReceived.Invoke(received, endPoint);
		}

		public virtual void Send(byte[] data, IPEndPoint ep) {
			if (!IsServer) {
				InnerClient.Send(data, data.Length);
				return;
			}

			InnerClient.Send(data, data.Length, ep);
		}
	}
}