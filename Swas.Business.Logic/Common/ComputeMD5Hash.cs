namespace Swas.Business.Logic.Common
{
    using Data.Access.Context;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    public static class CryptoProvider
    {
        #region Methods

        public static string ComputeMD5Hash(string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new Exception(String.Format("GSWC.Utilities.CryptoProvider.ComputeMD5Hash -> {0}", "Source is empty!"));

            StringBuilder result = new StringBuilder(32, 32);
            byte[] hashData = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(source));

            foreach (byte b in hashData)
                result.Append(b.ToString("X2"));

            return result.ToString().ToUpper();
        }

        #endregion
    }


}
