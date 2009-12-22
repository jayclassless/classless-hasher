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
using System.Collections.Generic;
using NUnit.Framework;
using Classless.Hasher.Methods;

namespace Classless.Hasher.Tests {
	[TestFixture]
	public class HashChainTests {
		static public byte[][] NumericCrc32VectorChain = new byte[][] {
			TestVectors.h("261DAEE5"),
			TestVectors.h("5007984D"),
			TestVectors.h("5EF3FB04"),
			TestVectors.h("4D31966B"),
			TestVectors.h("90E84517"),
			TestVectors.h("35288127"),
			TestVectors.h("577DDCB6"),
			TestVectors.h("24388FAE"),
			TestVectors.h("A9195936"),
			TestVectors.h("A61362E8"),
			TestVectors.h("AE3EE783"),
			TestVectors.h("C78BCE01"),
		};


		[Test]
		public void Constructor1Test() {
			HashChain chain = new HashChain(new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit)), TestVectors.Battery.Numeric);

			for (int i = 0; i < NumericCrc32VectorChain.Length; i++) {
				CustomAssert.AreEqual(NumericCrc32VectorChain[i], chain[i]);
			}
		}


		[Test]
		public void Constructor2Test() {
			HashChain chain = new HashChain(new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit)), TestVectors.Battery.Numeric, 5);

			for (int i = 0; i < NumericCrc32VectorChain.Length; i++) {
				CustomAssert.AreEqual(NumericCrc32VectorChain[i], chain[i]);
			}
		}


		[Test]
		public void Constructor3Test() {
			HashChain chain = new HashChain(new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit)), TestVectors.Battery.NumericRepeated, 0, 10);

			for (int i = 0; i < NumericCrc32VectorChain.Length; i++) {
				CustomAssert.AreEqual(NumericCrc32VectorChain[i], chain[i]);
			}
		}


		[Test]
		public void Constructor4Test() {
			HashChain chain = new HashChain(new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit)), TestVectors.Battery.NumericRepeated, 0, 10, 5);

			for (int i = 0; i < NumericCrc32VectorChain.Length; i++) {
				CustomAssert.AreEqual(NumericCrc32VectorChain[i], chain[i]);
			}
		}


		[Test, ExpectedException(typeof(ArgumentException))]
		public void ConstructorBadHasherTest() {
			HashChain chain = new HashChain(null, TestVectors.Battery.Numeric);
		}


		[Test, ExpectedException(typeof(ArgumentException))]
		public void ConstructorBadRangeTest() {
			HashChain chain = new HashChain(new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit)), TestVectors.Battery.Numeric, 4, 10);
		}


		[Test, ExpectedException(typeof(ArgumentException))]
		public void ConstructorBadInitTest() {
			HashChain chain = new HashChain(new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit)), TestVectors.Battery.Numeric, 0);
		}


		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void BadIndexTest() {
			HashChain chain = new HashChain(new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit)), TestVectors.Battery.Numeric);
			byte[] test = chain[-1];
		}


		[Test]
		public void Enumerator1Test() {
			HashChain chain = new HashChain(new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit)), TestVectors.Battery.Numeric);

			int i = 0;
			foreach (byte[] hash in chain) {
				CustomAssert.AreEqual(NumericCrc32VectorChain[i], hash);
				i++;
			}
		}


		[Test]
		public void Enumerator2Test() {
			HashChain chain = new HashChain(new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit)), TestVectors.Battery.Numeric);

			IEnumerator<byte[]> e = ((IEnumerable<byte[]>)chain).GetEnumerator();
			int i = 0;
			while (e.MoveNext()) {
				CustomAssert.AreEqual(NumericCrc32VectorChain[i], e.Current);
				i++;
			}
		}


		[Test]
		public void Enumerator3Test() {
			HashChain chain = new HashChain(new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit)), TestVectors.Battery.Numeric);

			IEnumerator e = ((IEnumerable)chain).GetEnumerator();
			int i = 0;
			while (e.MoveNext()) {
				CustomAssert.AreEqual(NumericCrc32VectorChain[i], (byte[])e.Current);
				i++;
			}
		}
	}
}
