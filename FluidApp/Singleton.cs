using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;

namespace FluidApp
{
    public sealed class Singleton
    {
        //The volatile keyword indicates that
        //a field might be modified by multiple threads,
        //that are executing at the same time
        private static volatile Singleton instance;
        private static object syncRoot = new Object();

        private Singleton()
        {
            //Write code (First)
            //Remember: instance = new Singleton(); (At the end)
        }



        //The instantiation is not performed until an object asks for an instance;
        //this approach is referred to as lazy instantiation.
        //Lazy instantiation avoids instantiating unnecessary singletons when the application starts.
        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Singleton();
                    }
                }

                return instance;
            }
        }
    }
}
