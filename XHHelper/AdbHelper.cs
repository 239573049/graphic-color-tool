using System;
using System.Collections.Generic;
using System.Linq;
using XHHelper.Model;

namespace XHHelper
{
    public class AdbHelper: IDisposable
    {
        private CmdHelper? cmdHelper;
        public AdbHelper(
            )
        {
            cmdHelper = new CmdHelper();
        }

        public void Dispose()
        {
            cmdHelper = null;
        }
        /// <summary>
        /// 安装APK
        /// </summary>
        /// <param name="path"></param>
        /// <param name="action"></param>
        public void InstallApk(string device, string path,Action<string> action)
        {
            cmdHelper!.Run("adb -s "+ device + " install " + path, action);
        }
        /// <summary>
        /// 获取分辨率
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Size GetSiez(string device)
        {
           var data= cmdHelper!.Run("adb -s "+ device + " shell wm size").FirstOrDefault();
            data = data.Replace("Physical size: ", "");
            var size=data.Split("x");
            if (size.Length != 2) throw new Exception("获取分辨率错误");
            return new Size(int.Parse(size[0]), int.Parse(size[1]));
        }
        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetDevices()
        {
            List<string>? list=new List<string>();
            bool isAdd=false;
            var devices= cmdHelper!.Run("adb devices -l");
            foreach (var d in devices)
            {
                if (isAdd && !string.IsNullOrEmpty(d))
                {
                    var names=d.Split(" ").Where(a=>!string.IsNullOrEmpty(a)).ToArray();
                    list.Add(names[0]);
                }
                if(d.Contains("List of devices attached"))
                {
                    isAdd=true;
                }
            }
            return list;
        }
    }
}