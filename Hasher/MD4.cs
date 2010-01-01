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
	/// <summary>Computes the MD4 hash for the input data using the managed library.</summary>
	public class MD4 : BlockHashAlgorithm {
		private readonly object syncLock = new object();

		internal uint[] accumulator = new uint[] { 0x67452301, 0xEFCDAB89, 0x98BADCFE, 0x10325476 };


		/// <summary>Initializes a new instance of the MD4 class.</summary>
		public MD4() : base(64) {
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
				uint A = accumulator[0];
				uint B = accumulator[1];
				uint C = accumulator[2];
				uint D = accumulator[3];

				workBuffer = Conversions.ByteToUInt(inputBuffer, inputOffset, BlockSize);

				#region Round 1
				A += workBuffer[ 0] + ((B & C) | (~B & D));
				A = (A <<  3) | (A >> (32 -  3));
				D += workBuffer[ 1] + ((A & B) | (~A & C));
				D = (D <<  7) | (D >> (32 -  7));
				C += workBuffer[ 2] + ((D & A) | (~D & B));
				C = (C << 11) | (C >> (32 - 11));
				B += workBuffer[ 3] + ((C & D) | (~C & A));
				B = (B << 19) | (B >> (32 - 19));
				A += workBuffer[ 4] + ((B & C) | (~B & D));
				A = (A <<  3) | (A >> (32 -  3));
				D += workBuffer[ 5] + ((A & B) | (~A & C));
				D = (D <<  7) | (D >> (32 -  7));
				C += workBuffer[ 6] + ((D & A) | (~D & B));
				C = (C << 11) | (C >> (32 - 11));
				B += workBuffer[ 7] + ((C & D) | (~C & A));
				B = (B << 19) | (B >> (32 - 19));
				A += workBuffer[ 8] + ((B & C) | (~B & D));
				A = (A <<  3) | (A >> (32 -  3));
				D += workBuffer[ 9] + ((A & B) | (~A & C));
				D = (D <<  7) | (D >> (32 -  7));
				C += workBuffer[10] + ((D & A) | (~D & B));
				C = (C << 11) | (C >> (32 - 11));
				B += workBuffer[11] + ((C & D) | (~C & A));
				B = (B << 19) | (B >> (32 - 19));
				A += workBuffer[12] + ((B & C) | (~B & D));
				A = (A <<  3) | (A >> (32 -  3));
				D += workBuffer[13] + ((A & B) | (~A & C));
				D = (D <<  7) | (D >> (32 -  7));
				C += workBuffer[14] + ((D & A) | (~D & B));
				C = (C << 11) | (C >> (32 - 11));
				B += workBuffer[15] + ((C & D) | (~C & A));
				B = (B << 19) | (B >> (32 - 19));
				#endregion

				#region Round 2
				A += workBuffer[ 0] + 0x5A827999 + ((B & C) | (B & D) | (C & D));
				A = (A <<  3) | (A >> (32 -  3));
				D += workBuffer[ 4] + 0x5A827999 + ((A & B) | (A & C) | (B & C));
				D = (D <<  5) | (D >> (32 -  5));
				C += workBuffer[ 8] + 0x5A827999 + ((D & A) | (D & B) | (A & B));
				C = (C <<  9) | (C >> (32 -  9));
				B += workBuffer[12] + 0x5A827999 + ((C & D) | (C & A) | (D & A));
				B = (B << 13) | (B >> (32 - 13));
				A += workBuffer[ 1] + 0x5A827999 + ((B & C) | (B & D) | (C & D));
				A = (A <<  3) | (A >> (32 -  3));
				D += workBuffer[ 5] + 0x5A827999 + ((A & B) | (A & C) | (B & C));
				D = (D <<  5) | (D >> (32 -  5));
				C += workBuffer[ 9] + 0x5A827999 + ((D & A) | (D & B) | (A & B));
				C = (C <<  9) | (C >> (32 -  9));
				B += workBuffer[13] + 0x5A827999 + ((C & D) | (C & A) | (D & A));
				B = (B << 13) | (B >> (32 - 13));
				A += workBuffer[ 2] + 0x5A827999 + ((B & C) | (B & D) | (C & D));
				A = (A <<  3) | (A >> (32 -  3));
				D += workBuffer[ 6] + 0x5A827999 + ((A & B) | (A & C) | (B & C));
				D = (D <<  5) | (D >> (32 -  5));
				C += workBuffer[10] + 0x5A827999 + ((D & A) | (D & B) | (A & B));
				C = (C <<  9) | (C >> (32 -  9));
				B += workBuffer[14] + 0x5A827999 + ((C & D) | (C & A) | (D & A));
				B = (B << 13) | (B >> (32 - 13));
				A += workBuffer[ 3] + 0x5A827999 + ((B & C) | (B & D) | (C & D));
				A = (A <<  3) | (A >> (32 -  3));
				D += workBuffer[ 7] + 0x5A827999 + ((A & B) | (A & C) | (B & C));
				D = (D <<  5) | (D >> (32 -  5));
				C += workBuffer[11] + 0x5A827999 + ((D & A) | (D & B) | (A & B));
				C = (C <<  9) | (C >> (32 -  9));
				B += workBuffer[15] + 0x5A827999 + ((C & D) | (C & A) | (D & A));
				B = (B << 13) | (B >> (32 - 13));
				#endregion

				#region Round 3
				A += workBuffer[ 0] + 0x6ED9EBA1 + (B ^ C ^ D);
				A = (A <<  3) | (A >> (32 -  3));
				D += workBuffer[ 8] + 0x6ED9EBA1 + (A ^ B ^ C);
				D = (D <<  9) | (D >> (32 -  9));
				C += workBuffer[ 4] + 0x6ED9EBA1 + (D ^ A ^ B);
				C = (C << 11) | (C >> (32 - 11));
				B += workBuffer[12] + 0x6ED9EBA1 + (C ^ D ^ A);
				B = (B << 15) | (B >> (32 - 15));
				A += workBuffer[ 2] + 0x6ED9EBA1 + (B ^ C ^ D);
				A = (A <<  3) | (A >> (32 -  3));
				D += workBuffer[10] + 0x6ED9EBA1 + (A ^ B ^ C);
				D = (D <<  9) | (D >> (32 -  9));
				C += workBuffer[ 6] + 0x6ED9EBA1 + (D ^ A ^ B);
				C = (C << 11) | (C >> (32 - 11));
				B += workBuffer[14] + 0x6ED9EBA1 + (C ^ D ^ A);
				B = (B << 15) | (B >> (32 - 15));
				A += workBuffer[ 1] + 0x6ED9EBA1 + (B ^ C ^ D);
				A = (A <<  3) | (A >> (32 -  3));
				D += workBuffer[ 9] + 0x6ED9EBA1 + (A ^ B ^ C);
				D = (D <<  9) | (D >> (32 -  9));
				C += workBuffer[ 5] + 0x6ED9EBA1 + (D ^ A ^ B);
				C = (C << 11) | (C >> (32 - 11));
				B += workBuffer[13] + 0x6ED9EBA1 + (C ^ D ^ A);
				B = (B << 15) | (B >> (32 - 15));
				A += workBuffer[ 3] + 0x6ED9EBA1 + (B ^ C ^ D);
				A = (A <<  3) | (A >> (32 -  3));
				D += workBuffer[11] + 0x6ED9EBA1 + (A ^ B ^ C);
				D = (D <<  9) | (D >> (32 -  9));
				C += workBuffer[ 7] + 0x6ED9EBA1 + (D ^ A ^ B);
				C = (C << 11) | (C >> (32 - 11));
				B += workBuffer[15] + 0x6ED9EBA1 + (C ^ D ^ A);
				B = (B << 15) | (B >> (32 - 15));
				#endregion

				accumulator[0] += A;
				accumulator[1] += B;
				accumulator[2] += C;
				accumulator[3] += D;
			}
		}


		/// <summary>Process the last block of data.</summary>
		/// <param name="inputBuffer">The block of data to process.</param>
		/// <param name="inputOffset">Where to start in the block.</param>
		/// <param name="inputCount">How many bytes should be processed.</param>
		/// <returns>The results of the completed hash calculation.</returns>
		override protected byte[] ProcessFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount) {
			lock (syncLock) {
				byte[] temp;
				int paddingSize;
				ulong size;

				// Figure out how much padding is needed between the last byte and the size.
				paddingSize = (int)(((ulong)inputCount + (ulong)Count) % (ulong)BlockSize);
				paddingSize = (BlockSize - 8) - paddingSize;
				if (paddingSize < 1) { paddingSize += BlockSize; }

				// Create the final, padded block(s).
				temp = new byte[inputCount + paddingSize + 8];
				Array.Copy(inputBuffer, inputOffset, temp, 0, inputCount);
				temp[inputCount] = 0x80;
				size = ((ulong)Count + (ulong)inputCount) * 8;
				Array.Copy(Conversions.ULongToByte(size), 0, temp, (inputCount + paddingSize), 8);

				// Push the final block(s) into the calculation.
				ProcessBlock(temp, 0);
				if (temp.Length == (BlockSize * 2)) {
					ProcessBlock(temp, BlockSize);
				}

				return Conversions.UIntToByte(accumulator);
			}
		}
	}
}
