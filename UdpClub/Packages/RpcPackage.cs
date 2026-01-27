using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UdpClub.Utils;

namespace UdpClub.Packages {
    public class RpcPackage : BasePackage {
        private const byte Separator = 0xFF;
        
        public readonly string RpcId;
        public readonly object Parameter;
        public readonly bool Loopback;
        
        public RpcPackage(byte[] data, IPEndPoint ep) : base(data, ep) {
            if (!IsIdValid(typeof(RpcPackage))) {
                throw new ArgumentException("ID from data bytes is invalid!");
            }

            if (UnhandledData.Length < 2) {
                throw new ArgumentException("Insufficient data for RPC Package!");
            }
            
            Loopback = ByteUtils.ByteToBool(UnhandledData[0]);

            int idx = Array.IndexOf(UnhandledData, Separator);
            
            if (idx < 0) {
                RpcId = Encoding.Default.GetString(UnhandledData.Subarray(1));
                Parameter = null;
            }
            else {
                RpcId = Encoding.Default.GetString(UnhandledData.Subarray(1, idx - 1));
                Parameter = ByteUtils.FromByteArray<object>(UnhandledData.Subarray(idx));
            }
            
#if DEBUG
            Console.WriteLine($"Separator idx: {idx}");
            Console.WriteLine("RPC ID: " + RpcId);
#endif
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

            if (Parameter != null) {
                data.Add(Separator);
                data.AddRange(ByteUtils.ToByteArray(Parameter));
            }
            
            return data.ToArray();
        }
    }
}