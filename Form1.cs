using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thuat_toan_DES
{
    public partial class GUI_DES : Form
    {
        public GUI_DES()
        {
            InitializeComponent();
            // Gán sự kiện cho nút
            btn_Ecrypt.Click += Btn_Ecrypt_Click;
            btn_Decrypt.Click += Btn_Decrypt_Click;
        }
        private void Btn_Ecrypt_Click(object sender, EventArgs e)
        {
            string plainText = txt_Plain_Text.Text;
            string keyText = txt_Key.Text;

            if (string.IsNullOrEmpty(plainText))
            {
                MessageBox.Show("Vui lòng nhập bản rõ!");
                return;
            }

            if (string.IsNullOrEmpty(keyText))
            {
                MessageBox.Show("Vui lòng nhập khóa!");
                return;
            }

            // Khóa 8 byte
            if (keyText.Length > 8)
                keyText = keyText.Substring(0, 8);
            else if (keyText.Length < 8)
                keyText = keyText.PadRight(8, '0');

            byte[] keyBytes = Encoding.ASCII.GetBytes(keyText);
            Thuat_Toan_DES des = new Thuat_Toan_DES(keyBytes);
            des.LogAction = msg =>
            {
                txt_LogEncrypt.Invoke(new Action(() =>
                {
                    txt_LogEncrypt.AppendText(msg + Environment.NewLine);
                }));
            };

            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);

            // --- Thêm PKCS#7 padding ---
            int padLen = 8 - (plainBytes.Length % 8);
            if (padLen == 0) padLen = 8;
            byte[] padded = new byte[plainBytes.Length + padLen];
            Array.Copy(plainBytes, padded, plainBytes.Length);
            for (int i = plainBytes.Length; i < padded.Length; i++)
                padded[i] = (byte)padLen;
            // ----------------------------

            List<byte> encryptedAll = new List<byte>();

            for (int i = 0; i < padded.Length; i += 8)
            {
                byte[] block = new byte[8];
                Array.Copy(padded, i, block, 0, 8);
                byte[] encBlock = des.encrypt_block(block);
                encryptedAll.AddRange(encBlock);
            }

            txt_cipher.Text = BitConverter.ToString(encryptedAll.ToArray()).Replace("-", " ");

        }


        private void Btn_Decrypt_Click(object sender, EventArgs e)
        {
            string cipherText = txt_cipher.Text.Trim();
            string keyText = txt_Key.Text;

            if (string.IsNullOrEmpty(cipherText))
            {
                MessageBox.Show("Không có dữ liệu mã hóa để giải mã!");
                return;
            }

            if (string.IsNullOrEmpty(keyText))
            {
                MessageBox.Show("Vui lòng nhập khóa!");
                return;
            }

            if (keyText.Length > 8)
                keyText = keyText.Substring(0, 8);
            else if (keyText.Length < 8)
                keyText = keyText.PadRight(8, '0');

            byte[] keyBytes = Encoding.ASCII.GetBytes(keyText);
            Thuat_Toan_DES des = new Thuat_Toan_DES(keyBytes);
            des.LogAction = msg =>
            {
                txt_LogDecrypt.Invoke(new Action(() =>
                {
                    txt_LogDecrypt.AppendText(msg + Environment.NewLine);
                }));
            };

            byte[] cipherBytes = cipherText.Split(' ')
                                           .Where(x => !string.IsNullOrWhiteSpace(x))
                                           .Select(x => Convert.ToByte(x, 16))
                                           .ToArray();

            List<byte> decryptedAll = new List<byte>();

            for (int i = 0; i < cipherBytes.Length; i += 8)
            {
                byte[] block = new byte[8];
                Array.Copy(cipherBytes, i, block, 0, 8);
                byte[] decBlock = des.decrypted_block(block);
                decryptedAll.AddRange(decBlock);
            }

            // --- Bỏ padding ---
            byte padVal = decryptedAll[decryptedAll.Count - 1];
            if (padVal > 0 && padVal <= 8)
                decryptedAll.RemoveRange(decryptedAll.Count - padVal, padVal);

            string plainOut = Encoding.ASCII.GetString(decryptedAll.ToArray());
            txt_plaint.Text = plainOut;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
