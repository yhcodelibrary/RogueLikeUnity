
using System;
using System.Collections.Generic;
using System.Text;

using System.Security.Cryptography;
using System.IO;

/// <summary>
/// 暗号、復号の共通関数
/// </summary>
public class CryptInformation
{
    /// <summary>
    /// 文字列を暗号化する
    /// </summary>
    /// <param name="target">暗号化する文字列</param>
    /// <returns>暗号化された文字列</returns>
    public static string EncryptString(string target, string key)
    {
        // NULLか空文字の場合はそのまま返す
        if (string.IsNullOrEmpty(target) == true)
        {
            return target;
        }

        // 文字列をバイト型配列にする
        byte[] bytesIn = Encoding.UTF8.GetBytes(target);
        byte[] bytesOut;

        // Rijndaelアルゴリズム暗号化オブジェクトの作成
        RijndaelManaged aes = new RijndaelManaged();

        // 共有キーと初期化ベクタを決定
        // パスワードをバイト配列にする
        byte[] bytesKey = Encoding.UTF8.GetBytes(key);

        // 共有キーと初期化ベクタを設定
        aes.Key = CryptInformation.ResizeBytesArray(bytesKey, aes.Key.Length);
        aes.IV = CryptInformation.ResizeBytesArray(bytesKey, aes.IV.Length);

        // 暗号化オブジェクトの作成
        ICryptoTransform desdecrypt = aes.CreateEncryptor();

        // 暗号化されたデータを書き出すためのMemoryStream
        using (MemoryStream msOut = new MemoryStream())
        // 書き込むためのCryptoStreamの作成
        using (CryptoStream cryptStreem = new CryptoStream(msOut, desdecrypt, CryptoStreamMode.Write))
        {
            // 書き込む
            cryptStreem.Write(bytesIn, 0, bytesIn.Length);
            cryptStreem.FlushFinalBlock();

            // 暗号化されたデータを取得
            bytesOut = msOut.ToArray();

            // 閉じる
            cryptStreem.Close();
            msOut.Close();
        }

        // Base64で文字列に変更して結果を返す
        return Convert.ToBase64String(bytesOut);
    }

    /// <summary>
    /// 暗号化された文字列を復号化する
    /// </summary>
    /// <param name="target">暗号化された文字列</param>
    /// <returns>復号化された文字列</returns>
    public static string DecryptString(string target, string key)
    {
        string result = null;

        // NULLか空文字の場合はそのまま返す
        if (string.IsNullOrEmpty(target) == true)
        {
            return target;
        }

        // Rijndaelアルゴリズム暗号化オブジェクトの作成
        RijndaelManaged aes = new RijndaelManaged();

        // 共有キーと初期化ベクタを決定
        // パスワードをバイト配列にする
        byte[] bytesKey = Encoding.UTF8.GetBytes(key);

        // 共有キーと初期化ベクタを設定
        aes.Key = CryptInformation.ResizeBytesArray(bytesKey, aes.Key.Length);
        aes.IV = CryptInformation.ResizeBytesArray(bytesKey, aes.IV.Length);

        // Base64で文字列をバイト配列に戻す
        byte[] bytesIn = Convert.FromBase64String(target);

        // 復号化オブジェクトの作成
        ICryptoTransform desdecrypt = aes.CreateDecryptor();

        // 暗号化されたデータを読み込むためのMemoryStream
        using (MemoryStream msIn = new MemoryStream(bytesIn))
        // 読み込むためのCryptoStreamの作成
        using (CryptoStream cryptStreem = new CryptoStream(msIn, desdecrypt, CryptoStreamMode.Read))
        // 復号化されたデータを取得するためのStreamReader
        using (StreamReader srOut = new StreamReader(cryptStreem, Encoding.UTF8))
        {
            // 復号化されたデータを取得する
            result = srOut.ReadToEnd();
        }

        return result;
    }



    public static byte[] EncryptByte(byte[] binData,string key)
    {
        // Rijndaelアルゴリズム暗号化オブジェクトの作成
        RijndaelManaged aes = new RijndaelManaged();
        aes.Padding = PaddingMode.Zeros;
        aes.Mode = CipherMode.CBC;

        // 共有キーと初期化ベクタを決定
        // パスワードをバイト配列にする
        byte[] bytesKey = Encoding.UTF8.GetBytes(key);
        
        // 共有キーと初期化ベクタを設定
        aes.Key = CryptInformation.ResizeBytesArray(bytesKey, aes.Key.Length);
        aes.IV = CryptInformation.ResizeBytesArray(bytesKey, aes.IV.Length);

        ICryptoTransform encryptor = aes.CreateEncryptor();

        MemoryStream msEncrypt = new MemoryStream();
        CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

        byte[] src = binData;

        // 暗号化する
        csEncrypt.Write(src, 0, src.Length);
        csEncrypt.FlushFinalBlock();

        byte[] dest = msEncrypt.ToArray();

        return dest;
    }

    /// <summary>
    /// 複合化スクリプト
    /// </summary>
    /// <returns>byte[] 複合化したbyte列</returns>
    public static byte[] DecryptByte(byte[] binData,string key)
    {
        RijndaelManaged aes = new RijndaelManaged();
        aes.Padding = PaddingMode.Zeros;
        aes.Mode = CipherMode.CBC;

        // 共有キーと初期化ベクタを決定
        // パスワードをバイト配列にする
        byte[] bytesKey = Encoding.UTF8.GetBytes(key);

        // 共有キーと初期化ベクタを設定
        aes.Key = CryptInformation.ResizeBytesArray(bytesKey, aes.Key.Length);
        aes.IV = CryptInformation.ResizeBytesArray(bytesKey, aes.IV.Length);

        ICryptoTransform decryptor = aes.CreateDecryptor();
        byte[] src = binData;
        byte[] dest = new byte[src.Length];

        MemoryStream msDecrypt = new MemoryStream(src);
        CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

        // 複号化する
        csDecrypt.Read(dest, 0, dest.Length);

        return dest;
    }

    public static byte[] EncryptFile(string inputFile,string outputFile,string key)
    {
        // バイナリ・ファイルの読み込み
        byte[] src = File.ReadAllBytes(inputFile);
        
        byte[]  result = CryptInformation.EncryptByte(src, key);

        File.WriteAllBytes(outputFile, result);

        return result;
    }

    public static byte[] DecryptFile(string inputFile, string outputFile, string key)
    {
        // バイナリ・ファイルの読み込み
        byte[] src = File.ReadAllBytes(inputFile);

        byte[] result = CryptInformation.DecryptByte(src, key);

        File.WriteAllBytes(outputFile, result);

        return result;
    }

    /// <summary>
    /// 共有キー用に、バイト配列のサイズを変更する
    /// </summary>
    /// <param name="bytes">サイズを変更するバイト配列</param>
    /// <param name="newSize">バイト配列の新しい大きさ</param>
    /// <returns>サイズが変更されたバイト配列</returns>
    private static byte[] ResizeBytesArray(byte[] bytes, int newSize)
    {
        byte[] newBytes = new byte[newSize];
        if (bytes.Length <= newSize)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                newBytes[i] = bytes[i];
            }
        }
        else
        {
            int pos = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                newBytes[pos++] ^= bytes[i];
                if (pos >= newBytes.Length)
                {
                    pos = 0;
                }
            }
        }

        return newBytes;
    }
}

