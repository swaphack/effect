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
        public int food;
        public int wood;
        public int iron;
        public int silver;
    }

    public class Role
    {
        private static Resource _resource = new Resource();
        public static Resource Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }

        public static void Save()
        {
            var instance = UserDefault.Instance;
            instance.Clear();

            var type = _resource.GetType();

            var fields = type.GetFields();
            for (var i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                instance.Set(field.Name, field.GetValue(_resource));
            }

            instance.Save();
        }

        public static void Load()
        {
            var instance = UserDefault.Instance;
            instance.Load();
            var type = _resource.GetType();
            var fields = type.GetFields();
            for (var i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                field.SetValue(_resource, instance.Get(field.Name));
            }
        }
    }
}
