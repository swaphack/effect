using Assets.Foundation.Actions;
using Assets.Foundation.DataAccess;
using Assets.Foundation.Effects;
using UnityEngine;
using System.Collections.Generic;
using Assets.Foundation.Protocol;
using UnityEngine.UI;
using System.IO;
using Assets.Foundation.Extensions;
using Assets.Foundation.UI;
using Assets.Game.UI;
using Assets.Foundation.Device;
using Assets.Foundation.Data;
using Assets.Foundation.TextFormat;
using System.Xml;
using Assets.Foundation.Creator;
using Assets.Foundation.Events;
using Assets.Foundation.Tool;
using Assets.Foundation.Managers;

namespace Assets.Game
{
    public class Test : MonoBehaviour
    {
        class TestD 
        {
            public string name;
            public List<float> score;
            public Dictionary<int, string> prop;

            public TestD()
            {
                name = string.Empty;
                score = new List<float>();
                prop = new Dictionary<int, string>();
            }
        }

        void TestRes()
        { 
            
        }

        void TestPacket()
        {
            TestD d = new TestD();
            d.name = "fasdfsd";
            d.score.Add(15.0f);
            d.score.Add(85.0f);
            d.score.Add(0.0f);
            d.prop.Add(1, "sdfasadf");
            d.prop.Add(2, "sdfdfdasadssf");
            d.prop.Add(3, "s12dfasadf");

            PacketWriter writer = new PacketWriter();
            writer.WriteObject(d);

            TestD t = new TestD();
            PacketReader reader = new PacketReader(writer.ToBytes());
            t = (TestD)reader.ReadObject(t);
        }

        void TestDownload()
        {
            string url = "https://aliosscdn.bluestacks.cn/package/BlueStacksGPSetup.exe";
            string local = "H:/Download/exe/BlueStacksGPSetup.exe";
            DownloadManager.Instance.AddTask(url, local, DownloadCallback);
        }

        void DownloadCallback(string error, string url, float progress)
        {
            if (string.IsNullOrEmpty(error))
            {
                Debug.LogFormat("Download {0}, progress {1}%", url, progress * 100);
            }
            else
            {
                Debug.LogErrorFormat("Download {0}, error {1}%", url, error);
            }
        }

        void TestWriteUserDefault()
        {
            TestD d = new TestD();
            d.name = "fasdfsd";
            d.score.Add(15.0f);
            d.score.Add(85.0f);
            d.score.Add(0.0f);
            d.prop.Add(1, "sdfasadf");
            d.prop.Add(2, "sdfdfdasadssf");
            d.prop.Add(3, "s12dfasadf");

            UserDefault.Instance.Set("id", (int)31);
            UserDefault.Instance.Set("value", d);
            UserDefault.Instance.Save();
        }

        void TestReadUserDefault()
        {
            UserDefault.Instance.Load();

            //int num = UserDefault.Instance.Get<int>("id");
            //TestD d = UserDefault.Instance.Get<TestD>("value");
        }

        void TestNet()
        {
            var client = Client.Instance;
            client.ConnectCallback = this.OnClientConnected;
            client.DisconnectCallback = this.OnClientDisconnected;
            client.SetRemote("10.15.122.63", 9547);
            client.Connect();
        }

        int nErrorCount = 3;

        void OnClientConnected(Client client)
        {
            if (client.Connected)
            {
                Debug.Log("connect");
                client.StartRecv();
            }
            else 
            {
                if (nErrorCount == 0)
                {
                    Debug.Log("can't connect");
                    client.Disconnect();
                }
                else
                {
                    nErrorCount--;

                    Debug.Log("try connect");
                    client.Reconnect();
                }
            }
        }

        void OnClientDisconnected(Client client)
        {
            Debug.Log("disconnect");
        }

        class TestT : TestD
        {
            public Dictionary<string, Dictionary<string, List<int>>> rank;

            public TestT()
            {
                rank = new Dictionary<string, Dictionary<string, List<int>>>();
            }
        }

        void TestJson()
        {
            TestT d = new TestT();
            d.name = "fasdfsd";
            d.score.Add(15.0f);
            d.score.Add(85.0f);
            d.score.Add(0.0f);
            d.prop.Add(1, "sdfasadf");
            d.prop.Add(2, "sdfdfdasadssf");
            d.prop.Add(3, "s12dfasadf");

            Dictionary<string, List<int>> data1 = new Dictionary<string,List<int>>();
            List<int> level1 = new List<int>();
            level1.Add(12);
            level1.Add(22);
            level1.Add(32);
            level1.Add(42);
            level1.Add(52);

            data1.Add("class1", level1);

            d.rank.Add("math", data1);
            d.rank.Add("english", data1);

            DJsonWriter writer = new DJsonWriter();
            writer.Write(d);

            DJsonReader reader = new DJsonReader(writer.ToString());
            TestT t = new TestT();
            var obj = (object)t;
            if (reader.Read(ref obj))
            {
                Debug.Log("read success");
            }
            else
            {
                Debug.Log("read failure");
            }
        }

        void TestXml()
        {
            TestT d = new TestT();
            d.name = "fasdfsd";
            d.score.Add(15.0f);
            d.score.Add(85.0f);
            d.score.Add(0.0f);
            d.prop.Add(1, "sdfasadf");
            d.prop.Add(2, "sdfdfdasadssf");
            d.prop.Add(3, "s12dfasadf");

            Dictionary<string, List<int>> data1 = new Dictionary<string, List<int>>();
            List<int> level1 = new List<int>();
            level1.Add(12);
            level1.Add(22);
            level1.Add(32);
            level1.Add(42);
            level1.Add(52);

            data1.Add("class1", level1);

            d.rank.Add("math", data1);
            d.rank.Add("english", data1);

            XmlDocument doc = new XmlDocument();
            var root = doc.CreateElement("root");
            doc.AppendChild(root);

            DXmlWriter writer = new DXmlWriter(root);
            writer.Write(d.GetType().Name, d);

            string path = Path.Combine(Application.persistentDataPath, "class.xml");
            doc.Save(path);

            TestT t = new TestT();
            var obj = (object)t;

            DXmlReader reader = new DXmlReader(root[t.GetType().Name]);
            if (reader.Read(obj.GetType().Name, ref obj))
            {
                Debug.Log("read success");
            }
            else
            {
                Debug.Log("read failure");
            }
        }

        void TestUnZip()
        {
            string srcUrl = "C:/Users/Administrator/Downloads/icsharpcode.sharpziplib.zip";
            string destUrl = "H:/upzip";

            Singleton.GetInstance<ZipResource>().AddTask(new ZipResource.LoadTask(srcUrl, destUrl, (IFileItem item) =>
            {
                Debug.Log(item.path);
            }));

        }

        void Start()
        {
            //Application.targetFrameRate = 30;

            //UIManager.ShowUI<UILoading>();
        }
    }
}
