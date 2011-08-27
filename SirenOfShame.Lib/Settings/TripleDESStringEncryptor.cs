using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SirenOfShame.Lib.Settings
{
    public class TripleDESStringEncryptor
    {
        private byte[] _key;
        private byte[] _iv;

        private TripleDESCryptoServiceProvider _provider;

        public TripleDESStringEncryptor()
        {
            _key = Encoding.ASCII.GetBytes("GSYAHAGCBDUUADIADKOPAAAW");
            _iv = Encoding.ASCII.GetBytes("USAZBGAW");
            _provider = new TripleDESCryptoServiceProvider();
        }

        public string EncryptString(string text)
        {
            ICryptoTransform transform = _provider.CreateEncryptor(_key, _iv);

            if (text == null)
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                {
                    byte[] input = Encoding.UTF8.GetBytes(text);
                    cryptoStream.Write(input, 0, input.Length);
                    cryptoStream.FlushFinalBlock();
                    return Convert.ToBase64String(stream.ToArray());
                }
            }
        }

        public string DecryptString(string text)
        {
            ICryptoTransform transform = _provider.CreateDecryptor(_key, _iv);

            if (text == null)
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                {
                    byte[] input = Convert.FromBase64String(text);
                    cryptoStream.Write(input, 0, input.Length);
                    cryptoStream.FlushFinalBlock();
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }
        }
    }
}