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

namespace Classless.Hasher.SpeedTests {
	public class TestResult : IComparable<TestResult> {
		private Type algorithmType;
		private int iterations;
		private int iterationSize;
		private TimeSpan processingTime;


		public Type AlgorithmType {
			get { return algorithmType; }
			set { algorithmType = value; }
		}

		public int Iterations {
			get { return iterations; }
			set { iterations = value; }
		}

		public int IterationSize {
			get { return iterationSize; }
			set { iterationSize = value; }
		}

		public int BytesProcessed {
			get { return Iterations * IterationSize; }
		}

		public TimeSpan ProcessingTime {
			get { return processingTime; }
			set { processingTime = value; }
		}

		public double ProcessingRate {
			get { return ((((double)BytesProcessed / 1024) / 1024) / ((double)ProcessingTime.TotalMilliseconds / 1000)); }
		}


		public TestResult() { }

		public TestResult(Type algorithmType, int iterations, int iterationSize) {
			AlgorithmType = algorithmType;
			Iterations = iterations;
			IterationSize = iterationSize;
		}


		public int CompareTo(TestResult other) {
			if (other == null) {
				return 1;
			} else {
				return AlgorithmType.Name.CompareTo(other.AlgorithmType.Name);
			}
		}
	}
}
