using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UdpClub.Utils;

namespace UdpClub.RPCs
{
    public static class RPCManager {
        private static readonly Dictionary<string, RPCAttribute> RpcProcedures = new Dictionary<string, RPCAttribute>();

        public static Action<RPCAttribute> ExecuteRpc;
        
        public static bool IsSubscribed(string id) {
            return RpcProcedures.ContainsKey(id);
        }

        public static void Subscribe(RPCAttribute rpc) {
            if (IsSubscribed(rpc.Id)) {
                return;
            }
            
#if _DEBUG
            Console.WriteLine($"Subscribing RPC: {rpc.Id}");
#endif
            RpcProcedures.Add(rpc.Id, rpc);
        }

        public static RPCAttribute GetRpc(string id) {
            return RpcProcedures
                .FirstOrDefault(x => x.Key == id)
                .Value;
        }
        
        public static void CallRpc(string id) {
            RPCAttribute rpc = GetRpc(id);
            
#if _DEBUG
            Console.WriteLine($"Executing RPC: {id}");
            Console.WriteLine($"Registered RPCs: {RpcProcedures.Count}");
            Console.WriteLine($"RPC is null? {rpc == null}");
#endif
            
            if (rpc == null) {
                return;
            }
            ExecuteRpc.Invoke(rpc);
        }
        
        public static void InvokeRpcInAssembly(Assembly asm, string id) {
#if _DEBUG
            Console.WriteLine($"Assembly: {asm.FullName}");
#endif
            
            foreach (Type t in asm.GetTypes()) {
                IEnumerable<MethodInfo> methods = t.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .Where(x => x.GetCustomAttributes<RPCAttribute>().Any());
                
                foreach (MethodInfo m in methods) {
                    if (!m.IsStatic) {
                        continue;
                    }

                    if (m.GetParameters().Length == 0) {
                        m.Invoke(null, null);
                    }
                    else {
                        throw new Exception("TODO: figure out parameters");
                    }
                }
            }
        }
        
        public static void InvokeRpcInAssembly(Assembly asm, RPCAttribute rpc) {
            InvokeRpcInAssembly(asm, rpc.Id);
        }
    }
}