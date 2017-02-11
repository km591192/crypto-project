namespace cryptoapi
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbinput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbcipher = new System.Windows.Forms.TextBox();
            this.tboutput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnencrypt = new System.Windows.Forms.Button();
            this.btndecrypt = new System.Windows.Forms.Button();
            this.btnclear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input string:";
            // 
            // tbinput
            // 
            this.tbinput.Location = new System.Drawing.Point(96, 70);
            this.tbinput.Multiline = true;
            this.tbinput.Name = "tbinput";
            this.tbinput.Size = new System.Drawing.Size(271, 75);
            this.tbinput.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cypher:";
            // 
            // tbcipher
            // 
            this.tbcipher.Location = new System.Drawing.Point(96, 39);
            this.tbcipher.Name = "tbcipher";
            this.tbcipher.Size = new System.Drawing.Size(271, 20);
            this.tbcipher.TabIndex = 3;
            // 
            // tboutput
            // 
            this.tboutput.Location = new System.Drawing.Point(96, 151);
            this.tboutput.Multiline = true;
            this.tboutput.Name = "tboutput";
            this.tboutput.Size = new System.Drawing.Size(271, 86);
            this.tboutput.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Output string:";
            // 
            // btnencrypt
            // 
            this.btnencrypt.Location = new System.Drawing.Point(96, 266);
            this.btnencrypt.Name = "btnencrypt";
            this.btnencrypt.Size = new System.Drawing.Size(75, 23);
            this.btnencrypt.TabIndex = 6;
            this.btnencrypt.Text = "Encrypt";
            this.btnencrypt.UseVisualStyleBackColor = true;
            this.btnencrypt.Click += new System.EventHandler(this.btnencrypt_Click);
            // 
            // btndecrypt
            // 
            this.btndecrypt.Location = new System.Drawing.Point(202, 266);
            this.btndecrypt.Name = "btndecrypt";
            this.btndecrypt.Size = new System.Drawing.Size(75, 23);
            this.btndecrypt.TabIndex = 7;
            this.btndecrypt.Text = "Decrypt";
            this.btndecrypt.UseVisualStyleBackColor = true;
            this.btndecrypt.Click += new System.EventHandler(this.btndecrypt_Click);
            // 
            // btnclear
            // 
            this.btnclear.Location = new System.Drawing.Point(305, 266);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(75, 23);
            this.btnclear.TabIndex = 8;
            this.btnclear.Text = "Clear";
            this.btnclear.UseVisualStyleBackColor = true;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 324);
            this.Controls.Add(this.btnclear);
            this.Controls.Add(this.btndecrypt);
            this.Controls.Add(this.btnencrypt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tboutput);
            this.Controls.Add(this.tbcipher);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbinput);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "v";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbinput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbcipher;
        private System.Windows.Forms.TextBox tboutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnencrypt;
        private System.Windows.Forms.Button btndecrypt;
        private System.Windows.Forms.Button btnclear;
    }
}

