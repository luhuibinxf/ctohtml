using System;
using System.Text;

namespace DecryptConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            string encrypted = "kkzI4Gy8No7vpAZuMSLF4rfumYQ03P4PVD1O2DTNq2eqLHxmYb3uXVRWRC1d9NHnFbIUgN4RFN3Qr3FliE17T0u4oO1cwudk3U2Y0Wuxj/0=";
            string decrypted = Decrypt(encrypted);
            Console.WriteLine("Decrypted config: " + decrypted);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static string Decrypt(string encrypted)
        {
            try
            {
                byte[] data = Convert.FromBase64String(encrypted);
                return Encoding.UTF8.GetString(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Decrypt error: " + ex.Message);
                return string.Empty;
            }
        }
    }
}