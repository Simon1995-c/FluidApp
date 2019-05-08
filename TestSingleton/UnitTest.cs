
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluidApp;
using Models;

namespace TestSingleton
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Singleton s = Singleton.Instance;
            Singleton s1 = Singleton.Instance;

            
            Assert.AreSame(s,s1);
        }
    }
}
