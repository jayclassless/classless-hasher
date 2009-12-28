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
using System.Reflection;
using NUnit.Framework;

namespace Classless.Hasher.Tests {
	[TestFixture]
	public class HashAlgorithmTests {
		[Test]
		public void DefaultCreateTest() {
			Assert.IsInstanceOf(typeof(Sha1), HashAlgorithm.Create());
		}


		static public bool IsDescendant(Type type, Type ancestor) {
			if (type.BaseType == null) {
				return false;
			} else if (type.BaseType == ancestor) {
				return true;
			} else {
				return IsDescendant(type.BaseType, ancestor);
			}
		}


		static public IEnumerable HashCreateNames {
			get {
				Assembly classless = Assembly.GetAssembly(typeof(HashAlgorithm));
				foreach (Type type in classless.GetTypes()) {
					if ((!type.IsAbstract) && (IsDescendant(type, typeof(HashAlgorithm))) && (!type.FullName.Contains(".Methods."))) {
						yield return new TestCaseData(type.FullName, type);
						yield return new TestCaseData(type.Name, type);
					}
				}
			}
		}

		[Test, TestCaseSource("HashCreateNames")]
		public void CreateTest(string name, Type expectedType) {
			Assert.IsInstanceOf(expectedType, HashAlgorithm.Create(name));
		}


		static public object[] GarbageCreateNames = {
			new object[] { "Blah" },
			new object[] { "System.Int32" },
			new object[] { "Classless.Hasher.Something" },
			new object[] { "System.Collections.ArrayList" },
		};

		[Test, TestCaseSource("GarbageCreateNames")]
		public void GarbageCreateTest(string name) {
			Assert.IsNull(HashAlgorithm.Create(name));
		}


		[Test, TestCaseSource("HashCreateNames")]
		public void ReusableTest(string name, Type expectedType) {
			System.Security.Cryptography.HashAlgorithm hashRepeater = HashAlgorithm.Create(name);
			System.Security.Cryptography.HashAlgorithm hashFresh = null;

			for (int i = 0; i < TestVectors.Battery.All.Length - 1; i++) {
				hashFresh = HashAlgorithm.Create(name);
				CustomAssert.AreEqual(hashFresh.ComputeHash(TestVectors.Battery.All[i]), hashRepeater.ComputeHash(TestVectors.Battery.All[i]));
			}
		}


		[Test, TestCaseSource("HashCreateNames")]
		public void ReusableInputArrayTest(string name, Type expectedType) {
			System.Security.Cryptography.HashAlgorithm hashRepeater = HashAlgorithm.Create(name);
			System.Security.Cryptography.HashAlgorithm hashFresh = null;
			byte[] input = (byte[])TestVectors.Battery.NumericRepeated.Clone();

			for (int i = 0; i < input.Length; i += 10) {
				hashFresh = HashAlgorithm.Create(name);
				CustomAssert.AreEqual(hashFresh.ComputeHash(TestVectors.Battery.Numeric), hashRepeater.ComputeHash(input, i, 10));
			}
		}


		[Test, TestCaseSource("HashCreateNames")]
		public void EnsureInputUnchangedTest(string name, Type expectedType) {
			System.Security.Cryptography.HashAlgorithm hasher = HashAlgorithm.Create(name);
			byte[] originalInput = (byte[])TestVectors.Battery.NumericRepeated.Clone();
			byte[] input = (byte[])TestVectors.Battery.NumericRepeated.Clone();

			byte[] output = hasher.ComputeHash(input);
			CustomAssert.AreEqual(originalInput, input);
		}


		static public object[] HashCreateDefaultedParametrizedNames = {
			new object[] { "Crc", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32Bit) },
			new object[] { "Classless.Hasher.Crc", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32Bit) },
			new object[] { "Fletcher", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.Fletcher32Bit) },
			new object[] { "Classless.Hasher.Fletcher", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.Fletcher32Bit) },
			new object[] { "Fnv", typeof(Fnv), FnvParameters.GetParameters(FnvStandard.Fnv32BitType1A) },
			new object[] { "Classless.Hasher.Fnv", typeof(Fnv), FnvParameters.GetParameters(FnvStandard.Fnv32BitType1A) },
			new object[] { "GHash", typeof(GHash), GHashParameters.GetParameters(GHashStandard.GHash5) },
			new object[] { "Classless.Hasher.GHash", typeof(GHash), GHashParameters.GetParameters(GHashStandard.GHash5) },
			new object[] { "Haval", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval256Bit5Pass) },
			new object[] { "Classless.Hasher.Haval", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval256Bit5Pass) },
			new object[] { "Snefru2", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru256Bit8Pass) },
			new object[] { "Classless.Hasher.Snefru2", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru256Bit8Pass) },
			new object[] { "Sum", typeof(Sum), SumParameters.GetParameters(SumStandard.Sum32Bit) },
			new object[] { "Classless.Hasher.Sum", typeof(Sum), SumParameters.GetParameters(SumStandard.Sum32Bit) },
			new object[] { "Tiger", typeof(Tiger), TigerParameters.GetParameters(TigerStandard.Tiger192BitVersion1) },
			new object[] { "Classless.Hasher.Tiger", typeof(Tiger), TigerParameters.GetParameters(TigerStandard.Tiger192BitVersion1) },
		};

		[Test, TestCaseSource("HashCreateDefaultedParametrizedNames")]
		public void CreateParametrizedTest(string name, Type expectedType, HashAlgorithmParameters expectedParameters) {
			System.Security.Cryptography.HashAlgorithm hasher = HashAlgorithm.Create(name);
			Assert.IsInstanceOf(expectedType, hasher);
			Assert.AreEqual(((IParametrizedHashAlgorithm)hasher).Parameters, expectedParameters);
		}


		static public object[] HashCreateFrameworkDefaultNames = {
			new object[] { "System.Security.Cryptography.SHA1", typeof(System.Security.Cryptography.SHA1) },
			new object[] { "System.Security.Cryptography.HashAlgorithm", typeof(System.Security.Cryptography.SHA1) },
			new object[] { "System.Security.Cryptography.MD5", typeof(System.Security.Cryptography.MD5) },
			new object[] { "System.Security.Cryptography.SHA256", typeof(System.Security.Cryptography.SHA256) },
			new object[] { "System.Security.Cryptography.SHA384", typeof(System.Security.Cryptography.SHA384) },
			new object[] { "System.Security.Cryptography.SHA512", typeof(System.Security.Cryptography.SHA512) },
		};

		[Test, TestCaseSource("HashCreateFrameworkDefaultNames")]
		public void CreateDefaultFrameworkTest(string name, Type expectedType) {
			Assert.IsInstanceOf(expectedType, HashAlgorithm.Create(name));
		}
	}
}
