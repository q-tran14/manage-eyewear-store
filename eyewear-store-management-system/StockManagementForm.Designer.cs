namespace eyewear_store_management_system
{
    partial class StockManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gBProductInformation = new System.Windows.Forms.GroupBox();
            this.dPEntryDate = new eyewear_store_management_system.CustomComponents.CustomDatePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbProductName = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtbProductID = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.txtbPrice = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbEntryQuantity = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.txtbQuantity = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new eyewear_store_management_system.CustomComponents.CustomButton();
            this.btnNew = new eyewear_store_management_system.CustomComponents.CustomButton();
            this.btnSave = new eyewear_store_management_system.CustomComponents.CustomButton();
            this.btnRemove = new eyewear_store_management_system.CustomComponents.CustomButton();
            this.btnPrint = new eyewear_store_management_system.CustomComponents.CustomButton();
            this.btnModify = new eyewear_store_management_system.CustomComponents.CustomButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvProductList = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnToggleImportQuantity = new eyewear_store_management_system.CustomComponents.CustomToggleButton();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dPMax = new eyewear_store_management_system.CustomComponents.CustomDatePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dPMin = new eyewear_store_management_system.CustomComponents.CustomDatePicker();
            this.btnToggleSoldOut = new eyewear_store_management_system.CustomComponents.CustomToggleButton();
            this.txtbQuantityMax = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtbQuantityMin = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.btnClear = new eyewear_store_management_system.CustomComponents.CustomButton();
            this.label9 = new System.Windows.Forms.Label();
            this.txtbPriceMax = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.txtbPriceMin = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.label10 = new System.Windows.Forms.Label();
            this.btnFind = new eyewear_store_management_system.CustomComponents.CustomButton();
            this.txtbSearchBox = new eyewear_store_management_system.CustomComponents.CustomTextInput();
            this.gBProductInformation.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductList)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnToggleImportQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnToggleSoldOut)).BeginInit();
            this.SuspendLayout();
            // 
            // gBProductInformation
            // 
            this.gBProductInformation.Controls.Add(this.dPEntryDate);
            this.gBProductInformation.Controls.Add(this.label1);
            this.gBProductInformation.Controls.Add(this.txtbProductName);
            this.gBProductInformation.Controls.Add(this.label5);
            this.gBProductInformation.Controls.Add(this.label8);
            this.gBProductInformation.Controls.Add(this.txtbProductID);
            this.gBProductInformation.Controls.Add(this.txtbPrice);
            this.gBProductInformation.Controls.Add(this.label2);
            this.gBProductInformation.Controls.Add(this.label4);
            this.gBProductInformation.Controls.Add(this.label3);
            this.gBProductInformation.Controls.Add(this.txtbEntryQuantity);
            this.gBProductInformation.Controls.Add(this.txtbQuantity);
            this.gBProductInformation.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.gBProductInformation.Location = new System.Drawing.Point(30, 25);
            this.gBProductInformation.Name = "gBProductInformation";
            this.gBProductInformation.Size = new System.Drawing.Size(770, 200);
            this.gBProductInformation.TabIndex = 1;
            this.gBProductInformation.TabStop = false;
            this.gBProductInformation.Text = "Product Information";
            // 
            // dPEntryDate
            // 
            this.dPEntryDate.BorderColor = System.Drawing.Color.LightGray;
            this.dPEntryDate.BorderSize = 1;
            this.dPEntryDate.CustomFormat = "dd/MM/yyyy";
            this.dPEntryDate.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dPEntryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dPEntryDate.Location = new System.Drawing.Point(502, 147);
            this.dPEntryDate.MinimumSize = new System.Drawing.Size(0, 32);
            this.dPEntryDate.Name = "dPEntryDate";
            this.dPEntryDate.Size = new System.Drawing.Size(250, 32);
            this.dPEntryDate.SkinColor = System.Drawing.Color.White;
            this.dPEntryDate.TabIndex = 6;
            this.dPEntryDate.TextColor = System.Drawing.Color.Black;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(400, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entry Date";
            // 
            // txtbProductName
            // 
            this.txtbProductName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbProductName.BackColor = System.Drawing.SystemColors.Window;
            this.txtbProductName.BorderColor = System.Drawing.Color.LightGray;
            this.txtbProductName.BorderFocusColor = System.Drawing.Color.DarkGray;
            this.txtbProductName.BorderRadius = 5;
            this.txtbProductName.BorderSize = 1;
            this.txtbProductName.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbProductName.ForeColor = System.Drawing.Color.Black;
            this.txtbProductName.Location = new System.Drawing.Point(502, 40);
            this.txtbProductName.Margin = new System.Windows.Forms.Padding(4);
            this.txtbProductName.Multiline = false;
            this.txtbProductName.Name = "txtbProductName";
            this.txtbProductName.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbProductName.PasswordChar = false;
            this.txtbProductName.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbProductName.PlaceholderText = "Product Name";
            this.txtbProductName.Size = new System.Drawing.Size(250, 32);
            this.txtbProductName.TabIndex = 2;
            this.txtbProductName.Texts = "";
            this.txtbProductName.UnderlinedStyle = false;
            this.txtbProductName.Leave += new System.EventHandler(this.txtbProductName_Leave);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(389, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "Product Name";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(403, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 17);
            this.label8.TabIndex = 24;
            this.label8.Text = "Unit Price";
            // 
            // txtbProductID
            // 
            this.txtbProductID.BackColor = System.Drawing.SystemColors.Window;
            this.txtbProductID.BorderColor = System.Drawing.Color.LightGray;
            this.txtbProductID.BorderFocusColor = System.Drawing.Color.DarkGray;
            this.txtbProductID.BorderRadius = 5;
            this.txtbProductID.BorderSize = 1;
            this.txtbProductID.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbProductID.ForeColor = System.Drawing.Color.Black;
            this.txtbProductID.Location = new System.Drawing.Point(109, 40);
            this.txtbProductID.Margin = new System.Windows.Forms.Padding(4);
            this.txtbProductID.Multiline = false;
            this.txtbProductID.Name = "txtbProductID";
            this.txtbProductID.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbProductID.PasswordChar = false;
            this.txtbProductID.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbProductID.PlaceholderText = "Product ID";
            this.txtbProductID.Size = new System.Drawing.Size(250, 32);
            this.txtbProductID.TabIndex = 1;
            this.txtbProductID.Texts = "";
            this.txtbProductID.UnderlinedStyle = false;
            // 
            // txtbPrice
            // 
            this.txtbPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbPrice.BackColor = System.Drawing.SystemColors.Window;
            this.txtbPrice.BorderColor = System.Drawing.Color.LightGray;
            this.txtbPrice.BorderFocusColor = System.Drawing.Color.DarkGray;
            this.txtbPrice.BorderRadius = 5;
            this.txtbPrice.BorderSize = 1;
            this.txtbPrice.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbPrice.ForeColor = System.Drawing.Color.Black;
            this.txtbPrice.Location = new System.Drawing.Point(502, 93);
            this.txtbPrice.Margin = new System.Windows.Forms.Padding(4);
            this.txtbPrice.Multiline = false;
            this.txtbPrice.Name = "txtbPrice";
            this.txtbPrice.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbPrice.PasswordChar = false;
            this.txtbPrice.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbPrice.PlaceholderText = "Price";
            this.txtbPrice.Size = new System.Drawing.Size(250, 32);
            this.txtbPrice.TabIndex = 4;
            this.txtbPrice.Texts = "";
            this.txtbPrice.UnderlinedStyle = false;
            this.txtbPrice.Leave += new System.EventHandler(this.txtbPrice_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(24, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "Product ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(31, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 34);
            this.label4.TabIndex = 20;
            this.label4.Text = "Current\r\nQuantity";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(29, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 34);
            this.label3.TabIndex = 19;
            this.label3.Text = "Incoming\r\nQuantity";
            // 
            // txtbEntryQuantity
            // 
            this.txtbEntryQuantity.BackColor = System.Drawing.SystemColors.Window;
            this.txtbEntryQuantity.BorderColor = System.Drawing.Color.LightGray;
            this.txtbEntryQuantity.BorderFocusColor = System.Drawing.Color.DarkGray;
            this.txtbEntryQuantity.BorderRadius = 5;
            this.txtbEntryQuantity.BorderSize = 1;
            this.txtbEntryQuantity.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbEntryQuantity.ForeColor = System.Drawing.Color.Black;
            this.txtbEntryQuantity.Location = new System.Drawing.Point(109, 146);
            this.txtbEntryQuantity.Margin = new System.Windows.Forms.Padding(4);
            this.txtbEntryQuantity.Multiline = false;
            this.txtbEntryQuantity.Name = "txtbEntryQuantity";
            this.txtbEntryQuantity.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbEntryQuantity.PasswordChar = false;
            this.txtbEntryQuantity.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbEntryQuantity.PlaceholderText = "Stock Entry Quantity";
            this.txtbEntryQuantity.Size = new System.Drawing.Size(250, 32);
            this.txtbEntryQuantity.TabIndex = 5;
            this.txtbEntryQuantity.Texts = "";
            this.txtbEntryQuantity.UnderlinedStyle = false;
            this.txtbEntryQuantity.Leave += new System.EventHandler(this.txtbEntryQuantity_Leave);
            // 
            // txtbQuantity
            // 
            this.txtbQuantity.BackColor = System.Drawing.SystemColors.Window;
            this.txtbQuantity.BorderColor = System.Drawing.Color.LightGray;
            this.txtbQuantity.BorderFocusColor = System.Drawing.Color.DarkGray;
            this.txtbQuantity.BorderRadius = 5;
            this.txtbQuantity.BorderSize = 1;
            this.txtbQuantity.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbQuantity.ForeColor = System.Drawing.Color.Black;
            this.txtbQuantity.Location = new System.Drawing.Point(109, 93);
            this.txtbQuantity.Margin = new System.Windows.Forms.Padding(4);
            this.txtbQuantity.Multiline = false;
            this.txtbQuantity.Name = "txtbQuantity";
            this.txtbQuantity.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbQuantity.PasswordChar = false;
            this.txtbQuantity.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbQuantity.PlaceholderText = "Quantity";
            this.txtbQuantity.Size = new System.Drawing.Size(250, 32);
            this.txtbQuantity.TabIndex = 3;
            this.txtbQuantity.Texts = "";
            this.txtbQuantity.UnderlinedStyle = false;
            this.txtbQuantity.Leave += new System.EventHandler(this.txtbQuantity_Leave);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnNew);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.Controls.Add(this.btnRemove);
            this.groupBox4.Controls.Add(this.btnPrint);
            this.groupBox4.Controls.Add(this.btnModify);
            this.groupBox4.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox4.Location = new System.Drawing.Point(832, 25);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(197, 200);
            this.groupBox4.TabIndex = 33;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tools";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancel.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancel.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnCancel.BorderRadius = 6;
            this.btnCancel.BorderSize = 0;
            this.btnCancel.ClickColor = System.Drawing.Color.LightGray;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Californian FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(16, 148);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 30);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextColor = System.Drawing.Color.White;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnNew.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnNew.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnNew.BorderRadius = 6;
            this.btnNew.BorderSize = 0;
            this.btnNew.ClickColor = System.Drawing.Color.LightGray;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Californian FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(16, 40);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(70, 30);
            this.btnNew.TabIndex = 25;
            this.btnNew.Text = "New";
            this.btnNew.TextColor = System.Drawing.Color.White;
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSave.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSave.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSave.BorderRadius = 6;
            this.btnSave.BorderSize = 0;
            this.btnSave.ClickColor = System.Drawing.Color.LightGray;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Californian FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(107, 148);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 30);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "Save";
            this.btnSave.TextColor = System.Drawing.Color.White;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnRemove.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnRemove.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnRemove.BorderRadius = 6;
            this.btnRemove.BorderSize = 0;
            this.btnRemove.ClickColor = System.Drawing.Color.LightGray;
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Californian FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(107, 94);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(70, 30);
            this.btnRemove.TabIndex = 29;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextColor = System.Drawing.Color.White;
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnPrint.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnPrint.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnPrint.BorderRadius = 6;
            this.btnPrint.BorderSize = 0;
            this.btnPrint.ClickColor = System.Drawing.Color.LightGray;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Californian FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(107, 40);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(70, 30);
            this.btnPrint.TabIndex = 26;
            this.btnPrint.Text = "Export";
            this.btnPrint.TextColor = System.Drawing.Color.White;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnModify
            // 
            this.btnModify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnModify.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnModify.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnModify.BorderRadius = 6;
            this.btnModify.BorderSize = 0;
            this.btnModify.ClickColor = System.Drawing.Color.LightGray;
            this.btnModify.FlatAppearance.BorderSize = 0;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModify.Font = new System.Drawing.Font("Californian FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnModify.ForeColor = System.Drawing.Color.White;
            this.btnModify.Location = new System.Drawing.Point(16, 94);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(70, 30);
            this.btnModify.TabIndex = 27;
            this.btnModify.Text = "Modify";
            this.btnModify.TextColor = System.Drawing.Color.White;
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dgvProductList);
            this.groupBox3.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox3.Location = new System.Drawing.Point(30, 240);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1572, 740);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Product List";
            // 
            // dgvProductList
            // 
            this.dgvProductList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProductList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProductList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProductList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductList.Location = new System.Drawing.Point(3, 22);
            this.dgvProductList.Name = "dgvProductList";
            this.dgvProductList.RowHeadersVisible = false;
            this.dgvProductList.RowTemplate.Height = 25;
            this.dgvProductList.Size = new System.Drawing.Size(1566, 715);
            this.dgvProductList.TabIndex = 0;
            this.dgvProductList.Click += new System.EventHandler(this.dgvProductList_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnToggleImportQuantity);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.dPMax);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.dPMin);
            this.groupBox2.Controls.Add(this.btnToggleSoldOut);
            this.groupBox2.Controls.Add(this.txtbQuantityMax);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtbQuantityMin);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtbPriceMax);
            this.groupBox2.Controls.Add(this.txtbPriceMin);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.btnFind);
            this.groupBox2.Controls.Add(this.txtbSearchBox);
            this.groupBox2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(1063, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(539, 200);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search && Filter";
            // 
            // btnToggleImportQuantity
            // 
            this.btnToggleImportQuantity.Image = global::eyewear_store_management_system.Properties.Resources.uncheck;
            this.btnToggleImportQuantity.IsOn = false;
            this.btnToggleImportQuantity.Location = new System.Drawing.Point(503, 98);
            this.btnToggleImportQuantity.Name = "btnToggleImportQuantity";
            this.btnToggleImportQuantity.OffImage = global::eyewear_store_management_system.Properties.Resources.uncheck;
            this.btnToggleImportQuantity.OnImage = global::eyewear_store_management_system.Properties.Resources.check;
            this.btnToggleImportQuantity.Size = new System.Drawing.Size(25, 25);
            this.btnToggleImportQuantity.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnToggleImportQuantity.TabIndex = 51;
            this.btnToggleImportQuantity.TabStop = false;
            this.btnToggleImportQuantity.Click += new System.EventHandler(this.btnToggleImportQuantity_Click);
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label14.Location = new System.Drawing.Point(390, 102);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 17);
            this.label14.TabIndex = 50;
            this.label14.Text = "Import Quantity";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(220, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 27);
            this.label13.TabIndex = 49;
            this.label13.Text = "-";
            // 
            // dPMax
            // 
            this.dPMax.BorderColor = System.Drawing.Color.LightGray;
            this.dPMax.BorderSize = 1;
            this.dPMax.CustomFormat = "dd/MM/yyyy";
            this.dPMax.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dPMax.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dPMax.Location = new System.Drawing.Point(244, 94);
            this.dPMax.MinimumSize = new System.Drawing.Size(0, 32);
            this.dPMax.Name = "dPMax";
            this.dPMax.Size = new System.Drawing.Size(126, 32);
            this.dPMax.SkinColor = System.Drawing.Color.White;
            this.dPMax.TabIndex = 102;
            this.dPMax.TextColor = System.Drawing.Color.Black;
            this.dPMax.ValueChanged += new System.EventHandler(this.dPMax_ValueChanged);
            this.dPMax.Leave += new System.EventHandler(this.dPMax_Leave);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(10, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 17);
            this.label12.TabIndex = 47;
            this.label12.Text = "Entry Date";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(145, 150);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 27);
            this.label11.TabIndex = 42;
            this.label11.Text = "-";
            // 
            // dPMin
            // 
            this.dPMin.BorderColor = System.Drawing.Color.LightGray;
            this.dPMin.BorderSize = 1;
            this.dPMin.CustomFormat = "dd/MM/yyyy";
            this.dPMin.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dPMin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dPMin.Location = new System.Drawing.Point(90, 94);
            this.dPMin.MinimumSize = new System.Drawing.Size(0, 32);
            this.dPMin.Name = "dPMin";
            this.dPMin.Size = new System.Drawing.Size(126, 32);
            this.dPMin.SkinColor = System.Drawing.Color.White;
            this.dPMin.TabIndex = 101;
            this.dPMin.TextColor = System.Drawing.Color.Black;
            this.dPMin.ValueChanged += new System.EventHandler(this.dPMin_ValueChanged);
            this.dPMin.Leave += new System.EventHandler(this.dPMin_Leave);
            // 
            // btnToggleSoldOut
            // 
            this.btnToggleSoldOut.Image = global::eyewear_store_management_system.Properties.Resources.uncheck;
            this.btnToggleSoldOut.IsOn = false;
            this.btnToggleSoldOut.Location = new System.Drawing.Point(503, 45);
            this.btnToggleSoldOut.Name = "btnToggleSoldOut";
            this.btnToggleSoldOut.OffImage = global::eyewear_store_management_system.Properties.Resources.uncheck;
            this.btnToggleSoldOut.OnImage = global::eyewear_store_management_system.Properties.Resources.check;
            this.btnToggleSoldOut.Size = new System.Drawing.Size(25, 25);
            this.btnToggleSoldOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnToggleSoldOut.TabIndex = 33;
            this.btnToggleSoldOut.TabStop = false;
            this.btnToggleSoldOut.Click += new System.EventHandler(this.btnToggleSoldOut_Click);
            // 
            // txtbQuantityMax
            // 
            this.txtbQuantityMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbQuantityMax.BackColor = System.Drawing.SystemColors.Window;
            this.txtbQuantityMax.BorderColor = System.Drawing.Color.LightGray;
            this.txtbQuantityMax.BorderFocusColor = System.Drawing.Color.DarkGray;
            this.txtbQuantityMax.BorderRadius = 5;
            this.txtbQuantityMax.BorderSize = 1;
            this.txtbQuantityMax.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbQuantityMax.ForeColor = System.Drawing.Color.Black;
            this.txtbQuantityMax.Location = new System.Drawing.Point(448, 147);
            this.txtbQuantityMax.Margin = new System.Windows.Forms.Padding(4);
            this.txtbQuantityMax.Multiline = false;
            this.txtbQuantityMax.Name = "txtbQuantityMax";
            this.txtbQuantityMax.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbQuantityMax.PasswordChar = false;
            this.txtbQuantityMax.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbQuantityMax.PlaceholderText = "      Max";
            this.txtbQuantityMax.Size = new System.Drawing.Size(80, 32);
            this.txtbQuantityMax.TabIndex = 106;
            this.txtbQuantityMax.Texts = "";
            this.txtbQuantityMax.UnderlinedStyle = false;
            this.txtbQuantityMax._TextChanged += new System.EventHandler(this.txtbQuantityMax__TextChanged);
            this.txtbQuantityMax.Leave += new System.EventHandler(this.txtbQuantityMax_Leave);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(270, 155);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 17);
            this.label7.TabIndex = 35;
            this.label7.Text = "Quantity";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(445, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 17);
            this.label6.TabIndex = 25;
            this.label6.Text = "Sold Out";
            // 
            // txtbQuantityMin
            // 
            this.txtbQuantityMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbQuantityMin.BackColor = System.Drawing.SystemColors.Window;
            this.txtbQuantityMin.BorderColor = System.Drawing.Color.LightGray;
            this.txtbQuantityMin.BorderFocusColor = System.Drawing.Color.DarkGray;
            this.txtbQuantityMin.BorderRadius = 5;
            this.txtbQuantityMin.BorderSize = 1;
            this.txtbQuantityMin.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbQuantityMin.ForeColor = System.Drawing.Color.Black;
            this.txtbQuantityMin.Location = new System.Drawing.Point(340, 147);
            this.txtbQuantityMin.Margin = new System.Windows.Forms.Padding(4);
            this.txtbQuantityMin.Multiline = false;
            this.txtbQuantityMin.Name = "txtbQuantityMin";
            this.txtbQuantityMin.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbQuantityMin.PasswordChar = false;
            this.txtbQuantityMin.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbQuantityMin.PlaceholderText = "      Min";
            this.txtbQuantityMin.Size = new System.Drawing.Size(80, 32);
            this.txtbQuantityMin.TabIndex = 105;
            this.txtbQuantityMin.Texts = "";
            this.txtbQuantityMin.UnderlinedStyle = false;
            this.txtbQuantityMin._TextChanged += new System.EventHandler(this.txtbQuantityMin__TextChanged);
            this.txtbQuantityMin.Leave += new System.EventHandler(this.txtbQuantityMin_Leave);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnClear.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnClear.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnClear.BorderRadius = 6;
            this.btnClear.BorderSize = 0;
            this.btnClear.ClickColor = System.Drawing.Color.LightGray;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Californian FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(370, 41);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(60, 30);
            this.btnClear.TabIndex = 32;
            this.btnClear.Text = "Clear";
            this.btnClear.TextColor = System.Drawing.Color.White;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(425, 150);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 27);
            this.label9.TabIndex = 38;
            this.label9.Text = "-";
            // 
            // txtbPriceMax
            // 
            this.txtbPriceMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbPriceMax.BackColor = System.Drawing.SystemColors.Window;
            this.txtbPriceMax.BorderColor = System.Drawing.Color.LightGray;
            this.txtbPriceMax.BorderFocusColor = System.Drawing.Color.DarkGray;
            this.txtbPriceMax.BorderRadius = 5;
            this.txtbPriceMax.BorderSize = 1;
            this.txtbPriceMax.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbPriceMax.ForeColor = System.Drawing.Color.Black;
            this.txtbPriceMax.Location = new System.Drawing.Point(170, 147);
            this.txtbPriceMax.Margin = new System.Windows.Forms.Padding(4);
            this.txtbPriceMax.Multiline = false;
            this.txtbPriceMax.Name = "txtbPriceMax";
            this.txtbPriceMax.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbPriceMax.PasswordChar = false;
            this.txtbPriceMax.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbPriceMax.PlaceholderText = "      Max";
            this.txtbPriceMax.Size = new System.Drawing.Size(80, 32);
            this.txtbPriceMax.TabIndex = 104;
            this.txtbPriceMax.Texts = "";
            this.txtbPriceMax.UnderlinedStyle = false;
            this.txtbPriceMax._TextChanged += new System.EventHandler(this.txtbPriceMax__TextChanged);
            this.txtbPriceMax.Leave += new System.EventHandler(this.txtbPriceMax_Leave);
            // 
            // txtbPriceMin
            // 
            this.txtbPriceMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbPriceMin.BackColor = System.Drawing.SystemColors.Window;
            this.txtbPriceMin.BorderColor = System.Drawing.Color.LightGray;
            this.txtbPriceMin.BorderFocusColor = System.Drawing.Color.DarkGray;
            this.txtbPriceMin.BorderRadius = 5;
            this.txtbPriceMin.BorderSize = 1;
            this.txtbPriceMin.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbPriceMin.ForeColor = System.Drawing.Color.Black;
            this.txtbPriceMin.Location = new System.Drawing.Point(60, 147);
            this.txtbPriceMin.Margin = new System.Windows.Forms.Padding(4);
            this.txtbPriceMin.Multiline = false;
            this.txtbPriceMin.Name = "txtbPriceMin";
            this.txtbPriceMin.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbPriceMin.PasswordChar = false;
            this.txtbPriceMin.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbPriceMin.PlaceholderText = "      Min";
            this.txtbPriceMin.Size = new System.Drawing.Size(80, 32);
            this.txtbPriceMin.TabIndex = 103;
            this.txtbPriceMin.Texts = "";
            this.txtbPriceMin.UnderlinedStyle = false;
            this.txtbPriceMin._TextChanged += new System.EventHandler(this.txtbPriceMin__TextChanged);
            this.txtbPriceMin.Leave += new System.EventHandler(this.txtbPriceMin_Leave);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(10, 155);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 17);
            this.label10.TabIndex = 39;
            this.label10.Text = "Price";
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnFind.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnFind.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnFind.BorderRadius = 6;
            this.btnFind.BorderSize = 0;
            this.btnFind.ClickColor = System.Drawing.Color.LightGray;
            this.btnFind.FlatAppearance.BorderSize = 0;
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.Font = new System.Drawing.Font("Californian FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFind.ForeColor = System.Drawing.Color.White;
            this.btnFind.Location = new System.Drawing.Point(300, 41);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(60, 30);
            this.btnFind.TabIndex = 31;
            this.btnFind.Text = "Find";
            this.btnFind.TextColor = System.Drawing.Color.White;
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtbSearchBox
            // 
            this.txtbSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbSearchBox.BackColor = System.Drawing.SystemColors.Window;
            this.txtbSearchBox.BorderColor = System.Drawing.Color.LightGray;
            this.txtbSearchBox.BorderFocusColor = System.Drawing.Color.DarkGray;
            this.txtbSearchBox.BorderRadius = 5;
            this.txtbSearchBox.BorderSize = 1;
            this.txtbSearchBox.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbSearchBox.ForeColor = System.Drawing.Color.Black;
            this.txtbSearchBox.Location = new System.Drawing.Point(10, 40);
            this.txtbSearchBox.Margin = new System.Windows.Forms.Padding(4);
            this.txtbSearchBox.Multiline = false;
            this.txtbSearchBox.Name = "txtbSearchBox";
            this.txtbSearchBox.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtbSearchBox.PasswordChar = false;
            this.txtbSearchBox.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtbSearchBox.PlaceholderText = "Product ID, Name";
            this.txtbSearchBox.Size = new System.Drawing.Size(270, 32);
            this.txtbSearchBox.TabIndex = 100;
            this.txtbSearchBox.Texts = "";
            this.txtbSearchBox.UnderlinedStyle = false;
            this.txtbSearchBox._TextChanged += new System.EventHandler(this.txtbSearchBox__TextChanged);
            // 
            // StockManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1632, 1002);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.gBProductInformation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StockManagementForm";
            this.Text = "StockManagementForm";
            this.Load += new System.EventHandler(this.StockManagementForm_Load);
            this.gBProductInformation.ResumeLayout(false);
            this.gBProductInformation.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductList)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnToggleImportQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnToggleSoldOut)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox gBProductInformation;
        private CustomComponents.CustomTextInput txtbProductName;
        private Label label5;
        private Label label8;
        private CustomComponents.CustomTextInput txtbProductID;
        private CustomComponents.CustomTextInput txtbPrice;
        private Label label2;
        private Label label4;
        private Label label3;
        private CustomComponents.CustomTextInput txtbEntryQuantity;
        private CustomComponents.CustomTextInput txtbQuantity;
        private GroupBox groupBox4;
        private GroupBox groupBox3;
        private DataGridView dgvProductList;
        private CustomComponents.CustomButton btnNew;
        private CustomComponents.CustomButton btnPrint;
        private CustomComponents.CustomButton btnModify;
        private CustomComponents.CustomButton btnSave;
        private CustomComponents.CustomButton btnRemove;
        private CustomComponents.CustomButton btnCancel;
        private GroupBox groupBox2;
        private Label label1;
        private CustomComponents.CustomDatePicker dPEntryDate;
        private CustomComponents.CustomTextInput txtbSearchBox;
        private CustomComponents.CustomButton btnFind;
        private CustomComponents.CustomButton btnClear;
        private Label label6;
        private CustomComponents.CustomToggleButton btnToggleSoldOut;
        private Label label7;
        private CustomComponents.CustomTextInput txtbQuantityMin;
        private CustomComponents.CustomTextInput txtbQuantityMax;
        private Label label9;
        private Label label10;
        private Label label11;
        private CustomComponents.CustomTextInput txtbPriceMax;
        private CustomComponents.CustomTextInput txtbPriceMin;
        private CustomComponents.CustomDatePicker dPMin;
        private Label label12;
        private Label label13;
        private CustomComponents.CustomDatePicker dPMax;
        private Label label14;
        private CustomComponents.CustomToggleButton btnToggleImportQuantity;
    }
}