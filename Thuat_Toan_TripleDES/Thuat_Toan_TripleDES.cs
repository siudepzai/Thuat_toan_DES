﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thuat_Toan_TripleDES
{
    internal class Thuat_Toan_DES
    {
        public Action<string> LogAction; // Dùng để ghi log ra giao diện

        // --- Các bảng hoán vị ---
        static readonly int[] IP = {
            58, 50, 42, 34, 26, 18, 10, 2,
            60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6,
            64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17, 9, 1,
            59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5,
            63, 55, 47, 39, 31, 23, 15, 7
        };

        static readonly int[] FP = {
            40, 8, 48, 16, 56, 24, 64, 32,
            39, 7, 47, 15, 55, 23, 63, 31,
            38, 6, 46, 14, 54, 22, 62, 30,
            37, 5, 45, 13, 53, 21, 61, 29,
            36, 4, 44, 12, 52, 20, 60, 28,
            35, 3, 43, 11, 51, 19, 59, 27,
            34, 2, 42, 10, 50, 18, 58, 26,
            33, 1, 41, 9, 49, 17, 57, 25
        };

        static readonly int[] E = {
            32, 1, 2, 3, 4, 5,
            4, 5, 6, 7, 8, 9,
            8, 9, 10, 11, 12, 13,
            12, 13, 14, 15, 16, 17,
            16, 17, 18, 19, 20, 21,
            20, 21, 22, 23, 24, 25,
            24, 25, 26, 27, 28, 29,
            28, 29, 30, 31, 32, 1
        };

        static readonly int[] P = {
            16, 7, 20, 21,
            29, 12, 28, 17,
            1, 15, 23, 26,
            5, 18, 31, 10,
            2, 8, 24, 14,
            32, 27, 3, 9,
            19, 13, 30, 6,
            22, 11, 4, 25
        };

        static readonly int[] PC1 = {
            57,49,41,33,25,17,9,1,58,50,42,34,26,18,
            10,2,59,51,43,35,27,19,11,3,60,52,44,36,
            63,55,47,39,31,23,15,7,62,54,46,38,30,22,
            14,6,61,53,45,37,29,21,13,5,28,20,12,4
        };

        static readonly int[] PC2 = {
            14,17,11,24,1,5,3,28,15,6,21,10,23,19,
            12,4,26,8,16,7,27,20,13,2,41,52,31,37,
            47,55,30,40,51,45,33,48,44,49,39,56,34,53,
            46,42,50,36,29,32
        };

        static readonly int[] SHIFT_BITS = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
        // Các S-boxes trong DES (S1 → S8)
        int[,,] S_boxes = new int[8, 4, 16]
        {
             { // S1
        {14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7},
        {0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8},
        {4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0},
        {15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13}
    },

             { // S2
        {15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10},
        {3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5},
        {0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15},
        {13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9}
    },

             { // S3
        {10,0,9,14,6,3,15,5,1,13,12,7,11,4,2,8},
        {13,7,0,9,3,4,6,10,2,8,5,14,12,11,15,1},
        {13,6,4,9,8,15,3,0,11,1,2,12,5,10,14,7},
        {1,10,13,0,6,9,8,7,4,15,14,3,11,5,2,12}
    },

             { // S4
        {7,13,14,3,0,6,9,10,1,2,8,5,11,12,4,15},
        {13,8,11,5,6,15,0,3,4,7,2,12,1,10,14,9},
        {10,6,9,0,12,11,7,13,15,1,3,14,5,2,8,4},
        {3,15,0,6,10,1,13,8,9,4,5,11,12,7,2,14}
    },

             { // S5
        {2,12,4,1,7,10,11,6,8,5,3,15,13,0,14,9},
        {14,11,2,12,4,7,13,1,5,0,15,10,3,9,8,6},
        {4,2,1,11,10,13,7,8,15,9,12,5,6,3,0,14},
        {11,8,12,7,1,14,2,13,6,15,0,9,10,4,5,3}
    },

             { // S6
        {12,1,10,15,9,2,6,8,0,13,3,4,14,7,5,11},
        {10,15,4,2,7,12,9,5,6,1,13,14,0,11,3,8},
        {9,14,15,5,2,8,12,3,7,0,4,10,1,13,11,6},
        {4,3,2,12,9,5,15,10,11,14,1,7,6,0,8,13}
    },

             { // S7
        {4,11,2,14,15,0,8,13,3,12,9,7,5,10,6,1},
        {13,0,11,7,4,9,1,10,14,3,5,12,2,15,8,6},
        {1,4,11,13,12,3,7,14,10,15,6,8,0,5,9,2},
        {6,11,13,8,1,4,10,7,9,5,0,15,14,2,3,12}
    },

             { // S8
        {13,2,8,4,6,15,11,1,10,9,3,14,5,0,12,7},
        {1,15,13,8,10,3,7,4,12,5,6,11,0,14,9,2},
        {7,11,4,1,9,12,14,2,0,6,10,13,15,3,5,8},
        {2,1,14,7,4,10,8,13,15,12,9,0,3,5,6,11}
    }
        };

        private List<int[]> subkeys;
        private byte[] key;

        public Thuat_Toan_DES(byte[] key)
        {
            this.key = key;
            this.subkeys = _generate_subkeys();
        }

        private List<int> _to_binary_List(byte[] byte_data, int num_bits)
        {
            List<int> bit_list = new List<int>();
            foreach (byte b in byte_data)
            {
                for (int i = 7; i >= 0; i--)
                {
                    bit_list.Add((b >> i) & 1);
                }
            }
            return bit_list;
        }

        private byte[] _from_binary_list(List<int> bit_list)
        {
            List<byte> result = new List<byte>();
            for (int i = 0; i < bit_list.Count; i += 8)
            {
                byte b = 0;
                for (int j = 0; j < 8; j++)
                {
                    b = (byte)((b << 1) | bit_list[i + j]);
                }
                result.Add(b);
            }
            return result.ToArray();
        }

        private List<int> _permute(List<int> data, int[] p_table)
        {
            return p_table.Select(pos => data[pos - 1]).ToList();
        }

        private List<int> _feistel_function(List<int> right, List<int> subkey)
        {
            var expanded_right = _permute(right, E);
            var xored = expanded_right.Zip(subkey, (a, b) => a ^ b).ToList();

            List<int> sbox_result = new List<int>();
            for (int i = 0; i < 8; i++)
            {
                var chunk = xored.Skip(i * 6).Take(6).ToList();
                int row = (chunk[0] << 1) | chunk[5];
                int col = (chunk[1] << 3) | (chunk[2] << 2) | (chunk[3] << 1) | chunk[4];
                int val = S_boxes[i, row, col];
                for (int k = 3; k >= 0; k--)
                    sbox_result.Add((val >> k) & 1);
            }
            return _permute(sbox_result, P);
        }

        private List<int[]> _generate_subkeys()
        {
            var key_bits = _to_binary_List(key, 64);
            var key_permuted = _permute(key_bits, PC1);

            List<int> C = key_permuted.Take(28).ToList();
            List<int> D = key_permuted.Skip(28).ToList();

            List<int[]> subkeys = new List<int[]>();

            for (int i = 0; i < 16; i++)
            {
                int shift = SHIFT_BITS[i];
                C = C.Skip(shift).Concat(C.Take(shift)).ToList();
                D = D.Skip(shift).Concat(D.Take(shift)).ToList();
                var combined = C.Concat(D).ToList();
                subkeys.Add(_permute(combined, PC2).ToArray());
            }
            return subkeys;
        }

        public byte[] encrypt_block(byte[] plaintext_block)
        {
            var data = _to_binary_List(plaintext_block, 64);
            var permuted_data = _permute(data, IP);
            var left = permuted_data.Take(32).ToList();
            var right = permuted_data.Skip(32).ToList();

            for (int i = 0; i < 16; i++)
            {
                var temp_left = new List<int>(left);
                left = new List<int>(right);
                var f_result = _feistel_function(right, subkeys[i].ToList());
                right = temp_left.Zip(f_result, (a, b) => a ^ b).ToList();

                var combined = right.Concat(left).ToList();
                var ciphertext_F = _permute(combined, FP);
                var ciphertext_TV = _from_binary_list(ciphertext_F);
                LogAction?.Invoke($"Dữ liệu mã hóa tại vòng {i + 1}: {BitConverter.ToString(ciphertext_TV).Replace("-", "")}");
            }

            var final_combined = right.Concat(left).ToList();
            var ciphertext = _permute(final_combined, FP);
            return _from_binary_list(ciphertext);
        }

        public byte[] decrypted_block(byte[] ciphertext_block)
        {
            var data = _to_binary_List(ciphertext_block, 64);
            var permuted_data = _permute(data, IP);
            var left = permuted_data.Take(32).ToList();
            var right = permuted_data.Skip(32).ToList();

            for (int i = 15; i >= 0; i--)
            {
                var temp_left = new List<int>(left);
                left = new List<int>(right);
                var f_result = _feistel_function(right, subkeys[i].ToList());
                right = temp_left.Zip(f_result, (a, b) => a ^ b).ToList();

                var combined = right.Concat(left).ToList();
                var decrypted_F = _permute(combined, FP);
                var decrypted_TV = _from_binary_list(decrypted_F);
                LogAction?.Invoke($"Dữ liệu giải mã tại vòng {16 - i}: {BitConverter.ToString(decrypted_TV).Replace("-", "")}");
            }

            var final_combined = right.Concat(left).ToList();
            var decrypted_data = _permute(final_combined, FP);
            return _from_binary_list(decrypted_data);
        }
    }
    internal class Thuat_Toan_TripleDES
    {
        private readonly Thuat_Toan_DES des1;
        private readonly Thuat_Toan_DES des2;
        private readonly Thuat_Toan_DES des3;

        public Action<string> LogAction; // ghi log ra GUI

        public Thuat_Toan_TripleDES(byte[] key1, byte[] key2, byte[] key3, Action<string> logger = null)
        {
            if (key1.Length != 8 || key2.Length != 8 || key3.Length != 8)
                throw new ArgumentException("Mỗi khóa DES phải có độ dài 8 byte.");

            des1 = new Thuat_Toan_DES(key1);
            des2 = new Thuat_Toan_DES(key2);
            des3 = new Thuat_Toan_DES(key3);

            // Nếu có truyền logger, gán cho từng DES
            if (logger != null)
            {
                LogAction = logger;
                des1.LogAction = (msg) => logger($"[DES1] {msg}");
                des2.LogAction = (msg) => logger($"[DES2] {msg}");
                des3.LogAction = (msg) => logger($"[DES3] {msg}");
            }
        }
        public byte[] EncryptBlock(byte[] plainBlock)
        {
            if (plainBlock.Length != 8)
                throw new ArgumentException("Khối đầu vào phải dài 8 byte.");

            LogAction?.Invoke("\n--- Bắt đầu Triple DES MÃ HÓA ---");
            LogAction?.Invoke("[B1] DES1: Mã hóa");
            byte[] step1 = des1.encrypt_block(plainBlock);

            LogAction?.Invoke("[B2] DES2: Giải mã");
            byte[] step2 = des2.decrypted_block(step1);

            LogAction?.Invoke("[B3] DES3: Mã hóa");
            byte[] ciphertext = des3.encrypt_block(step2);

            LogAction?.Invoke("--- Kết thúc Triple DES MÃ HÓA ---\n");
            return ciphertext;
        }

        public byte[] DecryptBlock(byte[] cipherBlock)
        {
            if (cipherBlock.Length != 8)
                throw new ArgumentException("Khối đầu vào phải dài 8 byte.");

            LogAction?.Invoke("\n--- Bắt đầu Triple DES GIẢI MÃ ---");
            LogAction?.Invoke("[B1] DES3: Giải mã");
            byte[] step1 = des3.decrypted_block(cipherBlock);

            LogAction?.Invoke("[B2] DES2: Mã hóa");
            byte[] step2 = des2.encrypt_block(step1);

            LogAction?.Invoke("[B3] DES1: Giải mã");
            byte[] plainBlock = des1.decrypted_block(step2);

            LogAction?.Invoke("--- Kết thúc Triple DES GIẢI MÃ ---\n");
            return plainBlock;
        }

    }
}


