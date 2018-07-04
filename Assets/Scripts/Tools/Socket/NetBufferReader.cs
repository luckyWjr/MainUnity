using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tool {
    class NetBufferReader {

        MemoryStream m_stream = null;
        BinaryReader m_reader = null;

        ushort m_dataLength;

        public NetBufferReader(byte[] data) {
            if(data != null) {
                m_stream = new MemoryStream(data);
                m_reader = new BinaryReader(m_stream);

                m_dataLength = ReadUShort();
            }
        }

        public byte ReadByte() {
            return m_reader.ReadByte();
        }

        public int ReadInt() {
            return m_reader.ReadInt32();
        }

        public uint ReadUInt() {
            return m_reader.ReadUInt32();
        }

        public short ReadShort() {
            return m_reader.ReadInt16();
        }

        public ushort ReadUShort() {
            return m_reader.ReadUInt16();
        }

        public long ReadLong() {
            return m_reader.ReadInt64();
        }

        public ulong ReadULong() {
            return m_reader.ReadUInt64();
        }

        public float ReadFloat() {
            byte[] temp = BitConverter.GetBytes(m_reader.ReadSingle());
            Array.Reverse(temp);
            return BitConverter.ToSingle(temp, 0);
        }

        public double ReadDouble() {
            byte[] temp = BitConverter.GetBytes(m_reader.ReadDouble());
            Array.Reverse(temp);
            return BitConverter.ToDouble(temp, 0);
        }

        public string ReadString() {
            ushort len = ReadUShort();
            byte[] buffer = new byte[len];
            buffer = m_reader.ReadBytes(len);
            return Encoding.UTF8.GetString(buffer);
        }

        public byte[] ReadBytes() {
            int len = ReadInt();
            return m_reader.ReadBytes(len);
        }

        public void Close() {
            if(m_reader != null) {
                m_reader.Close();
            }
            if(m_stream != null) {
                m_stream.Close();
            }
            m_reader = null;
            m_stream = null;
        }
    }
}
