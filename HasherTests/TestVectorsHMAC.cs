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
using Classless.Hasher;

namespace Classless.Hasher.Tests {
	class TestVectorsHMAC : TestVectors {
		static public object[] Vectors = {
			// http://tools.ietf.org/html/rfc2104
			new object[] { new MD5(), h("0B", 16), s("Hi There"), h("9294727A3638BB1C13F48EF8158BFC9D") },
			new object[] { new MD5(), s("Jefe"), s("what do ya want for nothing?"), h("750C783E6AB0B503EAA86E310A5DB738") },
			new object[] { new MD5(), h("AA", 16), h("DD", 50), h("56BE34521D144C88DBB8C733F0E8B3F6") },

			// http://csrc.nist.gov/publications/fips/fips198/fips-198a.pdf
			new object[] { new Sha1(), h("000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F"), s("Sample #1"), h("4F4CA3D5D68BA7CC0A1208C9C61E9C5DA0403C0A") },
			new object[] { new Sha1(), h("303132333435363738393A3B3C3D3E3F40414243"), s("Sample #2"), h("0922D3405FAA3D194F82A45830737D5CC6C75D24") },
			new object[] { new Sha1(), h("505152535455565758595A5B5C5D5E5F606162636465666768696A6B6C6D6E6F707172737475767778797A7B7C7D7E7F808182838485868788898A8B8C8D8E8F909192939495969798999A9B9C9D9E9FA0A1A2A3A4A5A6A7A8A9AAABACADAEAFB0B1B2B3"), s("Sample #3"), h("BCF41EAB8BB2D802F3D05CAF7CB092ECF8D1A3AA") },
			new object[] { new Sha1(), h("707172737475767778797A7B7C7D7E7F808182838485868788898A8B8C8D8E8F909192939495969798999A9B9C9D9E9FA0"), s("Sample #4"), h("9EA886EFE268DBECCE420C7524DF32E0751A2A26") },

