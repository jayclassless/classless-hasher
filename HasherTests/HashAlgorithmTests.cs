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
					if ((!type.IsAbstract) && (IsDescendant(type, typeof(HashAlgorithm)))) {
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


		static public object[] HashCreateParametrizedNames = {
			new object[] { "Crc8", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc8Bit) },
			new object[] { "Crc-8", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc8Bit) },
			new object[] { "Crc8-Icode", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc8BitIcode) },
			new object[] { "Crc-8-Icode", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc8BitIcode) },
			new object[] { "Crc8-Itu", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc8BitItu) },
			new object[] { "Crc-8-Itu", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc8BitItu) },
			new object[] { "Crc8-Maxim", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc8BitMaxim) },
			new object[] { "Crc-8-Maxim", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc8BitMaxim) },
			new object[] { "Crc8-Wcdma", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc8BitWcdma) },
			new object[] { "Crc-8-Wcdma", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc8BitWcdma) },
			new object[] { "Crc16", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16Bit) },
			new object[] { "Crc-16", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16Bit) },
			new object[] { "Crc16-Arc", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitArc) },
			new object[] { "Crc-16-Arc", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitArc) },
			new object[] { "Crc16-Ibm", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitIbm) },
			new object[] { "Crc-16-Ibm", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitIbm) },
			new object[] { "Crc16-Lha", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitLha) },
			new object[] { "Crc-16-Lha", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitLha) },
			new object[] { "Crc16-Ccitt", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitCcitt) },
			new object[] { "Crc-16-Ccitt", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitCcitt) },
			new object[] { "Crc16-Kermit", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitKermit) },
			new object[] { "Crc-16-Kermit", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitKermit) },
			new object[] { "Crc16-Ccitt-False", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitCcittFalse) },
			new object[] { "Crc-16-Ccitt-False", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitCcittFalse) },
			new object[] { "Crc16-Maxim", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitMaxim) },
			new object[] { "Crc-16-Maxim", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitMaxim) },
			new object[] { "Crc16-Usb", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitUsb) },
			new object[] { "Crc-16-Usb", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitUsb) },
			new object[] { "Crc16-X25", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitX25) },
			new object[] { "Crc-16-X25", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitX25) },
			new object[] { "Crc16-Xmodem", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitXmodem) },
			new object[] { "Crc-16-Xmodem", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitXmodem) },
			new object[] { "Crc16-Zmodem", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitZmodem) },
			new object[] { "Crc-16-Zmodem", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitZmodem) },
			new object[] { "Crc16-Xkermit", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitXkermit) },
			new object[] { "Crc-16-Xkermit", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc16BitXkermit) },
			new object[] { "Crc24", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc24Bit) },
			new object[] { "Crc-24", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc24Bit) },
			new object[] { "Crc24-OpenPgp", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc24BitOpenPgp) },
			new object[] { "Crc-24-OpenPgp", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc24BitOpenPgp) },
			new object[] { "Crc32", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32Bit) },
			new object[] { "Crc-32", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32Bit) },
			new object[] { "Crc32-Pkzip", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitPkzip) },
			new object[] { "Crc-32-Pkzip", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitPkzip) },
			new object[] { "Crc32-Itu", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitItu) },
			new object[] { "Crc-32-Itu", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitItu) },
			new object[] { "Crc32-Bzip2", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitBzip2) },
			new object[] { "Crc-32-Bzip2", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitBzip2) },
			new object[] { "Crc32-Iscsi", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitIscsi) },
			new object[] { "Crc-32-Iscsi", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitIscsi) },
			new object[] { "Crc32-Jam", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitJam) },
			new object[] { "Crc-32-Jam", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitJam) },
			new object[] { "Crc32-Posix", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitPosix) },
			new object[] { "Crc-32-Posix", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitPosix) },
			new object[] { "Crc32-Cksum", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitCksum) },
			new object[] { "Crc-32-Cksum", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitCksum) },
			new object[] { "Crc32-Mpeg2", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitMpeg2) },
			new object[] { "Crc-32-Mpeg2", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc32BitMpeg2) },
			new object[] { "Crc64", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc64Bit) },
			new object[] { "Crc-64", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc64Bit) },
			new object[] { "Crc64-WE", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc64BitWE) },
			new object[] { "Crc-64-WE", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc64BitWE) },
			new object[] { "Crc64-Iso", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc64BitIso) },
			new object[] { "Crc-64-Iso", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc64BitIso) },
			new object[] { "Crc64-Jones", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc64BitJones) },
			new object[] { "Crc-64-Jones", typeof(Crc), CrcParameters.GetParameters(CrcStandard.Crc64BitJones) },
			new object[] { "Fletcher32Bit", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.Fletcher32Bit) },
			new object[] { "Fletcher-32", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.Fletcher32Bit) },
			new object[] { "Fletcher16Bit", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.Fletcher16Bit) },
			new object[] { "Fletcher-16", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.Fletcher16Bit) },
			new object[] { "Fletcher8Bit", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.Fletcher8Bit) },
			new object[] { "Fletcher-8", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.Fletcher8Bit) },
			new object[] { "Fnv32", typeof(Fnv), FnvParameters.GetParameters(FnvStandard.Fnv32BitType1A) },
			new object[] { "Fnv-32", typeof(Fnv), FnvParameters.GetParameters(FnvStandard.Fnv32BitType1A) },
			new object[] { "Fnv64", typeof(Fnv), FnvParameters.GetParameters(FnvStandard.Fnv64BitType1A) },
			new object[] { "Fnv-64", typeof(Fnv), FnvParameters.GetParameters(FnvStandard.Fnv64BitType1A) },
			new object[] { "Fnv-0-32", typeof(Fnv), FnvParameters.GetParameters(FnvStandard.Fnv32BitType0) },
			new object[] { "Fnv-0-64", typeof(Fnv), FnvParameters.GetParameters(FnvStandard.Fnv64BitType0) },
			new object[] { "Fnv-1-32", typeof(Fnv), FnvParameters.GetParameters(FnvStandard.Fnv32BitType1) },
			new object[] { "Fnv-1-64", typeof(Fnv), FnvParameters.GetParameters(FnvStandard.Fnv64BitType1) },
			new object[] { "Fnv-1A-32", typeof(Fnv), FnvParameters.GetParameters(FnvStandard.Fnv32BitType1A) },
			new object[] { "Fnv-1A-64", typeof(Fnv), FnvParameters.GetParameters(FnvStandard.Fnv64BitType1A) },
			new object[] { "GHash3", typeof(GHash), GHashParameters.GetParameters(GHashStandard.GHash3) },
			new object[] { "GHash-3", typeof(GHash), GHashParameters.GetParameters(GHashStandard.GHash3) },
			new object[] { "GHash5", typeof(GHash), GHashParameters.GetParameters(GHashStandard.GHash5) },
			new object[] { "GHash-5", typeof(GHash), GHashParameters.GetParameters(GHashStandard.GHash5) },
			new object[] { "Haval128", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval128Bit5Pass) },
			new object[] { "Haval-128", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval128Bit5Pass) },
			new object[] { "Haval160", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval160Bit5Pass) },
			new object[] { "Haval-160", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval160Bit5Pass) },
			new object[] { "Haval192", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval192Bit5Pass) },
			new object[] { "Haval-192", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval192Bit5Pass) },
			new object[] { "Haval224", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval224Bit5Pass) },
			new object[] { "Haval-224", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval224Bit5Pass) },
			new object[] { "Haval256", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval256Bit5Pass) },
			new object[] { "Haval-256", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval256Bit5Pass) },
			new object[] { "Haval-3-128", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval128Bit3Pass) },
			new object[] { "Haval-3-160", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval160Bit3Pass) },
			new object[] { "Haval-3-192", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval192Bit3Pass) },
			new object[] { "Haval-3-224", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval224Bit3Pass) },
			new object[] { "Haval-3-256", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval256Bit3Pass) },
			new object[] { "Haval-4-128", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval128Bit4Pass) },
			new object[] { "Haval-4-160", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval160Bit4Pass) },
			new object[] { "Haval-4-192", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval192Bit4Pass) },
			new object[] { "Haval-4-224", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval224Bit4Pass) },
			new object[] { "Haval-4-256", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval256Bit4Pass) },
			new object[] { "Haval-5-128", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval128Bit5Pass) },
			new object[] { "Haval-5-160", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval160Bit5Pass) },
			new object[] { "Haval-5-192", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval192Bit5Pass) },
			new object[] { "Haval-5-224", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval224Bit5Pass) },
			new object[] { "Haval-5-256", typeof(Haval), HavalParameters.GetParameters(HavalStandard.Haval256Bit5Pass) },
			new object[] { "Snefru2128", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru128Bit8Pass) },
			new object[] { "Snefru2-128", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru128Bit8Pass) },
			new object[] { "Snefru2256", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru256Bit8Pass) },
			new object[] { "Snefru2-256", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru256Bit8Pass) },
			new object[] { "Snefru2-4-128", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru128Bit4Pass) },
			new object[] { "Snefru2-4-256", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru256Bit4Pass) },
			new object[] { "Snefru2-8-128", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru128Bit8Pass) },
			new object[] { "Snefru2-8-256", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru256Bit8Pass) },
			new object[] { "Sum8", typeof(Sum), SumParameters.GetParameters(SumStandard.Sum8Bit) },
			new object[] { "Sum-8", typeof(Sum), SumParameters.GetParameters(SumStandard.Sum8Bit) },
			new object[] { "Sum16", typeof(Sum), SumParameters.GetParameters(SumStandard.Sum16Bit) },
			new object[] { "Sum-16", typeof(Sum), SumParameters.GetParameters(SumStandard.Sum16Bit) },
			new object[] { "Sum24", typeof(Sum), SumParameters.GetParameters(SumStandard.Sum24Bit) },
			new object[] { "Sum-24", typeof(Sum), SumParameters.GetParameters(SumStandard.Sum24Bit) },
			new object[] { "Sum32", typeof(Sum), SumParameters.GetParameters(SumStandard.Sum32Bit) },
			new object[] { "Sum-32", typeof(Sum), SumParameters.GetParameters(SumStandard.Sum32Bit) },
			new object[] { "Sum64", typeof(Sum), SumParameters.GetParameters(SumStandard.Sum64Bit) },
			new object[] { "Sum-64", typeof(Sum), SumParameters.GetParameters(SumStandard.Sum64Bit) },
			new object[] { "Tiger128", typeof(Tiger), TigerParameters.GetParameters(TigerStandard.Tiger128Bit) },
			new object[] { "Tiger-128", typeof(Tiger), TigerParameters.GetParameters(TigerStandard.Tiger128Bit) },
			new object[] { "Tiger160", typeof(Tiger), TigerParameters.GetParameters(TigerStandard.Tiger160Bit) },
			new object[] { "Tiger-160", typeof(Tiger), TigerParameters.GetParameters(TigerStandard.Tiger160Bit) },
			new object[] { "Tiger192", typeof(Tiger), TigerParameters.GetParameters(TigerStandard.Tiger192Bit) },
			new object[] { "Tiger-192", typeof(Tiger), TigerParameters.GetParameters(TigerStandard.Tiger192Bit) },
		};

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
			new object[] { "Tiger", typeof(Tiger), TigerParameters.GetParameters(TigerStandard.Tiger192Bit) },
			new object[] { "Classless.Hasher.Tiger", typeof(Tiger), TigerParameters.GetParameters(TigerStandard.Tiger192Bit) },
		};

		[Test, TestCaseSource("HashCreateParametrizedNames"), TestCaseSource("HashCreateDefaultedParametrizedNames")]
		public void CreateParametrizedTest(string name, Type expectedType, HashAlgorithmParameters expectedParameters) {
			System.Security.Cryptography.HashAlgorithm hasher = HashAlgorithm.Create(name);
			Assert.IsInstanceOf(expectedType, hasher);
			Assert.AreEqual(((IParametrizedHashAlgorithm)hasher).Parameters, expectedParameters);
		}

		
		static public object[] HashCreateAlternateNames = {
			new object[] { "Adler-32", typeof(Adler32) },
			new object[] { "Elf", typeof(ElfHash) },
			new object[] { "Fcs-16", typeof(Fcs16) },
			new object[] { "Fcs-32", typeof(Fcs32) },
			new object[] { "Gost", typeof(GostHash) },
			new object[] { "JHash", typeof(JenkinsHash) },
			new object[] { "RipeMD-128", typeof(RipeMD128) },
			new object[] { "RipeMD-160", typeof(RipeMD160) },
			new object[] { "RipeMD-256", typeof(RipeMD256) },
			new object[] { "RipeMD-320", typeof(RipeMD320) },
			new object[] { "Classless.Hasher.HashAlgorithm", typeof(Sha1) },
			new object[] { "HashAlgorithm", typeof(Sha1) },
			new object[] { "Sha", typeof(Sha1) },
			new object[] { "Sha-224", typeof(Sha224) },
			new object[] { "Sha-256", typeof(Sha256) },
			new object[] { "Sha-384", typeof(Sha384) },
			new object[] { "Sha-512", typeof(Sha512) },
			new object[] { "Sum-Bsd", typeof(SumBsd) },
			new object[] { "Sum-SysV", typeof(SumSysV) },
			new object[] { "Xor-8", typeof(Xor8) },
			new object[] { "Xum-32", typeof(Xum32) },
		};

		[Test, TestCaseSource("HashCreateAlternateNames")]
		public void CreateAlternateTest(string name, Type expectedType) {
			Assert.IsInstanceOf(expectedType, HashAlgorithm.Create(name));
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
