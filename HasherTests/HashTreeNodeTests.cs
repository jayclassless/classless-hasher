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
	public class HashTreeNodeTests {
		[Test]
		public void Constructor1Test() {
			HashTreeNode node = new HashTreeNode();
			Assert.IsNull(node.Hash);
			Assert.IsNull(node.Parent);
			Assert.IsNull(node.Left);
			Assert.IsNull(node.Right);
			Assert.AreEqual(0, node.RangeStart);
			Assert.AreEqual(0, node.RangeEnd);
		}

		[Test]
		public void Constructor2Test() {
			HashTreeNode parent = new HashTreeNode();
			HashTreeNode node = new HashTreeNode(parent);
			Assert.IsNull(node.Hash);
			Assert.AreEqual(parent, node.Parent);
			Assert.IsNull(node.Left);
			Assert.IsNull(node.Right);
			Assert.AreEqual(0, node.RangeStart);
			Assert.AreEqual(0, node.RangeEnd);
		}

		[Test]
		public void Constructor3Test() {
			HashTreeNode node = new HashTreeNode(TestVectors.h("ABCDEF"));
			CustomAssert.AreEqual(TestVectors.h("ABCDEF"), node.Hash);
			Assert.IsNull(node.Parent);
			Assert.IsNull(node.Left);
			Assert.IsNull(node.Right);
			Assert.AreEqual(0, node.RangeStart);
			Assert.AreEqual(0, node.RangeEnd);
		}

		[Test]
		public void Constructor4Test() {
			HashTreeNode parent = new HashTreeNode();
			HashTreeNode node = new HashTreeNode(TestVectors.h("ABCDEF"), parent);
			CustomAssert.AreEqual(TestVectors.h("ABCDEF"), node.Hash);
			Assert.AreEqual(parent, node.Parent);
			Assert.IsNull(node.Left);
			Assert.IsNull(node.Right);
			Assert.AreEqual(0, node.RangeStart);
			Assert.AreEqual(0, node.RangeEnd);
		}

		[Test]
		public void Constructor5Test() {
			HashTreeNode node = new HashTreeNode(TestVectors.h("ABCDEF"), 1, 2);
			CustomAssert.AreEqual(TestVectors.h("ABCDEF"), node.Hash);
			Assert.IsNull(node.Parent);
			Assert.IsNull(node.Left);
			Assert.IsNull(node.Right);
			Assert.AreEqual(1, node.RangeStart);
			Assert.AreEqual(2, node.RangeEnd);
		}

		[Test]
		public void Constructor6Test() {
			HashTreeNode parent = new HashTreeNode();
			HashTreeNode node = new HashTreeNode(TestVectors.h("ABCDEF"), 1, 2, parent);
			CustomAssert.AreEqual(TestVectors.h("ABCDEF"), node.Hash);
			Assert.AreEqual(parent, node.Parent);
			Assert.IsNull(node.Left);
			Assert.IsNull(node.Right);
			Assert.AreEqual(1, node.RangeStart);
			Assert.AreEqual(2, node.RangeEnd);
		}


		[Test]
		public void HashTest() {
			HashTreeNode node = new HashTreeNode();
			node.Hash = TestVectors.h("ABCDEF");
			CustomAssert.AreEqual(TestVectors.h("ABCDEF"), node.Hash);
		}


		[Test]
		public void RangeStartTest() {
			HashTreeNode node = new HashTreeNode();
			node.RangeStart = 5;
			Assert.AreEqual(5, node.RangeStart);
		}

		[Test, ExpectedException(typeof(ArgumentException)), TestCase(-1), TestCase(long.MinValue)]
		public void BadRangeStartTest(int value) {
			HashTreeNode node = new HashTreeNode();
			node.RangeStart = value;
		}


		[Test]
		public void RangeEndTest() {
			HashTreeNode node = new HashTreeNode();
			node.RangeEnd = 5;
			Assert.AreEqual(5, node.RangeEnd);
		}

		[Test, ExpectedException(typeof(ArgumentException)), TestCase(-1), TestCase(long.MinValue)]
		public void BadRangeEndTest(int value) {
			HashTreeNode node = new HashTreeNode();
			node.RangeEnd = value;
		}


		[Test]
		[TestCase(0,0,1)]
		[TestCase(0,1,2)]
		[TestCase(5,9,5)]
		[TestCase(9,5,-3)]
		public void CountTest(long start, long end, long count) {
			HashTreeNode node = new HashTreeNode(null, start, end);
			Assert.AreEqual(count, node.Count);
		}


		[Test]
		public void ParentTest() {
			HashTreeNode node = new HashTreeNode();
			HashTreeNode test = new HashTreeNode();
			node.Parent = test;
			Assert.AreEqual(test, node.Parent);
		}


		[Test]
		public void LeftTest() {
			HashTreeNode node = new HashTreeNode();
			HashTreeNode test = new HashTreeNode();
			node.Left = test;
			Assert.AreEqual(test, node.Left);
		}


		[Test]
		public void RightTest() {
			HashTreeNode node = new HashTreeNode();
			HashTreeNode test = new HashTreeNode();
			node.Right = test;
			Assert.AreEqual(test, node.Right);
		}


		[Test, ExpectedException(typeof(ArgumentException))]
		public void ParentLeftTest() {
			HashTreeNode node = new HashTreeNode();
			HashTreeNode test = new HashTreeNode();
			node.Parent = test;
			node.Left = test;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void ParentRightTest() {
			HashTreeNode node = new HashTreeNode();
			HashTreeNode test = new HashTreeNode();
			node.Parent = test;
			node.Right = test;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void LeftParentTest() {
			HashTreeNode node = new HashTreeNode();
			HashTreeNode test = new HashTreeNode();
			node.Left = test;
			node.Parent = test;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void LeftRightTest() {
			HashTreeNode node = new HashTreeNode();
			HashTreeNode test = new HashTreeNode();
			node.Left = test;
			node.Right = test;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void RightParentTest() {
			HashTreeNode node = new HashTreeNode();
			HashTreeNode test = new HashTreeNode();
			node.Right = test;
			node.Parent = test;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void RightLeftTest() {
			HashTreeNode node = new HashTreeNode();
			HashTreeNode test = new HashTreeNode();
			node.Right = test;
			node.Left = test;
		}
	}
}
