using System;
using System.Runtime.CompilerServices;
using NaCl.Internal;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace NaCl
{
	public class XSalsa20Poly1305 : IDisposable
	{
		public XSalsa20Poly1305(ReadOnlySpan<byte> key)
		{
			if (key.Length != 32)
			{
				throw new ArgumentException(XSalsa20Poly1305.getString_0(107351450));
			}
			this.key = key.ToArray();
			this.poly1305 = new Poly1305();
		}

		[NullableContext(1)]
		public XSalsa20Poly1305(byte[] key)
		{
			if (key.Length != 32)
			{
				throw new ArgumentException(XSalsa20Poly1305.getString_0(107351450));
			}
			this.key = (byte[])key.Clone();
			this.poly1305 = new Poly1305();
		}

		internal XSalsa20Poly1305()
		{
			this.poly1305 = new Poly1305();
			this.key = new byte[32];
		}

		public unsafe void Encrypt(Span<byte> cipher, Span<byte> mac, ReadOnlySpan<byte> message, ReadOnlySpan<byte> nonce)
		{
			Span<byte> c = new Span<byte>(stackalloc byte[(UIntPtr)64], 64);
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)32], 32);
			HSalsa20.Transform(span, nonce, this.key, ReadOnlySpan<byte>.Empty);
			int num = message.Length;
			if (num > 32)
			{
				num = 32;
			}
			for (int i = 0; i < num; i++)
			{
				*c[i + 32] = *message[i];
			}
			StreamSalsa20Xor.Transform(c, c.Slice(0, num + 32), nonce.Slice(16), span, 0UL);
			this.poly1305.SetKey(c.Slice(0, 32));
			for (int j = 0; j < num; j++)
			{
				*cipher[j] = *c[32 + j];
			}
			c.Clear();
			if (message.Length > num)
			{
				StreamSalsa20Xor.Transform(cipher.Slice(num), message.Slice(num), nonce.Slice(16), span, 1UL);
			}
			span.Clear();
			this.poly1305.Update(cipher.Slice(0, message.Length));
			this.poly1305.Final(mac);
			this.poly1305.Reset();
		}

		[NullableContext(1)]
		public void Encrypt(byte[] cipher, int cipherOffset, byte[] mac, int macOffset, byte[] message, int messageOffset, int messageCount, byte[] nonce, int nonceOffset)
		{
			this.Encrypt(new Span<byte>(cipher, cipherOffset, messageCount), new Span<byte>(mac, macOffset, 16), new ReadOnlySpan<byte>(message, messageOffset, messageCount), new ReadOnlySpan<byte>(nonce, nonceOffset, 24));
		}

		public void Encrypt(Span<byte> cipher, ReadOnlySpan<byte> message, ReadOnlySpan<byte> nonce)
		{
			this.Encrypt(cipher.Slice(16), cipher.Slice(0, 16), message, nonce);
		}

		[NullableContext(1)]
		public void Encrypt(byte[] cipher, int cipherOffset, byte[] message, int messageOffset, int messageCount, byte[] nonce, int nonceOffset)
		{
			this.Encrypt(new Span<byte>(cipher, cipherOffset + 16, messageCount), new Span<byte>(cipher, cipherOffset, 16), new ReadOnlySpan<byte>(message, messageOffset, messageCount), new ReadOnlySpan<byte>(nonce, nonceOffset, 24));
		}

		public unsafe bool TryDecrypt(Span<byte> message, ReadOnlySpan<byte> cipher, ReadOnlySpan<byte> mac, ReadOnlySpan<byte> nonce)
		{
			Span<byte> c = new Span<byte>(stackalloc byte[(UIntPtr)64], 64);
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)32], 32);
			HSalsa20.Transform(span, nonce, this.key, ReadOnlySpan<byte>.Empty);
			StreamSalsa20.Transform(c.Slice(0, 32), nonce.Slice(16), span);
			this.poly1305.SetKey(c.Slice(0, 32));
			if (!this.poly1305.Verify(mac, cipher))
			{
				this.poly1305.Reset();
				span.Clear();
				return false;
			}
			int num = cipher.Length;
			if (num > 32)
			{
				num = 32;
			}
			for (int i = 0; i < num; i++)
			{
				*c[32 + i] = *cipher[i];
			}
			StreamSalsa20Xor.Transform(c, c.Slice(0, 32 + num), nonce.Slice(16), span, 0UL);
			for (int j = 0; j < num; j++)
			{
				*message[j] = *c[j + 32];
			}
			if (cipher.Length > num)
			{
				StreamSalsa20Xor.Transform(message.Slice(num), cipher.Slice(num), nonce.Slice(16), span, 1UL);
			}
			span.Clear();
			this.poly1305.Reset();
			return true;
		}

		[NullableContext(1)]
		public bool TryDecrypt(byte[] message, int messageOffset, byte[] cipher, int cipherOffset, int cipherCount, byte[] mac, int macOffset, byte[] nonce, int nonceOffset)
		{
			return this.TryDecrypt(new Span<byte>(message, messageOffset, cipherCount), new ReadOnlySpan<byte>(cipher, cipherOffset, cipherCount), new ReadOnlySpan<byte>(mac, macOffset, 16), new ReadOnlySpan<byte>(nonce, nonceOffset, 24));
		}

		public bool TryDecrypt(Span<byte> message, ReadOnlySpan<byte> cipher, ReadOnlySpan<byte> nonce)
		{
			return this.TryDecrypt(message, cipher.Slice(16), cipher.Slice(0, 16), nonce);
		}

		[NullableContext(1)]
		public bool TryDecrypt(byte[] message, int messageOffset, byte[] cipher, int cipherOffset, int cipherCount, byte[] nonce, int nonceOffset)
		{
			return this.TryDecrypt(new Span<byte>(message, messageOffset, cipherCount - 16), new ReadOnlySpan<byte>(cipher, cipherOffset, cipherCount), new ReadOnlySpan<byte>(nonce, nonceOffset, 24));
		}

		public void Dispose()
		{
			Array.Clear(this.key, 0, this.key.Length);
			this.poly1305.Dispose();
		}

		static XSalsa20Poly1305()
		{
			Strings.CreateGetStringDelegate(typeof(XSalsa20Poly1305));
		}

		private const int ZeroBytesLength = 32;

		public const int KeyLength = 32;

		public const int TagLength = 16;

		public const int NonceLength = 24;

		[Nullable(1)]
		private Poly1305 poly1305;

		[Nullable(1)]
		internal byte[] key;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
