using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XHHelper;
using CMDHelper;
using System.Diagnostics;

namespace TestProject1Tests
{
    [TestClass()]
    public class AdbTests
    {
        [TestMethod()]
        public void Test1Test()
        {
            var adb=new AdbHelper();
            var devices= adb.GetDevices();
            var sw=Stopwatch.StartNew();
            adb.GetSiez(devices[0]);
            sw.Stop();
            Console.WriteLine("Stopwatch:"+ sw.Elapsed.TotalMilliseconds);
            adb.Dispose();
        }
    }
}
