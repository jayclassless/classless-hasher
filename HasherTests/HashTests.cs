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
using Classless.Hasher.Utilities;

namespace Classless.Hasher.Tests {
	[TestFixture]
	public class HashTests {
		[Test, TestCaseSource(typeof(TestVectorsHash), "Vectors")]
		public void TestVector(HashAlgorithm hasher, byte[] input, byte[] expectedOutput) {
			byte[] result = hasher.ComputeHash(input);

			Assert.AreEqual(expectedOutput.Length, result.Length, "Digest sizes do not match");

			for (int i = 0; i < result.Length; i++) {
				Assert.AreEqual(
					expectedOutput[i],
					result[i],
					string.Format("{0} on {1} should have produced {2} but instead got {3}", hasher, Conversions.ByteToHexadecimal(input), Conversions.ByteToHexadecimal(expectedOutput), Conversions.ByteToHexadecimal(result))
				);
			}
		}
	}
}
