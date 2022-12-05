using Brave_Tool;
using AutoUpdaterDotNET;
using SharpConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net;
using ConsoleProgram;

namespace BK_Key_Brave
{
    public partial class Form1 : Form
    {
        UtilClassLib Poke = new UtilClassLib();
        #region Constant
        private static string urlUpdate = "https://braveupdate.netlify.app/Update/UpdateBkKey.xml";
        private static string keyoutput = "";
        //System.Environment.SetEnvironmentVariable("webdriver.chrome.driver","chromedriver.exe");
        static IWebDriver driver;
        //IWebDriver driver = new ChromeDriver();
        ChromeOptions chromeOptions = new ChromeOptions();
        private string chromeVersion = "This version of ChromeDriver only supports Chrome version";
        #endregion
        public Form1()
        {;
            System.Threading.Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("");
            Console.WriteLine(@"█▄▄ █▀█ ▄▀█ █░█ █▀▀   ▀█▀ █▀█ █▀█ █░░");
            Console.WriteLine(@"█▄█ █▀▄ █▀█ ▀▄▀ ██▄   ░█░ █▄█ █▄█ █▄▄");
            Console.WriteLine("");
            Console.WriteLine("Brave tool Version " + fvi.FileVersion);
            Console.WriteLine("@Pikazai");
            Console.ResetColor();
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {


            #region Load config
            var config = Configuration.LoadFromFile(@"Auto\Config.cfg");
            var MainConfig = config["MainConfig"];
            txtProfile.Text = MainConfig["PathProfile"].StringValue;
            txt_pathBrave.Text = MainConfig["PathBrave"].StringValue;

            // Main config UserDataDir
            var UserDataDir = config["UserDataDir"];
            String userDataDirStr = UserDataDir["UserDataDirConfig"].StringValue;
            if (userDataDirStr.Equals("1"))
            {
                UserData1.Checked = true;
                UserData2.Checked = false;
                UserData3.Checked = false;
            }
            else if (userDataDirStr.Equals("2"))
            {
                UserData1.Checked = false;
                UserData2.Checked = true;
                UserData3.Checked = false;
            }
            else
            {
                UserData1.Checked = false;
                UserData2.Checked = false;
                UserData3.Checked = true;
            }

            #endregion

            #region Quản lý Version
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
            Thread.Sleep(1500);
            AutoUpdater.Start(urlUpdate);

            #endregion

            string txtfileOuutPut = "Output\\" + ("Logs_" + DateTime.Now.ToString("yyMMddHHmmss") + ".txt");
            ConsoleFileOutput newOut = new ConsoleFileOutput(txtfileOuutPut, Console.Out);
            Console.SetOut((TextWriter)newOut);
        }
        private void btn_backup_Click(object sender, EventArgs e)
        {
            Thread StartTip = new Thread(new ParameterizedThreadStart(param => { BackupKey_New(); }));
            StartTip.SetApartmentState(ApartmentState.STA);
            StartTip.Start();
        }

        private void BackupKey_New()
        {
            SaveConfig();
            #region Clean Log
            try
            {
                ClearFolder("Output");
            }
            catch (Exception)
            {
                ClearFolder("Output");
            }
            #endregion
            #region Init
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("===========================> Backup Key <===========================");
            Console.WriteLine("===========================> @Pikazai <===========================");
            Console.WriteLine("Run time: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            Console.WriteLine("");
            string fullPathProfile = "";
            string strClaim = "";
            //string folder = "";
            string[] proxy;
            string ip;
            var rndIp = "";
            var rand = new Random();
            string[] proxyArr = null;
            int num1 = 0;
            string checkOpenRewardPage = "";
            string strKey = "";
            keyoutput = @"Key\Key_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".txt";
            // Xoá file nếu đã tồn tại
            deletetxt(keyoutput);
            #region Skip backup profile
            string[] arrSkipPrf;
            string skipBackupPrf = @"SkipProfile\skipBackupPrf.txt";
            if (!File.Exists(skipBackupPrf))
            {
                using (StreamWriter w = File.AppendText(skipBackupPrf))
                {
                }
            }
            #endregion
            #endregion

            #region Set up  User-data-dir
            List<string> listprofile;
            string strProfile = "";
            string duongdanprofile = "";
            string rootFolder = "";
            if (UserData3.Checked)
            {
                listprofile = Poke.getProfileFolder(txtProfile.Text);
            }
            else
            {
                listprofile = Poke.getProfile(txtProfile.Text);
                strProfile = "Default";
            }
            Console.WriteLine("listprofile.count:" + listprofile.Count);
            DialogResult dialogResult = MessageBox.Show("[" + listprofile.Count + " Profile] found");
            #endregion

            for (int index = 0; index < listprofile.Count; ++index)
            {
                bool chk = false;
                try
                {
                    #region Killchromedriver - StopBrave - CloseDriver
                    CloseDriver();
                    this.Killchromedriver();
                    this.StopBrave();
                    #endregion

                    Console.ResetColor();
                    if (UserData1.Checked)
                    {
                        duongdanprofile = listprofile[index];
                    }
                    else if (UserData2.Checked)
                    {
                        duongdanprofile = listprofile[index] + @"\Brave-Browser\User Data";
                    }
                    else if (UserData3.Checked)
                    {
                        duongdanprofile = txtProfile.Text;
                        strProfile = listprofile[index];
                    }
                    Console.WriteLine(listprofile[index]);

                    #region  Kiểm tra nếu có trong file SkipProfile\skipBackupPrf.txt thì bỏ qua
                    try
                    {
                        arrSkipPrf = Poke.fileArr(skipBackupPrf);
                        if (Array.IndexOf(arrSkipPrf, listprofile[index]) != -1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(">Skip " + listprofile[index] + @": Esxit in [skipProfile\skipBackupPrf.txt]");
                            Console.ResetColor();
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(@"Hãy chắc chắn file [skipProfile\skipBackupPrf.txt] tồn tại ?" + ex, "Đã có lỗi xảy ra",
                        //MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    #endregion
                    // Check Claim
                    if (chkBackupClaimed.Checked)
                    {
                        Thread.Sleep(1000);
                        if (Directory.Exists(duongdanprofile + @"\" + strProfile))
                        {
                            strClaim = GetClaim(duongdanprofile + @"\" + strProfile);
                            if (!strClaim.Contains("Claimed"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(">" + index + ":" + rootFolder + ": " + strClaim + " ══> Not Backup");
                                File.AppendAllText(skipBackupPrf, listprofile[index] + Environment.NewLine);
                                Console.ResetColor();
                                continue;
                            }
                        }
                    }
                    
                    #region Set chromeOptions
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--no-sandbox");
                    chromeOptions.AddArgument("--disable-notifications");
                    chromeOptions.AddArgument("--disable-push_messaging");
                    //chromeOptions.AddArguments(new string[2] { "--window-position=800,200", "--window-size=800,600", });
                    chromeOptions.AddArgument("--start-maximized");
                    chromeOptions.AddExcludedArgument("--incognito");
                    chromeOptions.AddExcludedArgument("enable-automation");
                    chromeOptions.AddArgument("--user-data-dir=" + duongdanprofile);
                    chromeOptions.AddArgument("--profile-directory=" + strProfile);
                    // Mở profile phiên bản hiện tại
                    chromeOptions.BinaryLocation = txt_pathBrave.Text;

                    // Brave phiên bản hiện tại
                    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                    service.SuppressInitialDiagnosticInformation = true;
                    service.HideCommandPromptWindow = true;
                    service.Port = 9999;
                    #endregion
                    System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"C:\Users\Admin\Desktop\Debug\chromedriver.exe");
                    driver = new ChromeDriver(service, chromeOptions);
                RW_FLG:
                    driver.Navigate().GoToUrl("brave://rewards/");
                    Thread.Sleep(1000);
                    while (isloadComplete(driver)) { break; }
                    // Kiểm tra đã hiển thị "RewardPage" chưa
                    try
                    {
                        checkOpenRewardPage = driver.FindElement(By.XPath("//*[@id=\"modal\"]/div/div[2]/div[2]/div/div/div[1]/div[1]/div")).Text.ToString();
                    }
                    catch (Exception ex)
                    {
                        // Tìm và click "Manage your wallet"
                        try
                        {
                            Thread.Sleep(1000);
                            Console.WriteLine("Click [Manage your wallet]");
                            driver.FindElement(By.XPath("//button[@data-test-id=\"manage-wallet-button\"]")).Click();
                            Thread.Sleep(1500);
                        }
                        catch (Exception)
                        {
                            if (!chk)
                            {
                                chk = true;
                                goto RW_FLG;
                            }
                            continue;
                        }

                    }
                    driver.Dispose();
                    driver.Quit();
                    Thread.Sleep(Convert.ToInt32(txt_delay_bk_key.Text));
                    // Mở Brave 1.11
                    string[] fileNames = Form1.GetFileNames(duongdanprofile + @"\" + strProfile + "\\Local Storage\\leveldb", "*.log");
                    for (int index1 = 0; index1 < ((IEnumerable<string>)fileNames).Count<string>(); ++index1)
                    {
                        string[] source = SplitString(System.IO.File.ReadAllText(duongdanprofile + @"\" + strProfile + "\\Local Storage\\leveldb\\" + fileNames[index1]).Replace("\0", ""), "\"paymentId\"");
                        for (int index2 = 0; index2 < ((IEnumerable<string>)source).Count<string>(); ++index2)
                        {
                            strKey = Regex.Match(source[index2], "\"recoveryKey\":\"(.*?)\"").Groups[1].Value;
                            if (strKey != "")
                            {
                                //((Dictionary<string, object>)LocalConfigure.allAccount().ToArray().GetValue(Convert.ToInt32(_index)))["_Key_info_seed"] = (object)key;
                                //LocalConfigure.saveAccountList();
                                //this.lsvInfo.Invoke((Delegate)(() => this.lsvInfo.Items[Convert.ToInt32(_index)].SubItems[4].Text = key));
                                //this.lsvInfo.Items[Convert.ToInt32(_index)].Checked = false;
                                break;
                            }
                        }
                    }

                    if (String.IsNullOrEmpty(strKey))
                    {
                        ErrorGetKey(listprofile[index]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(index + "> " + listprofile[index] + ": " + strClaim + " ══> Get key success|" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        Console.ResetColor();
                        Console.WriteLine(strKey);
                        // Ghép bat + 
                        if (chkBackupClaimed.Checked && strClaim.Contains("Claimed"))
                        {
                            List<String> lsClaim = new List<String>(strClaim.Split("|".ToCharArray()));
                            //string tongbat = strClaim.Substring(strClaim.IndexOf("Total Bat: ")).Replace("Total Bat: ", "");
                            strKey = strKey + "|" + lsClaim[1].Replace(":", "") + "|" + lsClaim[2].Replace(":", "");
                        }
                        File.AppendAllText(keyoutput, strKey + Environment.NewLine);
                        // Add profile in skipBackupPrf.txt
                        File.AppendAllText(skipBackupPrf, listprofile[index] + Environment.NewLine);
                        ++num1;
                    }
                    driver.Dispose();
                    driver.Quit();
                    Thread.Sleep(1500);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">Error: Catch End" + ex);
                    Console.ResetColor();
                    ErrorGetKey(listprofile[index]);
                    driver.Dispose();
                    driver.Quit();
                }
            }
            Console.ResetColor();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(">>>Profile success: {0}", (object)num1);
            Console.WriteLine("End time: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            MessageBox.Show("Get the completed backup key", "Completed backup key",
            MessageBoxButtons.OK, MessageBoxIcon.Information);

            CloseDriver();
            this.Killchromedriver();
            this.StopBrave();
        }
        private void ErrorGetKey(string str)
        {
            string profileError = ">Profile " + str + ": Error get key";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(profileError);
            File.AppendAllText(@"Key\Key_Error.txt", profileError + Environment.NewLine);
            driver.Quit();
        }
        private void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.IsReadOnly = false;
                try
                {
                    fi.Delete();
                }
                catch (Exception)
                {
                    continue;
                }
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete();
            }
        }
        public static string GetClaim(string profile)
        {
            string claim = "";
            if (Directory.Exists(profile))
            {
                string walletPaymentId = GetWalletPaymentID(profile);
                if (walletPaymentId.Replace("-", "").Length != 32)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    claim = "Error: Payment ID error";
                }
                else
                {
                    claim = Claim(walletPaymentId);
                }
                //claim = walletPaymentId.Replace("-", "").Length != 32 ? "Error: Payment ID error" : Claim(walletPaymentId);
            }
            return claim;
        }
        public static string GetWalletPaymentID(string profile)
        {
            string walletPaymentId = "";
            if (Directory.Exists(profile))
            {
                string path = Path.Combine(profile, "Preferences");
                if (System.IO.File.Exists(path))
                {
                    string str = System.IO.File.ReadAllText(path.ToString());
                    walletPaymentId = !str.Contains("payment_id\\") ? "Error: Payment ID error" : str.Substring(str.IndexOf("payment_id\\") + 15, 36);
                }
            }
            return walletPaymentId;
        }
        public static string Claim(string paymentID)
        {
            string str1 = "";
            using (WebClient webClient = new WebClient())
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                webClient.Headers.Add("Content-Type:application/json");
                webClient.Headers.Add("Accept:application/json");
                webClient.Headers.Add("Content-Type", "gzip, deflate, br");
                webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.75 Safari/537.36");
                //paymentID = "446c159c-29fa-48f1-b548-d835c79d650f";
                string str2 = webClient.DownloadString("https://grant.rewards.brave.com/v1/promotions?migrate=true&paymentId=" + paymentID + "&platform=windows");
                int num = CountSubString(str2, "id");
                if (num > 0)
                {
                    string str3 = "";
                    double total = 0;
                    string bat = "";
                    for (int index = 0; index < num; ++index)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        bat = GetBATPromotion(str2);
                        str3 = str3 + " Claimed " + (1 + index).ToString() + "|" + GetDatePromotion(str2) + "|" + bat; //+ ",";
                        str2 = GetLastSubstring(str2, str2.Length - GetSubStringIndex(str2, "publicKeys") - 2);
                        total = total + (Convert.ToDouble(bat.Replace(":", "")));
                    }
                    str1 = str3 + "|Total Bat: " + total;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    str1 = "No Claim !";
                }
            }
            return str1;
        }
        public static int CountSubString(string str, string substr) => Regex.Matches(str, substr).Count;
        public static string GetDatePromotion(string str) => str.Substring(str.IndexOf("createdAt")).Substring(12, 10);
        public static string GetBATPromotion(string str)
        {
            string batPromotion = str.Substring(str.IndexOf("approximateValue")).Substring(19, 10);
            string[] strArray = new string[6]
            {
        "\"",
        ",",
        "t",
        "y",
        "p",
        "e"
            };
            foreach (string oldValue in strArray)
                batPromotion = batPromotion.Replace(oldValue, string.Empty);
            return batPromotion;
        }
        public static string GetLastSubstring(string str, int index) => str.Substring(str.Length - index);
        public static int GetSubStringIndex(string str, string substr) => str.IndexOf(substr);
        private void CloseDriver()
        {

            if (driver != null)
            {
                try
                {
                    driver.Close();
                    driver.Quit();
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void Killchromedriver()
        {
            try
            {
                string str = Form1.configDirectory() + "\\chromedriver.exe";
                Process[] processByName = Form1.GetProcessByName("chromedriver");
                for (int index = 0; index < ((IEnumerable<Process>)processByName).Count<Process>(); ++index)
                {
                    if (processByName[index].MainModule.FileName == str)
                    {
                        try
                        {
                            processByName[index].Kill();
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
        }
        private static Boolean isloadComplete(IWebDriver driver)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("loaded")
                    || ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
        }
        private static string[] GetFileNames(string path, string filter)
        {
            string[] files = Directory.GetFiles(path, filter);
            for (int index = 0; index < files.Length; ++index)
                files[index] = Path.GetFileName(files[index]);
            return files;
        }
        public static string[] SplitString(string chuoi, string s)
        {
            try
            {
                return chuoi.Split(new string[1] { s }, StringSplitOptions.None);
            }
            catch
            {
                return (string[])null;
            }
        }
        public static string configDirectory() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private void StopBrave()
        {
            Process[] array = ((IEnumerable<Process>)Form1.GetProcessByNameBrave("brave")).OrderBy<Process, int>((Func<Process, int>)(p => p.Id)).ToArray<Process>();
            for (int index = 0; index < ((IEnumerable<Process>)array).Count<Process>(); ++index)
            {
                try
                {
                    Process process = array[index];
                    if (!process.HasExited)
                        process.Kill();
                }
                catch
                {
                }
            }
        }
        public static Process[] GetProcessByName(string processName)
        {
            Process[] processes = Process.GetProcesses();
            List<Process> processList = new List<Process>();
            foreach (Process process in processes)
            {
                if (process.ProcessName.StartsWith(processName))
                    processList.Add(process);
            }
            return processList.ToArray();
        }
        public static Process[] GetProcessByNameBrave(string processName)
        {
            Process[] processes = Process.GetProcesses();
            List<Process> processList = new List<Process>();
            foreach (Process process in processes)
            {
                if (process.ProcessName.StartsWith(processName))
                    processList.Add(process);
            }
            return processList.ToArray();
        }
        public void SaveConfig()
        {
            #region Save config
            string[] MainConfig = new string[2];
            MainConfig[0] = txtProfile.Text;
            // Đường dẫn Brave
            MainConfig[1] = txt_pathBrave.Text;
            //bool UserDataDir = new bool();
            string[] UserDataDir = new string[1];
            if (UserData1.Checked)
            {
                UserDataDir[0] = "1";
            }
            else if (UserData2.Checked)
            {
                UserDataDir[0] = "2";
            }
            else
            {
                UserDataDir[0] = "3";
            }

            #region Zcopy
            /*
            string[] ArrZcopy = new string[5];
            ArrZcopy[0] = txt_source_copy.Text;
            if (rd_s_1.Checked)
            {
                ArrZcopy[1] = "1";
            }
            else if (rd_s_2.Checked)
            {
                ArrZcopy[1] = "2";
            }
            else
            {
                ArrZcopy[1] = "3";
            }
            ArrZcopy[2] = txt_target_folder.Text;
            if (rd_t_1.Checked)
            {
                ArrZcopy[3] = "1";
            }
            else if (rd_t_1.Checked)
            {
                ArrZcopy[3] = "2";
            }
            else
            {
                ArrZcopy[3] = "3";
            }
            ArrZcopy[4] = txt_file_copy.Text;
            */
            #endregion

            Poke.SaveConfig(MainConfig, UserDataDir);
            #endregion
        }

        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.IsUpdateAvailable)
            {
                DialogResult dialogResult;
                dialogResult =
                        MessageBox.Show(
                            $@"Bạn ơi, phần mềm của bạn có phiên bản mới {args.CurrentVersion}. Phiên bản bạn đang sử dụng hiện tại {args.InstalledVersion}. Bạn có muốn cập nhật phần mềm không?", @"Cập nhật phần mềm",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);
                lnk_update.Text = $@"Đã có phiên bản mới: {args.CurrentVersion}";
                if (dialogResult.Equals(DialogResult.Yes) || dialogResult.Equals(DialogResult.OK))
                {
                    try
                    {
                        if (AutoUpdater.DownloadUpdate(args))
                        {
                            Application.Exit();
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                lnk_update.Text = $@"Phiên bản: {args.InstalledVersion}";
            }
        }

        private void deletetxt(string pathFile)
        {
            try
            {
                if (File.Exists(pathFile))
                {
                    File.Delete(pathFile);
                }
            }
            catch (Exception)
            {
            }
        }
        private void link_About_LinkClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://t.me/pikazai90");
        }

        private void btn_Delete_Skip_File_Click(object sender, EventArgs e)
        {
            string skipBackupPrf = @"SkipProfile\skipBackupPrf.txt";
            deletetxt(skipBackupPrf);
        }
    }
}
