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
			Assert.IsInstanceOf(typeof(SHA1), HashAlgorithm.Create());
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


		static public object[] HashCreateParametrizedNames = {
			new object[] { "CRC8", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC8) },
			new object[] { "CRC-8", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC8) },
			new object[] { "CRC8-ICODE", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC8_ICODE) },
			new object[] { "CRC-8-ICODE", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC8_ICODE) },
			new object[] { "CRC8-ITU", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC8_ITU) },
			new object[] { "CRC-8-ITU", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC8_ITU) },
			new object[] { "CRC8-MAXIM", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC8_MAXIM) },
			new object[] { "CRC-8-MAXIM", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC8_MAXIM) },
			new object[] { "CRC8-WCDMA", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC8_WCDMA) },
			new object[] { "CRC-8-WCDMA", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC8_WCDMA) },
			new object[] { "CRC16", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16) },
			new object[] { "CRC-16", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16) },
			new object[] { "CRC16-ARC", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_ARC) },
			new object[] { "CRC-16-ARC", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_ARC) },
			new object[] { "CRC16-IBM", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_IBM) },
			new object[] { "CRC-16-IBM", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_IBM) },
			new object[] { "CRC16-LHA", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_LHA) },
			new object[] { "CRC-16-LHA", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_LHA) },
			new object[] { "CRC16-CCITT", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_CCITT) },
			new object[] { "CRC-16-CCITT", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_CCITT) },
			new object[] { "CRC16-KERMIT", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_KERMIT) },
			new object[] { "CRC-16-KERMIT", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_KERMIT) },
			new object[] { "CRC16-CCITT-FALSE", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_CCITT_FALSE) },
			new object[] { "CRC-16-CCITT-FALSE", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_CCITT_FALSE) },
			new object[] { "CRC16-MAXIM", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_MAXIM) },
			new object[] { "CRC-16-MAXIM", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_MAXIM) },
			new object[] { "CRC16-USB", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_USB) },
			new object[] { "CRC-16-USB", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_USB) },
			new object[] { "CRC16-X25", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_X25) },
			new object[] { "CRC-16-X25", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_X25) },
			new object[] { "CRC16-XMODEM", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_XMODEM) },
			new object[] { "CRC-16-XMODEM", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_XMODEM) },
			new object[] { "CRC16-ZMODEM", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_ZMODEM) },
			new object[] { "CRC-16-ZMODEM", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_ZMODEM) },
			new object[] { "CRC16-XKERMIT", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_XKERMIT) },
			new object[] { "CRC-16-XKERMIT", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC16_XKERMIT) },
			new object[] { "CRC24", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC24) },
			new object[] { "CRC-24", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC24) },
			new object[] { "CRC24-OPENPGP", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC24_OPENPGP) },
			new object[] { "CRC-24-OPENPGP", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC24_OPENPGP) },
			new object[] { "CRC32", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32) },
			new object[] { "CRC-32", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32) },
			new object[] { "CRC32-PKZIP", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_PKZIP) },
			new object[] { "CRC-32-PKZIP", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_PKZIP) },
			new object[] { "CRC32-ITU", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_ITU) },
			new object[] { "CRC-32-ITU", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_ITU) },
			new object[] { "CRC32-BZIP2", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_BZIP2) },
			new object[] { "CRC-32-BZIP2", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_BZIP2) },
			new object[] { "CRC32-ISCSI", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_ISCSI) },
			new object[] { "CRC-32-ISCSI", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_ISCSI) },
			new object[] { "CRC32-JAM", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_JAM) },
			new object[] { "CRC-32-JAM", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_JAM) },
			new object[] { "CRC32-POSIX", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_POSIX) },
			new object[] { "CRC-32-POSIX", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_POSIX) },
			new object[] { "CRC32-CKSUM", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_CKSUM) },
			new object[] { "CRC-32-CKSUM", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_CKSUM) },
			new object[] { "CRC32-MPEG2", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_MPEG2) },
			new object[] { "CRC-32-MPEG2", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32_MPEG2) },
			new object[] { "CRC64", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC64) },
			new object[] { "CRC-64", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC64) },
			new object[] { "CRC64-WE", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC64_WE) },
			new object[] { "CRC-64-WE", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC64_WE) },
			new object[] { "CRC64-ISO", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC64_ISO) },
			new object[] { "CRC-64-ISO", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC64_ISO) },
			new object[] { "CRC64-JONES", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC64_JONES) },
			new object[] { "CRC-64-JONES", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC64_JONES) },
			new object[] { "Fletcher32", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.FLETCHER32) },
			new object[] { "Fletcher-32", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.FLETCHER32) },
			new object[] { "Fletcher16", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.FLETCHER16) },
			new object[] { "Fletcher-16", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.FLETCHER16) },
			new object[] { "Fletcher8", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.FLETCHER8) },
			new object[] { "Fletcher-8", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.FLETCHER8) },
			new object[] { "FNV32", typeof(FNV), FNVParameters.GetParameters(FNVStandard.FNV1A_32) },
			new object[] { "FNV-32", typeof(FNV), FNVParameters.GetParameters(FNVStandard.FNV1A_32) },
			new object[] { "FNV64", typeof(FNV), FNVParameters.GetParameters(FNVStandard.FNV1A_64) },
			new object[] { "FNV-64", typeof(FNV), FNVParameters.GetParameters(FNVStandard.FNV1A_64) },
			new object[] { "FNV-0-32", typeof(FNV), FNVParameters.GetParameters(FNVStandard.FNV0_32) },
			new object[] { "FNV-0-64", typeof(FNV), FNVParameters.GetParameters(FNVStandard.FNV0_64) },
			new object[] { "FNV-1-32", typeof(FNV), FNVParameters.GetParameters(FNVStandard.FNV1_32) },
			new object[] { "FNV-1-64", typeof(FNV), FNVParameters.GetParameters(FNVStandard.FNV1_64) },
			new object[] { "FNV-1A-32", typeof(FNV), FNVParameters.GetParameters(FNVStandard.FNV1A_32) },
			new object[] { "FNV-1A-64", typeof(FNV), FNVParameters.GetParameters(FNVStandard.FNV1A_64) },
			new object[] { "GHash3", typeof(GHash), GHashParameters.GetParameters(GHashStandard.GHash_3) },
			new object[] { "GHash-3", typeof(GHash), GHashParameters.GetParameters(GHashStandard.GHash_3) },
			new object[] { "GHash5", typeof(GHash), GHashParameters.GetParameters(GHashStandard.GHash_5) },
			new object[] { "GHash-5", typeof(GHash), GHashParameters.GetParameters(GHashStandard.GHash_5) },
			new object[] { "HAVAL128", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_128) },
			new object[] { "HAVAL-128", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_128) },
			new object[] { "HAVAL160", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_160) },
			new object[] { "HAVAL-160", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_160) },
			new object[] { "HAVAL192", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_192) },
			new object[] { "HAVAL-192", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_192) },
			new object[] { "HAVAL224", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_224) },
			new object[] { "HAVAL-224", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_224) },
			new object[] { "HAVAL256", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_256) },
			new object[] { "HAVAL-256", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_256) },
			new object[] { "HAVAL-3-128", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_3_128) },
			new object[] { "HAVAL-3-160", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_3_160) },
			new object[] { "HAVAL-3-192", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_3_192) },
			new object[] { "HAVAL-3-224", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_3_224) },
			new object[] { "HAVAL-3-256", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_3_256) },
			new object[] { "HAVAL-4-128", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_4_128) },
			new object[] { "HAVAL-4-160", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_4_160) },
			new object[] { "HAVAL-4-192", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_4_192) },
			new object[] { "HAVAL-4-224", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_4_224) },
			new object[] { "HAVAL-4-256", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_4_256) },
			new object[] { "HAVAL-5-128", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_128) },
			new object[] { "HAVAL-5-160", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_160) },
			new object[] { "HAVAL-5-192", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_192) },
			new object[] { "HAVAL-5-224", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_224) },
			new object[] { "HAVAL-5-256", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_256) },
			new object[] { "Snefru2128", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_128) },
			new object[] { "Snefru2-128", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_128) },
			new object[] { "Snefru2256", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_256) },
			new object[] { "Snefru2-256", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_256) },
			new object[] { "Snefru2-4-128", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_4_128) },
			new object[] { "Snefru2-4-256", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_4_256) },
			new object[] { "Snefru2-8-128", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_128) },
			new object[] { "Snefru2-8-256", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_256) },
			new object[] { "Sum8", typeof(Sum), SumParameters.GetParameters(SumStandard.SUM8) },
			new object[] { "Sum-8", typeof(Sum), SumParameters.GetParameters(SumStandard.SUM8) },
			new object[] { "Sum16", typeof(Sum), SumParameters.GetParameters(SumStandard.SUM16) },
			new object[] { "Sum-16", typeof(Sum), SumParameters.GetParameters(SumStandard.SUM16) },
			new object[] { "Sum24", typeof(Sum), SumParameters.GetParameters(SumStandard.SUM24) },
			new object[] { "Sum-24", typeof(Sum), SumParameters.GetParameters(SumStandard.SUM24) },
			new object[] { "Sum32", typeof(Sum), SumParameters.GetParameters(SumStandard.SUM32) },
			new object[] { "Sum-32", typeof(Sum), SumParameters.GetParameters(SumStandard.SUM32) },
			new object[] { "Sum64", typeof(Sum), SumParameters.GetParameters(SumStandard.SUM64) },
			new object[] { "Sum-64", typeof(Sum), SumParameters.GetParameters(SumStandard.SUM64) },
		};

		static public object[] HashCreateDefaultedParametrizedNames = {
			new object[] { "CRC", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32) },
			new object[] { "Classless.Hasher.CRC", typeof(CRC), CRCParameters.GetParameters(CRCStandard.CRC32) },
			new object[] { "Fletcher", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.FLETCHER32) },
			new object[] { "Classless.Hasher.Fletcher", typeof(Fletcher), FletcherParameters.GetParameters(FletcherStandard.FLETCHER32) },
			new object[] { "FNV", typeof(FNV), FNVParameters.GetParameters(FNVStandard.FNV1A_32) },
			new object[] { "Classless.Hasher.FNV", typeof(FNV), FNVParameters.GetParameters(FNVStandard.FNV1A_32) },
			new object[] { "GHash", typeof(GHash), GHashParameters.GetParameters(GHashStandard.GHash_5) },
			new object[] { "Classless.Hasher.GHash", typeof(GHash), GHashParameters.GetParameters(GHashStandard.GHash_5) },
			new object[] { "HAVAL", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_256) },
			new object[] { "Classless.Hasher.HAVAL", typeof(HAVAL), HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_256) },
			new object[] { "Snefru2", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_256) },
			new object[] { "Classless.Hasher.Snefru2", typeof(Snefru2), Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_256) },
			new object[] { "Sum", typeof(Sum), SumParameters.GetParameters(SumStandard.SUM32) },
			new object[] { "Classless.Hasher.Sum", typeof(Sum), SumParameters.GetParameters(SumStandard.SUM32) },
		};

		[Test, TestCaseSource("HashCreateParametrizedNames"), TestCaseSource("HashCreateDefaultedParametrizedNames")]
		public void CreateParametrizedTest(string name, Type expectedType, HashAlgorithmParameters expectedParameters) {
			System.Security.Cryptography.HashAlgorithm hasher = HashAlgorithm.Create(name);
			Assert.IsInstanceOf(expectedType, hasher);
			Assert.AreEqual(((IParametrizedHashAlgorithm)hasher).Parameters, expectedParameters);
		}

		
		static public object[] HashCreateAlternateNames = {
			new object[] { "Adler-32", typeof(Adler32) },
			new object[] { "ELF", typeof(ELFHash) },
			new object[] { "FCS-16", typeof(FCS16) },
			new object[] { "FCS-32", typeof(FCS32) },
			new object[] { "GOST", typeof(GOSTHash) },
			new object[] { "JHash", typeof(JenkinsHash) },
			new object[] { "RIPEMD-128", typeof(RIPEMD128) },
			new object[] { "RIPEMD-160", typeof(RIPEMD160) },
			new object[] { "RIPEMD-256", typeof(RIPEMD256) },
			new object[] { "RIPEMD-320", typeof(RIPEMD320) },
			new object[] { "Classless.Hasher.HashAlgorithm", typeof(SHA1) },
			new object[] { "HashAlgorithm", typeof(SHA1) },
			new object[] { "SHA", typeof(SHA1) },
			new object[] { "SHA-224", typeof(SHA224) },
			new object[] { "SHA-256", typeof(SHA256) },
			new object[] { "SHA-384", typeof(SHA384) },
			new object[] { "SHA-512", typeof(SHA512) },
			new object[] { "Sum-BSD", typeof(SumBSD) },
			new object[] { "Sum-SysV", typeof(SumSysV) },
			new object[] { "XOR-8", typeof(XOR8) },
			new object[] { "XUM-32", typeof(XUM32) },
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
