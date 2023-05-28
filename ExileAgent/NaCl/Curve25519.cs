using System;
using System.Runtime.CompilerServices;
using NaCl.Internal;

namespace NaCl
{
	[NullableContext(1)]
	[Nullable(0)]
	public static class Curve25519
	{
		[NullableContext(0)]
		public unsafe static void ScalarMultiplication(Span<byte> q, ReadOnlySpan<byte> n, ReadOnlySpan<byte> p)
		{
			Span<byte> span = q;
			Span<int> span2 = new Span<int>(stackalloc byte[(UIntPtr)40], 10);
			Span<int> span3 = new Span<int>(stackalloc byte[(UIntPtr)40], 10);
			Span<int> span4 = new Span<int>(stackalloc byte[(UIntPtr)40], 10);
			Span<int> span5 = new Span<int>(stackalloc byte[(UIntPtr)40], 10);
			Span<int> span6 = new Span<int>(stackalloc byte[(UIntPtr)40], 10);
			Span<int> span7 = new Span<int>(stackalloc byte[(UIntPtr)40], 10);
			Span<int> span8 = new Span<int>(stackalloc byte[(UIntPtr)40], 10);
			for (int i = 0; i < 32; i++)
			{
				*span[i] = *n[i];
			}
			ref byte ptr = ref span[0];
			ptr &= 248;
			ref byte ptr2 = ref span[31];
			ptr2 &= 127;
			ref byte ptr3 = ref span[31];
			ptr3 |= 64;
			Fe25519.FromBytes(span2, p);
			Fe25519.One(span3);
			Fe25519.Zero(span4);
			Fe25519.Copy(span5, span2);
			Fe25519.One(span6);
			int num = 0;
			for (int j = 254; j >= 0; j--)
			{
				int num2 = *span[j / 8] >> (j & 7);
				num2 &= 1;
				num ^= num2;
				Fe25519.CSwap(span3, span5, num);
				Fe25519.CSwap(span4, span6, num);
				num = num2;
				Fe25519.Sub(span7, span5, span6);
				Fe25519.Sub(span8, span3, span4);
				Fe25519.Add(span3, span3, span4);
				Fe25519.Add(span4, span5, span6);
				Fe25519.Mul(span6, span7, span3);
				Fe25519.Mul(span4, span4, span8);
				Fe25519.Sq(span7, span8);
				Fe25519.Sq(span8, span3);
				Fe25519.Add(span5, span6, span4);
				Fe25519.Sub(span4, span6, span4);
				Fe25519.Mul(span3, span8, span7);
				Fe25519.Sub(span8, span8, span7);
				Fe25519.Sq(span4, span4);
				Fe25519.Mul121666(span6, span8);
				Fe25519.Sq(span5, span5);
				Fe25519.Add(span7, span7, span6);
				Fe25519.Mul(span6, span2, span4);
				Fe25519.Mul(span4, span8, span7);
			}
			Fe25519.CSwap(span3, span5, num);
			Fe25519.CSwap(span4, span6, num);
			Fe25519.Invert(span4, span4);
			Fe25519.Mul(span3, span3, span4);
			Fe25519.ToBytes(q, span3);
		}

		[NullableContext(0)]
		[return: Nullable(1)]
		public static byte[] ScalarMultiplication(ReadOnlySpan<byte> n, ReadOnlySpan<byte> p)
		{
			byte[] array = new byte[32];
			Curve25519.ScalarMultiplication(array, n, p);
			return array;
		}

		public static byte[] ScalarMultiplication(byte[] n, byte[] p)
		{
			return Curve25519.ScalarMultiplication(new Span<byte>(n, 0, 32), new Span<byte>(p, 0, 32));
		}

		public static void ScalarMultiplication(byte[] q, byte[] n, byte[] p)
		{
			Curve25519.ScalarMultiplication(new Span<byte>(q, 0, 32), new Span<byte>(n, 0, 32), new Span<byte>(p, 0, 32));
		}

		public static void ScalarMultiplication(byte[] q, int qOffset, byte[] n, int nOffset, byte[] p, int pOffset)
		{
			Curve25519.ScalarMultiplication(new Span<byte>(q, qOffset, 32), new Span<byte>(n, nOffset, 32), new Span<byte>(p, pOffset, 32));
		}

		[NullableContext(0)]
		public unsafe static void ScalarMultiplicationBase(Span<byte> q, Span<byte> n)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)32], 32);
			*span[0] = 9;
			Curve25519.ScalarMultiplication(q, n, span);
		}

		public static void ScalarMultiplicationBase(byte[] q, byte[] n)
		{
			Curve25519.ScalarMultiplicationBase(new Span<byte>(q, 0, 32), new Span<byte>(n, 0, 32));
		}

		public static void ScalarMultiplicationBase(byte[] q, int qOffset, byte[] n, int nOffset)
		{
			Curve25519.ScalarMultiplicationBase(new Span<byte>(q, qOffset, 32), new Span<byte>(n, nOffset, 32));
		}

		[NullableContext(0)]
		[return: Nullable(1)]
		public static byte[] ScalarMultiplicationBase(Span<byte> n)
		{
			byte[] array = new byte[32];
			Curve25519.ScalarMultiplicationBase(array, n);
			return array;
		}

		public static byte[] ScalarMultiplicationBase(byte[] n)
		{
			byte[] array = new byte[32];
			Curve25519.ScalarMultiplicationBase(array, n);
			return array;
		}

		public const int ScalarLength = 32;
	}
}
