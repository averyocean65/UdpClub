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
            Console.WriteLine($"Subscribing RPC: {rpc.Id}");
            if (IsSubscribed(rpc.Id)) {
                return;
            }
            
            RpcProcedures.Add(rpc.Id, rpc);
        }

        public static RPCAttribute GetRpc(string id) {
            return RpcProcedures
                .FirstOrDefault(x => x.Key == id)
                .Value;
        }
        
        public static void CallRpc(string id) {
            Console.WriteLine($"Executing RPC: {id}");
            Console.WriteLine($"Registered RPCs: {RpcProcedures.Count}");
            RPCAttribute rpc = GetRpc(id);
            
            Console.WriteLine($"RPC is default: {rpc == default}");
            if (rpc == default) {
                return;
            }
            ExecuteRpc.Invoke(rpc);
        }
        
        public static void InvokeRpcInAssembly(Assembly asm, string id) {
            Console.WriteLine($"Assembly: {asm.FullName}");
            
            // from: https://stackoverflow.com/questions/65479133/c-sharp-attributes-and-methods
            IEnumerable<Type> typesWithMethod = from t in asm.GetTypes()
                where t.GetCustomAttributes<RPCAttribute>().Any(x => x.Id == id)
                select t;

            foreach (Type t in typesWithMethod) {
                MethodInfo[] methods = t.GetMethods();
                foreach (MethodInfo m in methods) {
                    if (!m.GetCustomAttributes().Any(x => x is RPCAttribute)) {
                        continue;
                    }
                    
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