using System;
using Game.Foundation.DataAccess;
using UnityEngine;

namespace Game.Foundation.Common
{
    /// <summary>
    /// 文件日志处理
    /// </summary>
    public class FileLogHandler : ILogHandler
    {
        private StorageFile _file;

        public FileLogHandler(string filepath)
        {
            _file = new StorageFile(filepath);
        }

        private void AppendLog(string logText)
        {
            if (string.IsNullOrEmpty(logText))
            {
                return;
            }

            string text = string.Format("[{0}] {1}\n", DateTime.Now.ToString(), logText);

            _file.Append(text);
            _file.Save();
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            if (exception == null)
            {
                return;
            }

            string textContext = context == null ? "" : context.ToString();

            string text = string.Format("[{0}] {1} {2}", "Exception", textContext, exception);
            this.AppendLog(text);
        }

        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            if (format == null)
            {
                return;
            }
            string textParams = string.Format(format, args);
            string textContext = context == null ? "" : context.ToString();
            string text = string.Format("[{0}] {1} {2}", logType.ToString(), textContext, textParams);
            this.AppendLog(text);
        }
    }
}
