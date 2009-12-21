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
 * Portions created by the Initial Developer are Copyright (C) 2009 the Initial
 * Developer. All Rights Reserved.
 *
 * Contributor(s):
 *		Jason Simeone (jay@classless.net)
 * 
 * ***** END LICENSE BLOCK ***** */
#endregion

using System;
using System.Collections;
using NUnit.Framework;
using Classless.Hasher.Utilities;

namespace Classless.Hasher.Tests {
	[TestFixture]
	public class BitToolsTests {
		static public IEnumerable RightShortTests {
			get {
				string mask = "1000000000000000";
				string rMask = mask, lMask = mask;
				yield return new TestCaseData(Convert.ToUInt16(mask, 2), 0, Convert.ToUInt16(mask, 2));
				for (int i = 1; i < 32; i++) {
					rMask = rMask.Substring(rMask.Length - 1, 1) + rMask.Substring(0, rMask.Length - 1);
					yield return new TestCaseData(Convert.ToUInt16(mask, 2), i, Convert.ToUInt16(rMask, 2));
					lMask = lMask.Substring(1, lMask.Length - 1) + lMask.Substring(0, 1);
					yield return new TestCaseData(Convert.ToUInt16(mask, 2), (i * -1), Convert.ToUInt16(lMask, 2));
				}
			}
		}

		[Test, TestCaseSource("RightShortTests")]
		public void RotateRightShortTest(ushort input, int shift, ushort expectedOutput) {
			Assert.AreEqual(expectedOutput, BitTools.RotateRight(input, shift));
		}


		static public IEnumerable LeftShortTests {
			get {
				string mask = "1000000000000000";
				string rMask = mask, lMask = mask;
				yield return new TestCaseData(Convert.ToUInt16(mask, 2), 0, Convert.ToUInt16(mask, 2));
				for (int i = 1; i < 32; i++) {
					rMask = rMask.Substring(rMask.Length - 1, 1) + rMask.Substring(0, rMask.Length - 1);
					yield return new TestCaseData(Convert.ToUInt16(mask, 2), (i * -1), Convert.ToUInt16(rMask, 2));
					lMask = lMask.Substring(1, lMask.Length - 1) + lMask.Substring(0, 1);
					yield return new TestCaseData(Convert.ToUInt16(mask, 2), i, Convert.ToUInt16(lMask, 2));
				}
			}
		}

		[Test, TestCaseSource("LeftShortTests")]
		public void RotateLeftShortTest(ushort input, int shift, ushort expectedOutput) {
			Assert.AreEqual(expectedOutput, BitTools.RotateLeft(input, shift));
		}


		static public IEnumerable RightIntTests {
			get {
				string mask = "10000000000000000000000000000000";
				string rMask = mask, lMask = mask;
				yield return new TestCaseData(Convert.ToUInt32(mask, 2), 0, Convert.ToUInt32(mask, 2));
				for (int i = 1; i < 64; i++) {
					rMask = rMask.Substring(rMask.Length - 1, 1) + rMask.Substring(0, rMask.Length - 1);
					yield return new TestCaseData(Convert.ToUInt32(mask, 2), i, Convert.ToUInt32(rMask, 2));
					lMask = lMask.Substring(1, lMask.Length - 1) + lMask.Substring(0, 1);
					yield return new TestCaseData(Convert.ToUInt32(mask, 2), (i * -1), Convert.ToUInt32(lMask, 2));
				}
			}
		}

		[Test, TestCaseSource("RightIntTests")]
		public void RotateRightIntTest(uint input, int shift, uint expectedOutput) {
			Assert.AreEqual(expectedOutput, BitTools.RotateRight(input, shift));
		}


		static public IEnumerable LeftIntTests {
			get {
				string mask = "10000000000000000000000000000000";
				string rMask = mask, lMask = mask;
				yield return new TestCaseData(Convert.ToUInt32(mask, 2), 0, Convert.ToUInt32(mask, 2));
				for (int i = 1; i < 64; i++) {
					rMask = rMask.Substring(rMask.Length - 1, 1) + rMask.Substring(0, rMask.Length - 1);
					yield return new TestCaseData(Convert.ToUInt32(mask, 2), (i * -1), Convert.ToUInt32(rMask, 2));
					lMask = lMask.Substring(1, lMask.Length - 1) + lMask.Substring(0, 1);
					yield return new TestCaseData(Convert.ToUInt32(mask, 2), i, Convert.ToUInt32(lMask, 2));
				}
			}
		}

		[Test, TestCaseSource("LeftIntTests")]
		public void RotateLeftIntTest(uint input, int shift, uint expectedOutput) {
			Assert.AreEqual(expectedOutput, BitTools.RotateLeft(input, shift));
		}

	
		static public IEnumerable RightLongTests {
			get {
				string mask = "1000000000000000000000000000000000000000000000000000000000000000";
				string rMask = mask, lMask = mask;
				yield return new TestCaseData(Convert.ToUInt64(mask, 2), 0, Convert.ToUInt64(mask, 2));
				for (int i = 1; i < 128; i++) {
					rMask = rMask.Substring(rMask.Length - 1, 1) + rMask.Substring(0, rMask.Length - 1);
					yield return new TestCaseData(Convert.ToUInt64(mask, 2), i, Convert.ToUInt64(rMask, 2));
					lMask = lMask.Substring(1, lMask.Length - 1) + lMask.Substring(0, 1);
					yield return new TestCaseData(Convert.ToUInt64(mask, 2), (i * -1), Convert.ToUInt64(lMask, 2));
				}
			}
		}

		[Test, TestCaseSource("RightLongTests")]
		public void RotateRightLongTest(ulong input, int shift, ulong expectedOutput) {
			Assert.AreEqual(expectedOutput, BitTools.RotateRight(input, shift));
		}


		static public IEnumerable LeftLongTests {
			get {
				string mask = "1000000000000000000000000000000000000000000000000000000000000000";
				string rMask = mask, lMask = mask;
				yield return new TestCaseData(Convert.ToUInt64(mask, 2), 0, Convert.ToUInt64(mask, 2));
				for (int i = 1; i < 128; i++) {
					rMask = rMask.Substring(rMask.Length - 1, 1) + rMask.Substring(0, rMask.Length - 1);
					yield return new TestCaseData(Convert.ToUInt64(mask, 2), (i * -1), Convert.ToUInt64(rMask, 2));
					lMask = lMask.Substring(1, lMask.Length - 1) + lMask.Substring(0, 1);
					yield return new TestCaseData(Convert.ToUInt64(mask, 2), i, Convert.ToUInt64(lMask, 2));
				}
			}
		}

		[Test, TestCaseSource("LeftLongTests")]
		public void RotateLeftLongTest(ulong input, int shift, ulong expectedOutput) {
			Assert.AreEqual(expectedOutput, BitTools.RotateLeft(input, shift));
		}
	}
}
