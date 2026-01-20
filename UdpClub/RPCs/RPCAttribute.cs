using System;

namespace UdpClub.RPCs
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public sealed class RPCAttribute : Attribute {
        public string Id { get; private set; }
        
        public RPCAttribute(string id) {
            Id = id;
            RPCManager.Subscribe(this);
        }
    }
}