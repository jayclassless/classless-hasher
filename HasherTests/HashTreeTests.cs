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
using System.Collections.Generic;
using NUnit.Framework;
using Classless.Hasher.Methods;
using Classless.Hasher.Utilities;

namespace Classless.Hasher.Tests {
	[TestFixture]
	public class HashTreeTests {
		static public object[] TigerTreeVectors = {
			new object[] { TestVectors.Battery.Empty, TestVectors.h("5D9ED00A030E638BDB753A6A24FB900E5A63B8E73E6C25B6") },
			new object[] { TestVectors.Battery.A, TestVectors.h("16614B1F68C5C25EAF6136286C9C12932F4F73E87E90A273") },
			new object[] { TestVectors.Battery.ABC, TestVectors.h("0487CA26443F59CFBC780F3CA0CE509C8C352C27C5DCCA20") },
			new object[] { TestVectors.Battery.Alphabet, TestVectors.h("5B0ED06AB8773DD7EA2371A71D097EBA8F96A357E0854C77") },
			new object[] { TestVectors.Battery.Alphanumeric, TestVectors.h("997FC234BF6175678C9BEB35B6C655525115622C9AEC03DA") },
			new object[] { TestVectors.Battery.Numeric, TestVectors.h("1D7F757E693C769954151767D8CB3244AC8D935D5E9A8B53") },
			new object[] { TestVectors.Battery.NumericRepeated, TestVectors.h("685420361A70DB395782FE1252ED31057EB54CA879E0D0A7") },
			new object[] { TestVectors.Battery.Message, TestVectors.h("C339BD324EBF6085A0FA5F26D77B44DBB7DB60D690649704") },
			new object[] { TestVectors.Battery.QuickFox, TestVectors.h("B2D9A622772CC50B91D876D9C22FA07925BEEDB05A3DE747") },
			new object[] { TestVectors.Battery.MillionAs, TestVectors.h("511F341A7C14145FCBA4A55A9CCAF743DAC0EEF270010973") },
			
			// http://open-content.net/specs/draft-jchapweske-thex-02.html
			new object[] { new byte[1] { 0 }, TestVectors.h("AABBCCA084ACECD0511D1F6232A17BFAEFA441B2982E5548") },
			new object[] { TestVectors.s("A", 1024), TestVectors.h("5FBD0E62AD016D596B77D1D28883B94FED78ECBAF4640914") },
			new object[] { TestVectors.s("A", 1025), TestVectors.h("7E591C1CD8F2E6121FDBCD8071BA279626B771642D10A3DB") },
		};

		[Test, TestCaseSource("TigerTreeVectors")]
		public void TigerTreeVectorTest(byte[] input, byte[] expectedOutput) {
			HashTree tree = new HashTree(new Tiger(TigerParameters.GetParameters(TigerStandard.Tiger192BitVersion1)), 1024);
			CustomAssert.AreEqual(expectedOutput, tree.ComputeHash(input));
		}


		[Test]
		public void DefaultConstructorTest() {
			HashTree tree = new HashTree();
			Assert.AreEqual(1024, tree.BlockSize);
		}

		[Test]
		public void Constructor1Test() {
			HashTree tree = new HashTree(new MD5());
			Assert.AreEqual(1024, tree.BlockSize);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void Constructor1NullTest() {
			HashTree tree = new HashTree(null);
		}

		[Test]
		public void Constructor2Test() {
			HashTree tree = new HashTree(new MD5(), 25);
			Assert.AreEqual(25, tree.BlockSize);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void Constructor2NullTest() {
			HashTree tree = new HashTree(null, 25);
		}


		[Test]
		public void RootTest() {
			HashTree tree = new HashTree(new Tiger(TigerParameters.GetParameters(TigerStandard.Tiger192BitVersion1)), 1024);
			byte[] topHash = tree.ComputeHash(TestVectors.Battery.MillionAs);
			CustomAssert.AreEqual(TestVectors.h("511F341A7C14145FCBA4A55A9CCAF743DAC0EEF270010973"), tree.Root.Hash);
			Assert.AreEqual(0, tree.Root.RangeStart);
			Assert.AreEqual(999999, tree.Root.RangeEnd);
		}
	}
}
