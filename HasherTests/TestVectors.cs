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
using System.Text;

namespace Classless.Hasher.Tests {
	abstract public class TestVectors {
		public class Battery {
			static public readonly byte[] Empty = s("");
			static public readonly byte[] A = s("a");
			static public readonly byte[] ABC = s("abc");
			static public readonly byte[] Alphabet = s("abcdefghijklmnopqrstuvwxyz");
			static public readonly byte[] Alphanumeric = s("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
			static public readonly byte[] Numeric = s("1234567890");
			static public readonly byte[] NumericRepeated = s("12345678901234567890123456789012345678901234567890123456789012345678901234567890");
			static public readonly byte[] Message = s("message digest");
			static public readonly byte[] QuickFox = s("The quick brown fox jumps over the lazy dog");
			static public readonly byte[] MillionAs = s(new string('a', 1000000));

			static public byte[][] All {
				get {
					return new byte[][] {
						Empty,
						A,
						ABC,
						Alphabet,
						Alphanumeric,
						Numeric,
						NumericRepeated,
						Message,
						QuickFox,
						MillionAs,
					};
				}
			}
		}


		static public byte[] s(string input) { return s(input, 1); }
		static public byte[] s(string input, int repeat) {
			if (input == null) {
				return System.Text.Encoding.ASCII.GetBytes(String.Empty);
			} else {
				StringBuilder sb = new StringBuilder(repeat * input.Length);
				for (int i = 0; i < repeat; i++) {
					sb.Append(input);
				}
				return System.Text.Encoding.ASCII.GetBytes(sb.ToString());
			}
		}


		static public byte[] h(string input) { return h(input, 1); }
		static public byte[] h(string input, int repeat) {
			if (input == null) {
				return new byte[0];
			} else {
				StringBuilder sb = new StringBuilder(repeat * input.Length);
				for (int i = 0; i < repeat; i++) {
					sb.Append(input);
				}
				return Classless.Hasher.Utilities.Conversions.HexadecimalToByte(sb.ToString());
			}
		}
	}
}
