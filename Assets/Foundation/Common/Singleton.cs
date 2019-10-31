using System;

namespace Assets.Foundation.Common
{
    public class Singleton<T> where T : class
    {
        private static T _sInstance;
		private static readonly object _sLock = new object();
 
		public static T Instance
		{
			get
			{
				lock (_sLock)
				{
					if (_sInstance == null)
					{
                        _sInstance = SingletonCreator.CreateSingleton<T>();
					}
				}
 
				return _sInstance;
			}
		}
 
		public static void Dispose()
		{
            _sInstance = null;
		}
    }
}
