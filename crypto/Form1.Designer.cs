namespace crypto
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnchoose = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btndecrypt = new System.Windows.Forms.Button();
            this.btnencrypt = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cbcmode = new System.Windows.Forms.ComboBox();
            this.cbpmode = new System.Windows.Forms.ComboBox();
            this.cbkz = new System.Windows.Forms.ComboBox();
            this.cbciphermode = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnchoose
            // 
            this.btnchoose.Location = new System.Drawing.Point(21, 28);
            this.btnchoose.Name = "btnchoose";
            this.btnchoose.Size = new System.Drawing.Size(75, 23);
            this.btnchoose.TabIndex = 0;
            this.btnchoose.Text = "ChooseFile";
            this.btnchoose.UseVisualStyleBackColor = true;
            this.btnchoose.Click += new System.EventHandler(this.btnchoose_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(127, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(397, 88);
            this.textBox1.TabIndex = 1;
            // 
            // btndecrypt
            // 
            this.btndecrypt.Location = new System.Drawing.Point(21, 163);
            this.btndecrypt.Name = "btndecrypt";
            this.btndecrypt.Size = new System.Drawing.Size(75, 23);
            this.btndecrypt.TabIndex = 2;
            this.btndecrypt.Text = "Decrypt";
            this.btndecrypt.UseVisualStyleBackColor = true;
            this.btndecrypt.Click += new System.EventHandler(this.btndecrypt_Click);
            // 
            // btnencrypt
            // 
            this.btnencrypt.Location = new System.Drawing.Point(21, 123);
            this.btnencrypt.Name = "btnencrypt";
            this.btnencrypt.Size = new System.Drawing.Size(75, 23);
            this.btnencrypt.TabIndex = 3;
            this.btnencrypt.Text = "Encrypt";
            this.btnencrypt.UseVisualStyleBackColor = true;
            this.btnencrypt.Click += new System.EventHandler(this.btnencrypt_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(127, 165);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(397, 88);
            this.textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(127, 123);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(397, 20);
            this.textBox3.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(727, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cbcmode
            // 
            this.cbcmode.FormattingEnabled = true;
            this.cbcmode.Items.AddRange(new object[] {
            "ECB",
            "CBC",
            "CFB",
            "OFB"});
            this.cbcmode.Location = new System.Drawing.Point(579, 28);
            this.cbcmode.Name = "cbcmode";
            this.cbcmode.Size = new System.Drawing.Size(121, 21);
            this.cbcmode.TabIndex = 7;
            // 
            // cbpmode
            // 
            this.cbpmode.FormattingEnabled = true;
            this.cbpmode.Items.AddRange(new object[] {
            "PKCS7",
            "Zeros",
            "ANSIX923",
            "ISO10126"});
            this.cbpmode.Location = new System.Drawing.Point(579, 69);
            this.cbpmode.Name = "cbpmode";
            this.cbpmode.Size = new System.Drawing.Size(121, 21);
            this.cbpmode.TabIndex = 8;
            // 
            // cbkz
            // 
            this.cbkz.FormattingEnabled = true;
            this.cbkz.Items.AddRange(new object[] {
            "128",
            "192",
            "256"});
            this.cbkz.Location = new System.Drawing.Point(579, 175);
            this.cbkz.Name = "cbkz";
            this.cbkz.Size = new System.Drawing.Size(121, 21);
            this.cbkz.TabIndex = 9;
            // 
            // cbciphermode
            // 
            this.cbciphermode.FormattingEnabled = true;
            this.cbciphermode.Items.AddRange(new object[] {
            "AES",
            "DES",
            "RC2"});
            this.cbciphermode.Location = new System.Drawing.Point(579, 148);
            this.cbciphermode.Name = "cbciphermode";
            this.cbciphermode.Size = new System.Drawing.Size(121, 21);
            this.cbciphermode.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 325);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "cert";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 346);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbciphermode);
            this.Controls.Add(this.cbkz);
            this.Controls.Add(this.cbpmode);
            this.Controls.Add(this.cbcmode);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btnencrypt);
            this.Controls.Add(this.btndecrypt);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnchoose);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnchoose;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btndecrypt;
        private System.Windows.Forms.Button btnencrypt;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ComboBox cbcmode;
        private System.Windows.Forms.ComboBox cbpmode;
        private System.Windows.Forms.ComboBox cbkz;
        private System.Windows.Forms.ComboBox cbciphermode;
        private System.Windows.Forms.Button button1;
    }
}

