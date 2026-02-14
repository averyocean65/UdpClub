using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UdpClub.Utils;

using static UdpClub.Utils.DebugUtils;

namespace UdpClub.Packages {
    public class RpcPackage : GenericPackage<RpcPackage> {
        private const byte Separator = 0xFF;
        
        public readonly string RpcId;
        public readonly object Parameter;
        public readonly bool Loopback;
        public readonly bool RunOnServer;
        
        public RpcPackage(byte[] data, IPEndPoint ep) : base(data, ep) {
            DebugPrintln("RPC package is valid!");

            if (UnhandledData.Length < 3) {
                throw new ArgumentException("Insufficient data for RPC Package!");
            }
            
            Loopback = ByteUtils.ByteToBool(UnhandledData[0]);
            RunOnServer = ByteUtils.ByteToBool(UnhandledData[1]);

            int start = 2;
            int idx = Array.IndexOf(UnhandledData, Separator);
            
            DebugPrintln($"Running subarray. Valid parameter? {idx < 1}");
            
            if (idx < 1) {
                RpcId = Encoding.UTF8.GetString(UnhandledData.Subarray(start));
                Parameter = null;
            }
            else {
                RpcId = Encoding.UTF8.GetString(UnhandledData.Subarray(start, idx - start));
                Parameter = ByteUtils.FromByteArray<object>(UnhandledData.Subarray(idx + 1));
            }
            
            DebugPrintln($"Separator idx: {idx}");
            DebugPrintln("RPC ID: " + RpcId);
            DebugPrintln($"Parameter: {Parameter}");
            DebugPrintln($"Loopback: {Loopback}");
        }

        public RpcPackage(string rpcId, object parameter, bool loopback = false, bool runOnServer = true) {
            this.RpcId = rpcId;
            this.Loopback = loopback;
            this.Parameter = parameter;
            this.RunOnServer = runOnServer;
        }
        
        public RpcPackage(string rpcId, bool loopback = false, bool runOnServer = true) : this(rpcId, null, loopback, runOnServer) { }
        
        public override byte[] ToBytes() {
            byte[] rpcIdBytes = Encoding.UTF8.GetBytes(RpcId);
            List<byte> data = new List<byte> {
                Id,
                ByteUtils.BoolToByte(Loopback),
                ByteUtils.BoolToByte(RunOnServer)
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