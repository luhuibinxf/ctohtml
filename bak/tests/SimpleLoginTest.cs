using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SimpleLoginTest
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "登录测试";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label();
            lblTitle.Text = "数据库存储过程调用工具";
            lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 16, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(50, 30);
            lblTitle.AutoSize = true;
            this.Controls.Add(lblTitle);

            Label lblUsername = new Label();
            lblUsername.Text = "用户名";
            lblUsername.Font = new System.Drawing.Font("Microsoft YaHei UI", 10);
            lblUsername.Location = new System.Drawing.Point(50, 80);
            lblUsername.AutoSize = true;
            this.Controls.Add(lblUsername);

            TextBox txtUsername = new TextBox();
            txtUsername.Location = new System.Drawing.Point(50, 105);
            txtUsername.Size = new System.Drawing.Size(300, 25);
            txtUsername.Font = new System.Drawing.Font("Microsoft YaHei UI", 10);
            txtUsername.Text = "lhbdb";
            this.Controls.Add(txtUsername);

            Label lblPassword = new Label();
            lblPassword.Text = "密码";
            lblPassword.Font = new System.Drawing.Font("Microsoft YaHei UI", 10);
            lblPassword.Location = new System.Drawing.Point(50, 140);
            lblPassword.AutoSize = true;
            this.Controls.Add(lblPassword);

            TextBox txtPassword = new TextBox();
            txtPassword.Location = new System.Drawing.Point(50, 165);
            txtPassword.Size = new System.Drawing.Size(300, 25);
            txtPassword.Font = new System.Drawing.Font("Microsoft YaHei UI", 10);
            txtPassword.PasswordChar = '*';
            txtPassword.Text = "241023";
            this.Controls.Add(txtPassword);

            Button btnLogin = new Button();
            btnLogin.Text = "登录";
            btnLogin.Font = new System.Drawing.Font("Microsoft YaHei UI", 12, System.Drawing.FontStyle.Bold);
            btnLogin.Location = new System.Drawing.Point(50, 200);
            btnLogin.Size = new System.Drawing.Size(300, 40);
            btnLogin.Click += (sender, e) =>
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("请输入用户名和密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    // 使用默认连接字符串
                    string connString = "Server=127.0.0.1,1433;Database=WiNEX_PACS;User Id=sa;Password=P@ssw0rd;";
                    MessageBox.Show("尝试连接数据库...", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    using (var connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        MessageBox.Show("数据库连接成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 检查用户表是否存在
                        using (var checkTableCmd = new SqlCommand("SELECT COUNT(*) FROM sysobjects WHERE name='TJYHB' AND xtype='U'", connection))
                        {
                            int tableCount = (int)checkTableCmd.ExecuteScalar();
                            if (tableCount == 0)
                            {
                                MessageBox.Show("用户表不存在，正在创建...", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CreateUserTable(connection);
                            }
                        }

                        // 验证用户
                        using (var validateCmd = new SqlCommand("SELECT * FROM TJYHB WHERE Username = @Username", connection))
                        {
                            validateCmd.Parameters.AddWithValue("@Username", username);
                            using (var reader = validateCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string storedPassword = reader["Password"].ToString();
                                    bool isLocked = (bool)reader["IsLocked"];

                                    if (isLocked)
                                    {
                                        MessageBox.Show("用户已被锁定！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    if (storedPassword == password)
                                    {
                                        MessageBox.Show("登录成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Hide();
                                        MainForm mainForm = new MainForm();
                                        mainForm.ShowDialog();
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("密码错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("用户不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("连接数据库失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            this.Controls.Add(btnLogin);
        }

        private bool CreateUserTable(SqlConnection connection)
        {
            try
            {
                string createTableSql = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TJYHB' AND xtype='U')
                CREATE TABLE TJYHB (
                    UserID INT IDENTITY(1,1) PRIMARY KEY,
                    Username NVARCHAR(50) UNIQUE NOT NULL,
                    Password NVARCHAR(100) NOT NULL,
                    CanViewStats BIT DEFAULT 1,
                    IsLocked BIT DEFAULT 0,
                    CreatedDate DATETIME DEFAULT GETDATE()
                );

                IF NOT EXISTS (SELECT * FROM TJYHB WHERE Username = 'lhbdb')
                INSERT INTO TJYHB (Username, Password, CanViewStats, IsLocked) VALUES ('lhbdb', '241023', 1, 0);

                IF NOT EXISTS (SELECT * FROM TJYHB WHERE Username = 'user')
                INSERT INTO TJYHB (Username, Password, CanViewStats, IsLocked) VALUES ('user', 'user123', 1, 0);

                IF NOT EXISTS (SELECT * FROM TJYHB WHERE Username = 'readonly')
                INSERT INTO TJYHB (Username, Password, CanViewStats, IsLocked) VALUES ('readonly', 'readonly123', 1, 0);";

                using (var command = new SqlCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("用户表创建成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建用户表失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "主界面";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label();
            lblTitle.Text = "程序已成功启动！";
            lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 16, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(50, 50);
            lblTitle.AutoSize = true;
            this.Controls.Add(lblTitle);

            Button btnExit = new Button();
            btnExit.Text = "退出";
            btnExit.Font = new System.Drawing.Font("Microsoft YaHei UI", 12);
            btnExit.Location = new System.Drawing.Point(200, 200);
            btnExit.Size = new System.Drawing.Size(100, 40);
            btnExit.Click += (sender, e) =>
            {
                this.Close();
            };
            this.Controls.Add(btnExit);
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}