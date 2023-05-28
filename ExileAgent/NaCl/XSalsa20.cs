using System;
using System.Runtime.CompilerServices;
using NaCl.Internal;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace NaCl
{
	public sealed class XSalsa20 : IDisposable
	{
		public XSalsa20(ReadOnlySpan<byte> key)
		{
			if (key.Length != 32)
			{
				throw new ArgumentException(XSalsa20.getString_0(107351463));
			}
			this.key = key.ToArray();
		}

		[NullableContext(1)]
		public XSalsa20(byte[] key)
		{
			if (key.Length != 32)
			{
				throw new ArgumentException(XSalsa20.getString_0(107351463));
			}
			this.key = (byte[])key.Clone();
		}

		public void Dispose()
		{
			Array.Clear(this.key, 0, 32);
		}

		public unsafe void Transform(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> nonce)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)32], 32);
			HSalsa20.Transform(span, nonce, this.key, Span<byte>.Empty);
			StreamSalsa20Xor.Transform(output, input, nonce.Slice(16), span, 0UL);
		}

		[NullableContext(1)]
		public void Transform(byte[] output, int outputOffset, byte[] input, int inputOffset, int inputCount, byte[] nonce, int nonceOffset)
		{
			this.Transform(new Span<byte>(output, outputOffset, inputCount), new ReadOnlySpan<byte>(input, inputOffset, inputCount), new ReadOnlySpan<byte>(nonce, nonceOffset, 24));
		}

		static XSalsa20()
		{
			Strings.CreateGetStringDelegate(typeof(XSalsa20));
		}

		public const int KeyLength = 32;

		public const int NonceLength = 24;

		[Nullable(1)]
		private byte[] key;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
