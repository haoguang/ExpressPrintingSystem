using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace ExpressPrintingSystem
{
    public class ClassHashing
    {



        



        public static byte[] generateSalt()
        {
            byte[] saltBytes;
            int minSaltSize = 4;
            int maxSaltSize = 8;

            Random random = new Random();
            int saltSize = random.Next(minSaltSize, maxSaltSize);

            saltBytes = new byte[saltSize];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            rng.GetNonZeroBytes(saltBytes);

            return saltBytes;
 
        }

        public static byte[] generateSaltedHash(string input, byte[] salt)
        {
            byte[] clearTextBytes = Encoding.UTF8.GetBytes(input);

            byte[] clearTextWithSaltBytes = new byte[input.Length + salt.Length];

            for (int i = 0; i < clearTextBytes.Length; i++)
                clearTextWithSaltBytes[i] = clearTextBytes[i];

            for (int i = 0; i < salt.Length; i++)
                clearTextWithSaltBytes[input.Length + i] = salt[i];

            HashAlgorithm hash = new SHA256Managed();
            byte[] hashBytes = hash.ComputeHash(clearTextWithSaltBytes);

            return hashBytes;
        }


        //    public static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        //{
        //    HashAlgorithm algorithm = new SHA256Managed();

        //    byte[] plainTextWithSaltBytes =
        //      new byte[plainText.Length + salt.Length];

        //    for (int i = 0; i < plainText.Length; i++)
        //    {
        //        plainTextWithSaltBytes[i] = plainText[i];
        //    }
        //    for (int i = 0; i < salt.Length; i++)
        //    {
        //        plainTextWithSaltBytes[plainText.Length + i] = salt[i];
        //    }

        //    return algorithm.ComputeHash(plainTextWithSaltBytes);
        //}

        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static string basicEncryption(string normalText)
        {
            return Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(normalText)));
        }

        public static string basicDecryption(string encryptedText)
        {
            return Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(encryptedText)));
        }
      
    }
    


}

  