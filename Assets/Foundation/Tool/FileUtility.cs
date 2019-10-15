using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Foundation.Tool
{
    public class FileUtility
    {
        /// <summary>
        /// 自动创建文件，文件不存在，创建
        /// </summary>
        /// <param name="fullpath"></param>
        public static void AutoCreateFile(string fullpath)
        {
            if (!File.Exists(fullpath))
            {
                FileInfo info = new FileInfo(fullpath);
                Directory.CreateDirectory(info.DirectoryName);
                File.Create(fullpath).Dispose();
            }
        }

        /// <summary>
        /// 读取文本
        /// </summary>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public static string ReadText(string fullpath)
        {
            if (!File.Exists(fullpath))
            {
                return null;
            }

            return File.ReadAllText(fullpath);
        }

        /// <summary>
        /// 读取字节
        /// </summary>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(string fullpath)
        {
            if (!File.Exists(fullpath))
            {
                return null;
            }

            return File.ReadAllBytes(fullpath);
        }

        
    }
}
