using DAL.Utils;
using DotNetEnv;
using System.Data.SqlClient;
using System.Diagnostics;
using eyewear_store_management_system.Utils;
using BLL;
using DAL.Object;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;

namespace eyewear_store_management_system
{
    public partial class LoginForm : Form
    {
        private (string path, bool existed) _envPath;
        private bool _connected;
        #region Variables Animation
        private int[] _loginPanelY = new int[] { 0, 80 };
        private int[] _loginPanelHeight = new int[] { 264, 511 };
        private int[] _panelParentX = new int[] {0, 440};
        private int _action = -1; // -1 : Change Password -> None | 1 : Change Password | 2 : Show Database Information | -2 : Show Database Information -> None
        private int _prevAction = -1;
        #endregion

        public LoginForm()
        {
            this.Hide();
            InitializeComponent();
        }

        #region Change Login Form
        private void btnForgotPassword_MouseDown(object sender, MouseEventArgs e)
        {
            if (_action > 0 && _action != 1) _prevAction = _action;
            _action = 1;
            btnForgotPassword.ForeColor = Color.LightGray;
            timerLoginForm.Start();
        }
        private void btnForgotPassword_MouseUp(object sender, MouseEventArgs e)
        {
            btnForgotPassword.ForeColor= Color.Black;
        }
        private void btnDatabaseInformation_MouseDown(object sender, MouseEventArgs e)
        {
            _action = (_action == 2 ? -2 : 2);
            timerLoginForm.Start();
        }
        private void btnBackToSignIn_MouseDown(object sender, MouseEventArgs e)
        {
            _action = -1;
            btnBackToSignIn.ForeColor = Color.LightGray;
            timerLoginForm.Start();
            ClearChangePassword();
        }
        private void btnBackToSignIn_MouseUp(object sender, MouseEventArgs e)
        {
            btnBackToSignIn.ForeColor = Color.Black;
        }
        private void timerLoginForm_Tick(object sender, EventArgs e)
        {
            int loginPanelYStepY = 20;
            int loginPanelHeightStepHeight = 19;
            int panelParentXStepX = 40;
            if (_action == 2) // Show Database Information
            {
                if (panelSignIn.Location.Y > _loginPanelY[0])
                {
                    panelSignIn.Location = new Point(panelSignIn.Location.X, panelSignIn.Location.Y - loginPanelYStepY);
                }else if (panelSignIn.Location.Y <= _loginPanelY[0])
                {
                    panelSignIn.Location = new Point(panelSignIn.Location.X, _loginPanelY[0]);
                    if (panelSignIn.Height < _loginPanelHeight[1])
                    {
                        panelSignIn.Size = new Size(panelSignIn.Width, panelSignIn.Height + loginPanelHeightStepHeight);
                    }else if (panelSignIn.Height >= _loginPanelHeight[1])
                    {
                        panelSignIn.Size = new Size(panelSignIn.Width, _loginPanelHeight[1]);
                        timerLoginForm.Stop();
                    }
                }
            }
            if (_action == 1) // Change Password
            {
                if (panelParent.Location.X > _panelParentX[0])
                {
                    panelParent.Location = new Point(panelParent.Location.X - panelParentXStepX, panelParent.Location.Y);
                }else if (panelParent.Location.X <= _panelParentX[0])
                {
                    panelParent.Location = new Point(_panelParentX[0], panelParent.Location.Y);
                    timerLoginForm.Stop();
                }
            }
            if (_action == -1) // Change Password -> Default
            {
                if (panelParent.Location.X < _panelParentX[1])
                {
                    panelParent.Location = new Point(panelParent.Location.X + panelParentXStepX, panelParent.Location.Y);
                }
                else if (panelParent.Location.X >= _panelParentX[1])
                {
                    panelParent.Location = new Point(_panelParentX[1], panelParent.Location.Y);
                    timerLoginForm.Stop();
                    if (_prevAction > 0) _action = _prevAction;
                }
            }
            if (_action == -2) // Show Database Information -> Default
            {
                if (panelSignIn.Height > _loginPanelHeight[0])
                {
                    panelSignIn.Size = new Size(panelSignIn.Width, panelSignIn.Height - loginPanelHeightStepHeight);
                }
                else if (panelSignIn.Height <= _loginPanelHeight[0])
                {
                    panelSignIn.Size = new Size(panelSignIn.Width, _loginPanelHeight[0]);
                    if (panelSignIn.Location.Y < _loginPanelY[1])
                    {
                        panelSignIn.Location = new Point(panelSignIn.Location.X, panelSignIn.Location.Y + loginPanelYStepY);
                    }
                    else if (panelSignIn.Location.Y >= _loginPanelY[1])
                    {
                        panelSignIn.Location = new Point(panelSignIn.Location.X, _loginPanelY[1]);
                        timerLoginForm.Stop();
                    }
                }
            }
        }
        #endregion

