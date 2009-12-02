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
	/// <summary>A class that contains the parameters necessary to initialize a HAVAL algorithm.</summary>
	public class HAVALParameters : HashAlgorithmParameters {
		private short passes;
		private short length;


		/// <summary>Gets or sets the number of passes.</summary>
		public short Passes {
			get { return passes; }
			set {
				if ((value != 3) && (value != 4) && (value != 5)) {
					throw new ArgumentOutOfRangeException("value", Hasher.Properties.Resources.invalidHavalPasses);
				} else {
					passes = value;
				}
			}
		}

		/// <summary>Gets or sets the bit length.</summary>
		public short Length {
			get { return length; }
			set {
				if ((value != 128) && (value != 160) && (value != 192) && (value != 224) && (value != 256)) {
					throw new ArgumentOutOfRangeException("value", Hasher.Properties.Resources.invalidHavalLength);
				} else {
					length = value;
				}
			}
		}


		/// <summary>Initializes a new instance of the HAVALParamters class.</summary>
		/// <param name="passes">How many transformation passes to do.</param>
		/// <param name="length">The bit length of the final hash.</param>
		public HAVALParameters(short passes, short length) {
			this.Passes = passes;
			this.Length = length;
		}


		/// <summary>Returns a String that represents the current Object.</summary>
		/// <returns>A String that represents the current Object.</returns>
		override public string ToString() {
			return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}({1:d}:{2:d})", this.GetType().Name, Passes, Length);
		}


		/// <summary>Retrieves a standard set of HAVAL parameters.</summary>
		/// <param name="standard">The name of the standard parameter set to retrieve.</param>
		/// <returns>The HAVAL Parameters for the given standard.</returns>
		public static HAVALParameters GetParameters(HAVALStandard standard) {
			HAVALParameters temp = null;

			switch (standard) {
				case HAVALStandard.HAVAL_3_128:	temp = new HAVALParameters(3, 128);	break;
				case HAVALStandard.HAVAL_3_160:	temp = new HAVALParameters(3, 160);	break;
				case HAVALStandard.HAVAL_3_192:	temp = new HAVALParameters(3, 192);	break;
				case HAVALStandard.HAVAL_3_224:	temp = new HAVALParameters(3, 224);	break;
				case HAVALStandard.HAVAL_3_256:	temp = new HAVALParameters(3, 256);	break;
				case HAVALStandard.HAVAL_4_128:	temp = new HAVALParameters(4, 128);	break;
				case HAVALStandard.HAVAL_4_160:	temp = new HAVALParameters(4, 160);	break;
				case HAVALStandard.HAVAL_4_192:	temp = new HAVALParameters(4, 192);	break;
				case HAVALStandard.HAVAL_4_224:	temp = new HAVALParameters(4, 224);	break;
				case HAVALStandard.HAVAL_4_256:	temp = new HAVALParameters(4, 256);	break;
				case HAVALStandard.HAVAL_5_128:	temp = new HAVALParameters(5, 128);	break;
				case HAVALStandard.HAVAL_5_160:	temp = new HAVALParameters(5, 160);	break;
				case HAVALStandard.HAVAL_5_192:	temp = new HAVALParameters(5, 192);	break;
				case HAVALStandard.HAVAL_5_224:	temp = new HAVALParameters(5, 224);	break;
				case HAVALStandard.HAVAL_5_256:	temp = new HAVALParameters(5, 256);	break;
			}

			return temp;
		}
	}
}
