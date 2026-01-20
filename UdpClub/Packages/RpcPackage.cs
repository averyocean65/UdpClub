using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace UdpClub.Packages {
    public class RpcPackage : BasePackage {
        public string rpcId;
        
        public RpcPackage(byte[] data, IPEndPoint ep) : base(data, ep) {
            if (!IsIdValid(typeof(RpcPackage))) {
                throw new ArgumentException("ID from data bytes is invalid!");
            }

            rpcId = Encoding.Default.GetString(UnhandledData);
        }

        public RpcPackage(string rpcId) {
            Id = PackageMap.GetPackageId(typeof(RpcPackage));
            this.rpcId = rpcId;
        }
        
        public override byte[] ToBytes() {
            byte[] rpcIdBytes = Encoding.Default.GetBytes(rpcId);
            List<byte> data = new List<byte>() { Id };
            data.AddRange(rpcIdBytes);
            return data.ToArray();
        }
    }
}