using System;
using System.IO;

namespace HelperKit
{
    public static partial class Extensions
    {
        /// <summary>
        /// Convert stream value to byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this Stream stream)
        {
            _ = stream ?? throw new ArgumentNullException(nameof(stream));

            using MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
    }
}