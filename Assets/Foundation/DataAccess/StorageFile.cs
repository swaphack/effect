using System;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;
using Game.Foundation.Tool;

namespace Game.Foundation.DataAccess
{
    /// <summary>
    /// 存储文件
    /// </summary>
    public class StorageFile : IDisposable
    {
        public string Fullpath { get; }

        protected FileStream Stream { get; private set; }

        /// <summary>
        /// 文件长度
        /// </summary>
        public long Length
        {
            get
            {
                if (Stream == null)
                {
                    return 0;
                }
                return Stream.Length;
            }
        }

        /// <summary>
        /// 读写位置
        /// </summary>
        public long Position
        {
            get
            {
                if (Stream == null)
                {
                    return 0;
                }
                return Stream.Position;
            }
        }

        public StorageFile(string fullpath)
        {
            Fullpath = fullpath;
            FileUtility.AutoCreateFile(fullpath);
            Stream = new FileStream(fullpath, FileMode.OpenOrCreate);
        }

        public byte[] ToBytes()
        {
            long pos = Stream.Position;
            int length = (int)Stream.Length;
            byte[] buff = new byte[length];
            Stream.Position = 0;
            Stream.Read(buff, 0, length);
            Stream.Position = pos;
            return buff;
        }

        public string ToText()
        {
            byte[] bytes = ToBytes();
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            if (Stream == null)
            {
                return;
            }
            Stream.Flush();
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (Stream == null)
            {
                return;
            }
            Stream.Close();
            Stream.Dispose();
            Stream = null;
        }

        /// <summary>
        /// 追加数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public void Append(byte[] data, long offset, long length)
        {
            if (Stream == null)
            {
                return;
            }
            if (data == null || data.Length == 0)
            {
                return;
            }

            Stream.Write(data, (int)offset, (int)length);
        }

        /// <summary>
        /// 追加数据
        /// </summary>
        /// <param name="text"></param>
        public void Append(string text)
        {
            if (text == null)
            {
                return;
            }

            byte[] data = Encoding.UTF8.GetBytes(text);
            if (data.Length == 0)
            {
                return;
            }
            Append(data, 0, data.Length);
        }

        public void SeekEnd()
        {
            if (Stream == null)
            {
                return;
            }

            Stream.Position = Stream.Length;
        }

        public void SeekBegin()
        {
            if (Stream == null)
            {
                return;
            }
            Stream.Position = 0;
        }

        public void Seek(int positon)
        {
            if (Stream == null)
            {
                return;
            }
            if (positon < 0 || positon > Stream.Length)
            {
                return;
            }

            Stream.Position = positon;
        }
    }
}
