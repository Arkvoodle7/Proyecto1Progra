using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Negocio
{
    public class Encriptacion_Usuario
    {
        private static readonly int IvSize = 12; // 96 bits
        private static readonly int TagSize = 16; // 128 bits

        public static (string encryptedDataBase64, string ivBase64, string tagBase64) Encrypt(string plaintext, byte[] key)
        {
            // genera un IV aleatorio
            byte[] iv = new byte[IvSize];
            new SecureRandom().NextBytes(iv);

            // convierte el texto plano a bytes
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] encryptedData = new byte[plaintextBytes.Length + TagSize];
            byte[] tag = new byte[TagSize];

            // configura el cifrado AES-GCM con bouncycastle
            var cipher = CipherUtilities.GetCipher("AES/GCM/NoPadding");
            var parameters = new AeadParameters(new KeyParameter(key), TagSize * 8, iv);

            // inicia el cifrado
            cipher.Init(true, parameters);
            int len = cipher.ProcessBytes(plaintextBytes, 0, plaintextBytes.Length, encryptedData, 0);
            cipher.DoFinal(encryptedData, len);

            // extrae el tag del final de los datos encriptados
            Array.Copy(encryptedData, encryptedData.Length - TagSize, tag, 0, TagSize);

            // convierte los resultados a Base64
            string encryptedDataBase64 = Convert.ToBase64String(encryptedData);
            string ivBase64 = Convert.ToBase64String(iv);
            string tagBase64 = Convert.ToBase64String(tag);

            return (encryptedDataBase64, ivBase64, tagBase64);
        }

    }
}