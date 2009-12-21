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
			new object[] { typeof(CRC) },
			new object[] { typeof(FNV) },
			new object[] { typeof(GHash) },
			new object[] { typeof(HAVAL) },
			new object[] { typeof(Sum) },
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
			CRCParameters param = new CRCParameters(order, 1, 1, 1, true);
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
			FNVParameters param = new FNVParameters(order, 1, 1, FNVAlgorithmType.FNV1);
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
			HAVALParameters param = new HAVALParameters(passes, 128);
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
			HAVALParameters param = new HAVALParameters(3, length);
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


		static public object[] CrcParameterAliases = {
			new object[] { CRCStandard.CRC16_ARC, CRCStandard.CRC16 },
			new object[] { CRCStandard.CRC16_IBM, CRCStandard.CRC16 },
			new object[] { CRCStandard.CRC16_LHA, CRCStandard.CRC16 },
			new object[] { CRCStandard.CRC16_KERMIT, CRCStandard.CRC16_CCITT },
			new object[] { CRCStandard.CRC16_ZMODEM, CRCStandard.CRC16_XMODEM },
			new object[] { CRCStandard.CRC24_OPENPGP, CRCStandard.CRC24 },
			new object[] { CRCStandard.CRC32_PKZIP, CRCStandard.CRC32 },
			new object[] { CRCStandard.CRC32_ITU, CRCStandard.CRC32 },
			new object[] { CRCStandard.CRC32_CKSUM, CRCStandard.CRC32_POSIX },
		};

		[Test, TestCaseSource("CrcParameterAliases")]
		public void CrcParameterAliasesTest(CRCStandard alias, CRCStandard master) {
			Assert.AreEqual(CRCParameters.GetParameters(master), CRCParameters.GetParameters(alias));
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


		[Test]
		public void EqualsNullTest() {
			Assert.IsFalse(CRCParameters.GetParameters(CRCStandard.CRC32).Equals(null));
		}
	}
}
