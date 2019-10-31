using Assets.Foundation.DataAccess;
using Assets.Foundation.Common;
using System.Reflection;

namespace Assets.Home.Logic
{
    /// <summary>
    /// 资源
    /// </summary>
    public class Resource
    {
        public int food { get; set; }
        public int wood { get; set; }
        public int iron { get; set; }
        public int silver { get; set; }
    }

    public class Role
    {
        public static Resource Resource { get; set; } = new Resource();

        public static void Save()
        {
            var instance = UserDefault.Instance;
            instance.Clear();

            var type = Resource.GetType();

            var fields = type.GetFields();
            for (var i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                instance.Set(field.Name, field.GetValue(Resource));
            }

            instance.Save();
        }

        public static void Load()
        {
            var instance = UserDefault.Instance;
            instance.Load();
            var type = Resource.GetType();
            var fields = type.GetFields();
            for (var i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                field.SetValue(Resource, instance.Get(field.Name));
            }
        }
    }
}
