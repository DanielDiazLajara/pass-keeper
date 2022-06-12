using System;
using System.Security.Cryptography;
using System.Text;
using EnzoicClient;
using System.IO;

namespace Pass
{
    public class Password
    {
        public string App { get; set; }
        public string Key { get; set; }
        public byte[] IV { get; set; }
        public Password(string app, string password, byte[] IV)
        {
            if (string.IsNullOrEmpty(app))
            {
                throw new ArgumentNullException(nameof(app));
            }
            else if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password)); 
            }
            else
            {
                App = app;
                Key = password;
                this.IV = IV;
            }
        }

        public Password(string app, string password)
        {
            if (string.IsNullOrEmpty(app))
            {
                throw new ArgumentNullException(nameof(app));
            }
            else if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
            else
            {
                App = app;
                Key = password;
                var aes = new AesCryptoServiceProvider();
                aes.GenerateIV();
                IV = aes.IV;
            }
        }

        public void EscribirFichero(string pass, string user)
        {
            byte[] bytes = IV;
            File.WriteAllText(App + "_" + user + ".txt", Encoding.Default.GetString(bytes) + Encoding.Default.GetString(AES.EncryptStringToBytes_Aes(Key, Encoding.Default.GetBytes((pass+Hash.GetNHash(pass,10)).Substring(0, 32)), bytes)));
        }
        
        public static bool CheckPawned(string pass)
        {
            Enzoic enzoic = new Enzoic("dff49cc5dc594e169ca5cf68a3296709", "sYY%r&8TxB#QDCjGJQnc5gUfx+h55MEn");

            // Check whether a password has been compromised
            if (enzoic.CheckPassword(pass))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string ToPlainText(string pass)
        {
        string passreturn = AES.DecryptStringFromBytes_Aes(@Encoding.Default.GetBytes(Key), @Encoding.Default.GetBytes((pass+Hash.GetNHash(pass, 10)).Substring(0, 32)), IV);
        return passreturn;
            
        }
    }
}
