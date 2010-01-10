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
	/// <summary>Computes the MD5 hash for the input data using the managed library.</summary>
	public class MD5 : BlockHashAlgorithm {
		private readonly object syncLock = new object();

		internal uint[] accumulator = new uint[] { 0x67452301, 0xEFCDAB89, 0x98BADCFE, 0x10325476 };


		/// <summary>Initializes a new instance of the MD5 class.</summary>
		public MD5() : base(64) {
			HashSizeValue = 128;
		}


		/// <summary>Initializes the algorithm.</summary>
		override public void Initialize() {
			lock (syncLock) {
				base.Initialize();
				accumulator = new uint[] { 0x67452301, 0xEFCDAB89, 0x98BADCFE, 0x10325476 };
			}
		}


		/// <summary>Process a block of data.</summary>
		/// <param name="inputBuffer">The block of data to process.</param>
		/// <param name="inputOffset">Where to start in the block.</param>
		override protected void ProcessBlock(byte[] inputBuffer, int inputOffset) {
			lock (syncLock) {
				uint[] workBuffer;
				uint a = accumulator[0];
				uint b = accumulator[1];
				uint c = accumulator[2];
				uint d = accumulator[3];

				workBuffer = Conversions.ByteToUInt(inputBuffer, inputOffset, BlockSize);

				#region Round 1
				a += workBuffer[ 0] + 0xD76AA478 + (d ^ (b & (c ^ d)));
				a = ((a <<  7) | (a >> (32 -  7))) + b;
				d += workBuffer[ 1] + 0xE8C7B756 + (c ^ (a & (b ^ c)));
				d = ((d << 12) | (d >> (32 - 12))) + a;
				c += workBuffer[ 2] + 0x242070DB + (b ^ (d & (a ^ b)));
				c = ((c << 17) | (c >> (32 - 17))) + d;
				b += workBuffer[ 3] + 0xC1BDCEEE + (a ^ (c & (d ^ a)));
				b = ((b << 22) | (b >> (32 - 22))) + c;
				a += workBuffer[ 4] + 0xF57C0FAF + (d ^ (b & (c ^ d)));
				a = ((a <<  7) | (a >> (32 -  7))) + b;
				d += workBuffer[ 5] + 0x4787C62A + (c ^ (a & (b ^ c)));
				d = ((d << 12) | (d >> (32 - 12))) + a;
				c += workBuffer[ 6] + 0xA8304613 + (b ^ (d & (a ^ b)));
				c = ((c << 17) | (c >> (32 - 17))) + d;
				b += workBuffer[ 7] + 0xFD469501 + (a ^ (c & (d ^ a)));
				b = ((b << 22) | (b >> (32 - 22))) + c;
				a += workBuffer[ 8] + 0x698098D8 + (d ^ (b & (c ^ d)));
				a = ((a <<  7) | (a >> (32 -  7))) + b;
				d += workBuffer[ 9] + 0x8B44F7AF + (c ^ (a & (b ^ c)));
				d = ((d << 12) | (d >> (32 - 12))) + a;
				c += workBuffer[10] + 0xFFFF5BB1 + (b ^ (d & (a ^ b)));
				c = ((c << 17) | (c >> (32 - 17))) + d;
				b += workBuffer[11] + 0x895CD7BE + (a ^ (c & (d ^ a)));
				b = ((b << 22) | (b >> (32 - 22))) + c;
				a += workBuffer[12] + 0x6B901122 + (d ^ (b & (c ^ d)));
				a = ((a <<  7) | (a >> (32 -  7))) + b;
				d += workBuffer[13] + 0xFD987193 + (c ^ (a & (b ^ c)));
				d = ((d << 12) | (d >> (32 - 12))) + a;
				c += workBuffer[14] + 0xA679438E + (b ^ (d & (a ^ b)));
				c = ((c << 17) | (c >> (32 - 17))) + d;
				b += workBuffer[15] + 0x49B40821 + (a ^ (c & (d ^ a)));
				b = ((b << 22) | (b >> (32 - 22))) + c;
				#endregion

				#region Round 2
				a += workBuffer[ 1] + 0xF61E2562 + (c ^ (d & (b ^ c)));
				a = ((a <<  5) | (a >> (32 -  5))) + b;
				d += workBuffer[ 6] + 0xC040B340 + (b ^ (c & (a ^ b)));
				d = ((d <<  9) | (d >> (32 -  9))) + a;
				c += workBuffer[11] + 0x265E5A51 + (a ^ (b & (d ^ a)));
				c = ((c << 14) | (c >> (32 - 14))) + d;
				b += workBuffer[ 0] + 0xE9B6C7AA + (d ^ (a & (c ^ d)));
				b = ((b << 20) | (b >> (32 - 20))) + c;
				a += workBuffer[ 5] + 0xD62F105D + (c ^ (d & (b ^ c)));
				a = ((a <<  5) | (a >> (32 -  5))) + b;
				d += workBuffer[10] + 0x02441453 + (b ^ (c & (a ^ b)));
				d = ((d <<  9) | (d >> (32 -  9))) + a;
				c += workBuffer[15] + 0xD8A1E681 + (a ^ (b & (d ^ a)));
				c = ((c << 14) | (c >> (32 - 14))) + d;
				b += workBuffer[ 4] + 0xE7D3FBC8 + (d ^ (a & (c ^ d)));
				b = ((b << 20) | (b >> (32 - 20))) + c;
				a += workBuffer[ 9] + 0x21E1CDE6 + (c ^ (d & (b ^ c)));
				a = ((a <<  5) | (a >> (32 -  5))) + b;
				d += workBuffer[14] + 0xC33707D6 + (b ^ (c & (a ^ b)));
				d = ((d <<  9) | (d >> (32 -  9))) + a;
				c += workBuffer[ 3] + 0xF4D50D87 + (a ^ (b & (d ^ a)));
				c = ((c << 14) | (c >> (32 - 14))) + d;
				b += workBuffer[ 8] + 0x455A14ED + (d ^ (a & (c ^ d)));
				b = ((b << 20) | (b >> (32 - 20))) + c;
				a += workBuffer[13] + 0xA9E3E905 + (c ^ (d & (b ^ c)));
				a = ((a <<  5) | (a >> (32 -  5))) + b;
				d += workBuffer[ 2] + 0xFCEFA3F8 + (b ^ (c & (a ^ b)));
				d = ((d <<  9) | (d >> (32 -  9))) + a;
				c += workBuffer[ 7] + 0x676F02D9 + (a ^ (b & (d ^ a)));
				c = ((c << 14) | (c >> (32 - 14))) + d;
				b += workBuffer[12] + 0x8D2A4C8A + (d ^ (a & (c ^ d)));
				b = ((b << 20) | (b >> (32 - 20))) + c;
				#endregion

				#region Round 3
				a += workBuffer[ 5] + 0xFFFA3942 + (b ^ c ^ d);
				a = ((a <<  4) | (a >> (32 -  4))) + b;
				d += workBuffer[ 8] + 0x8771F681 + (a ^ b ^ c);
				d = ((d << 11) | (d >> (32 - 11))) + a;
				c += workBuffer[11] + 0x6D9D6122 + (d ^ a ^ b);
				c = ((c << 16) | (c >> (32 - 16))) + d;
				b += workBuffer[14] + 0xFDE5380C + (c ^ d ^ a);
				b = ((b << 23) | (b >> (32 - 23))) + c;
				a += workBuffer[ 1] + 0xA4BEEA44 + (b ^ c ^ d);
				a = ((a <<  4) | (a >> (32 -  4))) + b;
				d += workBuffer[ 4] + 0x4BDECFA9 + (a ^ b ^ c);
				d = ((d << 11) | (d >> (32 - 11))) + a;
				c += workBuffer[ 7] + 0xF6BB4B60 + (d ^ a ^ b);
				c = ((c << 16) | (c >> (32 - 16))) + d;
				b += workBuffer[10] + 0xBEBFBC70 + (c ^ d ^ a);
				b = ((b << 23) | (b >> (32 - 23))) + c;
				a += workBuffer[13] + 0x289B7EC6 + (b ^ c ^ d);
				a = ((a <<  4) | (a >> (32 -  4))) + b;
				d += workBuffer[ 0] + 0xEAA127FA + (a ^ b ^ c);
				d = ((d << 11) | (d >> (32 - 11))) + a;
				c += workBuffer[ 3] + 0xD4EF3085 + (d ^ a ^ b);
				c = ((c << 16) | (c >> (32 - 16))) + d;
				b += workBuffer[ 6] + 0x04881D05 + (c ^ d ^ a);
				b = ((b << 23) | (b >> (32 - 23))) + c;
				a += workBuffer[ 9] + 0xD9D4D039 + (b ^ c ^ d);
				a = ((a <<  4) | (a >> (32 -  4))) + b;
				d += workBuffer[12] + 0xE6DB99E5 + (a ^ b ^ c);
				d = ((d << 11) | (d >> (32 - 11))) + a;
				c += workBuffer[15] + 0x1FA27CF8 + (d ^ a ^ b);
				c = ((c << 16) | (c >> (32 - 16))) + d;
				b += workBuffer[ 2] + 0xC4AC5665 + (c ^ d ^ a);
				b = ((b << 23) | (b >> (32 - 23))) + c;
				#endregion

				#region Round 4
				a += workBuffer[ 0] + 0xF4292244 + (c  ^ (b | ~d));
				a = ((a <<  6) | (a >> (32 -  6))) + b;
				d += workBuffer[ 7] + 0x432AFF97 + (b  ^ (a | ~c));
				d = ((d << 10) | (d >> (32 - 10))) + a;
				c += workBuffer[14] + 0xAB9423A7 + (a  ^ (d | ~b));
				c = ((c << 15) | (c >> (32 - 15))) + d;
				b += workBuffer[ 5] + 0xFC93A039 + (d  ^ (c | ~a));
				b = ((b << 21) | (b >> (32 - 21))) + c;
				a += workBuffer[12] + 0x655B59C3 + (c  ^ (b | ~d));
				a = ((a <<  6) | (a >> (32 -  6))) + b;
				d += workBuffer[ 3] + 0x8F0CCC92 + (b  ^ (a | ~c));
				d = ((d << 10) | (d >> (32 - 10))) + a;
				c += workBuffer[10] + 0xFFEFF47D + (a  ^ (d | ~b));
				c = ((c << 15) | (c >> (32 - 15))) + d;
				b += workBuffer[ 1] + 0x85845DD1 + (d  ^ (c | ~a));
				b = ((b << 21) | (b >> (32 - 21))) + c;
				a += workBuffer[ 8] + 0x6FA87E4F + (c  ^ (b | ~d));
				a = ((a <<  6) | (a >> (32 -  6))) + b;
				d += workBuffer[15] + 0xFE2CE6E0 + (b  ^ (a | ~c));
				d = ((d << 10) | (d >> (32 - 10))) + a;
				c += workBuffer[ 6] + 0xA3014314 + (a  ^ (d | ~b));
				c = ((c << 15) | (c >> (32 - 15))) + d;
				b += workBuffer[13] + 0x4E0811A1 + (d  ^ (c | ~a));
				b = ((b << 21) | (b >> (32 - 21))) + c;
				a += workBuffer[ 4] + 0xF7537E82 + (c  ^ (b | ~d));
				a = ((a <<  6) | (a >> (32 -  6))) + b;
				d += workBuffer[11] + 0xBD3AF235 + (b  ^ (a | ~c));
				d = ((d << 10) | (d >> (32 - 10))) + a;
				c += workBuffer[ 2] + 0x2AD7D2BB + (a  ^ (d | ~b));
				c = ((c << 15) | (c >> (32 - 15))) + d;
				b += workBuffer[ 9] + 0xEB86D391 + (d  ^ (c | ~a));
				b = ((b << 21) | (b >> (32 - 21))) + c;
				#endregion

				accumulator[0] += a;
				accumulator[1] += b;
				accumulator[2] += c;
				accumulator[3] += d;
			}
		}


		/// <summary>Process the last block of data.</summary>
		/// <param name="inputBuffer">The block of data to process.</param>
		/// <param name="inputOffset">Where to start in the block.</param>
		/// <param name="inputCount">How many blocks have been processed so far.</param>
		/// <returns>The results of the completed hash calculation.</returns>
		override protected byte[] ProcessFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount) {
			lock (syncLock) {
				StandardDigestPadding(inputBuffer, inputOffset, inputCount, EndianType.LittleEndian);
				return Conversions.UIntToByte(accumulator);
			}
		}
	}
}
