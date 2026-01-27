using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UdpClub.Utils;

namespace UdpClub.RPCs
{
    public static class RpcManager {
        private static readonly Dictionary<string, RpcAttribute> RpcProcedures = new Dictionary<string, RpcAttribute>();

        public static Action<RpcAttribute, object> ExecuteRpc = InvokeRpc;
        
        public static bool IsSubscribed(string id) {
            return RpcProcedures.ContainsKey(id);
        }

        public static void Subscribe(RpcAttribute rpc) {
            if (IsSubscribed(rpc.Id)) {
                return;
            }
            
#if DEBUG
            Console.WriteLine($"Subscribing RPC: {rpc.Id}");
#endif
            RpcProcedures.Add(rpc.Id, rpc);
        }

        public static RpcAttribute GetRpc(string id) {
#if DEBUG
            Console.WriteLine($"Getting RPC: {id}");
#endif
            
            return RpcProcedures
                .FirstOrDefault(x => x.Key == id)
                .Value;
        }
        
        public static void CallRpc(string id, object parameter) {
            RpcAttribute rpc = GetRpc(id);
            
#if DEBUG
            Console.WriteLine($"Executing RPC: {id}");
            Console.WriteLine($"Registered RPCs: {RpcProcedures.Count}");
            Console.WriteLine($"RPC is null? {rpc == null}");
#endif
            
            if (rpc == null) {
                return;
            }
            ExecuteRpc.Invoke(rpc, parameter);
        }
        
        public static void InvokeRpc(Assembly asm, string id, object parameter) {
#if DEBUG
            Console.WriteLine($"Assembly: {asm.FullName}");
#endif
            
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
        
        public static void InvokeRpc(RpcAttribute rpc, object parameter) {
            InvokeRpc(UdpBase.ProgramAssembly, rpc.Id, parameter);
        }
        
        public static void InvokeRpc(string id, object parameter) {
            InvokeRpc(UdpBase.ProgramAssembly, id, parameter);
        }
    }
}