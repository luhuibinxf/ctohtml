using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace DbProcedureCaller
{
    public static class TrayApplication
    {
        private static NotifyIcon trayIcon;
        private static ContextMenuStrip trayMenu;
        private static Thread serverThread;
        private static string serverPort = "8081";

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoadServerConfig();
            InitializeTrayIcon();
            StartServerInBackground();

            Application.Run();
        }

        private static void LoadServerConfig()
        {
            try
            {
                string exeDir = AppContext.BaseDirectory;
                string configFile = Path.Combine(exeDir, "server_config.dat");

                if (!File.Exists(configFile))
                {
                    string rootConfigFile = Path.Combine(exeDir, "..", "..", "..", "server_config.dat");
                    rootConfigFile = Path.GetFullPath(rootConfigFile);
                    if (File.Exists(rootConfigFile))
                    {
                        configFile = rootConfigFile;
                    }
                }

                if (!File.Exists(configFile))
                {
                    string cwdConfigFile = Path.Combine(Environment.CurrentDirectory, "server_config.dat");
                    if (File.Exists(cwdConfigFile))
                    {
                        configFile = cwdConfigFile;
                    }
                }

                if (File.Exists(configFile))
                {
                    string encrypted = File.ReadAllText(configFile).Trim();
                    string decrypted = Decrypt(encrypted);
                    if (!string.IsNullOrEmpty(decrypted))
                    {
                        serverPort = decrypted.Trim();
                    }
                }
            }
            catch
            {
                serverPort = "8081";
            }
        }

        private static string Decrypt(string input)
        {
            try
            {
                byte[] data = Convert.FromBase64String(input);
                return System.Text.Encoding.UTF8.GetString(data);
            }
            catch
            {
                return input;
            }
        }

        private static void InitializeTrayIcon()
        {
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("打开管理页面", null, OpenBrowser);
            trayMenu.Items.Add("显示日志", null, ShowLog);
            trayMenu.Items.Add("-");
            trayMenu.Items.Add("退出", null, ExitApplication);

            trayIcon = new NotifyIcon();
            trayIcon.Icon = CreateIcon();
            trayIcon.Text = $"统计分析系统 - 端口 {serverPort}";
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Visible = true;
            trayIcon.DoubleClick += OpenBrowser;

            trayIcon.ShowBalloonTip(2000, "服务已启动", $"HTTP服务器运行在端口 {serverPort}", ToolTipIcon.Info);
        }

        private static Icon CreateIcon()
        {
            using (Bitmap bmp = new Bitmap(64, 64))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Transparent);
                    Brush brush = new SolidBrush(Color.FromArgb(59, 130, 246));
                    g.FillEllipse(brush, 8, 8, 48, 48);
                    brush.Dispose();

                    Brush textBrush = new SolidBrush(Color.White);
                    Font font = new Font("Arial", 20, FontStyle.Bold);
                    g.DrawString("S", font, textBrush, 22, 16);
                    font.Dispose();
                    textBrush.Dispose();
                }
                return Icon.FromHandle(bmp.GetHicon());
            }
        }

        private static void StartServerInBackground()
        {
            serverThread = new Thread(() =>
            {
                try
                {
                    ConsoleProgram.StartServer(serverPort);
                }
                catch (Exception ex)
                {
                    trayIcon.ShowBalloonTip(2000, "启动失败", ex.Message, ToolTipIcon.Error);
                }
            });
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        private static void OpenBrowser(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start($"http://localhost:{serverPort}/");
            }
            catch
            {
                trayIcon.ShowBalloonTip(2000, "提示", "请手动打开浏览器访问 http://localhost:" + serverPort, ToolTipIcon.Info);
            }
        }

        private static void ShowLog(object sender, EventArgs e)
        {
            try
            {
                string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.log");
                if (System.IO.File.Exists(logPath))
                {
                    System.Diagnostics.Process.Start("notepad.exe", logPath);
                }
                else
                {
                    trayIcon.ShowBalloonTip(2000, "提示", "日志文件不存在", ToolTipIcon.Info);
                }
            }
            catch (Exception ex)
            {
                trayIcon.ShowBalloonTip(2000, "错误", ex.Message, ToolTipIcon.Error);
            }
        }

        private static void ExitApplication(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();
            ConsoleProgram.StopServer();
            Application.Exit();
        }
    }
}