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
	class ParallelHashTests {
		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void ConstructorNullTest() {
			HashAlgorithmCollection hashCollection = null;
			ParallelHash hasher = new ParallelHash(hashCollection);
		}


		[Test]
		public void ConstructorCollectionTest() {
			HashAlgorithmCollection hashCollection = new HashAlgorithmCollection();
			MD5 md5 = new MD5();
			hashCollection.Add(md5);
			SHA1 sha1 = new SHA1();
			hashCollection.Add(sha1);
			XOR8 xor8 = new XOR8();
			hashCollection.Add(xor8);
			ParallelHash hasher = new ParallelHash(hashCollection);
			Assert.Contains(md5, hasher.HashAlgorithms);
			Assert.Contains(sha1, hasher.HashAlgorithms);
			Assert.Contains(xor8, hasher.HashAlgorithms);
		}


		[Test]
		public void ChangeHasherGoodTest() {
			ParallelHash hasher = new ParallelHash(new MD5(), new SHA1());
			hasher.HashAlgorithms.Add(new RIPEMD128());
			hasher.TransformBlock(TestVectors.s("1234567890"), 0, 10, null, 0);
			hasher.TransformFinalBlock(new byte[1], 0, 0);
		}

	
		[Test, ExpectedException(typeof(System.Security.Cryptography.CryptographicException))]
		public void ChangeHasherBadTest() {
			ParallelHash hasher = new ParallelHash(new MD5(), new SHA1());
			hasher.TransformBlock(TestVectors.s("1234567890"), 0, 10, null, 0);
			hasher.HashAlgorithms.Add(new RIPEMD128());
			hasher.TransformFinalBlock(new byte[1], 0, 0);
		}


		[Test, ExpectedException(typeof(System.Security.Cryptography.CryptographicException))]
		public void ChangeHasherBadOrderTest() {
			ParallelHash hasher = new ParallelHash(new MD5(), new SHA1());
			hasher.TransformBlock(TestVectors.s("1234567890"), 0, 10, null, 0);
			hasher.HashAlgorithms.Reverse();
			hasher.TransformFinalBlock(new byte[1], 0, 0);
		}
	}
}
