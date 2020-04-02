using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Services
{
    #region Implementations
    public class EncryptionService : IEncryptionService
    {
        public byte[] EncryptWord(string word, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(word));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 1; // 1 Thread
            argon2.Iterations = 4;
            argon2.MemorySize = 1 * 1024; // 1 MB

            return argon2.GetBytes(16);
        }

        public byte[] GenerateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }
        public bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = EncryptWord(password, salt);
            return hash.SequenceEqual(newHash);
        }
    }
    #endregion

    #region Interfaces
    public interface IEncryptionService
    {
        /// <summary>
        /// Method used for encrypting a string with the technique: argon2
        /// </summary>
        /// <param name="word">The word that will be encrypted.</param>
        /// <param name="salt">The Salt that will be used for the encryption.</param>
        /// <returns>Encryption</returns>
        byte[] EncryptWord(string word, byte[] salt);
        /// <summary>
        /// Generates a salt
        /// </summary>
        /// <returns>Salt</returns>
        byte[] GenerateSalt();
        /// <summary>
        /// Method used for verifying a word using the salt and the hash
        /// </summary>
        /// <param name="word">word that you want to check it is the hash</param>
        /// <param name="salt">salt that is used assumably in creating the hash</param>
        /// <param name="hash">that you want to check means the word with the salt</param>
        /// <returns>if the word that will be generated is the same outcome as the hash</returns>
        public bool VerifyHash(string word, byte[] salt, byte[] hash);
    }
    #endregion 
}
