namespace Thuat_toan_DES
{
    partial class GUI_DES
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
            this.label1 = new System.Windows.Forms.Label();
            this.lab_Ban_Ro = new System.Windows.Forms.Label();
            this.lab_Key = new System.Windows.Forms.Label();
            this.lab_enrypt = new System.Windows.Forms.Label();
            this.lab_Decrypt = new System.Windows.Forms.Label();
            this.txt_cipher = new System.Windows.Forms.TextBox();
            this.txt_plaint = new System.Windows.Forms.TextBox();
            this.txt_Plain_Text = new System.Windows.Forms.TextBox();
            this.txt_Key = new System.Windows.Forms.TextBox();
            this.btn_Ecrypt = new System.Windows.Forms.Button();
            this.btn_Decrypt = new System.Windows.Forms.Button();
            this.txt_LogEncrypt = new System.Windows.Forms.TextBox();
            this.txt_LogDecrypt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1100, 55);
            this.label1.TabIndex = 7;
            this.label1.Text = "Thuật toán DES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lab_Ban_Ro
            // 
            this.lab_Ban_Ro.AutoSize = true;
            this.lab_Ban_Ro.Location = new System.Drawing.Point(130, 101);
            this.lab_Ban_Ro.Name = "lab_Ban_Ro";
            this.lab_Ban_Ro.Size = new System.Drawing.Size(243, 22);
            this.lab_Ban_Ro.TabIndex = 8;
            this.lab_Ban_Ro.Text = "Nhập vào đoạn muốn mã hóa:";
            // 
            // lab_Key
            // 
            this.lab_Key.AutoSize = true;
            this.lab_Key.Location = new System.Drawing.Point(234, 158);
            this.lab_Key.Name = "lab_Key";
            this.lab_Key.Size = new System.Drawing.Size(139, 22);
            this.lab_Key.TabIndex = 9;
            this.lab_Key.Text = "Nhập vào khóa :";
            // 
            // lab_enrypt
            // 
            this.lab_enrypt.AutoSize = true;
            this.lab_enrypt.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_enrypt.Location = new System.Drawing.Point(173, 208);
            this.lab_enrypt.Name = "lab_enrypt";
            this.lab_enrypt.Size = new System.Drawing.Size(159, 33);
            this.lab_enrypt.TabIndex = 10;
            this.lab_enrypt.Text = "Bảng mã hóa";
            // 
            // lab_Decrypt
            // 
            this.lab_Decrypt.AutoSize = true;
            this.lab_Decrypt.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_Decrypt.Location = new System.Drawing.Point(771, 208);
            this.lab_Decrypt.Name = "lab_Decrypt";
            this.lab_Decrypt.Size = new System.Drawing.Size(160, 33);
            this.lab_Decrypt.TabIndex = 0;
            this.lab_Decrypt.Text = "Bảng giải mã";
            // 
            // txt_cipher
            // 
            this.txt_cipher.Location = new System.Drawing.Point(12, 495);
            this.txt_cipher.Multiline = true;
            this.txt_cipher.Name = "txt_cipher";
            this.txt_cipher.ReadOnly = true;
            this.txt_cipher.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_cipher.Size = new System.Drawing.Size(485, 39);
            this.txt_cipher.TabIndex = 3;
            // 
            // txt_plaint
            // 
            this.txt_plaint.Location = new System.Drawing.Point(593, 495);
            this.txt_plaint.Multiline = true;
            this.txt_plaint.Name = "txt_plaint";
            this.txt_plaint.ReadOnly = true;
            this.txt_plaint.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_plaint.Size = new System.Drawing.Size(485, 39);
            this.txt_plaint.TabIndex = 4;
            // 
            // txt_Plain_Text
            // 
            this.txt_Plain_Text.Location = new System.Drawing.Point(379, 93);
            this.txt_Plain_Text.Name = "txt_Plain_Text";
            this.txt_Plain_Text.Size = new System.Drawing.Size(434, 30);
            this.txt_Plain_Text.TabIndex = 1;
            // 
            // txt_Key
            // 
            this.txt_Key.Location = new System.Drawing.Point(379, 150);
            this.txt_Key.Name = "txt_Key";
            this.txt_Key.Size = new System.Drawing.Size(434, 30);
            this.txt_Key.TabIndex = 2;
            // 
            // btn_Ecrypt
            // 
            this.btn_Ecrypt.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Ecrypt.Location = new System.Drawing.Point(179, 556);
            this.btn_Ecrypt.Name = "btn_Ecrypt";
            this.btn_Ecrypt.Size = new System.Drawing.Size(153, 50);
            this.btn_Ecrypt.TabIndex = 5;
            this.btn_Ecrypt.Text = "Encrypt";
            this.btn_Ecrypt.UseVisualStyleBackColor = true;
            // 
            // btn_Decrypt
            // 
            this.btn_Decrypt.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Decrypt.Location = new System.Drawing.Point(777, 556);
            this.btn_Decrypt.Name = "btn_Decrypt";
            this.btn_Decrypt.Size = new System.Drawing.Size(154, 50);
            this.btn_Decrypt.TabIndex = 6;
            this.btn_Decrypt.Text = "Decrypt";
            this.btn_Decrypt.UseVisualStyleBackColor = true;
            // 
            // txt_LogEncrypt
            // 
            this.txt_LogEncrypt.Location = new System.Drawing.Point(12, 257);
            this.txt_LogEncrypt.Multiline = true;
            this.txt_LogEncrypt.Name = "txt_LogEncrypt";
            this.txt_LogEncrypt.ReadOnly = true;
            this.txt_LogEncrypt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_LogEncrypt.Size = new System.Drawing.Size(485, 221);
            this.txt_LogEncrypt.TabIndex = 3;
            // 
            // txt_LogDecrypt
            // 
            this.txt_LogDecrypt.Location = new System.Drawing.Point(593, 257);
            this.txt_LogDecrypt.Multiline = true;
            this.txt_LogDecrypt.Name = "txt_LogDecrypt";
            this.txt_LogDecrypt.ReadOnly = true;
            this.txt_LogDecrypt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_LogDecrypt.Size = new System.Drawing.Size(485, 221);
            this.txt_LogDecrypt.TabIndex = 4;
            // 
            // GUI_DES
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1100, 619);
            this.Controls.Add(this.btn_Decrypt);
            this.Controls.Add(this.btn_Ecrypt);
            this.Controls.Add(this.txt_Key);
            this.Controls.Add(this.txt_Plain_Text);
            this.Controls.Add(this.txt_LogDecrypt);
            this.Controls.Add(this.txt_LogEncrypt);
            this.Controls.Add(this.txt_plaint);
            this.Controls.Add(this.txt_cipher);
            this.Controls.Add(this.lab_Decrypt);
            this.Controls.Add(this.lab_enrypt);
            this.Controls.Add(this.lab_Key);
            this.Controls.Add(this.lab_Ban_Ro);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "GUI_DES";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giao diện thuật toán DES";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab_Ban_Ro;
        private System.Windows.Forms.Label lab_Key;
        private System.Windows.Forms.Label lab_enrypt;
        private System.Windows.Forms.Label lab_Decrypt;
        private System.Windows.Forms.TextBox txt_cipher;
        private System.Windows.Forms.TextBox txt_plaint;
        private System.Windows.Forms.TextBox txt_Plain_Text;
        private System.Windows.Forms.TextBox txt_Key;
        private System.Windows.Forms.Button btn_Ecrypt;
        private System.Windows.Forms.Button btn_Decrypt;
        private System.Windows.Forms.TextBox txt_LogEncrypt;
        private System.Windows.Forms.TextBox txt_LogDecrypt;
    }
}

