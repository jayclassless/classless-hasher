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
using Classless.Hasher.Utilities;

namespace Classless.Hasher.Tests {
	[TestFixture]
	public class HashListTests {
		static public byte[][] AlphanumericCrc32VectorList = new byte[][] {
			TestVectors.h("321E6D05"),
			TestVectors.h("3CDEC33B"),
			TestVectors.h("67C2DCD1"),
			TestVectors.h("B280DEC1"),
			TestVectors.h("9B11CF41"),
			TestVectors.h("C4F96AF3"),
			TestVectors.h("0943260C"),
		};
		static public byte[] AlphanumericCrc32TopHash = TestVectors.h("4C26B5E2");


		static public byte[][] AlphanumericMD5VectorList = new byte[][] {
			TestVectors.h("E86410FA2D6E2634FD8AC5F4B3AFE7F3"),
			TestVectors.h("D123D9C26465577A2D10958881C9B31A"),
			TestVectors.h("0203F00980F0FBC9ED4DA0516D718DB0"),
			TestVectors.h("E1B96525C4675C0D15CE2F93F4C6B902"),
			TestVectors.h("2F241F38B6985B4C9EAE4321A6203B4C"),
			TestVectors.h("81AA85A65DE320FBB0C24EA4B8E296D8"),
			TestVectors.h("7647966B7343C29048673252E490F736"),
		};
		static public byte[] AlphanumericMD5TopHash = TestVectors.h("3C3C68010BAC0A8CDA33E2BD74538256");


		[Test]
		public void DefaultConstructorTest() {
			HashList list = new HashList();
			Assert.AreEqual(1024, list.BlockSize);
		}

		[Test]
		public void Constructor1Test() {
			HashList list = new HashList(new MD5());
			Assert.AreEqual(1024, list.BlockSize);
		}

		[Test]
		public void Constructor2Test() {
			HashList list = new HashList(new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit)), 10);

			CustomAssert.AreEqual(AlphanumericCrc32TopHash, list.ComputeHash(TestVectors.Battery.Alphanumeric));

			for (int i = 0; i < AlphanumericCrc32VectorList.Length; i++) {
				CustomAssert.AreEqual(AlphanumericCrc32VectorList[i], list[i].Hash);
			}

			Assert.AreEqual(10, list.BlockSize);
		}

		[Test]
		public void Constructor2BTest() {
			HashList list = new HashList(new MD5(), 10);

			CustomAssert.AreEqual(AlphanumericMD5TopHash, list.ComputeHash(TestVectors.Battery.Alphanumeric));

			for (int i = 0; i < AlphanumericMD5VectorList.Length; i++) {
				CustomAssert.AreEqual(AlphanumericMD5VectorList[i], list[i].Hash);
			}

			Assert.AreEqual(10, list.BlockSize);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void ConstructorBadHasherTest() {
			HashList list = new HashList(null);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void ConstructorBadHasher2Test() {
			HashList list = new HashList(null, 10);
		}


		[Test]
		public void Enumerator1Test() {
			HashList list = new HashList(new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit)), 10);
			list.ComputeHash(TestVectors.Battery.Alphanumeric);

			int i = 0;
			foreach (HashNode node in list) {
				CustomAssert.AreEqual(AlphanumericCrc32VectorList[i], node.Hash);
				i++;
			}
		}


		[Test]
		public void Enumerator2Test() {
			HashList list = new HashList(new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit)), 10);
			list.ComputeHash(TestVectors.Battery.Alphanumeric);

			IEnumerator<HashNode> e = ((IEnumerable<HashNode>)list).GetEnumerator();
			int i = 0;
			while (e.MoveNext()) {
				CustomAssert.AreEqual(AlphanumericCrc32VectorList[i], e.Current.Hash);
				i++;
			}
		}


		[Test]
		public void Enumerator3Test() {
			HashList list = new HashList(new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit)), 10);
			list.ComputeHash(TestVectors.Battery.Alphanumeric);

			IEnumerator e = ((IEnumerable)list).GetEnumerator();
			int i = 0;
			while (e.MoveNext()) {
				CustomAssert.AreEqual(AlphanumericCrc32VectorList[i], ((HashNode)e.Current).Hash);
				i++;
			}
		}
	}
}
