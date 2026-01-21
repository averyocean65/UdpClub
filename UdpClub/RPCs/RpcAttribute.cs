using System;

namespace UdpClub.RPCs
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class RpcAttribute : Attribute {
        public string Id { get; private set; }
        
        public RpcAttribute(string id) {
            Id = id;
        }
    }
}