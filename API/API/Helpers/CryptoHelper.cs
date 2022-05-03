using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.API.Helpers
{
    public static class CryptoHelper
    {
        public static string GenerateSalt() 
        {
            var saltLength = 72;
            var salt = new byte[saltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            var s = new StringBuilder();

            foreach (var b in salt)
                s.Append(b.ToString("x2"));

            return s.ToString().ToUpper()[0..71];
        }

        public static async Task<string> GetHash(string input)
        {
            var x = new SHA512Managed();

            var bs = Encoding.Unicode.GetBytes(input);
            bs = await x.ComputeHashAsync(new MemoryStream(bs));
            var s = new StringBuilder();

            foreach (var b in bs)
                s.Append(b.ToString("x2"));

            return s.ToString().ToUpper();
        }
    }
}
