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

namespace Classless.Hasher.Tests {
	[TestFixture]
	public class HashNodeTests {
		[Test]
		public void Constructor1Test() {
			HashNode node = new HashNode();
			Assert.IsNull(node.Hash);
			Assert.AreEqual(0, node.RangeStart);
			Assert.AreEqual(0, node.RangeEnd);
		}


		[Test]
		public void Constructor2Test() {
			HashNode node = new HashNode(TestVectors.h("ABCDEF"));
			CustomAssert.AreEqual(TestVectors.h("ABCDEF"), node.Hash);
			Assert.AreEqual(0, node.RangeStart);
			Assert.AreEqual(0, node.RangeEnd);
		}


		[Test]
		public void Constructor3Test() {
			HashNode node = new HashNode(TestVectors.h("ABCDEF"), 1, 2);
			CustomAssert.AreEqual(TestVectors.h("ABCDEF"), node.Hash);
			Assert.AreEqual(1, node.RangeStart);
			Assert.AreEqual(2, node.RangeEnd);
		}


		[Test]
		public void HashTest() {
			HashNode node = new HashNode();
			node.Hash = TestVectors.h("ABCDEF");
			CustomAssert.AreEqual(TestVectors.h("ABCDEF"), node.Hash);
		}


		[Test]
		public void RangeStartTest() {
			HashNode node = new HashNode();
			node.RangeStart = 5;
			Assert.AreEqual(5, node.RangeStart);
		}

		[Test, ExpectedException(typeof(ArgumentException)), TestCase(-1), TestCase(long.MinValue)]
		public void BadRangeStartTest(int value) {
			HashNode node = new HashNode();
			node.RangeStart = value;
		}


		[Test]
		public void RangeEndTest() {
			HashNode node = new HashNode();
			node.RangeEnd = 5;
			Assert.AreEqual(5, node.RangeEnd);
		}

		[Test, ExpectedException(typeof(ArgumentException)), TestCase(-1), TestCase(long.MinValue)]
		public void BadRangeEndTest(int value) {
			HashNode node = new HashNode();
			node.RangeEnd = value;
		}


		[Test]
		[TestCase(0,0,1)]
		[TestCase(0,1,2)]
		[TestCase(5,9,5)]
		[TestCase(9,5,-3)]
		public void CountTest(long start, long end, long count) {
			HashNode node = new HashNode(null, start, end);
			Assert.AreEqual(count, node.Count);
		}
	}
}
