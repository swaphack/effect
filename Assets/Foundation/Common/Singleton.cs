using System;

namespace Assets.Foundation.Common
{
    public class Singleton<T> where T : class
    {
        private static T mInstance;
		private static readonly object mLock = new object();
 
		public static T Instance
		{
			get
			{
				lock (mLock)
				{
					if (mInstance == null)
					{
						mInstance = SingletonCreator.CreateSingleton<T>();
					}
				}
 
				return mInstance;
			}
		}
 
		public static void Dispose()
		{
			mInstance = null;
		}
    }
}
