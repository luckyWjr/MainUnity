using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tool {
    class NetBufferWriter {
        MemoryStream m_stream = null;
        BinaryWriter m_writer = null;

        int m_finishLength;
        public int finishLength {
            get { return m_finishLength; }
        }

        public NetBufferWriter() {
            m_finishLength = 0;
            m_stream = new MemoryStream();
            m_writer = new BinaryWriter(m_stream);
        }

        public void WriteByte(byte v) {
            m_writer.Write(v);
        }

        public void WriteInt(int v) {
            m_writer.Write(v);
        }

        public void WriteUInt(uint v) {
            m_writer.Write(v);
        }

        public void WriteShort(short v) {
            m_writer.Write(v);
        }

        public void WriteUShort(ushort v) {
            m_writer.Write(v);
        }

        public void WriteLong(long v) {
            m_writer.Write(v);
        }

        public void WriteULong(ulong v) {
            m_writer.Write(v);
        }

        public void WriteFloat(float v) {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            m_writer.Write(BitConverter.ToSingle(temp, 0));
        }

        public void WriteDouble(double v) {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            m_writer.Write(BitConverter.ToDouble(temp, 0));
        }

        public void WriteString(string v) {
            byte[] bytes = Encoding.UTF8.GetBytes(v);
            m_writer.Write((ushort)bytes.Length);
            m_writer.Write(bytes);
        }

        public void WriteBytes(byte[] v) {
            m_writer.Write(v.Length);
            m_writer.Write(v);
        }

        public byte[] ToBytes() {
            m_writer.Flush();
            return m_stream.ToArray();
        }

        public void Close() {
            m_writer.Close();
            m_stream.Close();
            m_writer = null;
            m_stream = null;
        }

        /// <summary>  
        /// 数据转换，网络发送需要两部分数据，一是数据长度，二是主体数据  
        /// </summary>  
        public byte[] Finish() {
            byte[] message = ToBytes();
            MemoryStream ms = new MemoryStream();
            ms.Position = 0;
            BinaryWriter writer = new BinaryWriter(ms);
            writer.Write((ushort)message.Length);
            writer.Write(message);
            writer.Flush();
            byte[] result = ms.ToArray();
            m_finishLength = result.Length;
            return result;
        }
    }
}
