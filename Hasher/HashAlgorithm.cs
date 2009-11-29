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
 * Portions created by the Initial Developer are Copyright (C) 2004 the Initial
 * Developer. All Rights Reserved.
 *
 * Contributor(s):
 *		Jason Simeone (jay@classless.net)
 * 
 * ***** END LICENSE BLOCK ***** */
#endregion

using System;
using Classless.Hasher.Utilities;

namespace Classless.Hasher {
	/// <summary>Represents the base class from which all implementations of cryptographic hash algorithms must derive.</summary>
	abstract public class HashAlgorithm : System.Security.Cryptography.HashAlgorithm {
		/// <summary>Initializes the algorithm.</summary>
		/// <remarks>If this function is overriden in a derived class, the new function should call back to
		/// this function or you could risk garbage being carried over from one calculation to the next.</remarks>
		override public void Initialize() {
			State = 0;
		}


		/// <summary>Returns a String that represents the current Object.</summary>
		/// <returns>A String that represents the current Object.</returns>
		override public string ToString() {
			IParametrizedHashAlgorithm ipha = this as IParametrizedHashAlgorithm;
			if (ipha != null) {
				return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}({1})", base.ToString(), ipha.Parameters.ToString());
			} else {
				return base.ToString();
			}
		}


		/// <summary>Creates an instance of the default implementation of HashAlgorithm.</summary>
		/// <returns>An instance of a cryptographic object to perform the hash algorithm.</returns>
		/// <remarks>The default implementation of HashAlgorithm is SHA1.</remarks>
		new public static System.Security.Cryptography.HashAlgorithm Create() {
			return HashAlgorithm.Create(typeof(HashAlgorithm).FullName);
		}

