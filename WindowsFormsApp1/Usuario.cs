using System.Text;
using System.IO;
using System;
using System.Collections.Generic;

namespace Pass
{
    public class Usuario
    {
        public string nick { get; set; }
        public string password { get; set; }
        public int n { get; set; }
        public List<Password> mypasswords = new List<Password>();
        public Usuario(string nick, string password, int n)
        {
            if (n < 0) { n = 0; }
            this.n = n;
            this.nick = nick;
            this.password = Hash.GetNHash(password, 10);
        }

        public Usuario(string nick, string pass) {
            string filename = nick + ".txt";
            byte[] bytes = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string[] lineas;
            try
            {
                lineas = File.ReadAllLines(filename);
            }
            catch (Exception e) { throw e; }

            this.nick = nick;
            password = Hash.GetNHash(pass, 10);
            pass += password;
            Console.WriteLine(lineas[0]);
            Console.WriteLine(password);
            if (lineas[0] != password)
            {
                throw new Exception("Password errónea");
            }
            byte[] texto = Encoding.Default.GetBytes(lineas[1]);
            string descifrado = AES.DecryptStringFromBytes_Aes(texto, Encoding.Default.GetBytes(pass.Substring(0, 32)), bytes);
            n = Int32.Parse(descifrado);
        }
        public void CargaContraseñas()
        {
            DirectoryInfo d = new DirectoryInfo("."); 
            FileInfo[] Files = d.GetFiles("*_"+nick+".txt"); //Getting Text files
            string str = "";
            string plataforma = "";
            string password = "";
            byte[] iv;
            foreach (FileInfo file in Files)
            {
                Console.WriteLine(file.Name);
                string contenido = File.ReadAllText(file.Name);
                plataforma = file.Name.Split('_')[0];
                iv = Encoding.Default.GetBytes(contenido.Substring(0, 16));
                password = contenido.Substring(16, contenido.Length-16);
                Console.WriteLine(iv + "     "+ iv.Length);
                Console.WriteLine("_____________");
                Console.WriteLine(password+"      "+password.Length);
                Password p = new Password(plataforma, password, iv);
                mypasswords.Add(p);
            }
        }

        public void EscribirFichero(string pass)
        {
            string filename = nick + ".txt";
            if(File.Exists(filename)) {
                throw new Exception("Usuario existente");
            }
            pass += password;
            byte[] bytes = new byte[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
            string[] lineas = { @password, @Encoding.Default.GetString(AES.EncryptStringToBytes_Aes(n.ToString(), Encoding.Default.GetBytes(pass.Substring(0,32)), bytes))};
            Fichero.Escribir(nick+".txt", lineas);
        }

        public static bool ValidaContraseña(string passWord)
        {
            int validConditions = 0;
            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            if (validConditions == 2)
            {
                char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' }; // or whatever    
                if (passWord.IndexOfAny(special) == -1) return false;
            }
            return true;
        }

        public static string CrearContraseña(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890|@#~€¬=)(/&%$!";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public void AddContraseña(string plataforma, string pass, string mypass)
        {
            if (password != Hash.GetNHash(mypass, 10))
            {
                throw new Exception();
            }
            else
            {
                try
                {
                    Password newpass = new Password(plataforma, pass+Hash.GetNHash(pass, 10));
                    newpass.EscribirFichero(mypass + Hash.GetNHash(mypass, 10), nick);
                    mypasswords.Add(newpass);
                }
                catch (Exception ex) { throw ex; }
            }
        }

        public string GetPassword(string plataforma, string mypass)
        {
            string passreturn = "";
            if (password != Hash.GetNHash(mypass, 10))
            {
                throw new Exception("contraseña errónea");
            }
            else
            {
                try
                {
                    for (int i = 0; i < mypasswords.Count; i++)
                    {
                        if (plataforma == mypasswords[i].App)
                        {
                            return mypasswords[i].ToPlainText(mypass);
                        }
                    }
                }
                catch (Exception ex) { throw ex; }
            }
            return passreturn;
        }
    } 
}