using System;

namespace CriptografiaJúlioCésar
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "the quick brown fox jumps over the lazy dog";
            string messageCrypt = "wkh txlfn eurzq ira mxpsv ryhu wkh odcb grj";

            CesarCypher cypher = new CesarCypher();            

            string newMessageCrypt = cypher.Crypt(message);
            string newMessageDecrypt = cypher.Decrypt(messageCrypt);

            if (messageCrypt == newMessageCrypt)
                Console.WriteLine("Criptografia realizada correntamente");

            if (newMessageDecrypt == message)
                Console.WriteLine("Descriptografia realizada correntamente");
        }
    }
}
