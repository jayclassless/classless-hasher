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
using NUnit.Framework;
using Classless.Hasher.Mac;
using Classless.Hasher.Utilities;

namespace Classless.Hasher.Tests {
	[TestFixture]
	public class HMACTests {
		[Test]
		public void ConstructorDefaultTest() {
			Hmac hmac = new Hmac();
			Sha1 sha = new Sha1();
			Assert.AreEqual(sha.GetType(), hmac.HashAlgorithm.GetType());
			Assert.AreEqual(sha.HashSize, hmac.HashAlgorithm.HashSize);
			Assert.AreEqual(sha.BlockSize, hmac.Key.Length);
		}


		[Test]
		public void ConstructorSingleTest() {
			Hmac hmac = new Hmac(new MD5());
			MD5 md = new MD5();
			Assert.AreEqual(md.GetType(), hmac.HashAlgorithm.GetType());
			Assert.AreEqual(md.HashSize, hmac.HashAlgorithm.HashSize);
			Assert.AreEqual(md.BlockSize, hmac.Key.Length);
		}


		[Test]
		public void ConstructorDoubleTest() {
			byte[] key = TestVectors.s("1234567890");
			Hmac hmac = new Hmac(new MD5(), key);
			MD5 md = new MD5();
			Assert.AreEqual(md.GetType(), hmac.HashAlgorithm.GetType());
			Assert.AreEqual(md.HashSize, hmac.HashAlgorithm.HashSize);
			Assert.AreEqual(key.Length, hmac.Key.Length);
			for (int i = 0; i < key.Length; i++) {
				Assert.AreEqual(key[i], hmac.Key[i]);
			}
		}


		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void ConstructorNullHasherTest() {
			Hmac hmac = new Hmac(null);
		}


		static public object[] HashSizeHashers = {
			new object[] { new MD5() },
			new object[] { new Sha1() },
			new object[] { new Tiger() },
			new object[] { new Whirlpool() },
			new object[] { new Sha384() },
			new object[] { new Sha224() },
		};

		[Test, TestCaseSource("HashSizeHashers")]
		public void HashSizeTest(BlockHashAlgorithm hasher) {
			Hmac hmac = new Hmac(hasher);
			Assert.AreEqual(hasher.HashSize, hmac.HashSize);
		}


		[Test]
		public void ChangeHashGoodTest() {
			byte[] input = TestVectors.s("1234567890");
			Hmac hmac = new Hmac(new MD5());
			hmac.HashAlgorithm = new Sha1();
			hmac.TransformBlock(input, 0, 4, null, 0);
		}


		[Test, ExpectedException(typeof(System.Security.Cryptography.CryptographicException))]
		public void ChangeHashBadTest() {
			byte[] input = TestVectors.s("1234567890");
			Hmac hmac = new Hmac(new MD5());
			hmac.TransformBlock(input, 0, 4, null, 0);
			hmac.HashAlgorithm = new Sha1();
		}


		[Test]
		public void ChangeKeyGoodTest() {
			byte[] input = TestVectors.s("1234567890");
			Hmac hmac = new Hmac(new MD5());
			hmac.Key = TestVectors.s("new key!");
			hmac.TransformBlock(input, 0, 4, null, 0);
		}


		[Test, ExpectedException(typeof(System.Security.Cryptography.CryptographicException))]
		public void ChangeKeyBadTest() {
			byte[] input = TestVectors.s("1234567890");
			Hmac hmac = new Hmac(new MD5());
			hmac.TransformBlock(input, 0, 4, null, 0);
			hmac.Key = TestVectors.s("new key!");
		}


		[Test, TestCaseSource(typeof(TestVectorsHMAC), "Vectors")]
		public void TestVector(BlockHashAlgorithm hasher, byte[] key, byte[] input, byte[] expectedOutput) {
			Hmac hmac = new Hmac(hasher, key);
			byte[] result = hmac.ComputeHash(input);

			Assert.AreEqual(expectedOutput.Length, result.Length, "Digest sizes do not match");

			for (int i = 0; i < result.Length; i++) {
				Assert.AreEqual(
					expectedOutput[i],
					result[i],
					string.Format("{0}({1}) on {2} should have produced {3} but instead got {4}", hmac, Conversions.ByteToHexadecimal(key), Conversions.ByteToHexadecimal(input), Conversions.ByteToHexadecimal(expectedOutput), Conversions.ByteToHexadecimal(result))
				);
			}
		}
	}
}
