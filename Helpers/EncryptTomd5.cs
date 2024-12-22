using System.Security.Cryptography;

namespace api_rest_netcore.Helpers
{
    public class EncryptToMd5 
    {
        public static string Encrypt(string value)
        {
            try
            {
                using (MD5 md5 = MD5.Create()) // Usar MD5.Create()
                {
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(value);
                    byte[] hash = md5.ComputeHash(data);

                    string resp = "";
                    for (int i = 0; i < hash.Length; i++)
                    {
                        resp += hash[i].ToString("x2").ToLower();
                    }
                    return resp; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ""; 
            }
        }
    }
}

