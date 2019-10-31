using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Collections;

namespace Assets.Foundation.DataAccess
{
    /// <summary>
    /// zip资源
    /// </summary>
    public class ZipResource : ResourceLoad
    {
        /// <summary>
        /// 每次读取的块大小
        /// </summary>
        public const int ReadBlockSize = 1024 * 4;

        internal class ResourceItem : IExchangeFileItem
        {
            /// <summary>
            /// 资源路径
            /// </summary>
            public string path { get; }
            /// <summary>
            /// 目标路径
            /// </summary>
            public string destPath { get; }

            public ResourceItem(string path, string destPath)
            {
                this.path = path;
                this.destPath = destPath;
            }
        }

        public class LoadTask : Task
        {
            private ZipInputStream _zipInputStream;
            public LoadTask(string path, string destPath, ResourceItemDelegate hand)
            {
                this._item = new ResourceItem(path, destPath);

                this._callback = hand;
            }

            public override void Init()
            {
                string directoryName = this.GetItem<IExchangeFileItem>().destPath;
                if (!string.IsNullOrEmpty(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
            }

            public override IEnumerator Run()
            {
                string unzipPath = this.GetItem<IExchangeFileItem>().destPath;
                string zipFilePath = this.GetItem<IExchangeFileItem>().path;
                _zipInputStream = new ZipInputStream(File.OpenRead(zipFilePath));
                ZipEntry theEntry;
                while ((theEntry = _zipInputStream.GetNextEntry()) != null)
                {
                    string fileName = Path.GetFileName(theEntry.Name);

                    if (fileName == String.Empty)
                    {
                        continue;
                    }
                    if (theEntry.CompressedSize == 0)
                        continue;

                    string fullpath = Path.Combine(unzipPath, theEntry.Name);

                    string directoryName = Path.GetDirectoryName(fullpath);
                    Directory.CreateDirectory(directoryName);

                    FileStream streamWriter = File.Create(fullpath);

                    int size = ReadBlockSize;
                    byte[] data = new byte[ReadBlockSize];
                    while (true)
                    {
                        size = _zipInputStream.Read(data, 0, data.Length);

                        yield return null;

                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    streamWriter.Close();
                }
            }

            public override void End()
            {
                _zipInputStream.Close();
            }
        }
    }
}
