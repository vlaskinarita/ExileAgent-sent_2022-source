using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using NaCl.Internal;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace NaCl
{
	public sealed class Curve25519XSalsa20Poly1305 : XSalsa20Poly1305
	{
		public unsafe Curve25519XSalsa20Poly1305(ReadOnlySpan<byte> secretKey, ReadOnlySpan<byte> publicKey)
		{
			ReadOnlySpan<byte> input = new Span<byte>(stackalloc byte[(UIntPtr)16], 16);
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)32], 32);
			Curve25519.ScalarMultiplication(span, secretKey, publicKey);
			HSalsa20.Transform(this.key, input, span, Span<byte>.Empty);
			span.Clear();
		}

		public unsafe static void KeyPair(Span<byte> secretKey, Span<byte> publicKey)
		{
			if (secretKey.Length != 32)
			{
				throw new ArgumentException(Curve25519XSalsa20Poly1305.getString_1(107351533));
			}
			if (publicKey.Length != 32)
			{
				throw new ArgumentException(Curve25519XSalsa20Poly1305.getString_1(107351520));
			}
			using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
			{
				byte[] array = new byte[32];
				randomNumberGenerator.GetBytes(array);
				for (int i = 0; i < 32; i++)
				{
					*secretKey[i] = array[i];
				}
				Array.Clear(array, 0, 32);
				Curve25519.ScalarMultiplicationBase(publicKey, secretKey);
			}
		}

		[NullableContext(1)]
		public static void KeyPair(byte[] secretKey, byte[] publicKey)
		{
			Curve25519XSalsa20Poly1305.KeyPair(new Span<byte>(secretKey), new Span<byte>(publicKey));
		}

		[NullableContext(1)]
		public static void KeyPair(out byte[] secretKey, out byte[] publicKey)
		{
			secretKey = new byte[32];
			publicKey = new byte[32];
			Curve25519XSalsa20Poly1305.KeyPair(secretKey, publicKey);
		}

		[return: Nullable(new byte[]
		{
			0,
			1,
			1
		})]
		public static ValueTuple<byte[], byte[]> KeyPair()
		{
			byte[] array = new byte[32];
			byte[] array2 = new byte[32];
			Curve25519XSalsa20Poly1305.KeyPair(array, array2);
			return new ValueTuple<byte[], byte[]>(array, array2);
		}

		static Curve25519XSalsa20Poly1305()
		{
			Strings.CreateGetStringDelegate(typeof(Curve25519XSalsa20Poly1305));
		}

		public const int SecretKeyLength = 32;

		public const int PublicKeyLength = 32;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