			// http://tools.ietf.org/html/rfc4231
			new object[] { new Sha224(), h("0B", 20), s("Hi There"), h("896FB1128ABBDF196832107CD49DF33F47B4B1169912BA4F53684B22") },
			new object[] { new Sha256(), h("0B", 20), s("Hi There"), h("B0344C61D8DB38535CA8AFCEAF0BF12B881DC200C9833DA726E9376C2E32CFF7") },
			new object[] { new Sha384(), h("0B", 20), s("Hi There"), h("AFD03944D84895626B0825F4AB46907F15F9DADBE4101EC682AA034C7CEBC59CFAEA9EA9076EDE7F4AF152E8B2FA9CB6") },
			new object[] { new Sha512(), h("0B", 20), s("Hi There"), h("87AA7CDEA5EF619D4FF0B4241A1D6CB02379F4E2CE4EC2787AD0B30545E17CDEDAA833B7D6B8A702038B274EAEA3F4E4BE9D914EEB61F1702E696C203A126854") },
			new object[] { new Sha224(), s("Jefe"), s("what do ya want for nothing?"), h("A30E01098BC6DBBF45690F3A7E9E6D0F8BBEA2A39E6148008FD05E44") },
			new object[] { new Sha256(), s("Jefe"), s("what do ya want for nothing?"), h("5BDCC146BF60754E6A042426089575C75A003F089D2739839DEC58B964EC3843") },
			new object[] { new Sha384(), s("Jefe"), s("what do ya want for nothing?"), h("AF45D2E376484031617F78D2B58A6B1B9C7EF464F5A01B47E42EC3736322445E8E2240CA5E69E2C78B3239ECFAB21649") },
			new object[] { new Sha512(), s("Jefe"), s("what do ya want for nothing?"), h("164B7A7BFCF819E2E395FBE73B56E0A387BD64222E831FD610270CD7EA2505549758BF75C05A994A6D034F65F8F0E6FDCAEAB1A34D4A6B4B636E070A38BCE737") },
			new object[] { new Sha224(), h("AA", 20), h("DD", 50), h("7FB3CB3588C6C1F6FFA9694D7D6AD2649365B0C1F65D69D1EC8333EA") },
			new object[] { new Sha256(), h("AA", 20), h("DD", 50), h("773EA91E36800E46854DB8EBD09181A72959098B3EF8C122D9635514CED565FE") },
			new object[] { new Sha384(), h("AA", 20), h("DD", 50), h("88062608D3E6AD8A0AA2ACE014C8A86F0AA635D947AC9FEBE83EF4E55966144B2A5AB39DC13814B94E3AB6E101A34F27") },
			new object[] { new Sha512(), h("AA", 20), h("DD", 50), h("FA73B0089D56A284EFB0F0756C890BE9B1B5DBDD8EE81A3655F83E33B2279D39BF3E848279A722C806B485A47E67C807B946A337BEE8942674278859E13292FB") },
			new object[] { new Sha224(), h("0102030405060708090A0B0C0D0E0F10111213141516171819"), h("CD", 50), h("6C11506874013CAC6A2ABC1BB382627CEC6A90D86EFC012DE7AFEC5A") },
			new object[] { new Sha256(), h("0102030405060708090A0B0C0D0E0F10111213141516171819"), h("CD", 50), h("82558A389A443C0EA4CC819899F2083A85F0FAA3E578F8077A2E3FF46729665B") },
			new object[] { new Sha384(), h("0102030405060708090A0B0C0D0E0F10111213141516171819"), h("CD", 50), h("3E8A69B7783C25851933AB6290AF6CA77A9981480850009CC5577C6E1F573B4E6801DD23C4A7D679CCF8A386C674CFFB") },
			new object[] { new Sha512(), h("0102030405060708090A0B0C0D0E0F10111213141516171819"), h("CD", 50), h("B0BA465637458C6990E5A8C5F61D4AF7E576D97FF94B872DE76F8050361EE3DBA91CA5C11AA25EB4D679275CC5788063A5F19741120C4F2DE2ADEBEB10A298DD") },
			new object[] { new Sha224(), h("AA", 131), s("Test Using Larger Than Block-Size Key - Hash Key First"), h("95E9A0DB962095ADAEBE9B2D6F0DBCE2D499F112F2D2B7273FA6870E") },
			new object[] { new Sha256(), h("AA", 131), s("Test Using Larger Than Block-Size Key - Hash Key First"), h("60E431591EE0B67F0D8A26AACBF5B77F8E0BC6213728C5140546040F0EE37F54") },
			new object[] { new Sha384(), h("AA", 131), s("Test Using Larger Than Block-Size Key - Hash Key First"), h("4ECE084485813E9088D2C63A041BC5B44F9EF1012A2B588F3CD11F05033AC4C60C2EF6AB4030FE8296248DF163F44952") },
			new object[] { new Sha512(), h("AA", 131), s("Test Using Larger Than Block-Size Key - Hash Key First"), h("80B24263C7C1A3EBB71493C1DD7BE8B49B46D1F41B4AEEC1121B013783F8F3526B56D037E05F2598BD0FD2215D6A1E5295E64F73F63F0AEC8B915A985D786598") },
			new object[] { new Sha224(), h("AA", 131), s("This is a test using a larger than block-size key and a larger than block-size data. The key needs to be hashed before being used by the HMAC algorithm."), h("3A854166AC5D9F023F54D517D0B39DBD946770DB9C2B95C9F6F565D1") },
			new object[] { new Sha256(), h("AA", 131), s("This is a test using a larger than block-size key and a larger than block-size data. The key needs to be hashed before being used by the HMAC algorithm."), h("9B09FFA71B942FCB27635FBCD5B0E944BFDC63644F0713938A7F51535C3A35E2") },
			new object[] { new Sha384(), h("AA", 131), s("This is a test using a larger than block-size key and a larger than block-size data. The key needs to be hashed before being used by the HMAC algorithm."), h("6617178E941F020D351E2F254E8FD32C602420FEB0B8FB9ADCCEBB82461E99C5A678CC31E799176D3860E6110C46523E") },
			new object[] { new Sha512(), h("AA", 131), s("This is a test using a larger than block-size key and a larger than block-size data. The key needs to be hashed before being used by the HMAC algorithm."), h("E37B6A775DC87DBAA4DFA9F96E5E3FFDDEBD71F8867289865DF5A32D20CDC944B6022CAC3C4982B10D5EEB55C3E4DE15134676FB6DE0446065C97440FA8C6A58") },