        #region Action
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Check Configuration file
            _envPath = Program.FindEnvPathNearExe();
            Debug.WriteLine("Env Path: " + _envPath.path);
        }
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            // Account Authentication
            if (string.IsNullOrEmpty(txtbUsername.Texts.Trim()))
            {
                ToastManager.ShowToastNotification("Invalid Username", "Please fill the username", "error", this);
                return;
            }

            if (string.IsNullOrEmpty(txtbPassword.Texts.Trim()))
            {
                ToastManager.ShowToastNotification("Invalid Password", "Please fill the password", "error", this);
                return;
            }

            string username = txtbUsername.Texts.Trim();
            string password = UtilitySecurity.HashPassword(txtbPassword.Texts.Trim());
            try
            {
                Employee employee = EmployeeBLL.Authentication(username, password);
                if (employee == null)
                {
                    ToastManager.ShowToastNotification("Login Fail", "Username or Password is incorrect", "error", this);
                    return;
                }
                MainForm mainForm = new MainForm(employee);
                mainForm.Show();
                this.Hide();

                mainForm.FormClosed += (s, args) => this.Show();
            }
            catch (Exception ex)
            {
                ToastManager.ShowToastNotification("Login Error",ex.Message,"error",this);
                return;
            }
        }
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            _action = -1;

            // Save password to database
            if (myRecoveryCode == false)
            {
                ToastManager.ShowToastNotification("Email not verified yet", "You have not verified your email", "error", this);
                return;
            }

            if (txtbChangePasswordPassword.Texts.Trim() != txtbChangePasswordConfirmPassword.Texts.Trim())
            {
                ToastManager.ShowToastNotification("Incorrect password", "The two passwords do not match", "error", this);
                return;
            }

            bool changePassSuccess = EmployeeBLL.ChangePassword(txtbChangePasswordUserName.Texts.Trim(),UtilitySecurity.HashPassword(txtbChangePasswordPassword.Texts.Trim()));
            if (changePassSuccess)
            {
                ToastManager.ShowToastNotification("Change Password Success", "You have successfully changed your password", "success", this);
            }
            timerLoginForm.Start();
            ClearChangePassword();
        }
        private void LoginForm_Shown(object sender, EventArgs e)
        {
            if (_envPath.existed == false)
            {
                SignInDisable();
                btnDatabaseInformation_MouseDown(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
            }
            else
            {
                LoadEnvFile();
                DatabaseConnect();
            }
            this.Show();
            if (_envPath.existed == false) ToastManager.ShowToastNotification("Missing Configuration", "Configuration information has not been set up", "error", this);
            else if (_connected == false) ToastManager.ShowToastNotification("Incorrect Database Information", "Make sure your .env file is filled in correctly", "error", this);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save Configuration in file
            if (string.IsNullOrEmpty(txtbServerName.Texts) || string.IsNullOrEmpty(txtbDatabaseName.Texts))
            {
                string errorString = (string.IsNullOrEmpty(txtbServerName.Texts) ? "Server" : "Database");
                ToastManager.ShowToastNotification($"Missing {errorString} Name", $"{errorString} name cannot be empty", "error", this);
                return;
            }

            string serverName = txtbServerName.Texts.Trim();
            string databaseName = txtbDatabaseName.Texts.Trim();
            string databaseUsername = txtbDatabaseUsername.Texts.Trim();
            string databasePassword = txtbDatabasePassword.Texts.Trim();

            WriteEnvFile(serverName,databaseName,databaseUsername,databasePassword);
            LoadEnvFile();
            DatabaseConnect();
            if (_connected == true)
            {
                _action = -2;
                timerLoginForm.Start();
                ToastManager.ShowToastNotification("Database Connected", "Database connection successful", "success", this);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (_connected == true) UtilityDatabase.Instance.CloseConnection();
            this.Close();
        }
        private void LoadEnvFile()
        {
            Env.Load(_envPath.path);
            string server = Env.GetString("DB_SERVER");
            string database = Env.GetString("DB_NAME");
            string username = Env.GetString("DB_USERNAME");
            string password = Env.GetString("DB_PASSWORD");

            txtbServerName.Texts = server;
            txtbDatabaseName.Texts = database;
            txtbDatabaseUsername.Texts = username;
            txtbDatabasePassword.Texts = password;

            string connectMethod = "";
            string connectionString = "";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                connectMethod = "WinAuthConnection";
                // Connection string cho Windows Authentication
                connectionString = $"Server={server};Database={database};Integrated Security=True;";
            }
            else
            {
                connectMethod = "SqlAuthConnection";
                // Connection string cho SQL Server Authentication
                connectionString = $"Server={server};Database={database};User Id={username};Password={password};";
            }
            Debug.WriteLine(connectMethod);
            Debug.WriteLine(connectionString);
            // Cập nhật App.config
            Program.UpdateAppConfig(connectMethod, connectionString);
        }
        private void DatabaseConnect()
        {
            UtilityDatabase db = null;
            try
            {
                // Khởi tạo kết nối SQL Server
                db = UtilityDatabase.Instance;
                SignInEnable();
                _connected = true;
            }
            catch (SqlException ex)
            {
                _connected = false;
                Debug.WriteLine(ex.Message);
                if (txtbUsername.Enabled) SignInDisable();
                if (_action != 2) btnDatabaseInformation_MouseDown(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                ToastManager.ShowToastNotification("Incorrect Database Information", "Make sure your .env file is filled in correctly", "error", this);
            }
        }
        public void WriteEnvFile(string server, string dbName, string username, string password)
        {
            string directory = Path.GetDirectoryName(_envPath.path);
            // Debug.WriteLine("Directory: " + directory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var content = new List<string>
            {
                $"DB_SERVER={server}",
                $"DB_NAME={dbName}",
                $"DB_USERNAME={username}",
                $"DB_PASSWORD={password}"
            };

            File.WriteAllLines(_envPath.path, content);
        }
        private void SignInDisable()
        {
            txtbUsername.Enabled = false;
            txtbPassword.Enabled = false;
            btnForgotPassword.Enabled = false;
            btnSignIn.Enabled = false;
            txtbUsername.BorderColor = Color.Gainsboro;
            txtbPassword.BorderColor = Color.Gainsboro;
            btnForgotPassword.ForeColor = Color.DimGray;
            btnSignIn.BackColor = Color.DarkGray;
        }
        private void SignInEnable()
        {
            txtbUsername.Enabled = true;
            txtbPassword.Enabled = true;
            btnForgotPassword.Enabled = true;
            btnSignIn.Enabled = true;
            txtbUsername.BorderColor = Color.DarkGray;
            txtbPassword.BorderColor = Color.DarkGray;
            btnForgotPassword.ForeColor = Color.Black;
            btnSignIn.BackColor = Color.FromArgb(51, 51, 51);
        }

        private int countdown;
        private string? recoveryCode;
        private bool myRecoveryCode;
        private void btnSendMail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbChangePasswordUserName.Texts.Trim()))
            {
                ToastManager.ShowToastNotification("Invalid Email", "Cannot send email", "error", this);
                return;
            }
            if (!EmployeeBLL.ExistedEmail(txtbChangePasswordUserName.Texts.Trim()))
            {
                ToastManager.ShowToastNotification("Nonexistent Email", "No account found with this email", "error", this);
                return;
            }

            txtbChangePasswordRecoveryCode.Enabled = true;
            txtbChangePasswordRecoveryCode.BorderColor = Color.DarkGray;
            myRecoveryCode = false;
            SendMailDisable();
            countdown = 5;
            timerSendMailButton.Start();

            recoveryCode = new Random().Next(100000, 1000000).ToString();
            using (MailMessage mail = new MailMessage("dev.creation.dream.stu@gmail.com", txtbChangePasswordUserName.Texts.Trim(), "Recovery Code", $"Your recovery code is {recoveryCode}. Keep this email secret. Shhh."))
            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("dev.creation.dream.stu@gmail.com", "syvo hstq kade hjah");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                ToastManager.ShowToastNotification("Recovery Code Sent", "The recovery code has been sent to your email. If you don't see it, please check your Spam or Junk folder","success", this);
            }
        }

        private void timerSendMailButton_Tick(object sender, EventArgs e)
        {
            if (countdown > 0)
            {
                btnSendMail.Text = $"Send ({countdown}s)";
                countdown--;
            }
            else
            {
                timerSendMailButton.Stop(); // Dừng timer
                btnSendMail.Text = "Send";      // Đặt lại text gốc
                SendMailEnable();
            }
        }

        private void SendMailDisable()
        {
            btnSendMail.Enabled = false;
            btnSendMail.BackColor = Color.DarkGray;
        }

        private void SendMailEnable()
        {
            btnSendMail.Enabled = true;
            btnSendMail.BackColor = Color.FromArgb(51, 51, 51);
        }

        private void txtbChangePasswordRecoveryCode_Leave(object sender, EventArgs e)
        {
            if (txtbChangePasswordRecoveryCode.Texts.Trim() != recoveryCode)
            {
                txtbChangePasswordRecoveryCode.BackColor = Color.IndianRed;
                ToastManager.ShowToastNotification("Invalid Recovery Code", "Please double-check your email and recovery code", "error", this);
                return;
            }
            else
            {
                txtbChangePasswordRecoveryCode.BackColor = Color.LightGray;
                myRecoveryCode = true;
            }
        }

        private void ClearChangePassword()
        {
            txtbChangePasswordUserName.Texts = "";
            txtbChangePasswordRecoveryCode.Texts = "";
            txtbChangePasswordPassword.Texts = "";
            txtbChangePasswordConfirmPassword.Texts = "";
            txtbChangePasswordRecoveryCode.BorderColor = Color.Gainsboro;
            txtbChangePasswordRecoveryCode.Enabled = false;
            myRecoveryCode = false;
            recoveryCode = "";
        }
        private void txtbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnSignIn_Click(sender, EventArgs.Empty);
            }
        }
        #endregion


    }
}