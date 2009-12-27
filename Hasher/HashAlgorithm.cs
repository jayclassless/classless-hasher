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
using Classless.Hasher.Methods;
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
				case "Classless.Hasher.APHash": return new APHash();
				case "Classless.Hasher.BkdrHash": return new BkdrHash();
				case "Classless.Hasher.Cksum": return new Cksum();
				case "Classless.Hasher.Crc": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit));
				case "Classless.Hasher.Dha256": return new Dha256();
				case "Classless.Hasher.DjbHash": return new DjbHash();
				case "Classless.Hasher.ElfHash": return new ElfHash();
				case "Classless.Hasher.Fcs16": return new Fcs16();
				case "Classless.Hasher.Fcs32": return new Fcs32();
				case "Classless.Hasher.Fletcher": return new Fletcher(FletcherParameters.GetParameters(FletcherStandard.Fletcher32Bit));
				case "Classless.Hasher.Fnv": return new Fnv(FnvParameters.GetParameters(FnvStandard.Fnv32BitType1A));
				case "Classless.Hasher.GHash": return new GHash(GHashParameters.GetParameters(GHashStandard.GHash5));
				case "Classless.Hasher.GostHash": return new GostHash();
				case "Classless.Hasher.Has160": return new Has160();
				case "Classless.Hasher.Haval": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval256Bit5Pass));
				case "Classless.Hasher.JenkinsHash": return new JenkinsHash();
				case "Classless.Hasher.JSHash": return new JSHash();
				case "Classless.Hasher.MD2": return new MD2();
				case "Classless.Hasher.MD4": return new MD4();
				case "Classless.Hasher.MD5": return new MD5();
				case "Classless.Hasher.Panama": return new Panama();
				case "Classless.Hasher.Pjw32": return new Pjw32();
				case "Classless.Hasher.RipeMD128": return new RipeMD128();
				case "Classless.Hasher.RipeMD160": return new RipeMD160();
				case "Classless.Hasher.RipeMD256": return new RipeMD256();
				case "Classless.Hasher.RipeMD320": return new RipeMD320();
				case "Classless.Hasher.RSHash": return new RSHash();
				case "Classless.Hasher.SdbmHash": return new SdbmHash();
				case "Classless.Hasher.Sha0": return new Sha0();
				case "Classless.Hasher.Sha1": return new Sha1();
				case "Classless.Hasher.Sha224": return new Sha224();
				case "Classless.Hasher.Sha256": return new Sha256();
				case "Classless.Hasher.Sha384": return new Sha384();
				case "Classless.Hasher.Sha512": return new Sha512();
				case "Classless.Hasher.Snefru2": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru256Bit8Pass));
				case "Classless.Hasher.Sum": return new Sum(SumParameters.GetParameters(SumStandard.Sum32Bit));
				case "Classless.Hasher.SumBsd": return new SumBsd();
				case "Classless.Hasher.SumSysV": return new SumSysV();
				case "Classless.Hasher.Tiger": return new Tiger(TigerParameters.GetParameters(TigerStandard.Tiger192Bit));
				case "Classless.Hasher.Whirlpool": return new Whirlpool();
				case "Classless.Hasher.Xor8": return new Xor8();
				case "Classless.Hasher.Xum32": return new Xum32();
				case "Classless.Hasher.Methods.HashList": return new HashList(HashAlgorithm.Create());
				case "Classless.Hasher.Methods.HashTree": return new HashTree(new Tiger(TigerParameters.GetParameters(TigerStandard.Tiger192Bit)), 1024);
				case "Classless.Hasher.Methods.MultiHash": return new MultiHash();
				case "Classless.Hasher.Methods.ParallelHash": return new ParallelHash();
				case "Classless.Hasher.HashAlgorithm": return new Sha1();

				// Look up by class name.
				case "Adler32": return new Adler32();
				case "APHash": return new APHash();
				case "BkdrHash": return new BkdrHash();
				case "Cksum": return new Cksum();
				case "Crc": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit));
				case "Dha256": return new Dha256();
				case "DjbHash": return new DjbHash();
				case "ElfHash": return new ElfHash();
				case "Fcs16": return new Fcs16();
				case "Fcs32": return new Fcs32();
				case "Fletcher": return new Fletcher(FletcherParameters.GetParameters(FletcherStandard.Fletcher32Bit));
				case "Fnv": return new Fnv(FnvParameters.GetParameters(FnvStandard.Fnv32BitType1A));
				case "GHash": return new GHash(GHashParameters.GetParameters(GHashStandard.GHash5));
				case "GostHash": return new GostHash();
				case "Has160": return new Has160();
				case "Haval": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval256Bit5Pass));
				case "JenkinsHash": return new JenkinsHash();
				case "JSHash": return new JSHash();
				case "MD2": return new MD2();
				case "MD4": return new MD4();
				case "MD5": return new MD5();
				case "Panama": return new Panama();
				case "Pjw32": return new Pjw32();
				case "RipeMD128": return new RipeMD128();
				case "RipeMD160": return new RipeMD160();
				case "RipeMD256": return new RipeMD256();
				case "RipeMD320": return new RipeMD320();
				case "RSHash": return new RSHash();
				case "SdbmHash": return new SdbmHash();
				case "Sha0": return new Sha0();
				case "Sha1": return new Sha1();
				case "Sha224": return new Sha224();
				case "Sha256": return new Sha256();
				case "Sha384": return new Sha384();
				case "Sha512": return new Sha512();
				case "Snefru2": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru256Bit8Pass));
				case "Sum": return new Sum(SumParameters.GetParameters(SumStandard.Sum32Bit));
				case "SumBsd": return new SumBsd();
				case "SumSysV": return new SumSysV();
				case "Tiger": return new Tiger(TigerParameters.GetParameters(TigerStandard.Tiger192Bit));
				case "Whirlpool": return new Whirlpool();
				case "Xor8": return new Xor8();
				case "Xum32": return new Xum32();
				case "HashList": return new HashList(HashAlgorithm.Create());
				case "HashTree": return new HashTree(new Tiger(TigerParameters.GetParameters(TigerStandard.Tiger192Bit)), 1024);
				case "MultiHash": return new MultiHash();
				case "ParallelHash": return new ParallelHash();
				case "HashAlgorithm": return new Sha1();

				// Look up by alternate names.
				case "Adler-32": return new Adler32();
				case "Elf": return new ElfHash();
				case "Fcs-16": return new Fcs16();
				case "Fcs-32": return new Fcs32();
				case "Gost": return new GostHash();
				case "JHash": return new JenkinsHash();
				case "RipeMD-128": return new RipeMD128();
				case "RipeMD-160": return new RipeMD160();
				case "RipeMD-256": return new RipeMD256();
				case "RipeMD-320": return new RipeMD320();
				case "Sha": return new Sha1();
				case "Sha-224": return new Sha224();
				case "Sha-256": return new Sha256();
				case "Sha-384": return new Sha384();
				case "Sha-512": return new Sha512();
				case "Sum-Bsd": return new SumBsd();
				case "Sum-SysV": return new SumSysV();
				case "Xor-8": return new Xor8();
				case "Xum-32": return new Xum32();

				// Look up names for paramatized classes.
				case "Crc8": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc8Bit));
				case "Crc-8": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc8Bit));
				case "Crc8-Icode": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc8BitIcode));
				case "Crc-8-Icode": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc8BitIcode));
				case "Crc8-Itu": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc8BitItu));
				case "Crc-8-Itu": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc8BitItu));
				case "Crc8-Maxim": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc8BitMaxim));
				case "Crc-8-Maxim": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc8BitMaxim));
				case "Crc8-Wcdma": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc8BitWcdma));
				case "Crc-8-Wcdma": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc8BitWcdma));
				case "Crc16": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16Bit));
				case "Crc-16": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16Bit));
				case "Crc16-Arc": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitArc));
				case "Crc-16-Arc": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitArc));
				case "Crc16-Ibm": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitIbm));
				case "Crc-16-Ibm": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitIbm));
				case "Crc16-Lha": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitLha));
				case "Crc-16-Lha": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitLha));
				case "Crc16-Ccitt": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitCcitt));
				case "Crc-16-Ccitt": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitCcitt));
				case "Crc16-Kermit": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitKermit));
				case "Crc-16-Kermit": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitKermit));
				case "Crc16-Ccitt-False": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitCcittFalse));
				case "Crc-16-Ccitt-False": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitCcittFalse));
				case "Crc16-Maxim": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitMaxim));
				case "Crc-16-Maxim": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitMaxim));
				case "Crc16-Usb": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitUsb));
				case "Crc-16-Usb": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitUsb));
				case "Crc16-X25": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitX25));
				case "Crc-16-X25": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitX25));
				case "Crc16-Xmodem": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitXmodem));
				case "Crc-16-Xmodem": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitXmodem));
				case "Crc16-Zmodem": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitZmodem));
				case "Crc-16-Zmodem": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitZmodem));
				case "Crc16-Xkermit": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitXkermit));
				case "Crc-16-Xkermit": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc16BitXkermit));
				case "Crc24": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc24Bit));
				case "Crc-24": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc24Bit));
				case "Crc24-OpenPgp": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc24BitOpenPgp));
				case "Crc-24-OpenPgp": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc24BitOpenPgp));
				case "Crc32": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit));
				case "Crc-32": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32Bit));
				case "Crc32-Pkzip": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitPkzip));
				case "Crc-32-Pkzip": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitPkzip));
				case "Crc32-Itu": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitItu));
				case "Crc-32-Itu": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitItu));
				case "Crc32-Bzip2": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitBzip2));
				case "Crc-32-Bzip2": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitBzip2));
				case "Crc32-Iscsi": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitIscsi));
				case "Crc-32-Iscsi": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitIscsi));
				case "Crc32-Jam": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitJam));
				case "Crc-32-Jam": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitJam));
				case "Crc32-Posix": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitPosix));
				case "Crc-32-Posix": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitPosix));
				case "Crc32-Cksum": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitCksum));
				case "Crc-32-Cksum": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitCksum));
				case "Crc32-Mpeg2": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitMpeg2));
				case "Crc-32-Mpeg2": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc32BitMpeg2));
				case "Crc64": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc64Bit));
				case "Crc-64": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc64Bit));
				case "Crc64-WE": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc64BitWE));
				case "Crc-64-WE": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc64BitWE));
				case "Crc64-Iso": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc64BitIso));
				case "Crc-64-Iso": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc64BitIso));
				case "Crc64-Jones": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc64BitJones));
				case "Crc-64-Jones": return new Crc(CrcParameters.GetParameters(CrcStandard.Crc64BitJones));
				case "Fletcher32Bit": return new Fletcher(FletcherParameters.GetParameters(FletcherStandard.Fletcher32Bit));
				case "Fletcher-32": return new Fletcher(FletcherParameters.GetParameters(FletcherStandard.Fletcher32Bit));
				case "Fletcher16Bit": return new Fletcher(FletcherParameters.GetParameters(FletcherStandard.Fletcher16Bit));
				case "Fletcher-16": return new Fletcher(FletcherParameters.GetParameters(FletcherStandard.Fletcher16Bit));
				case "Fletcher8Bit": return new Fletcher(FletcherParameters.GetParameters(FletcherStandard.Fletcher8Bit));
				case "Fletcher-8": return new Fletcher(FletcherParameters.GetParameters(FletcherStandard.Fletcher8Bit));
				case "Fnv32": return new Fnv(FnvParameters.GetParameters(FnvStandard.Fnv32BitType1A));
				case "Fnv-32": return new Fnv(FnvParameters.GetParameters(FnvStandard.Fnv32BitType1A));
				case "Fnv64": return new Fnv(FnvParameters.GetParameters(FnvStandard.Fnv64BitType1A));
				case "Fnv-64": return new Fnv(FnvParameters.GetParameters(FnvStandard.Fnv64BitType1A));
				case "Fnv-0-32": return new Fnv(FnvParameters.GetParameters(FnvStandard.Fnv32BitType0));
				case "Fnv-0-64": return new Fnv(FnvParameters.GetParameters(FnvStandard.Fnv64BitType0));
				case "Fnv-1-32": return new Fnv(FnvParameters.GetParameters(FnvStandard.Fnv32BitType1));
				case "Fnv-1-64": return new Fnv(FnvParameters.GetParameters(FnvStandard.Fnv64BitType1));
				case "Fnv-1A-32": return new Fnv(FnvParameters.GetParameters(FnvStandard.Fnv32BitType1A));
				case "Fnv-1A-64": return new Fnv(FnvParameters.GetParameters(FnvStandard.Fnv64BitType1A));
				case "GHash3": return new GHash(GHashParameters.GetParameters(GHashStandard.GHash3));
				case "GHash-3": return new GHash(GHashParameters.GetParameters(GHashStandard.GHash3));
				case "GHash5": return new GHash(GHashParameters.GetParameters(GHashStandard.GHash5));
				case "GHash-5": return new GHash(GHashParameters.GetParameters(GHashStandard.GHash5));
				case "Haval128": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval128Bit5Pass));
				case "Haval-128": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval128Bit5Pass));
				case "Haval160": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval160Bit5Pass));
				case "Haval-160": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval160Bit5Pass));
				case "Haval192": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval192Bit5Pass));
				case "Haval-192": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval192Bit5Pass));
				case "Haval224": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval224Bit5Pass));
				case "Haval-224": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval224Bit5Pass));
				case "Haval256": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval256Bit5Pass));
				case "Haval-256": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval256Bit5Pass));
				case "Haval-3-128": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval128Bit3Pass));
				case "Haval-3-160": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval160Bit3Pass));
				case "Haval-3-192": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval192Bit3Pass));
				case "Haval-3-224": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval224Bit3Pass));
				case "Haval-3-256": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval256Bit3Pass));
				case "Haval-4-128": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval128Bit4Pass));
				case "Haval-4-160": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval160Bit4Pass));
				case "Haval-4-192": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval192Bit4Pass));
				case "Haval-4-224": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval224Bit4Pass));
				case "Haval-4-256": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval256Bit4Pass));
				case "Haval-5-128": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval128Bit5Pass));
				case "Haval-5-160": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval160Bit5Pass));
				case "Haval-5-192": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval192Bit5Pass));
				case "Haval-5-224": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval224Bit5Pass));
				case "Haval-5-256": return new Haval(HavalParameters.GetParameters(HavalStandard.Haval256Bit5Pass));
				case "Snefru2128": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru128Bit8Pass));
				case "Snefru2-128": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru128Bit8Pass));
				case "Snefru2256": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru256Bit8Pass));
				case "Snefru2-256": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru256Bit8Pass));
				case "Snefru2-4-128": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru128Bit4Pass));
				case "Snefru2-4-256": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru256Bit4Pass));
				case "Snefru2-8-128": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru128Bit8Pass));
				case "Snefru2-8-256": return new Snefru2(Snefru2Parameters.GetParameters(Snefru2Standard.Snefru256Bit8Pass));
				case "Sum8": return new Sum(SumParameters.GetParameters(SumStandard.Sum8Bit));
				case "Sum-8": return new Sum(SumParameters.GetParameters(SumStandard.Sum8Bit));
				case "Sum16": return new Sum(SumParameters.GetParameters(SumStandard.Sum16Bit));
				case "Sum-16": return new Sum(SumParameters.GetParameters(SumStandard.Sum16Bit));
				case "Sum24": return new Sum(SumParameters.GetParameters(SumStandard.Sum24Bit));
				case "Sum-24": return new Sum(SumParameters.GetParameters(SumStandard.Sum24Bit));
				case "Sum32": return new Sum(SumParameters.GetParameters(SumStandard.Sum32Bit));
				case "Sum-32": return new Sum(SumParameters.GetParameters(SumStandard.Sum32Bit));
				case "Sum64": return new Sum(SumParameters.GetParameters(SumStandard.Sum64Bit));
				case "Sum-64": return new Sum(SumParameters.GetParameters(SumStandard.Sum64Bit));
				case "Tiger128": return new Tiger(TigerParameters.GetParameters(TigerStandard.Tiger128Bit));
				case "Tiger-128": return new Tiger(TigerParameters.GetParameters(TigerStandard.Tiger128Bit));
				case "Tiger160": return new Tiger(TigerParameters.GetParameters(TigerStandard.Tiger160Bit));
				case "Tiger-160": return new Tiger(TigerParameters.GetParameters(TigerStandard.Tiger160Bit));
				case "Tiger192": return new Tiger(TigerParameters.GetParameters(TigerStandard.Tiger192Bit));
				case "Tiger-192": return new Tiger(TigerParameters.GetParameters(TigerStandard.Tiger192Bit));

				// When in doubt, let the host framework try to figure it out.
				default: return System.Security.Cryptography.HashAlgorithm.Create(hashName);
			}
		}
	}
}
