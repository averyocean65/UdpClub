using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using UdpClub.Packages;
using static UdpClub.Utils.DebugUtils;

namespace UdpClub.RPCs
{
    public static class RpcManager {
        private static readonly Dictionary<string, RpcAttribute> RpcProcedures = new Dictionary<string, RpcAttribute>();

        public static Action<RpcAttribute, object> ExecuteLocalRpc = InvokeRpcFromId;
        
        public static bool IsSubscribed(string id) {
            return RpcProcedures.ContainsKey(id);
        }

        public static void Subscribe(RpcAttribute rpc) {
            if (IsSubscribed(rpc.Id)) {
                return;
            }
            
            DebugPrintln($"Subscribing RPC: {rpc.Id}");
            RpcProcedures.Add(rpc.Id, rpc);
        }

        public static RpcAttribute GetRpcFromRegistry(string id) {
            DebugPrintln($"Receiving RPC from registry: {id}");
            
            return RpcProcedures
                .FirstOrDefault(x => x.Key == id)
                .Value;
        }
        
        public static void CallLocalRpc(string id, object parameter) {
            RpcAttribute rpc = GetRpcFromRegistry(id);
            
            DebugPrintln($"Executing RPC: {id}");
            DebugPrintln($"Registered RPCs: {RpcProcedures.Count}");
            DebugPrintln($"RPC is null? {rpc == null}");
            
            if (rpc == null) {
                return;
            }
            ExecuteLocalRpc.Invoke(rpc, parameter);
        }
        
        public static void InvokeRpcFromId(Assembly asm, string id, object parameter) {
            DebugPrintln($"Assembly: {asm.FullName}");
            
            foreach (Type t in asm.GetTypes()) {
                IEnumerable<MethodInfo> methods = t.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .Where(x => x.GetCustomAttributes<RpcAttribute>().Any(y => y.Id == id));
                
                foreach (MethodInfo m in methods) {
                    if (!m.IsStatic) {
                        continue;
                    }

                    if (m.GetParameters().Length == 0) {
                        m.Invoke(null, null);
                    }
                    else {
                        m.Invoke(null, new[] { parameter });
                    }
                }
            }
        }
        
        public static void InvokeRpcFromId(RpcAttribute rpc, object parameter) {
            InvokeRpcFromId(UdpBase.ProgramAssembly, rpc.Id, parameter);
        }
        
        public static void InvokeRpcFromId(string id, object parameter) {
            InvokeRpcFromId(UdpBase.ProgramAssembly, id, parameter);
        }

        public static void BroadcastRpc(UdpBase client, IPEndPoint ep, string rpcName, object parameter, bool loopback = false) {
            RpcPackage package = new RpcPackage(rpcName, parameter, loopback);
            PackageHandler.SendPackage(client, ep, package);
        }
        
        public static void BroadcastRpc(UdpBase client, string rpcName, object parameter, bool loopback = false) {
            RpcPackage package = new RpcPackage(rpcName, parameter, loopback);
            PackageHandler.SendPackage(client, null, package);
        }
        
        public static void BroadcastRpcToClients(UdpBase client, IEnumerable<IPEndPoint> receivers, string rpcName, object parameter) {
            RpcPackage package = new RpcPackage(rpcName, parameter);
            PackageHandler.SendPackageToAll(client, receivers, package);
        }
        
        public static void BroadcastRpcToClients(UdpBase client, IEnumerable<IPEndPoint> receivers, IPEndPoint exception, string rpcName, object parameter) {
            RpcPackage package = new RpcPackage(rpcName, parameter);
            PackageHandler.SendPackageToAllExcept(client, receivers, exception, package);
        }
    }
}