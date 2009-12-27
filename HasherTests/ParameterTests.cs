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

namespace Classless.Hasher.Tests {
	[TestFixture]
	public class ParameterTests {
		static public object[] Algorithms = {
			new object[] { typeof(Crc) },
			new object[] { typeof(Fletcher) },
			new object[] { typeof(Fnv) },
			new object[] { typeof(GHash) },
			new object[] { typeof(Haval) },
			new object[] { typeof(Sum) },
			new object[] { typeof(Tiger) },
		};

		[Test, TestCaseSource("Algorithms"), ExpectedException(typeof(ArgumentNullException))]
		public void NullParameterTest(Type hasherType) {
			try {
				Activator.CreateInstance(hasherType, new object[] { null });
			} catch (System.Reflection.TargetInvocationException ex) {
				throw ex.InnerException;
			}
		}

		[Test, ExpectedException(typeof(NullReferenceException))]
		public void NullParameterTestSnefru() {
			Snefru2 snefru = new Snefru2(null);
		}


		static public IEnumerable BadCrcOrders {
			get {
				for (int i = -500; i <= 500; i++) {
					if ((i < 0) || (i > 64) || ((i % 8) > 0)) {
						yield return new TestCaseData(i);
					}
				}
			}
		} 

		[Test, TestCaseSource("BadCrcOrders"), ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void CrcOrderTest(int order) {
			CrcParameters param = new CrcParameters(order, 1, 1, 1, true);
		}


		static public IEnumerable BadFnvOrders {
			get {
				for (int i = -500; i <= 500; i++) {
					if ((i != 32) && (i != 64)) {
						yield return new TestCaseData(i);
					}
				}
			}
		}

		[Test, TestCaseSource("BadFnvOrders"), ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void FnvOrderTest(int order) {
			FnvParameters param = new FnvParameters(order, 1, 1, FnvAlgorithmType.Fnv1);
		}


		static public IEnumerable BadHavalPasses {
			get {
				for (short i = -500; i <= 500; i++) {
					if ((i != 3) && (i != 4) && (i != 5)) {
						yield return new TestCaseData(i);
					}
				}
			}
		}

		[Test, TestCaseSource("BadHavalPasses"), ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void HavalPassesTest(short passes) {
			HavalParameters param = new HavalParameters(passes, 128);
		}


		static public IEnumerable BadHavalLengths {
			get {
				for (short i = -500; i <= 500; i++) {
					if ((i != 128) && (i != 160) && (i != 192) && (i != 224) && (i != 256)) {
						yield return new TestCaseData(i);
					}
				}
			}
		}

		[Test, TestCaseSource("BadHavalLengths"), ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void HavalLengthTest(short length) {
			HavalParameters param = new HavalParameters(3, length);
		}


		static public IEnumerable BadSnefruPasses {
			get {
				for (short i = -500; i <= 500; i++) {
					if ((i != 4) && (i != 8)) {
						yield return new TestCaseData(i);
					}
				}
			}
		}

		[Test, TestCaseSource("BadSnefruPasses"), ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void SnefruPassesTest(short passes) {
			Snefru2Parameters param = new Snefru2Parameters(passes, 128);
		}


		static public IEnumerable BadSnefruLengths {
			get {
				for (short i = -500; i <= 500; i++) {
					if ((i != 128) && (i != 256)) {
						yield return new TestCaseData(i);
					}
				}
			}
		}

		[Test, TestCaseSource("BadSnefruLengths"), ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void SnefruLengthTest(short length) {
			Snefru2Parameters param = new Snefru2Parameters(4, length);
		}


		static public IEnumerable BadTigerLengths {
			get {
				for (short i = -500; i <= 500; i++) {
					if ((i != 128) && (i != 160) && (i != 192)) {
						yield return new TestCaseData(i);
					}
				}
			}
		}

		[Test, TestCaseSource("BadTigerLengths"), ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TigerLengthTest(short length) {
			TigerParameters param = new TigerParameters(length, TigerAlgorithmType.Tiger1);
		}


		static public object[] CrcParameterAliases = {
			new object[] { CrcStandard.Crc16BitArc, CrcStandard.Crc16Bit },
			new object[] { CrcStandard.Crc16BitIbm, CrcStandard.Crc16Bit },
			new object[] { CrcStandard.Crc16BitLha, CrcStandard.Crc16Bit },
			new object[] { CrcStandard.Crc16BitKermit, CrcStandard.Crc16BitCcitt },
			new object[] { CrcStandard.Crc16BitZmodem, CrcStandard.Crc16BitXmodem },
			new object[] { CrcStandard.Crc24BitOpenPgp, CrcStandard.Crc24Bit },
			new object[] { CrcStandard.Crc32BitPkzip, CrcStandard.Crc32Bit },
			new object[] { CrcStandard.Crc32BitItu, CrcStandard.Crc32Bit },
			new object[] { CrcStandard.Crc32BitCksum, CrcStandard.Crc32BitPosix },
		};

		[Test, TestCaseSource("CrcParameterAliases")]
		public void CrcParameterAliasesTest(CrcStandard alias, CrcStandard master) {
			Assert.AreEqual(CrcParameters.GetParameters(master), CrcParameters.GetParameters(alias));
		}


		static public IEnumerable BadSumOrders {
			get {
				for (int i = -500; i <= 500; i++) {
					if ((i < 0) || (i > 64) || ((i % 8) > 0)) {
						yield return new TestCaseData(i);
					}
				}
			}
		} 

		[Test, TestCaseSource("BadSumOrders"), ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void SumOrderTest(int order) {
			SumParameters param = new SumParameters(order);
		}


		static public IEnumerable BadFletcherOrders {
			get {
				for (int i = -500; i <= 500; i++) {
					if ((i != 8) && (i != 16) && (i != 32)) {
						yield return new TestCaseData(i);
					}
				}
			}
		} 

		[Test, TestCaseSource("BadFletcherOrders"), ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void FletcherOrderTest(int order) {
			FletcherParameters param = new FletcherParameters(order);
		}


		[Test]
		public void EqualsNullTest() {
			Assert.IsFalse(CrcParameters.GetParameters(CrcStandard.Crc32Bit).Equals(null));
		}
	}
}
