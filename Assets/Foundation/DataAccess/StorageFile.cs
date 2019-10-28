using System;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;
using Assets.Foundation.Tool;

namespace Assets.Foundation.DataAccess
{
    /// <summary>
    /// 存储文件
    /// </summary>
    public class StorageFile : IDisposable
    {
        /// <summary>
        /// 完整路径
        /// </summary>
        private string _fullpath;
        /// <summary>
        /// 文件流
        /// </summary>
        private FileStream _stream;

        public string Fullpath
        {
            get
            {
                return _fullpath;
            }
        }

        protected FileStream Stream
        {
            get
            {
                return _stream;
            }
        }

        /// <summary>
        /// 文件长度
        /// </summary>
        public long Length
        {
            get
            {
                if (_stream == null)
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
                if (_stream == null)
                {
                    return 0;
                }
                return _stream.Position;
            }
        }

        public StorageFile(string fullpath)
        {
            _fullpath = fullpath;
            FileUtility.AutoCreateFile(fullpath);
            _stream = new FileStream(fullpath, FileMode.OpenOrCreate);
        }

        public byte[] ToBytes()
        {
            long pos = _stream.Position;
            int length = (int)_stream.Length;
            byte[] buff = new byte[length];
            _stream.Position = 0;
            _stream.Read(buff, 0, length);
            _stream.Position = pos;
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
            if (_stream == null)
            {
                return;
            }
            _stream.Flush();
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (_stream == null)
            {
                return;
            }
            _stream.Close();
            _stream.Dispose();
            _stream = null;
        }

        /// <summary>
        /// 追加数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public void Append(byte[] data, long offset, long length)
        {
            if (_stream == null)
            {
                return;
            }
            if (data == null || data.Length == 0)
            {
                return;
            }

            _stream.Write(data, (int)offset, (int)length);
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
            if (_stream == null)
            {
                return;
            }

            _stream.Position = Stream.Length;
        }

        public void SeekBegin()
        {
            if (_stream == null)
            {
                return;
            }
            _stream.Position = 0;
        }

        public void Seek(int positon)
        {
            if (_stream == null)
            {
                return;
            }
            if (positon < 0 || positon > Stream.Length)
            {
                return;
            }

            _stream.Position = positon;
        }
    }
}
