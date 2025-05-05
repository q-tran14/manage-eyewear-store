namespace eyewear_store_management_system
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbUsername = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.txtbPassword = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.btnExit = new eyewear_store_management_system.CustomComponents.CustomButton();
            this.btnSignIn = new eyewear_store_management_system.CustomComponents.CustomButton();
            this.panelParent = new System.Windows.Forms.Panel();
            this.txtbChangePasswordRecoveryCode = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.btnSendMail = new eyewear_store_management_system.CustomComponents.CustomButton();
            this.btnBackToSignIn = new System.Windows.Forms.Label();
            this.panelSignIn = new System.Windows.Forms.Panel();
            this.btnForgotPassword = new System.Windows.Forms.Label();
            this.panelDatabase = new System.Windows.Forms.Panel();
            this.btnDatabaseInformation = new System.Windows.Forms.Label();
            this.btnSave = new eyewear_store_management_system.CustomComponents.CustomButton();
            this.txtbDatabasePassword = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.txtbDatabaseUsername = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.txtbDatabaseName = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.txtbServerName = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.divider1 = new eyewear_store_management_system.CustomComponents.Divider();
            this.btnChangePassword = new eyewear_store_management_system.CustomComponents.CustomButton();
            this.txtbChangePasswordConfirmPassword = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.txtbChangePasswordPassword = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.txtbChangePasswordUserName = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.label3 = new System.Windows.Forms.Label();
            this.timerLoginForm = new System.Windows.Forms.Timer(this.components);
            this.timerSendMailButton = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelParent.SuspendLayout();
            this.panelSignIn.SuspendLayout();
            this.panelDatabase.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::eyewear_store_management_system.Properties.Resources.intro;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(440, 550);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Californian FB", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(122, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sign In";
            // 
            // txtbUsername
            // 
            this.txtbUsername.BackColor = System.Drawing.SystemColors.Window;
            this.txtbUsername.BorderColor = System.Drawing.Color.DarkGray;
            this.txtbUsername.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.txtbUsername.BorderRadius = 5;
            this.txtbUsername.BorderSize = 3;
            this.txtbUsername.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbUsername.ForeColor = System.Drawing.Color.Black;
            this.txtbUsername.Location = new System.Drawing.Point(20, 56);
            this.txtbUsername.Margin = new System.Windows.Forms.Padding(4);
            this.txtbUsername.Multiline = false;
            this.txtbUsername.Name = "txtbUsername";
            this.txtbUsername.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbUsername.PasswordChar = false;
            this.txtbUsername.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbUsername.PlaceholderText = "Username (Phone or Email)";
            this.txtbUsername.Size = new System.Drawing.Size(340, 40);
            this.txtbUsername.TabIndex = 2;
            this.txtbUsername.Texts = "";
            this.txtbUsername.UnderlinedStyle = false;
            // 
            // txtbPassword
            // 
            this.txtbPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtbPassword.BorderColor = System.Drawing.Color.DarkGray;
            this.txtbPassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.txtbPassword.BorderRadius = 5;
            this.txtbPassword.BorderSize = 3;
            this.txtbPassword.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbPassword.ForeColor = System.Drawing.Color.Black;
            this.txtbPassword.Location = new System.Drawing.Point(20, 113);
            this.txtbPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtbPassword.Multiline = false;
            this.txtbPassword.Name = "txtbPassword";
            this.txtbPassword.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbPassword.PasswordChar = true;
            this.txtbPassword.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbPassword.PlaceholderText = "Password";
            this.txtbPassword.Size = new System.Drawing.Size(340, 40);
            this.txtbPassword.TabIndex = 3;
            this.txtbPassword.Texts = "";
            this.txtbPassword.UnderlinedStyle = false;
            this.txtbPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbPassword_KeyPress);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Silver;
            this.btnExit.BackgroundColor = System.Drawing.Color.Silver;
            this.btnExit.BackgroundImage = global::eyewear_store_management_system.Properties.Resources.reject;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.BorderColor = System.Drawing.Color.Silver;
            this.btnExit.BorderRadius = 0;
            this.btnExit.BorderSize = 0;
            this.btnExit.ClickColor = System.Drawing.Color.Silver;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(842, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(30, 30);
            this.btnExit.TabIndex = 5;
            this.btnExit.TextColor = System.Drawing.Color.White;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSignIn
            // 
            this.btnSignIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSignIn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSignIn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSignIn.BorderRadius = 10;
            this.btnSignIn.BorderSize = 0;
            this.btnSignIn.ClickColor = System.Drawing.Color.LightGray;
            this.btnSignIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSignIn.FlatAppearance.BorderSize = 0;
            this.btnSignIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignIn.Font = new System.Drawing.Font("Californian FB", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.btnSignIn.ForeColor = System.Drawing.Color.White;
            this.btnSignIn.Location = new System.Drawing.Point(237, 169);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(100, 40);
            this.btnSignIn.TabIndex = 4;
            this.btnSignIn.Text = "Sign In";
            this.btnSignIn.TextColor = System.Drawing.Color.White;
            this.btnSignIn.UseVisualStyleBackColor = false;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // panelParent
            // 
            this.panelParent.Controls.Add(this.txtbChangePasswordRecoveryCode);
            this.panelParent.Controls.Add(this.btnSendMail);
            this.panelParent.Controls.Add(this.btnBackToSignIn);
            this.panelParent.Controls.Add(this.panelSignIn);
            this.panelParent.Controls.Add(this.btnChangePassword);
            this.panelParent.Controls.Add(this.txtbChangePasswordConfirmPassword);
            this.panelParent.Controls.Add(this.txtbChangePasswordPassword);
            this.panelParent.Controls.Add(this.txtbChangePasswordUserName);
            this.panelParent.Controls.Add(this.label3);
            this.panelParent.Location = new System.Drawing.Point(440, 39);
            this.panelParent.Name = "panelParent";
            this.panelParent.Size = new System.Drawing.Size(880, 511);
            this.panelParent.TabIndex = 7;
            // 
            // txtbChangePasswordRecoveryCode
            // 
            this.txtbChangePasswordRecoveryCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtbChangePasswordRecoveryCode.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtbChangePasswordRecoveryCode.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.txtbChangePasswordRecoveryCode.BorderRadius = 5;
            this.txtbChangePasswordRecoveryCode.BorderSize = 3;
            this.txtbChangePasswordRecoveryCode.Enabled = false;
            this.txtbChangePasswordRecoveryCode.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbChangePasswordRecoveryCode.ForeColor = System.Drawing.Color.Black;
            this.txtbChangePasswordRecoveryCode.Location = new System.Drawing.Point(495, 193);
            this.txtbChangePasswordRecoveryCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtbChangePasswordRecoveryCode.Multiline = false;
            this.txtbChangePasswordRecoveryCode.Name = "txtbChangePasswordRecoveryCode";
            this.txtbChangePasswordRecoveryCode.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbChangePasswordRecoveryCode.PasswordChar = false;
            this.txtbChangePasswordRecoveryCode.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbChangePasswordRecoveryCode.PlaceholderText = "Recovery Code";
            this.txtbChangePasswordRecoveryCode.Size = new System.Drawing.Size(340, 40);
            this.txtbChangePasswordRecoveryCode.TabIndex = 24;
            this.txtbChangePasswordRecoveryCode.Texts = "";
            this.txtbChangePasswordRecoveryCode.UnderlinedStyle = false;
            this.txtbChangePasswordRecoveryCode.Leave += new System.EventHandler(this.txtbChangePasswordRecoveryCode_Leave);
            // 
            // btnSendMail
            // 
            this.btnSendMail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSendMail.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSendMail.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSendMail.BorderRadius = 10;
            this.btnSendMail.BorderSize = 0;
            this.btnSendMail.ClickColor = System.Drawing.Color.LightGray;
            this.btnSendMail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendMail.FlatAppearance.BorderSize = 0;
            this.btnSendMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendMail.Font = new System.Drawing.Font("Californian FB", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.btnSendMail.ForeColor = System.Drawing.Color.White;
            this.btnSendMail.Location = new System.Drawing.Point(738, 142);
            this.btnSendMail.Name = "btnSendMail";
            this.btnSendMail.Size = new System.Drawing.Size(97, 40);
            this.btnSendMail.TabIndex = 23;
            this.btnSendMail.Text = "Send";
            this.btnSendMail.TextColor = System.Drawing.Color.White;
            this.btnSendMail.UseVisualStyleBackColor = false;
            this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
            // 
            // btnBackToSignIn
            // 
            this.btnBackToSignIn.AutoSize = true;
            this.btnBackToSignIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackToSignIn.Font = new System.Drawing.Font("Californian FB", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.btnBackToSignIn.Location = new System.Drawing.Point(712, 355);
            this.btnBackToSignIn.Name = "btnBackToSignIn";
            this.btnBackToSignIn.Size = new System.Drawing.Size(101, 22);
            this.btnBackToSignIn.TabIndex = 22;
            this.btnBackToSignIn.Text = "Back to Sign In";
            this.btnBackToSignIn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnBackToSignIn_MouseDown);
            this.btnBackToSignIn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnBackToSignIn_MouseUp);
            // 
            // panelSignIn
            // 
            this.panelSignIn.Controls.Add(this.btnForgotPassword);
            this.panelSignIn.Controls.Add(this.btnSignIn);
            this.panelSignIn.Controls.Add(this.panelDatabase);
            this.panelSignIn.Controls.Add(this.txtbPassword);
            this.panelSignIn.Controls.Add(this.txtbUsername);
            this.panelSignIn.Controls.Add(this.label1);
            this.panelSignIn.Location = new System.Drawing.Point(29, 80);
            this.panelSignIn.Name = "panelSignIn";
            this.panelSignIn.Size = new System.Drawing.Size(373, 264);
            this.panelSignIn.TabIndex = 21;
            // 
            // btnForgotPassword
            // 
            this.btnForgotPassword.AutoSize = true;
            this.btnForgotPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnForgotPassword.Font = new System.Drawing.Font("Californian FB", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.btnForgotPassword.ForeColor = System.Drawing.Color.Black;
            this.btnForgotPassword.Location = new System.Drawing.Point(54, 178);
            this.btnForgotPassword.Name = "btnForgotPassword";
            this.btnForgotPassword.Size = new System.Drawing.Size(118, 22);
            this.btnForgotPassword.TabIndex = 23;
            this.btnForgotPassword.Text = "Forgot Password";
            this.btnForgotPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnForgotPassword_MouseDown);
            this.btnForgotPassword.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnForgotPassword_MouseUp);
            // 
            // panelDatabase
            // 
            this.panelDatabase.Controls.Add(this.btnDatabaseInformation);
            this.panelDatabase.Controls.Add(this.btnSave);
            this.panelDatabase.Controls.Add(this.txtbDatabasePassword);
            this.panelDatabase.Controls.Add(this.txtbDatabaseUsername);
            this.panelDatabase.Controls.Add(this.txtbDatabaseName);
            this.panelDatabase.Controls.Add(this.txtbServerName);
            this.panelDatabase.Controls.Add(this.divider1);
            this.panelDatabase.Location = new System.Drawing.Point(0, 215);
            this.panelDatabase.Name = "panelDatabase";
            this.panelDatabase.Size = new System.Drawing.Size(373, 296);
            this.panelDatabase.TabIndex = 20;
            // 
            // btnDatabaseInformation
            // 
            this.btnDatabaseInformation.AutoSize = true;
            this.btnDatabaseInformation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDatabaseInformation.Font = new System.Drawing.Font("Californian FB", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.btnDatabaseInformation.Location = new System.Drawing.Point(108, 13);
            this.btnDatabaseInformation.Name = "btnDatabaseInformation";
            this.btnDatabaseInformation.Size = new System.Drawing.Size(167, 22);
            this.btnDatabaseInformation.TabIndex = 24;
            this.btnDatabaseInformation.Text = "Database Infomation";
            this.btnDatabaseInformation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDatabaseInformation_MouseDown);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSave.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSave.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSave.BorderRadius = 10;
            this.btnSave.BorderSize = 0;
            this.btnSave.ClickColor = System.Drawing.Color.LightGray;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Californian FB", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(238, 246);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 40);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.TextColor = System.Drawing.Color.White;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtbDatabasePassword
            // 
            this.txtbDatabasePassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtbDatabasePassword.BorderColor = System.Drawing.Color.LightGray;
            this.txtbDatabasePassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.txtbDatabasePassword.BorderRadius = 5;
            this.txtbDatabasePassword.BorderSize = 2;
            this.txtbDatabasePassword.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbDatabasePassword.ForeColor = System.Drawing.Color.Black;
            this.txtbDatabasePassword.Location = new System.Drawing.Point(21, 199);
            this.txtbDatabasePassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtbDatabasePassword.Multiline = false;
            this.txtbDatabasePassword.Name = "txtbDatabasePassword";
            this.txtbDatabasePassword.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbDatabasePassword.PasswordChar = true;
            this.txtbDatabasePassword.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbDatabasePassword.PlaceholderText = "Password";
            this.txtbDatabasePassword.Size = new System.Drawing.Size(340, 40);
            this.txtbDatabasePassword.TabIndex = 18;
            this.txtbDatabasePassword.Texts = "";
            this.txtbDatabasePassword.UnderlinedStyle = false;
            // 
            // txtbDatabaseUsername
            // 
            this.txtbDatabaseUsername.BackColor = System.Drawing.SystemColors.Window;
            this.txtbDatabaseUsername.BorderColor = System.Drawing.Color.LightGray;
            this.txtbDatabaseUsername.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.txtbDatabaseUsername.BorderRadius = 5;
            this.txtbDatabaseUsername.BorderSize = 2;
            this.txtbDatabaseUsername.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbDatabaseUsername.ForeColor = System.Drawing.Color.Black;
            this.txtbDatabaseUsername.Location = new System.Drawing.Point(21, 151);
            this.txtbDatabaseUsername.Margin = new System.Windows.Forms.Padding(4);
            this.txtbDatabaseUsername.Multiline = false;
            this.txtbDatabaseUsername.Name = "txtbDatabaseUsername";
            this.txtbDatabaseUsername.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbDatabaseUsername.PasswordChar = false;
            this.txtbDatabaseUsername.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbDatabaseUsername.PlaceholderText = "Username";
            this.txtbDatabaseUsername.Size = new System.Drawing.Size(340, 40);
            this.txtbDatabaseUsername.TabIndex = 17;
            this.txtbDatabaseUsername.Texts = "";
            this.txtbDatabaseUsername.UnderlinedStyle = false;
            // 
            // txtbDatabaseName
            // 
            this.txtbDatabaseName.BackColor = System.Drawing.SystemColors.Window;
            this.txtbDatabaseName.BorderColor = System.Drawing.Color.LightGray;
            this.txtbDatabaseName.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.txtbDatabaseName.BorderRadius = 5;
            this.txtbDatabaseName.BorderSize = 2;
            this.txtbDatabaseName.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbDatabaseName.ForeColor = System.Drawing.Color.Black;
            this.txtbDatabaseName.Location = new System.Drawing.Point(21, 103);
            this.txtbDatabaseName.Margin = new System.Windows.Forms.Padding(4);
            this.txtbDatabaseName.Multiline = false;
            this.txtbDatabaseName.Name = "txtbDatabaseName";
            this.txtbDatabaseName.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbDatabaseName.PasswordChar = false;
            this.txtbDatabaseName.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbDatabaseName.PlaceholderText = "Database name";
            this.txtbDatabaseName.Size = new System.Drawing.Size(340, 40);
            this.txtbDatabaseName.TabIndex = 16;
            this.txtbDatabaseName.Texts = "";
            this.txtbDatabaseName.UnderlinedStyle = false;
            // 
            // txtbServerName
            // 
            this.txtbServerName.BackColor = System.Drawing.SystemColors.Window;
            this.txtbServerName.BorderColor = System.Drawing.Color.LightGray;
            this.txtbServerName.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.txtbServerName.BorderRadius = 5;
            this.txtbServerName.BorderSize = 2;
            this.txtbServerName.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbServerName.ForeColor = System.Drawing.Color.Black;
            this.txtbServerName.Location = new System.Drawing.Point(21, 55);
            this.txtbServerName.Margin = new System.Windows.Forms.Padding(4);
            this.txtbServerName.Multiline = false;
            this.txtbServerName.Name = "txtbServerName";
            this.txtbServerName.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbServerName.PasswordChar = false;
            this.txtbServerName.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbServerName.PlaceholderText = "Server name";
            this.txtbServerName.Size = new System.Drawing.Size(340, 40);
            this.txtbServerName.TabIndex = 15;
            this.txtbServerName.Texts = "";
            this.txtbServerName.UnderlinedStyle = false;
            // 
            // divider1
            // 
            this.divider1.IsVertical = false;
            this.divider1.LineColor = System.Drawing.Color.Gray;
            this.divider1.Location = new System.Drawing.Point(10, 8);
            this.divider1.Name = "divider1";
            this.divider1.Size = new System.Drawing.Size(350, 2);
            this.divider1.TabIndex = 13;
            this.divider1.Text = "divider1";
            this.divider1.Thickness = 2;
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnChangePassword.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnChangePassword.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnChangePassword.BorderRadius = 10;
            this.btnChangePassword.BorderSize = 0;
            this.btnChangePassword.ClickColor = System.Drawing.Color.LightGray;
            this.btnChangePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePassword.FlatAppearance.BorderSize = 0;
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Californian FB", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.btnChangePassword.ForeColor = System.Drawing.Color.White;
            this.btnChangePassword.Location = new System.Drawing.Point(495, 346);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(170, 40);
            this.btnChangePassword.TabIndex = 11;
            this.btnChangePassword.Text = "Change Password";
            this.btnChangePassword.TextColor = System.Drawing.Color.White;
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // txtbChangePasswordConfirmPassword
            // 
            this.txtbChangePasswordConfirmPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtbChangePasswordConfirmPassword.BorderColor = System.Drawing.Color.DarkGray;
            this.txtbChangePasswordConfirmPassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.txtbChangePasswordConfirmPassword.BorderRadius = 5;
            this.txtbChangePasswordConfirmPassword.BorderSize = 3;
            this.txtbChangePasswordConfirmPassword.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbChangePasswordConfirmPassword.ForeColor = System.Drawing.Color.Black;
            this.txtbChangePasswordConfirmPassword.Location = new System.Drawing.Point(495, 295);
            this.txtbChangePasswordConfirmPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtbChangePasswordConfirmPassword.Multiline = false;
            this.txtbChangePasswordConfirmPassword.Name = "txtbChangePasswordConfirmPassword";
            this.txtbChangePasswordConfirmPassword.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbChangePasswordConfirmPassword.PasswordChar = true;
            this.txtbChangePasswordConfirmPassword.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbChangePasswordConfirmPassword.PlaceholderText = "Confirm Password";
            this.txtbChangePasswordConfirmPassword.Size = new System.Drawing.Size(340, 40);
            this.txtbChangePasswordConfirmPassword.TabIndex = 10;
            this.txtbChangePasswordConfirmPassword.Texts = "";
            this.txtbChangePasswordConfirmPassword.UnderlinedStyle = false;
            // 
            // txtbChangePasswordPassword
            // 
            this.txtbChangePasswordPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtbChangePasswordPassword.BorderColor = System.Drawing.Color.DarkGray;
            this.txtbChangePasswordPassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.txtbChangePasswordPassword.BorderRadius = 5;
            this.txtbChangePasswordPassword.BorderSize = 3;
            this.txtbChangePasswordPassword.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbChangePasswordPassword.ForeColor = System.Drawing.Color.Black;
            this.txtbChangePasswordPassword.Location = new System.Drawing.Point(495, 244);
            this.txtbChangePasswordPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtbChangePasswordPassword.Multiline = false;
            this.txtbChangePasswordPassword.Name = "txtbChangePasswordPassword";
            this.txtbChangePasswordPassword.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbChangePasswordPassword.PasswordChar = true;
            this.txtbChangePasswordPassword.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbChangePasswordPassword.PlaceholderText = "Password";
            this.txtbChangePasswordPassword.Size = new System.Drawing.Size(340, 40);
            this.txtbChangePasswordPassword.TabIndex = 9;
            this.txtbChangePasswordPassword.Texts = "";
            this.txtbChangePasswordPassword.UnderlinedStyle = false;
            // 
            // txtbChangePasswordUserName
            // 
            this.txtbChangePasswordUserName.BackColor = System.Drawing.SystemColors.Window;
            this.txtbChangePasswordUserName.BorderColor = System.Drawing.Color.DarkGray;
            this.txtbChangePasswordUserName.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.txtbChangePasswordUserName.BorderRadius = 5;
            this.txtbChangePasswordUserName.BorderSize = 3;
            this.txtbChangePasswordUserName.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbChangePasswordUserName.ForeColor = System.Drawing.Color.Black;
            this.txtbChangePasswordUserName.Location = new System.Drawing.Point(495, 142);
            this.txtbChangePasswordUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtbChangePasswordUserName.Multiline = false;
            this.txtbChangePasswordUserName.Name = "txtbChangePasswordUserName";
            this.txtbChangePasswordUserName.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbChangePasswordUserName.PasswordChar = false;
            this.txtbChangePasswordUserName.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbChangePasswordUserName.PlaceholderText = "Mail";
            this.txtbChangePasswordUserName.Size = new System.Drawing.Size(236, 40);
            this.txtbChangePasswordUserName.TabIndex = 8;
            this.txtbChangePasswordUserName.Texts = "";
            this.txtbChangePasswordUserName.UnderlinedStyle = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Californian FB", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(517, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(296, 43);
            this.label3.TabIndex = 7;
            this.label3.Text = "Change Password";
            // 
            // timerLoginForm
            // 
            this.timerLoginForm.Interval = 5;
            this.timerLoginForm.Tick += new System.EventHandler(this.timerLoginForm_Tick);
            // 
            // timerSendMailButton
            // 
            this.timerSendMailButton.Interval = 5000;
            this.timerSendMailButton.Tick += new System.EventHandler(this.timerSendMailButton_Tick);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(880, 550);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panelParent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.Shown += new System.EventHandler(this.LoginForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelParent.ResumeLayout(false);
            this.panelParent.PerformLayout();
            this.panelSignIn.ResumeLayout(false);
            this.panelSignIn.PerformLayout();
            this.panelDatabase.ResumeLayout(false);
            this.panelDatabase.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private CustomComponents.CustomTextInput txtbUsername;
        private CustomComponents.CustomTextInput txtbPassword;
        private CustomComponents.CustomButton btnExit;
        private CustomComponents.CustomButton btnSignIn;
        private Panel panelParent;
        private Label label3;
        private CustomComponents.CustomTextInput txtbChangePasswordConfirmPassword;
        private CustomComponents.CustomTextInput txtbChangePasswordPassword;
        private CustomComponents.CustomTextInput txtbChangePasswordUserName;
        private CustomComponents.CustomButton btnChangePassword;
        private CustomComponents.Divider divider1;
        private CustomComponents.CustomTextInput txtbDatabaseUsername;
        private CustomComponents.CustomTextInput txtbDatabaseName;
        private CustomComponents.CustomTextInput txtbServerName;
        private CustomComponents.CustomButton btnSave;
        private CustomComponents.CustomTextInput txtbDatabasePassword;
        private Panel panelDatabase;
        private Panel panelSignIn;
        private Label btnBackToSignIn;
        private Label btnForgotPassword;
        private Label btnDatabaseInformation;
        private System.Windows.Forms.Timer timerLoginForm;
        private CustomComponents.CustomButton btnSendMail;
        private System.Windows.Forms.Timer timerSendMailButton;
        private CustomComponents.CustomTextInput txtbChangePasswordRecoveryCode;
    }
}