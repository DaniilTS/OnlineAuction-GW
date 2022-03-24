using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Helpers
{
    public static class SHA512
    {
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
