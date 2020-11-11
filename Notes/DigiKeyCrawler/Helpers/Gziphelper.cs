using System;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace DigiKeyCrawler.Helpers
{
    public class Gziphelper
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Compress(string text)
        {
            var bytes = Encoding.Unicode.GetBytes(text);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        /// <summary>
        /// 解压 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Decompress(string text)
        {
            var bytes = Convert.FromBase64String(text);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }
    }
}
