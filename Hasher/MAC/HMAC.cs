#region License
/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is Classless.Hasher - C#/.NET Hash and Checksum Algorithm Library.
 *
 * The Initial Developer of the Original Code is Classless.net.
 * Portions created by the Initial Developer are Copyright (C) 2004 the Initial
 * Developer. All Rights Reserved.
 *
 * Contributor(s):
 *		Jason Simeone (jay@classless.net)
 * 
 * ***** END LICENSE BLOCK ***** */
#endregion

using System;
using Classless.Hasher.Utilities;

namespace Classless.Hasher.MAC {
	/// <summary>Implements the HMAC keyed message authentication code algorithm.</summary>
	public class HMAC : System.Security.Cryptography.KeyedHashAlgorithm {
		private readonly object syncLock = new object();

		private System.Security.Cryptography.HashAlgorithm hash;
		private byte[] keyBuffer;
		private bool isHashing;


		/// <summary>Gets the size of the computed hash code in bits.</summary>
		override public int HashSize {
			get { return hash.HashSize; }
		}


		/// <summary>Initializes a new instance of the HMAC class.</summary>
		/// <param name="hash">The base hash algorithm to use.</param>
		/// <remarks>A random key will be used.</remarks>
		public HMAC(System.Security.Cryptography.HashAlgorithm hash) : this(hash, (byte[])null) { }

		/// <summary>Initializes a new instance of the HMAC class.</summary>
		/// <param name="hash">The base hash algorithm to use.</param>
		/// <param name="rgbKey">The key to use for the HMAC.</param>
		public HMAC(System.Security.Cryptography.HashAlgorithm hash, string rgbKey) : this(hash, Conversions.HexadecimalToByte(rgbKey)) { }

		/// <summary>Initializes a new instance of the HMAC class.</summary>
		/// <param name="hash">The base hash algorithm to use.</param>
		/// <param name="rgbKey">The key to use for the HMAC.</param>
		/// <remarks>If rgbKey is null, a random key will be used.</remarks>
		public HMAC(System.Security.Cryptography.HashAlgorithm hash, byte[] rgbKey) {
			lock (syncLock) {
				if (hash == null) { throw new ArgumentNullException("hash", Hasher.Properties.Resources.hashCantBeNull); }
				if (rgbKey == null) {
					Random r = new Random((int)System.DateTime.Now.Ticks);
					rgbKey = new byte[hash.HashSize / 8];
					r.NextBytes(rgbKey);
				}

				this.hash = hash;
				KeyValue = (byte[])rgbKey.Clone();
			}
		}


		/// <summary>Initializes the MAC.</summary>
		override public void Initialize() {
			lock (syncLock) {
				hash.Initialize();
				isHashing = false;
			}
		}


		/// <summary>Routes data written to the object into the hash algorithm for computing the hash.</summary>
		/// <param name="array">The input for which to compute the hash code.</param>
		/// <param name="ibStart">The offset into the byte array from which to begin using data. </param>
		/// <param name="cbSize">The number of bytes in the byte array to use as data. </param>
		override protected void HashCore(byte[] array, int ibStart, int cbSize) {
			lock (syncLock) {
				if (!isHashing) {
					keyBuffer = new byte[64];
					byte[] temp = new byte[64];
					byte[] key;

					if (this.Key.Length > 64) {
						key = hash.ComputeHash(this.Key);
					} else {
						key = this.Key;
					}
					Array.Copy(key, 0, keyBuffer, 0, key.Length);

					for (int i = 0; i < 64; i++) {
						temp[i] = (byte)(keyBuffer[i] ^ 0x36);
					}
					hash.TransformBlock(temp, 0, temp.Length, temp, 0);

					isHashing = true;
				}

				hash.TransformBlock(array, ibStart, cbSize, array, ibStart);
			}
		}


		/// <summary>Finalizes the hash computation after the last data is processed by the cryptographic stream object.</summary>
		/// <returns>The computed hash code.</returns>
		override protected byte[] HashFinal() {
			lock (syncLock) {
				byte[] data;
				byte[] temp = new byte[64];

				hash.TransformFinalBlock(new byte[0], 0, 0);
				data = hash.Hash;

				for (int i = 0; i < 64; i++) {
					temp[i] = (byte)(keyBuffer[i] ^ 0x5C);
				}
				hash.Initialize();
				hash.TransformBlock(temp, 0, temp.Length, temp, 0);
				hash.TransformFinalBlock(data, 0, data.Length);
				data = hash.Hash;

				keyBuffer = null;
				isHashing = false;
				return data;
			}
		}
	}
}
