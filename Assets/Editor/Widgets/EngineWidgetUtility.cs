using System.Collections.Generic;
using System.IO;

namespace Assets.Editor.Widgets
{
    /// <summary>
    /// 控件工具
    /// </summary>
    public static class WidgetUtility
    {
        /// <summary>
        /// 当前工程所在目录
        /// </summary>
        public static string EditorRoot
        {
            get
            {
                return string.Format("{0}/Assets/", System.Environment.CurrentDirectory);
            }
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="root"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string[] GetFilePaths(string root, string pattern)
        {
            if (string.IsNullOrEmpty(root))
            {
                return null;
            }

            if (!Directory.Exists(root))
            {
                return null;
            }

            if (string.IsNullOrEmpty(pattern))
            {
                pattern = "*";
            }

            string[] allpath = Directory.GetFiles(root, pattern, SearchOption.AllDirectories);

            string dir = System.Environment.CurrentDirectory;
            dir = dir.Replace('\\', '/');

            List<string> filepaths = new List<string>();

            for (int i = 0; i < allpath.Length; i++)
            {
                string fullpath = allpath[i].Replace('\\', '/');
                fullpath = fullpath.Substring(dir.Length + 1);

                filepaths.Add(fullpath);
            }

            return filepaths.ToArray();
        }
    }
}
