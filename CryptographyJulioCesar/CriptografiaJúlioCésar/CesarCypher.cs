using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CriptografiaJúlioCésar
{
    public class CesarCypher : ICrypt, IDecrypt
    {

        /// <summary>
        /// Define quantiade de casas no Crypt e Decrypt
        /// </summary>
        private const int STEP = 3;

        public enum Type
        {
            Crypt,
            Decrypt
        }

        public string Crypt(string message)
        {
            if (message == "")
                return "";

            return ProcessMessage(message, Type.Crypt).ToLower();
        }

        public string Decrypt(string cryptedMessage)
        {
            return ProcessMessage(cryptedMessage.ToLower(), Type.Decrypt).ToLower();
        }

        /// <summary>
        /// Método responsável por criptografar e descriptografar baseado no tipo passado
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string ProcessMessage(string message, Type type)
        {

            // Validando string
            Validate(message);

            message = message.ToUpper();
            char[] alphabet = getAlphabet();
            string newText = "";

            for (int i = 0; i < message.Length; i++)
            {
                char letter = message[i];
                if (letter.ToString() == " " || char.IsDigit(letter))
                {
                    newText += letter.ToString();
                    continue;
                }

                int index = alphabet.ToList().FindIndex(x => x.ToString() == letter.ToString().ToUpper());
                if (type == Type.Decrypt)
                {
                    int currentPosition = index - STEP;
                    newText += currentPosition < 0 ? alphabet[alphabet.Length - (currentPosition * -1)] : alphabet[currentPosition];
                } else
                {
                    int currentPosition = index + STEP;
                    newText += (currentPosition > alphabet.Length - 1) ? alphabet[currentPosition - alphabet.Length] : alphabet[currentPosition];
                }
            }

            return newText;
        }

        /// <summary>
        /// Validamos a string antes de iniciar o Crypt ou Decrypt
        /// </summary>
        /// <param name="message"></param>
        protected void Validate(string message)
        {

            // Verificando se a mensagem está null
            if (message == null)
                throw new ArgumentNullException();

            // Regex para verificar se existe algum caracter especial na String
            if (!Regex.Match(message.ToLower(), @"^[a-z0-9 ]+$").Success)
                throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        private static char[] getAlphabet()
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        }
    }
}
