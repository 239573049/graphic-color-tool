using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace XHHelper
{
    internal class CmdHelper
    {
        private readonly ProcessStartInfo PROCES_INFO;

        public CmdHelper((string key, string value)[] EnvironmentVariable = null, bool createNoWindow = true, bool redirectStandardOutput = true)
        {
            PROCES_INFO = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardOutput = redirectStandardOutput,
                CreateNoWindow = createNoWindow
            };
            SetEnvVariables(EnvironmentVariable);
        }

        public List<string> Run(string command)
        {
            return Run___(command);
        }
        public List<string> Run(string command, Action<string>? action = null)
        {
            return Run___(command, action);
        }
        private List<string> Run___(string command,Action<string>? action = null)
        {
            List<string> list = new List<string>();
            PROCES_INFO.Arguments = "/C " + command;
            Process process = new Process
            {
                StartInfo = PROCES_INFO
            };
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                var data=process.StandardOutput.ReadLine();
                action?.Invoke(data);
                list.Add(data);
            }
            if (process.ExitCode != 0)
            {
                throw new Exception($"Exit code: '{process.ExitCode}' for command: '{command}'");
            }

            return list;
        }
        private void SetEnvVariables((string key, string value)[] EnvVariable)
        {
            if (EnvVariable == null)
            {
                return;
            }

            for (int i = 0; i < EnvVariable.Length; i++)
            {
                (string, string) tuple = EnvVariable[i];
                if (PROCES_INFO.EnvironmentVariables.ContainsKey(tuple.Item1))
                {
                    string text = PROCES_INFO.EnvironmentVariables[tuple.Item1];
                    PROCES_INFO.EnvironmentVariables.Remove(tuple.Item1);
                    PROCES_INFO.EnvironmentVariables.Add(tuple.Item1, text + ";" + tuple.Item2);
                }
                else
                {
                    PROCES_INFO.EnvironmentVariables.Add(tuple.Item1, tuple.Item2);
                }
            }
        }
    }
}
