using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

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
      
    }
    


}

        

//        public string EncodePassword(string pass, string salt)
//        {
//            byte[] bytes = Encoding.Unicode.GetBytes(pass);
//            byte[] src = Encoding.Unicode.GetBytes(salt);
//            byte[] dst = new byte[src.Length + bytes.Length];
//            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
//            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
//            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
//            byte[] inArray = algorithm.ComputeHash(dst);
//            return Convert.ToBase64String(inArray);
//        }

//    }

    
//}