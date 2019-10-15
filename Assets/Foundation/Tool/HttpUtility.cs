using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace Assets.Foundation.Tool
{
    public class HttpUtility
    {

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Encode(string name)
        {
            //var bytes = Encoding.UTF8.GetBytes(name);
            //string encode = Convert.ToBase64String(bytes);
            string encode = name;
            encode = encode.Replace("%3D",  "=");
            encode = encode.Replace("%2F", "/");
            encode = encode.Replace("+", "%20");
            encode = encode.Replace("%26", "&");
            return encode;
        }
        public static UnityWebRequest DoPost(string url, Dictionary<string, string> formFields)
        {
            Dictionary<string, string> newFields = new Dictionary<string, string>();
            foreach (var item in formFields)
            {
                newFields.Add(Encode(item.Key), Encode(item.Value));
            }

            return UnityWebRequest.Post(url, newFields);
        }

        public static UnityWebRequest DoGet(string url, Dictionary<string, string> formFields)
        {
            string values = "";
            foreach(var item in formFields)
            {
                values = values + string.Format("{0}={1}&", Encode(item.Key), Encode(item.Value));
            }
            if (values.Length != 0)
            {
                values = values.Substring(0, values.Length - 1);
            }
            string uri = string.Format("{0}?{1}", url, values);
            return UnityWebRequest.Get(uri);

        }
    }
}
