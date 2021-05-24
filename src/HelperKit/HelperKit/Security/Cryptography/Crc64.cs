using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace HelperKit.Security.Cryptography
{
    /// <summary>
    /// Implements a 64-bit CRC hash algorithm for a given polynomial.
    /// </summary>
    public class Crc64 : HashAlgorithm
    {
        /// <summary>
        /// Seed Value
        /// </summary>
        public const ulong DefaultSeed = 0x0;

        private readonly ulong[] _table;

        private readonly ulong _seed;
        private ulong _hash;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="polynomial"></param>
        /// <param name="seed"></param>
        /// <exception cref="PlatformNotSupportedException"></exception>
        protected Crc64(ulong polynomial, ulong seed = DefaultSeed)
        {
            if (!BitConverter.IsLittleEndian)
                throw new PlatformNotSupportedException("Not supported on Big Endian processors");

            _table = InitializeTable(polynomial);
            _seed = _hash = seed;
        }

        /// <summary>
        /// Initialize seed
        /// </summary>
        public override void Initialize()
        {
            _hash = _seed;
        }

        /// <summary>
        /// Executes the case hash action
        /// </summary>
        /// <param name="array"></param>
        /// <param name="ibStart"></param>
        /// <param name="cbSize"></param>
        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            _hash = CalculateHash(_hash, _table, array, ibStart, cbSize);
        }

        protected override byte[] HashFinal()
        {
            var hashBuffer = UInt64ToBigEndianBytes(_hash);
            HashValue = hashBuffer;
            return hashBuffer;
        }

        /// <summary>
        /// Default hash size
        /// </summary>
        public override int HashSize => 64;

        private static ulong CalculateHash(ulong seed, ulong[] table, IList<byte> buffer, int start, int size)
        {
            var hash = seed;
            for (var i = start; i < start + size; i++)
                unchecked
                {
                    hash = (hash >> 8) ^ table[(buffer[i] ^ hash) & 0xff];
                }

            return hash;
        }

        private static byte[] UInt64ToBigEndianBytes(ulong value)
        {
            var result = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return result;
        }

        private static ulong[] InitializeTable(ulong polynomial)
        {
            if (polynomial == Crc64Iso.Iso3309Polynomial && Crc64Iso.Table != null)
                return Crc64Iso.Table;

            var createTable = CreateTable(polynomial);

            if (polynomial == Crc64Iso.Iso3309Polynomial)
                Crc64Iso.Table = createTable;

            return createTable;
        }

        private static ulong[] CreateTable(ulong polynomial)
        {
            var createTable = new ulong[256];
            for (var i = 0; i < 256; ++i)
            {
                var entry = (ulong) i;
                for (var j = 0; j < 8; ++j)
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ polynomial;
                    else
                        entry >>= 1;
                createTable[i] = entry;
            }

            return createTable;
        }

        /// <summary>
        /// Create Crc64
        /// </summary>
        /// <param name="polynomial"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static Crc64 Create(ulong polynomial, ulong seed = DefaultSeed)
        {
            return new(polynomial, seed);
        }
    }

    /// <summary>
    /// Implements a 64-bit CRC ISO 3309 hash algorithm for a given polynomial.
    /// </summary>
    public sealed class Crc64Iso : Crc64
    {
        internal static ulong[] Table;

        /// <summary>
        /// Iso3309 value
        /// </summary>
        public const ulong Iso3309Polynomial = 0xD800000000000000;

        private Crc64Iso()
            : base(Iso3309Polynomial)
        {
        }

        private Crc64Iso(ulong seed)
            : base(Iso3309Polynomial, seed)
        {
        }

        /// <summary>
        /// Create Crc64Iso
        /// </summary>
        /// <returns></returns>
        public new static Crc64Iso Create()
        {
            return new();
        }

        /// <summary>
        /// Create Crc64Iso
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static Crc64Iso Create(ulong seed)
        {
            return new(seed);
        }
    }
}