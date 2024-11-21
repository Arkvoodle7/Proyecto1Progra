using System;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace WSAdministracion
{
    public class Encriptar
    {
        private readonly byte[] key; // Clave de 256 bits

        public Encriptar(string base64Key)
        {
            key = Convert.FromBase64String(base64Key); // Convierte la clave de Base64 a bytes
        }

        public string Encrypt(string plainText)
        {
            var iv = new byte[12]; // IV de 96 bits para AES-GCM
            new SecureRandom().NextBytes(iv); // Genera IV aleatorio

            var plaintextBytes = Encoding.UTF8.GetBytes(plainText);
            var cipherText = new byte[plaintextBytes.Length + 16]; // Agregar espacio para el tag

            var parameters = new AeadParameters(new KeyParameter(key), 128, iv, null);
            var cipher = new GcmBlockCipher(new AesEngine());
            cipher.Init(true, parameters);

            int len = cipher.ProcessBytes(plaintextBytes, 0, plaintextBytes.Length, cipherText, 0);
            cipher.DoFinal(cipherText, len);

            // Combina IV, tag y texto cifrado para almacenarlo o transmitirlo juntos
            var result = new byte[iv.Length + cipherText.Length];
            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(cipherText, 0, result, iv.Length, cipherText.Length);

            return Convert.ToBase64String(result); // Devuelve en Base64
        }

        public string Decrypt(string encryptedText)
        {
            var fullCipher = Convert.FromBase64String(encryptedText);

            var iv = new byte[12];
            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);

            var cipherTextWithTag = new byte[fullCipher.Length - iv.Length];
            Buffer.BlockCopy(fullCipher, iv.Length, cipherTextWithTag, 0, cipherTextWithTag.Length);

            var parameters = new AeadParameters(new KeyParameter(key), 128, iv, null);
            var cipher = new GcmBlockCipher(new AesEngine());
            cipher.Init(false, parameters);

            var decryptedBytes = new byte[cipher.GetOutputSize(cipherTextWithTag.Length)];
            int len = cipher.ProcessBytes(cipherTextWithTag, 0, cipherTextWithTag.Length, decryptedBytes, 0);
            cipher.DoFinal(decryptedBytes, len);

            return Encoding.UTF8.GetString(decryptedBytes).TrimEnd('\0'); // Devuelve el texto desencriptado
        }
    }
}
