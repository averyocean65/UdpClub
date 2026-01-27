using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UdpClub.Utils;

using static UdpClub.Utils.DebugUtils;

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
            
            DebugPrintln("RPC package is valid!");

            if (UnhandledData.Length < 2) {
                throw new ArgumentException("Insufficient data for RPC Package!");
            }
            
            Loopback = ByteUtils.ByteToBool(UnhandledData[0]);
            int idx = Array.IndexOf(UnhandledData, Separator);
            
            DebugPrintln($"Running subarray. Valid parameter? {idx < 1}");
            
            if (idx < 1) {
                RpcId = Encoding.Default.GetString(UnhandledData.Subarray(1));
                Parameter = null;
            }
            else {
                RpcId = Encoding.Default.GetString(UnhandledData.Subarray(1, idx - 1));
                Parameter = ByteUtils.FromByteArray<object>(UnhandledData.Subarray(idx + 1));
            }
            
            DebugPrintln($"Separator idx: {idx}");
            DebugPrintln("RPC ID: " + RpcId);
            DebugPrintln($"Parameter: {Parameter}");
            DebugPrintln($"Loopback: {Loopback}");
        }

        public RpcPackage(string rpcId, object parameter, bool loopback = false) {
            Id = PackageMap.GetPackageId(typeof(RpcPackage));
            this.RpcId = rpcId;
            this.Loopback = loopback;
            this.Parameter = parameter;
        }
        
        public RpcPackage(string rpcId, bool loopback = false) : this(rpcId, null, loopback) {
            
        }
        
        public override byte[] ToBytes() {
            byte[] rpcIdBytes = Encoding.Default.GetBytes(RpcId);
            List<byte> data = new List<byte> {
                Id,
                ByteUtils.BoolToByte(Loopback)
            };
            data.AddRange(rpcIdBytes);

            byte[] parameterBytes = ByteUtils.ToByteArray(Parameter);
            if (Parameter != null) {
                data.Add(Separator);
                data.AddRange(parameterBytes);
            }
            
            return data.ToArray();
        }
    }
}