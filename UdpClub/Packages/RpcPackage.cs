using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UdpClub.Utils;

namespace UdpClub.Packages {
    public class RpcPackage : BasePackage {
        public readonly string RpcId;
        public readonly bool Loopback;
        
        public RpcPackage(byte[] data, IPEndPoint ep) : base(data, ep) {
            if (!IsIdValid(typeof(RpcPackage))) {
                throw new ArgumentException("ID from data bytes is invalid!");
            }

            if (UnhandledData.Length < 2) {
                throw new ArgumentException("Insufficient data for RPC Package!");
            }
            
            Loopback = ByteUtils.ByteToBool(UnhandledData[0]);
            RpcId = Encoding.Default.GetString(UnhandledData.Subarray(1));
        }

        public RpcPackage(string rpcId, bool loopback = false) {
            Id = PackageMap.GetPackageId(typeof(RpcPackage));
            this.RpcId = rpcId;
            this.Loopback = loopback;
        }
        
        public override byte[] ToBytes() {
            byte[] rpcIdBytes = Encoding.Default.GetBytes(RpcId);
            List<byte> data = new List<byte> {
                Id,
                ByteUtils.BoolToByte(Loopback)
            };
            data.AddRange(rpcIdBytes);
            return data.ToArray();
        }
    }
}