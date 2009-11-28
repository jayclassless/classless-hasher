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

namespace Classless.Hasher {
	/// <summary>Predefined standards for CRC algorithms.</summary>
	public enum CRCStandard {
		/// <summary>8bit CRC</summary>
		CRC8,

		/// <summary>8bit CRC</summary>
		CRC8_ICODE,

		/// <summary>8bit CRC; Used in ATM HEC</summary>
		CRC8_ITU,

		/// <summary>8bit CRC</summary>
		CRC8_MAXIM,

		/// <summary>8bit CRC</summary>
		CRC8_WCDMA,

		/// <summary>16bit CRC</summary>
		CRC16,

		/// <summary>16bit CRC; Alias for CRC16</summary>
		CRC16_ARC,

		/// <summary>16bit CRC; Alias for CRC16</summary>
		CRC16_IBM,

		/// <summary>16bit CRC; Alias for CRC16</summary>
		CRC16_LHA,

		/// <summary>16bit CRC</summary>
		CRC16_CCITT,

		/// <summary>16bit CRC; Alias for CRC16_CCITT</summary>
		CRC16_KERMIT,

		/// <summary>16bit CRC</summary>
		CRC16_CCITT_FALSE,

		/// <summary>16bit CRC</summary>
		CRC16_MAXIM,

		/// <summary>16bit CRC</summary>
		CRC16_USB,

		/// <summary>16bit CRC</summary>
		CRC16_X25,

		/// <summary>16bit CRC</summary>
		CRC16_XMODEM,

		/// <summary>16bit CRC; Alias for CRC16_ZMODEM</summary>
		CRC16_ZMODEM,

		/// <summary>16bit CRC</summary>
		CRC16_XKERMIT,

		/// <summary>24bit CRC</summary>
		CRC24,

		/// <summary>24bit CRC; Alias for CRC24</summary>
		CRC24_OPENPGP,

		/// <summary>32bit CRC</summary>
		CRC32,

		/// <summary>32bit CRC; Alias for CRC32</summary>
		CRC32_PKZIP,

		/// <summary>32bit CRC; Alias for CRC32</summary>
		CRC32_ITU,

		/// <summary>32bit CRC</summary>
		CRC32_BZIP2,

		/// <summary>32bit CRC</summary>
		CRC32_ISCSI,

		/// <summary>32bit CRC</summary>
		CRC32_JAM,

		/// <summary>32bit CRC</summary>
		CRC32_POSIX,

		/// <summary>32bit CRC; Alias for CRC32_POSIX</summary>
		CRC32_CKSUM,

		/// <summary>32bit CRC</summary>
		CRC32_MPEG2,

		/// <summary>64bit CRC</summary>
		CRC64,

		/// <summary>64bit CRC</summary>
		CRC64_WE,

		/// <summary>64bit CRC</summary>
		CRC64_ISO,

		/// <summary>64bit CRC</summary>
		CRC64_JONES,
	}
}
