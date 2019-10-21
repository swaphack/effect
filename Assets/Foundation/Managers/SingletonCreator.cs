using System;
using System.Reflection;
using UnityEngine;

namespace Assets.Foundation.Managers
{
    public static class SingletonCreator
    {
        public static T CreateSingleton<T>() where T : class
        {
            // 获取私有构造函数
            var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            // 获取无参构造函数
            var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

            if (ctor == null)
            {
                Debug.LogError("Non-Public Constructor() not found! in " + typeof(T));
                return null;
            }

            // 通过构造函数，创建实例
            var retInstance = ctor.Invoke(null) as T;

            return retInstance;
        }
    }
}
