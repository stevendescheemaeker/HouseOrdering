using System;
using System.Collections.Generic;
using System.Text;

namespace HouseOrdering
{
    public abstract class Singleton<T> where T : class
    {
        private static bool initcalled = false;
        private static object syncRoot = new Object();

        public void Init()
        {
            initcalled = true;
            Initialise();
        }


        protected abstract void Initialise();

        private static readonly Lazy<T> instance = new Lazy<T>(() => Activator.CreateInstance(typeof(T), true) as T);

        public static T Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (!initcalled && instance.IsValueCreated)
                    {
                        throw new Exception("Init not called");
                    }

                    return instance.Value;
                }

            }
        }
    }

    public class HourseOrdering : Singleton<HourseOrdering>
    {
        public HourseOrdering()
        {

        }

        protected override void Initialise()
        {
            
        }
    }
}
