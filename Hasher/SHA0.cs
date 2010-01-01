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
	/// <summary>Computes the SHA0 hash for the input data using the managed library.</summary>
	public class Sha0 : BlockHashAlgorithm {
		private readonly object syncLock = new object();

		private uint[] accumulator = new uint[] { 0x67452301, 0xEFCDAB89, 0x98BADCFE, 0x10325476, 0xC3D2E1F0 };


		/// <summary>Initializes a new instance of the SHA0 class.</summary>
		public Sha0() : base(64) {
			HashSizeValue = 160;
		}


		/// <summary>Initializes an implementation of System.Security.Cryptography.HashAlgorithm.</summary>
		override public void Initialize() {
			lock (syncLock) {
				base.Initialize();
				accumulator = new uint[] { 0x67452301, 0xEFCDAB89, 0x98BADCFE, 0x10325476, 0xC3D2E1F0 };
			}
		}


		/// <summary>Process a block of data.</summary>
		/// <param name="inputBuffer">The block of data to process.</param>
		/// <param name="inputOffset">Where to start in the block.</param>
		override protected void ProcessBlock(byte[] inputBuffer, int inputOffset) {
			lock (syncLock) {
				uint[] workBuffer = new uint[80];
				uint A = accumulator[0];
				uint B = accumulator[1];
				uint C = accumulator[2];
				uint D = accumulator[3];
				uint E = accumulator[4];

				uint[] temp = Conversions.ByteToUInt(inputBuffer, inputOffset, BlockSize, EndianType.BigEndian);
				Array.Copy(temp, 0, workBuffer, 0, temp.Length);
				for (int i = 16; i < 80; i++) {
					workBuffer[i] = workBuffer[i - 16] ^ workBuffer[i - 14] ^ workBuffer[i - 8] ^ workBuffer[i - 3];
				}

				#region Round 1
				E += ((A << 5) | (A >> 27)) + ((D ^ (B & (C ^ D))) + 0x5A827999) + workBuffer[0];  B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + ((C ^ (A & (B ^ C))) + 0x5A827999) + workBuffer[1];  A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + ((B ^ (E & (A ^ B))) + 0x5A827999) + workBuffer[2];  E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + ((A ^ (D & (E ^ A))) + 0x5A827999) + workBuffer[3];  D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + ((E ^ (C & (D ^ E))) + 0x5A827999) + workBuffer[4];  C = ((C << 30) | (C >> 2));
				E += ((A << 5) | (A >> 27)) + ((D ^ (B & (C ^ D))) + 0x5A827999) + workBuffer[5];  B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + ((C ^ (A & (B ^ C))) + 0x5A827999) + workBuffer[6];  A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + ((B ^ (E & (A ^ B))) + 0x5A827999) + workBuffer[7];  E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + ((A ^ (D & (E ^ A))) + 0x5A827999) + workBuffer[8];  D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + ((E ^ (C & (D ^ E))) + 0x5A827999) + workBuffer[9];  C = ((C << 30) | (C >> 2));
				E += ((A << 5) | (A >> 27)) + ((D ^ (B & (C ^ D))) + 0x5A827999) + workBuffer[10]; B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + ((C ^ (A & (B ^ C))) + 0x5A827999) + workBuffer[11]; A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + ((B ^ (E & (A ^ B))) + 0x5A827999) + workBuffer[12]; E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + ((A ^ (D & (E ^ A))) + 0x5A827999) + workBuffer[13]; D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + ((E ^ (C & (D ^ E))) + 0x5A827999) + workBuffer[14]; C = ((C << 30) | (C >> 2));
				E += ((A << 5) | (A >> 27)) + ((D ^ (B & (C ^ D))) + 0x5A827999) + workBuffer[15]; B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + ((C ^ (A & (B ^ C))) + 0x5A827999) + workBuffer[16]; A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + ((B ^ (E & (A ^ B))) + 0x5A827999) + workBuffer[17]; E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + ((A ^ (D & (E ^ A))) + 0x5A827999) + workBuffer[18]; D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + ((E ^ (C & (D ^ E))) + 0x5A827999) + workBuffer[19]; C = ((C << 30) | (C >> 2));
				#endregion

				#region Round 2
				E += ((A << 5) | (A >> 27)) + ((B ^ C ^ D) + 0x6ED9EBA1) + workBuffer[20]; B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + ((A ^ B ^ C) + 0x6ED9EBA1) + workBuffer[21]; A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + ((E ^ A ^ B) + 0x6ED9EBA1) + workBuffer[22]; E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + ((D ^ E ^ A) + 0x6ED9EBA1) + workBuffer[23]; D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + ((C ^ D ^ E) + 0x6ED9EBA1) + workBuffer[24]; C = ((C << 30) | (C >> 2));
				E += ((A << 5) | (A >> 27)) + ((B ^ C ^ D) + 0x6ED9EBA1) + workBuffer[25]; B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + ((A ^ B ^ C) + 0x6ED9EBA1) + workBuffer[26]; A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + ((E ^ A ^ B) + 0x6ED9EBA1) + workBuffer[27]; E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + ((D ^ E ^ A) + 0x6ED9EBA1) + workBuffer[28]; D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + ((C ^ D ^ E) + 0x6ED9EBA1) + workBuffer[29]; C = ((C << 30) | (C >> 2));
				E += ((A << 5) | (A >> 27)) + ((B ^ C ^ D) + 0x6ED9EBA1) + workBuffer[30]; B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + ((A ^ B ^ C) + 0x6ED9EBA1) + workBuffer[31]; A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + ((E ^ A ^ B) + 0x6ED9EBA1) + workBuffer[32]; E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + ((D ^ E ^ A) + 0x6ED9EBA1) + workBuffer[33]; D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + ((C ^ D ^ E) + 0x6ED9EBA1) + workBuffer[34]; C = ((C << 30) | (C >> 2));
				E += ((A << 5) | (A >> 27)) + ((B ^ C ^ D) + 0x6ED9EBA1) + workBuffer[35]; B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + ((A ^ B ^ C) + 0x6ED9EBA1) + workBuffer[36]; A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + ((E ^ A ^ B) + 0x6ED9EBA1) + workBuffer[37]; E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + ((D ^ E ^ A) + 0x6ED9EBA1) + workBuffer[38]; D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + ((C ^ D ^ E) + 0x6ED9EBA1) + workBuffer[39]; C = ((C << 30) | (C >> 2));
				#endregion

				#region Round 3
				E += ((A << 5) | (A >> 27)) + (((B & C) | (D & (B | C))) + 0x8F1BBCDC) + workBuffer[40]; B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + (((A & B) | (C & (A | B))) + 0x8F1BBCDC) + workBuffer[41]; A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + (((E & A) | (B & (E | A))) + 0x8F1BBCDC) + workBuffer[42]; E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + (((D & E) | (A & (D | E))) + 0x8F1BBCDC) + workBuffer[43]; D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + (((C & D) | (E & (C | D))) + 0x8F1BBCDC) + workBuffer[44]; C = ((C << 30) | (C >> 2));
				E += ((A << 5) | (A >> 27)) + (((B & C) | (D & (B | C))) + 0x8F1BBCDC) + workBuffer[45]; B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + (((A & B) | (C & (A | B))) + 0x8F1BBCDC) + workBuffer[46]; A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + (((E & A) | (B & (E | A))) + 0x8F1BBCDC) + workBuffer[47]; E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + (((D & E) | (A & (D | E))) + 0x8F1BBCDC) + workBuffer[48]; D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + (((C & D) | (E & (C | D))) + 0x8F1BBCDC) + workBuffer[49]; C = ((C << 30) | (C >> 2));
				E += ((A << 5) | (A >> 27)) + (((B & C) | (D & (B | C))) + 0x8F1BBCDC) + workBuffer[50]; B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + (((A & B) | (C & (A | B))) + 0x8F1BBCDC) + workBuffer[51]; A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + (((E & A) | (B & (E | A))) + 0x8F1BBCDC) + workBuffer[52]; E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + (((D & E) | (A & (D | E))) + 0x8F1BBCDC) + workBuffer[53]; D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + (((C & D) | (E & (C | D))) + 0x8F1BBCDC) + workBuffer[54]; C = ((C << 30) | (C >> 2));
				E += ((A << 5) | (A >> 27)) + (((B & C) | (D & (B | C))) + 0x8F1BBCDC) + workBuffer[55]; B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + (((A & B) | (C & (A | B))) + 0x8F1BBCDC) + workBuffer[56]; A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + (((E & A) | (B & (E | A))) + 0x8F1BBCDC) + workBuffer[57]; E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + (((D & E) | (A & (D | E))) + 0x8F1BBCDC) + workBuffer[58]; D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + (((C & D) | (E & (C | D))) + 0x8F1BBCDC) + workBuffer[59]; C = ((C << 30) | (C >> 2));
				#endregion

				#region Round 4
				E += ((A << 5) | (A >> 27)) + ((B ^ C ^ D) + 0xCA62C1D6) + workBuffer[60]; B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + ((A ^ B ^ C) + 0xCA62C1D6) + workBuffer[61]; A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + ((E ^ A ^ B) + 0xCA62C1D6) + workBuffer[62]; E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + ((D ^ E ^ A) + 0xCA62C1D6) + workBuffer[63]; D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + ((C ^ D ^ E) + 0xCA62C1D6) + workBuffer[64]; C = ((C << 30) | (C >> 2));
				E += ((A << 5) | (A >> 27)) + ((B ^ C ^ D) + 0xCA62C1D6) + workBuffer[65]; B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + ((A ^ B ^ C) + 0xCA62C1D6) + workBuffer[66]; A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + ((E ^ A ^ B) + 0xCA62C1D6) + workBuffer[67]; E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + ((D ^ E ^ A) + 0xCA62C1D6) + workBuffer[68]; D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + ((C ^ D ^ E) + 0xCA62C1D6) + workBuffer[69]; C = ((C << 30) | (C >> 2));
				E += ((A << 5) | (A >> 27)) + ((B ^ C ^ D) + 0xCA62C1D6) + workBuffer[70]; B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + ((A ^ B ^ C) + 0xCA62C1D6) + workBuffer[71]; A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + ((E ^ A ^ B) + 0xCA62C1D6) + workBuffer[72]; E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + ((D ^ E ^ A) + 0xCA62C1D6) + workBuffer[73]; D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + ((C ^ D ^ E) + 0xCA62C1D6) + workBuffer[74]; C = ((C << 30) | (C >> 2));
				E += ((A << 5) | (A >> 27)) + ((B ^ C ^ D) + 0xCA62C1D6) + workBuffer[75]; B = ((B << 30) | (B >> 2));
				D += ((E << 5) | (E >> 27)) + ((A ^ B ^ C) + 0xCA62C1D6) + workBuffer[76]; A = ((A << 30) | (A >> 2));
				C += ((D << 5) | (D >> 27)) + ((E ^ A ^ B) + 0xCA62C1D6) + workBuffer[77]; E = ((E << 30) | (E >> 2));
				B += ((C << 5) | (C >> 27)) + ((D ^ E ^ A) + 0xCA62C1D6) + workBuffer[78]; D = ((D << 30) | (D >> 2));
				A += ((B << 5) | (B >> 27)) + ((C ^ D ^ E) + 0xCA62C1D6) + workBuffer[79]; C = ((C << 30) | (C >> 2));
				#endregion

				accumulator[0] += A;
				accumulator[1] += B;
				accumulator[2] += C;
				accumulator[3] += D;
				accumulator[4] += E;
			}
		}


		/// <summary>Process the last block of data.</summary>
		/// <param name="inputBuffer">The block of data to process.</param>
		/// <param name="inputOffset">Where to start in the block.</param>
		/// <param name="inputCount">How many bytes need to be processed.</param>
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
				Array.Copy(Conversions.ULongToByte(size, EndianType.BigEndian), 0, temp, (inputCount + paddingSize), 8);

				// Push the final block(s) into the calculation.
				ProcessBlock(temp, 0);
				if (temp.Length == (BlockSize * 2)) {
					ProcessBlock(temp, BlockSize);
				}

				return Conversions.UIntToByte(accumulator, EndianType.BigEndian);
			}
		}
	}
}
