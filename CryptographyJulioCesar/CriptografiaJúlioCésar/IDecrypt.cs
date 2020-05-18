using System;
using System.Collections.Generic;
using System.Text;

namespace CriptografiaJúlioCésar
{
    public interface IDecrypt
    {
        string Decrypt(string cryptedMessage);
    }
}