		/// <summary>Creates an instance of the specified implementation of HashAlgorithm.</summary>
		/// <param name="hashName">The implementation of HashAlgorithm to create.</param>
		/// <returns>An instance of a cryptographic object to perform the hash algorithm.</returns>
		new public static System.Security.Cryptography.HashAlgorithm Create(string hashName) {
			switch (hashName) {
				// Lookup by fully qualified class name.
				case "Classless.Hasher.Adler32": return new Adler32();
				case "Classless.Hasher.Cksum": return new Cksum();
				case "Classless.Hasher.CRC": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32));
				case "Classless.Hasher.ELFHash": return new ELFHash();
				case "Classless.Hasher.FCS16": return new FCS16();
				case "Classless.Hasher.FCS32": return new FCS32();
				case "Classless.Hasher.FNV": return new FNV(FNVParameters.GetParameters(FNVStandard.FNV1A_32));
				case "Classless.Hasher.GHash": return new GHash(GHashParameters.GetParameters(GHashStandard.GHash_5));
				case "Classless.Hasher.GOSTHash": return new GOSTHash();
				case "Classless.Hasher.HAVAL": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_256));
				case "Classless.Hasher.JenkinsHash": return new JenkinsHash();
				case "Classless.Hasher.MD2": return new MD2();
				case "Classless.Hasher.MD4": return new MD4();
				case "Classless.Hasher.MD5": return new MD5();
				case "Classless.Hasher.Panama": return new Panama();
				case "Classless.Hasher.RIPEMD128": return new RIPEMD128();
				case "Classless.Hasher.RIPEMD160": return new RIPEMD160();
				case "Classless.Hasher.RIPEMD256": return new RIPEMD256();
				case "Classless.Hasher.RIPEMD320": return new RIPEMD320();
				case "Classless.Hasher.SHA0": return new SHA0();
				case "Classless.Hasher.SHA1": return new SHA1();
				case "Classless.Hasher.SHA224": return new SHA224();
				case "Classless.Hasher.SHA256": return new SHA256();
				case "Classless.Hasher.SHA384": return new SHA384();
				case "Classless.Hasher.SHA512": return new SHA512();
				case "Classless.Hasher.Snefru2": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_256));
				case "Classless.Hasher.Sum": return new Sum(SumParameters.GetParameters(SumStandard.SUM32));
				case "Classless.Hasher.SumBSD": return new SumBSD();
				case "Classless.Hasher.SumSysV": return new SumSysV();
				case "Classless.Hasher.Tiger": return new Tiger();
				case "Classless.Hasher.Whirlpool": return new Whirlpool();
				case "Classless.Hasher.XOR8": return new XOR8();
				case "Classless.Hasher.XUM32": return new XUM32();
				case "Classless.Hasher.Utilities.MultiHash": return new MultiHash();
				case "Classless.Hasher.Utilities.ParallelHash": return new ParallelHash();
				case "Classless.Hasher.HashAlgorithm": return new SHA1();

				// Look up by class name.
				case "Adler32": return new Adler32();
				case "Cksum": return new Cksum();
				case "CRC": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32));
				case "ELFHash": return new ELFHash();
				case "FCS16": return new FCS16();
				case "FCS32": return new FCS32();
				case "FNV": return new FNV(FNVParameters.GetParameters(FNVStandard.FNV1A_32));
				case "GHash": return new GHash(GHashParameters.GetParameters(GHashStandard.GHash_5));
				case "GOSTHash": return new GOSTHash();
				case "HAVAL": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_256));
				case "JenkinsHash": return new JenkinsHash();
				case "MD2": return new MD2();
				case "MD4": return new MD4();
				case "MD5": return new MD5();
				case "Panama": return new Panama();
				case "RIPEMD128": return new RIPEMD128();
				case "RIPEMD160": return new RIPEMD160();
				case "RIPEMD256": return new RIPEMD256();
				case "RIPEMD320": return new RIPEMD320();
				case "SHA0": return new SHA0();
				case "SHA1": return new SHA1();
				case "SHA224": return new SHA224();
				case "SHA256": return new SHA256();
				case "SHA384": return new SHA384();
				case "SHA512": return new SHA512();
				case "Snefru2": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_256));
				case "Sum": return new Sum(SumParameters.GetParameters(SumStandard.SUM32));
				case "SumBSD": return new SumBSD();
				case "SumSysV": return new SumSysV();
				case "Tiger": return new Tiger();
				case "Whirlpool": return new Whirlpool();
				case "XOR8": return new XOR8();
				case "XUM32": return new XUM32();
				case "MultiHash": return new MultiHash();
				case "ParallelHash": return new ParallelHash();
				case "HashAlgorithm": return new SHA1();

				// Look up by alternate names.
				case "Adler-32": return new Adler32();
				case "ELF": return new ELFHash();
				case "FCS-16": return new FCS16();
				case "FCS-32": return new FCS32();
				case "GOST": return new GOSTHash();
				case "JHash": return new JenkinsHash();
				case "RIPEMD-128": return new RIPEMD128();
				case "RIPEMD-160": return new RIPEMD160();
				case "RIPEMD-256": return new RIPEMD256();
				case "RIPEMD-320": return new RIPEMD320();
				case "SHA": return new SHA1();
				case "SHA-224": return new SHA224();
				case "SHA-256": return new SHA256();
				case "SHA-384": return new SHA384();
				case "SHA-512": return new SHA512();
				case "Sum-BSD": return new SumBSD();
				case "Sum-SysV": return new SumSysV();
				case "XOR-8": return new XOR8();
				case "XUM-32": return new XUM32();

				// Look up names for paramatized classes.
				case "CRC8": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC8));
				case "CRC-8": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC8));
				case "CRC8-ICODE": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC8_ICODE));
				case "CRC-8-ICODE": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC8_ICODE));
				case "CRC8-ITU": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC8_ITU));
				case "CRC-8-ITU": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC8_ITU));
				case "CRC8-MAXIM": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC8_MAXIM));
				case "CRC-8-MAXIM": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC8_MAXIM));
				case "CRC8-WCDMA": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC8_WCDMA));
				case "CRC-8-WCDMA": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC8_WCDMA));
				case "CRC16": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16));
				case "CRC-16": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16));
				case "CRC16-ARC": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_ARC));
				case "CRC-16-ARC": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_ARC));
				case "CRC16-IBM": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_IBM));
				case "CRC-16-IBM": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_IBM));
				case "CRC16-LHA": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_LHA));
				case "CRC-16-LHA": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_LHA));
				case "CRC16-CCITT": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_CCITT));
				case "CRC-16-CCITT": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_CCITT));
				case "CRC16-KERMIT": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_KERMIT));
				case "CRC-16-KERMIT": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_KERMIT));
				case "CRC16-CCITT-FALSE": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_CCITT_FALSE));
				case "CRC-16-CCITT-FALSE": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_CCITT_FALSE));
				case "CRC16-MAXIM": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_MAXIM));
				case "CRC-16-MAXIM": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_MAXIM));
				case "CRC16-USB": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_USB));
				case "CRC-16-USB": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_USB));
				case "CRC16-X25": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_X25));
				case "CRC-16-X25": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_X25));
				case "CRC16-XMODEM": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_XMODEM));
				case "CRC-16-XMODEM": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_XMODEM));
				case "CRC16-ZMODEM": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_ZMODEM));
				case "CRC-16-ZMODEM": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_ZMODEM));
				case "CRC16-XKERMIT": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_XKERMIT));
				case "CRC-16-XKERMIT": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC16_XKERMIT));
				case "CRC24": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC24));
				case "CRC-24": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC24));
				case "CRC24-OPENPGP": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC24_OPENPGP));
				case "CRC-24-OPENPGP": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC24_OPENPGP));
				case "CRC32": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32));
				case "CRC-32": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32));
				case "CRC32-PKZIP": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_PKZIP));
				case "CRC-32-PKZIP": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_PKZIP));
				case "CRC32-ITU": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_ITU));
				case "CRC-32-ITU": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_ITU));
				case "CRC32-BZIP2": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_BZIP2));
				case "CRC-32-BZIP2": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_BZIP2));
				case "CRC32-ISCSI": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_ISCSI));
				case "CRC-32-ISCSI": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_ISCSI));
				case "CRC32-JAM": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_JAM));
				case "CRC-32-JAM": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_JAM));
				case "CRC32-POSIX": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_POSIX));
				case "CRC-32-POSIX": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_POSIX));
				case "CRC32-CKSUM": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_CKSUM));
				case "CRC-32-CKSUM": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_CKSUM));
				case "CRC32-MPEG2": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_MPEG2));
				case "CRC-32-MPEG2": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC32_MPEG2));
				case "CRC64": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC64));
				case "CRC-64": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC64));
				case "CRC64-WE": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC64_WE));
				case "CRC-64-WE": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC64_WE));
				case "CRC64-ISO": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC64_ISO));
				case "CRC-64-ISO": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC64_ISO));
				case "CRC64-JONES": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC64_JONES));
				case "CRC-64-JONES": return new CRC(CRCParameters.GetParameters(CRCStandard.CRC64_JONES));
				case "FNV32": return new FNV(FNVParameters.GetParameters(FNVStandard.FNV1A_32));
				case "FNV-32": return new FNV(FNVParameters.GetParameters(FNVStandard.FNV1A_32));
				case "FNV64": return new FNV(FNVParameters.GetParameters(FNVStandard.FNV1A_64));
				case "FNV-64": return new FNV(FNVParameters.GetParameters(FNVStandard.FNV1A_64));
				case "FNV-0-32": return new FNV(FNVParameters.GetParameters(FNVStandard.FNV0_32));
				case "FNV-0-64": return new FNV(FNVParameters.GetParameters(FNVStandard.FNV0_64));
				case "FNV-1-32": return new FNV(FNVParameters.GetParameters(FNVStandard.FNV1_32));
				case "FNV-1-64": return new FNV(FNVParameters.GetParameters(FNVStandard.FNV1_64));
				case "FNV-1A-32": return new FNV(FNVParameters.GetParameters(FNVStandard.FNV1A_32));
				case "FNV-1A-64": return new FNV(FNVParameters.GetParameters(FNVStandard.FNV1A_64));
				case "GHash3": return new GHash(GHashParameters.GetParameters(GHashStandard.GHash_3));
				case "GHash-3": return new GHash(GHashParameters.GetParameters(GHashStandard.GHash_3));
				case "GHash5": return new GHash(GHashParameters.GetParameters(GHashStandard.GHash_5));
				case "GHash-5": return new GHash(GHashParameters.GetParameters(GHashStandard.GHash_5));
				case "HAVAL128": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_128));
				case "HAVAL-128": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_128));
				case "HAVAL160": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_160));
				case "HAVAL-160": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_160));
				case "HAVAL192": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_192));
				case "HAVAL-192": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_192));
				case "HAVAL224": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_224));
				case "HAVAL-224": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_224));
				case "HAVAL256": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_256));
				case "HAVAL-256": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_256));
				case "HAVAL-3-128": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_3_128));
				case "HAVAL-3-160": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_3_160));
				case "HAVAL-3-192": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_3_192));
				case "HAVAL-3-224": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_3_224));
				case "HAVAL-3-256": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_3_256));
				case "HAVAL-4-128": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_4_128));
				case "HAVAL-4-160": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_4_160));
				case "HAVAL-4-192": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_4_192));
				case "HAVAL-4-224": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_4_224));
				case "HAVAL-4-256": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_4_256));
				case "HAVAL-5-128": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_128));
				case "HAVAL-5-160": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_160));
				case "HAVAL-5-192": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_192));
				case "HAVAL-5-224": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_224));
				case "HAVAL-5-256": return new HAVAL(HAVALParameters.GetParameters(HAVALStandard.HAVAL_5_256));
				case "Snefru2128": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_128));
				case "Snefru2-128": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_128));
				case "Snefru2256": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_256));
				case "Snefru2-256": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_256));
				case "Snefru2-4-128": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_4_128));
				case "Snefru2-4-256": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_4_256));
				case "Snefru2-8-128": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_128));
				case "Snefru2-8-256": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru2_8_256));
				case "Sum8": return new Sum(SumParameters.GetParameters(SumStandard.SUM8));
				case "Sum-8": return new Sum(SumParameters.GetParameters(SumStandard.SUM8));
				case "Sum16": return new Sum(SumParameters.GetParameters(SumStandard.SUM16));
				case "Sum-16": return new Sum(SumParameters.GetParameters(SumStandard.SUM16));
				case "Sum24": return new Sum(SumParameters.GetParameters(SumStandard.SUM24));
				case "Sum-24": return new Sum(SumParameters.GetParameters(SumStandard.SUM24));
				case "Sum32": return new Sum(SumParameters.GetParameters(SumStandard.SUM32));
				case "Sum-32": return new Sum(SumParameters.GetParameters(SumStandard.SUM32));
				case "Sum64": return new Sum(SumParameters.GetParameters(SumStandard.SUM64));
				case "Sum-64": return new Sum(SumParameters.GetParameters(SumStandard.SUM64));

				// When in doubt, let the host framework try to figure it out.
				default: return System.Security.Cryptography.HashAlgorithm.Create(hashName);
			}
		}
	}
}
