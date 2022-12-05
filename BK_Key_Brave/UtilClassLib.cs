//using AutoUpdaterDotNET;
//using KAutoHelper;
using Newtonsoft.Json;
using SharpConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Brave_Tool
{
    class UtilClassLib
    {
        public string[] fileArr(string pathfile)
        {
            string[] lines = File.ReadAllLines(pathfile);
            return lines;
        }
        public Boolean ToolOnline(string url)
        {
            string strOnline = Onlinefile(url).Trim();
            if (!strOnline.Equals("1"))
            {
                return false;
            }
            return true;
        }
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        public static bool CheckNet()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }
        public void CheckInternet()
        {
            int desc;
            Boolean lostConnect = false;
            //while (!InternetGetConnectedState(out desc, 0))
            while (String.IsNullOrEmpty(GetIPAddress()))
            {
                if (!lostConnect)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("> Lost internet connection !!!");
                    Console.ResetColor();
                }
                lostConnect = true;
                Thread.Sleep(3000);
            }
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine(GetIPAddress());
            //Console.ResetColor();
        }
        //get client ip address
        public string GetIPAddress()
        {
            String address = "";
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebRequest request = WebRequest.Create("https://api.ipify.org/");
                using (WebResponse response = request.GetResponse())
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    address = stream.ReadToEnd();
                }
            }
            catch (Exception)
            {

                address = "";
            }

            return address;
        }
        public string Onlinefile(String url)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(url);
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();
            return content;
        }
        public class JsonResponse
        {
            public string dateTime { get; set; }
            public string date { get; set; }
        }
        public static Random random = new Random();
        public string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string RandomNumber(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string RandomNumberAndString(int length)
        {
            const string chars = "0123456789aqwertyuiopasdfghjklzxcvbnm";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public int Randomso(int length)
        {
            const string chars = "123";
            string returns = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return Convert.ToInt32(returns);
        }
        public void RunBatFile(string batfile)
        {
            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
                proc.StartInfo.FileName = batfile;
                proc.Start();
            }
            catch (Exception) { }
        }
        public void RunPowershellScript(string ps)
        {
            try
            {
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy unrestricted \"{ps}\"",
                    UseShellExecute = false
                };
                Process.Start(startInfo);
            }
            catch (Exception)
            {

                Console.WriteLine("Error run powershell");
            }

        }
        public void RunBatFilearg(string source, string target)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
            proc.StartInfo.FileName = @"Auto\xcopy.bat";
            proc.StartInfo.Arguments = String.Format("{0} {1}", source, target);
            proc.Start();
            proc.WaitForExit();
            Console.WriteLine("Copying: " + target);
        }
        public void CreateBatFile()
        {
            //and save it:
            if (File.Exists(@"Bat\Auto.bat"))
            {
                File.Delete(@"Bat\Auto.bat");
            }
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Default.ppx";
            // Create a new file     
            using (StreamWriter sw = File.CreateText(@"Bat\Auto.bat"))
            {
                sw.WriteLine("\"C:\\Program Files (x86)\\Proxifier\\Proxifier.exe\" " + path);
            }
        }
        public double isDouble(string str)
        {
            double totalBat;
            if (Double.TryParse(str, out totalBat))
            {
                return totalBat;
            }
            return 0;
        }
        public double CalculatorBatSendTip(double input, double banance)
        {
            double sendTip;
            if (input < banance)
            {
                sendTip = input;
            }
            else if (input > banance)
            {
                sendTip = banance;
            }
            else
            {
                sendTip = input;
            }
            return sendTip;
        }
        // Lấy foder Profile lớn nhất
        public int getMaxProfile(string pathProfile)
        {
            try
            {
                string[] directoryCount = System.IO.Directory.GetDirectories(pathProfile, "Profile *");
                if (directoryCount.Length == 0)
                {
                    return 0;
                }
                List<int> MyList = new List<int>();
                Match m;
                Regex regex = new Regex(@"Profile \d+");

                for (int i = 0; i < directoryCount.Length; i++)
                {
                    m = regex.Match(directoryCount[i]);
                    MyList.Add(Convert.ToInt32(m.Value.Replace("Profile ", "")));
                }
                return MyList.Max();
            }
            catch (Exception)
            {
            }
            return 0;
        }
        public List<string> getProfileFolder(string pathProfile)
        {
            IEnumerable<string> sam = new List<string>();

            List<string> MyList = new List<string>();
            List<string> MyList1 = new List<string>();
            string[] directoryCount = System.IO.Directory.GetDirectories(pathProfile, "Profile *");
            if (directoryCount.Length == 0)
            {
                return null;
            }

            Match m;
            Regex regex = new Regex(@"Profile \d+", RegexOptions.IgnoreCase);

            for (int i = 0; i < directoryCount.Length; i++)
            {
                m = regex.Match(directoryCount[i]);
                //MyList.Add(Convert.ToInt32(m.Value.Replace("Profile ", "")));
                MyList.Add(m.Value);
            }
            foreach (var x in MyList.OrderBy(s => Int32.Parse(Regex.Match(s, @" (\d*)").Groups[1].Value)))
                //Console.WriteLine(x);
                MyList1.Add(x);
            return MyList1;
        }
           
        public List<string> getProfile(string pathProfile)
        {
            string[] directoryCount = System.IO.Directory.GetDirectories(pathProfile, "*");
            if (directoryCount.Length == 0)
            {
                return null;
            }
            List<string> MyList = new List<string>();
            for (int i = 0; i < directoryCount.Length; i++)
            {
                MyList.Add(directoryCount[i]);
            }
            return MyList;
        }
        public void SaveConfig(string[] MainConfig, string[] UserDataDir)
        {
            string filename = @"Auto\Config.cfg";
            var cfg = new Configuration();
            // MainConfig
            cfg["MainConfig"]["PathProfile"].StringValue = MainConfig[0];
            cfg["MainConfig"]["PathBrave"].StringValue = MainConfig[1];
            //UserDataDir
            cfg["UserDataDir"]["UserDataDirConfig"].StringValue = UserDataDir[0];

            cfg.SaveToFile(filename);
        }
        // Kill Port
        public class PRC
        {
            public int PID { get; set; }
            public int Port { get; set; }
            public string Protocol { get; set; }
        }
        public void KillByPort(int port)
        {
            var processes = GetAllProcesses();
            if (processes.Any(p => p.Port == port))
                try
                {
                    Process.GetProcessById(processes.First(p => p.Port == port).PID).Kill();
                    Console.WriteLine("Killed port" + port);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            else
            {
                Console.WriteLine("No process to kill!");
            }
        }
        public List<PRC> GetAllProcesses()
        {
            var pStartInfo = new ProcessStartInfo();
            pStartInfo.FileName = "netstat.exe";
            pStartInfo.Arguments = "-a -n -o";
            pStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            pStartInfo.UseShellExecute = false;
            pStartInfo.RedirectStandardInput = true;
            pStartInfo.RedirectStandardOutput = true;
            pStartInfo.RedirectStandardError = true;

            var process = new Process()
            {
                StartInfo = pStartInfo
            };
            process.Start();

            var soStream = process.StandardOutput;

            var output = soStream.ReadToEnd();
            if (process.ExitCode != 0)
                throw new Exception("somethign broke");

            var result = new List<PRC>();

            var lines = Regex.Split(output, "\r\n");
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("Proto"))
                    continue;

                var parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var len = parts.Length;
                if (len > 2)
                    result.Add(new PRC
                    {
                        Protocol = parts[0],
                        Port = int.Parse(parts[1].Split(':').Last()),
                        PID = int.Parse(parts[len - 1])
                    });


            }
            return result;
        }
    }
}
