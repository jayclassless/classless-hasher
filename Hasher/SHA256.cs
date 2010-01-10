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

namespace Classless.Hasher {
	/// <summary>Computes the SHA256 hash for the input data using the managed library.</summary>
	public class Sha256 : BlockHashAlgorithm {
		private readonly object syncLock = new object();

		private uint[] accumulator = new uint[] { 0x6A09E667, 0xBB67AE85, 0x3C6EF372, 0xA54FF53A, 0x510E527F, 0x9B05688C, 0x1F83D9AB, 0x5BE0CD19 };


		#region Table
		static private uint[] K = new uint[] {
			0x428A2F98, 0x71374491, 0xB5C0FBCF, 0xE9B5DBA5,
			0x3956C25B, 0x59F111F1, 0x923F82A4, 0xAB1C5ED5,
			0xD807AA98, 0x12835B01, 0x243185BE, 0x550C7DC3,
			0x72BE5D74, 0x80DEB1FE, 0x9BDC06A7, 0xC19BF174,
			0xE49B69C1, 0xEFBE4786, 0x0FC19DC6, 0x240CA1CC,
			0x2DE92C6F, 0x4A7484AA, 0x5CB0A9DC, 0x76F988DA,
			0x983E5152, 0xA831C66D, 0xB00327C8, 0xBF597FC7,
			0xC6E00BF3, 0xD5A79147, 0x06CA6351, 0x14292967,
			0x27B70A85, 0x2E1B2138, 0x4D2C6DFC, 0x53380D13,
			0x650A7354, 0x766A0ABB, 0x81C2C92E, 0x92722C85,
			0xA2BFE8A1, 0xA81A664B, 0xC24B8B70, 0xC76C51A3,
			0xD192E819, 0xD6990624, 0xF40E3585, 0x106AA070,
			0x19A4C116, 0x1E376C08, 0x2748774C, 0x34B0BCB5,
			0x391C0CB3, 0x4ED8AA4A, 0x5B9CCA4F, 0x682E6FF3,
			0x748F82EE, 0x78A5636F, 0x84C87814, 0x8CC70208,
			0x90BEFFFA, 0xA4506CEB, 0xBEF9A3F7, 0xC67178F2
		};
		#endregion


		/// <summary>Initializes a new instance of the SHA256 class.</summary>
		public Sha256() : base(64) {
			HashSizeValue = 256;
		}


		/// <summary>Initializes the algorithm.</summary>
		override public void Initialize() {
			lock (syncLock) {
				base.Initialize();
				accumulator = new uint[] { 0x6A09E667, 0xBB67AE85, 0x3C6EF372, 0xA54FF53A, 0x510E527F, 0x9B05688C, 0x1F83D9AB, 0x5BE0CD19 };
			}
		}


		/// <summary>Process a block of data.</summary>
		/// <param name="inputBuffer">The block of data to process.</param>
		/// <param name="inputOffset">Where to start in the block.</param>
		override protected void ProcessBlock(byte[] inputBuffer, int inputOffset) {
			lock (syncLock) {
				uint[] workBuffer = new uint[64];
				uint a = accumulator[0];
				uint b = accumulator[1];
				uint c = accumulator[2];
				uint d = accumulator[3];
				uint e = accumulator[4];
				uint f = accumulator[5];
				uint g = accumulator[6];
				uint h = accumulator[7];
				uint T1, T2;
				int i;

				uint[] temp = Conversions.ByteToUInt(inputBuffer, inputOffset, BlockSize, EndianType.BigEndian);
				Array.Copy(temp, 0, workBuffer, 0, temp.Length);
				for (i = 16; i < 64; i++) {
					workBuffer[i] =
						(((workBuffer[i - 2] >> 17) | (workBuffer[i - 2] << 15)) ^ ((workBuffer[i - 2] >> 19) | (workBuffer[i - 2] << 13)) ^ (workBuffer[i - 2] >> 10))
						+ workBuffer[i - 7]
						+ (((workBuffer[i - 15] >> 7) | (workBuffer[i - 15] << 25)) ^ ((workBuffer[i - 15] >> 18) | (workBuffer[i - 15] << 14)) ^ (workBuffer[i - 15] >> 3))
						+ workBuffer[i - 16]
					;
				}

				for (i = 0; i < 64; i++) {
					T1 = h + (((e >> 6) | (e << 26)) ^ ((e >> 11) | (e << 21)) ^ ((e >> 25) | (e << 7))) + ((e & f) ^ (~e & g)) + K[i] + workBuffer[i];
					T2 = (((a >> 2) | (a << 30)) ^ ((a >> 13) | (a << 19)) ^ ((a >> 22) | (a << 10))) + ((a & b) ^ (a & c) ^ (b & c));

					h = g; g = f; f = e;
					e = d + T1;
					d = c; c = b; b = a;
					a = T1 + T2;
				}

				accumulator[0] += a;
				accumulator[1] += b;
				accumulator[2] += c;
				accumulator[3] += d;
				accumulator[4] += e;
				accumulator[5] += f;
				accumulator[6] += g;
				accumulator[7] += h;
			}
		}


		/// <summary>Process the last block of data.</summary>
		/// <param name="inputBuffer">The block of data to process.</param>
		/// <param name="inputOffset">Where to start in the block.</param>
		/// <param name="inputCount">How many bytes need to be processed.</param>
		/// <returns>The results of the completed hash calculation.</returns>
		override protected byte[] ProcessFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount) {
			lock (syncLock) {
				StandardDigestPadding(inputBuffer, inputOffset, inputCount, EndianType.BigEndian);
				return Conversions.UIntToByte(accumulator, EndianType.BigEndian);
			}
		}
	}
}