			// http://www.ietf.org/old/2009/proceedings/02nov/I-D/draft-ietf-ipsec-ciph-sha-256-01.txt
			new object[] { new Sha256(), h("0102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20"), s("abc"), h("A21B1F5D4CF4F73A4DD939750F7A066A7F98CC131CB16A6692759021CFAB8181") },
			new object[] { new Sha256(), h("0102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20"), s("abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq"), h("104FDC1257328F08184BA73131C53CAEE698E36119421149EA8C712456697D30") },
			new object[] { new Sha256(), h("0102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20"), s("abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopqabcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq"), h("470305FC7E40FE34D3EEB3E773D95AAB73ACF0FD060447A5EB4595BF33A9D1A3") },
			new object[] { new Sha256(), h("0B", 32), s("Hi There"), h("198A607EB44BFBC69903A0F1CF2BBDC5BA0AA3F3D9AE3C1C7A3B1696A0B68CF7") },
			new object[] { new Sha256(), s("Jefe"), s("what do ya want for nothing?"), h("5BDCC146BF60754E6A042426089575C75A003F089D2739839DEC58B964EC3843") },
			new object[] { new Sha256(), h("AA", 32), h("DD", 50), h("CDCB1220D1ECCCEA91E53ABA3092F962E549FE6CE9ED7FDC43191FBDE45C30B0") },
			new object[] { new Sha256(), h("0102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425"), h("CD", 50), h("D4633C17F6FB8D744C66DEE0F8F074556EC4AF55EF07998541468EB49BD2E917") },
			new object[] { new Sha256(), h("0C", 32), s("Test With Truncation"), h("7546AF01841FC09B1AB9C3749A5F1C17D4F589668A587B2700A9C97C1193CF42") },
			new object[] { new Sha256(), h("AA", 80), s("Test Using Larger Than Block-Size Key - Hash Key First"), h("6953025ED96F0C09F80A96F78E6538DBE2E7B820E3DD970E7DDD39091B32352F") },
			new object[] { new Sha256(), h("AA", 80), s("Test Using Larger Than Block-Size Key and Larger Than One Block-Size Data"), h("6355AC22E890D0A3C8481A5CA4825BC884D3E7A1FF98A2FC2AC7D8E064C3B2E6") },

			// http://www.randombit.net/text/has160.html
			new object[] { new Has160(), h("0B", 20), s("Hi There"), h("F5B44115A53F716B6F488DE1098EE7C251418623") },
			new object[] { new Has160(), s("Jefe"), s("what do ya want for nothing?"), h("A74547C1EF0AA147C7428AB7E71664549BE2A412") },
			new object[] { new Has160(), h("AA", 20), h("DD", 50), h("E4C91BC71782FA44A56BE1A34AAE167E8FFC9734") },
			new object[] { new Has160(), h("0102030405060708090A0B0C0D0E0F10111213141516171819"), h("CD", 50), h("14D1055DA875222053BF1180BBEF8892EBA3AC30") },
			new object[] { new Has160(), h("0C", 20), s("Test With Truncation"), h("124131A293F1FDF3D6B11E2B7F7A1F5B12E42D58") },
			new object[] { new Has160(), h("AA", 80), s("Test Using Larger Than Block-Size Key - Hash Key First"), h("63750D67AF40E3FDE33526545D300972A1527053") },
			new object[] { new Has160(), h("AA", 80), s("Test Using Larger Than Block-Size Key and Larger Than One Block-Size Data"), h("1BDB821E399E208352C64F0655F6601E2A8A087C") },
		};
	}
}
