using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Note: Assuming Thuat_Toan_TripleDES is in the same namespace or accessible.
// Since the original file is named Thuat_Toan_TripleDES.cs, the namespace is Thuat_Toan_TripleDES.

namespace Thuat_Toan_TripleDES
{
    public partial class GUI_TripleDES : Form
    {
        public GUI_TripleDES()
        {
            InitializeComponent();
            // Gán sự kiện cho nút
            btn_Ecrypt_TripleDES.Click += Btn_Encrypt_TripleDES_Click;
            btn_Decrypt_TripleDES.Click += Btn_Decrypt_TripleDES_Click;
        }

        // --- Đảm bảo chuỗi khóa đủ 8 kí tự thiếu thì thêm ô trống vào cuối  ---
        private byte[] PrepareKey(string keyText, string keyName)
        {
            if (string.IsNullOrEmpty(keyText))
                throw new ArgumentException($"Vui lòng nhập {keyName}!");
            if (keyText.Any(ch => ch > 127))
            
                throw new ArgumentException($"{keyName} chỉ được phép chứa ký tự không dấu (ASCII)!");


            

            // Khóa DES phải là 8 byte. Cắt hoặc thêm padding '0'
            if (keyText.Length > 8)
                keyText = keyText.Substring(0, 8);
            else if (keyText.Length < 8)
                keyText = keyText.PadRight(8, '0');

            return Encoding.ASCII.GetBytes(keyText);
        }

        // --- Đảm bảo độ dài của dữ liệu bạn nhập là bội của 8 tức tròn 8byte và trả về một list gồm mỗi khối 8 byte ---
        private byte[] PadData(byte[] data)
        {
            int block_size = 8;
            int padLen = block_size - (data.Length % block_size);
            if (padLen == 0) padLen = block_size;

            byte[] padded = new byte[data.Length + padLen];
            Array.Copy(data, padded, data.Length);

            for (int i = data.Length; i < padded.Length; i++)
                padded[i] = (byte)padLen;

            return padded;
        }

        // --- Loại bỏ phần dư ---
        private byte[] UnpadData(List<byte> paddedData)
        {
            if (paddedData.Count == 0) return new byte[0];

            byte padVal = paddedData[paddedData.Count - 1];
            // Kiểm tra giá trị dư có hợp lễ không (1 -> 8 tức 1 khối có 8 byte)
            if (padVal > 0 && padVal <= 8 && paddedData.Count >= padVal)
            {
                // đảm bảo tất cả các byte dư đều có giá trị bằng padVal
                bool validPadding = true;
                for (int i = 0; i < padVal; i++)
                {
                    if (paddedData[paddedData.Count - 1 - i] != padVal)
                    {
                        validPadding = false;
                        break;
                    }
                }

                if (validPadding)
                {
                    paddedData.RemoveRange(paddedData.Count - padVal, padVal);
                }
            }

            return paddedData.ToArray();
        }

        // Thực hiện mã hóa Triple DES
        private void Btn_Encrypt_TripleDES_Click(object sender, EventArgs e)
        {
            try
            {
                string plainText = txt_Plain_Text_TripleDES.Text;

                if (string.IsNullOrEmpty(plainText))
                {
                    MessageBox.Show("Vui lòng nhập bản rõ!");
                    return;
                }

                // 1. Khởi tạo kháo từ các text box
                byte[] key1 = PrepareKey(txt_Key_1.Text, "Khóa 1");
                byte[] key2 = PrepareKey(txt_Key_2.Text, "Khóa 2");
                byte[] key3 = PrepareKey(txt_Key_3.Text, "Khóa 3");
               
                // 2. Khởi tọa thuật toán Triple DES
                Thuat_Toan_TripleDES tripleDES = new Thuat_Toan_TripleDES(
    key1, key2, key3,
    (msg) => txt_LogEncrypt_TripleDES.AppendText(msg + Environment.NewLine)
);

                // 3. Chuyển dữ liệu bản rõ thành mảng byte và thêm padding nếu cần
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] paddedData = PadData(plainBytes);

                // 4. Mã hóa từng khối 8 byte và lưu trữ kết quả
                List<byte> encryptedAll = new List<byte>();

                for (int i = 0; i < paddedData.Length; i += 8)
                {
                    byte[] block = new byte[8];
                    Array.Copy(paddedData, i, block, 0, 8);
                    byte[] encBlock = tripleDES.EncryptBlock(block);
                    encryptedAll.AddRange(encBlock);
                }

                // 5. Trả dữ liệu và hiển thị dưới dạng chuỗi hex
                txt_cipher.Text = BitConverter.ToString(encryptedAll.ToArray()).Replace("-", " ");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Khóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mã hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thực hiện giải mã Triple DES
        private void Btn_Decrypt_TripleDES_Click(object sender, EventArgs e)
        {
            try
            {
                string cipherText = txt_cipher.Text.Trim();

                if (string.IsNullOrEmpty(cipherText))
                {
                    MessageBox.Show("Không có dữ liệu mã hóa để giải mã!");
                    return;
                }

                // 1. Lấy dữ liệu khóa từ các bản text box
                byte[] key1 = PrepareKey(txt_Key_1.Text, "Khóa 1");
                byte[] key2 = PrepareKey(txt_Key_2.Text, "Khóa 2");
                byte[] key3 = PrepareKey(txt_Key_3.Text, "Khóa 3");

                // 2. KHởi tạo thuật toán Triple
                Thuat_Toan_TripleDES tripleDES = new Thuat_Toan_TripleDES(key1, key2, key3,
                    (msg) => txt_LogDecrypt_TripleDES.AppendText(msg + Environment.NewLine));


                // 3. chuyển chuỗi hex sang mảng byte 
                byte[] cipherBytes = cipherText.Split(new char[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(x => Convert.ToByte(x, 16))
                      .ToArray();

                if (cipherBytes.Length % 8 != 0)
                {
                    MessageBox.Show("Dữ liệu mã hóa không hợp lệ (Không phải bội số của 8 byte).", "Lỗi Giải Mã", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 4. Tọa một danh sách để lưu trữ tất cả các byte đã giải mã
                List<byte> decryptedAll = new List<byte>();

                for (int i = 0; i < cipherBytes.Length; i += 8)
                {
                    byte[] block = new byte[8];
                    Array.Copy(cipherBytes, i, block, 0, 8);
                    byte[] decBlock = tripleDES.DecryptBlock(block);
                    decryptedAll.AddRange(decBlock);
                }

                // 5. Xóa dấu cách trong dữ liệu đã giải mã
                byte[] unpaddedBytes = UnpadData(decryptedAll);

                // 6. Chuyển đổi mảng byte thành chuỗi và hiển thị theo mã UTF8
                string plainOut = Encoding.UTF8.GetString(unpaddedBytes);
                txt_plaint.Text = plainOut;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Khóa/Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Dữ liệu mã hóa không đúng định dạng Hex.", "Lỗi Giải Mã", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi giải mã: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lab_TT_TripleDES_Click(object sender, EventArgs e)
        {

        }
    }
}
