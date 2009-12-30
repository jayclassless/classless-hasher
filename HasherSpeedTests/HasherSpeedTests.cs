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
using System.Collections.Generic;
using System.Reflection;

namespace Classless.Hasher.SpeedTests {
	public class HasherSpeedTests {
		/// <summary>The list of HashAlgorithms provided by Hasher.</summary>
		static public IEnumerable<Type> ClasslessHashAlgorithms {
			get {
				Assembly assembly = Assembly.GetAssembly(typeof(Classless.Hasher.HashAlgorithm));
				foreach (Type type in assembly.GetTypes()) {
					if ((!type.IsAbstract) && (IsDescendant(type, typeof(System.Security.Cryptography.HashAlgorithm))) && (!type.FullName.Contains(".Methods."))) {
						yield return type;
					}
				}
			}
		}


		/// <summary>The list of HashAlgorithms provided by the host framework.</summary>
		static public IEnumerable<Type> FrameworkHashAlgorithms {
			get {
				Assembly assembly = Assembly.GetAssembly(typeof(System.Security.Cryptography.HashAlgorithm));
				foreach (Type type in assembly.GetTypes()) {
					if ((!type.IsAbstract) && (IsDescendant(type, typeof(System.Security.Cryptography.HashAlgorithm)))) {
						yield return type;
					}
				}
			}
		}


		/// <summary>The list of equivalent HashAlgorithms between the host framework and Hasher.</summary>
		static public Type[][] MatchingAlgorithms = new Type[][] {
			// Standard library implementations.
			new Type[] { Type.GetType("System.Security.Cryptography.HMACSHA1"), Assembly.GetAssembly(typeof(Classless.Hasher.HashAlgorithm)).GetType("Classless.Hasher.Mac.Hmac") },
			new Type[] { Type.GetType("System.Security.Cryptography.SHA1Managed"), Assembly.GetAssembly(typeof(Classless.Hasher.HashAlgorithm)).GetType("Classless.Hasher.Sha1") },
			new Type[] { Type.GetType("System.Security.Cryptography.SHA256Managed"), Assembly.GetAssembly(typeof(Classless.Hasher.HashAlgorithm)).GetType("Classless.Hasher.Sha256") },
			new Type[] { Type.GetType("System.Security.Cryptography.SHA384Managed"), Assembly.GetAssembly(typeof(Classless.Hasher.HashAlgorithm)).GetType("Classless.Hasher.Sha384") },
			new Type[] { Type.GetType("System.Security.Cryptography.SHA512Managed"), Assembly.GetAssembly(typeof(Classless.Hasher.HashAlgorithm)).GetType("Classless.Hasher.Sha512") },
			new Type[] { Type.GetType("System.Security.Cryptography.RIPEMD160Managed"), Assembly.GetAssembly(typeof(Classless.Hasher.HashAlgorithm)).GetType("Classless.Hasher.RipeMD160") },

			// Mono extensions.
			//   Type.GetType() isn't going to work here, these aren't in mscorlib.
			//new Type[] { Type.GetType("Mono.Security.Cryptography.MD2Managed"), Assembly.GetAssembly(typeof(Classless.Hasher.HashAlgorithm)).GetType("Classless.Hasher.MD2") },
			//new Type[] { Type.GetType("Mono.Security.Cryptography.MD4Managed"), Assembly.GetAssembly(typeof(Classless.Hasher.HashAlgorithm)).GetType("Classless.Hasher.MD4") },
			//new Type[] { Type.GetType("Mono.Security.Cryptography.SHA224Managed"), Assembly.GetAssembly(typeof(Classless.Hasher.HashAlgorithm)).GetType("Classless.Hasher.Sha224") },
		};


		static void Main(string[] args) {
			byte[] input = System.Text.Encoding.ASCII.GetBytes(new string('a', 1000000));
			int iterations = 100;

			PrintBanner();
			
			List<TestResult> frameworkResults = TestSuite("Framework", FrameworkHashAlgorithms, input, iterations);
			List<TestResult> classlessResults = TestSuite("Classless.Hasher", ClasslessHashAlgorithms, input, iterations);

			CompareMatchingAlgorithms(MatchingAlgorithms, frameworkResults, classlessResults);
		}


		/// <summary>Compares the equivalent HashAlgorithm test results.</summary>
		/// <param name="matchingAlgorithms">The list of equivalent HashAlgorithms to compare.</param>
		/// <param name="frameworkResults">The TestResults from the host framework's HashAlgorithms.</param>
		/// <param name="classlessResults">The TestResults from Hasher's HashAlgorithms.</param>
		static public void CompareMatchingAlgorithms(Type[][] matchingAlgorithms, List<TestResult> frameworkResults, List<TestResult> classlessResults) {
			Console.Out.WriteLine();
			Console.Out.WriteLine("Matching Algorithm Comparison:");

			TestResult[] results = new TestResult[3];
			foreach (Type[] pair in matchingAlgorithms) {
				if ((pair[0] == null) || (pair[1] == null)) { continue; }

				results[0] = frameworkResults.Find(delegate(TestResult tr) { return tr.AlgorithmType == pair[0]; });
				results[1] = classlessResults.Find(delegate(TestResult tr) { return tr.AlgorithmType == pair[1]; });

				if (results[0].ProcessingTime.TotalMilliseconds > results[1].ProcessingTime.TotalMilliseconds) {
					results[2] = results[0];
					results[0] = results[1];
					results[1] = results[2];
				}

				Console.Out.WriteLine(" {0} is faster than {1} ({2:F3} MiB/s, {3:F2}%)",
					results[0].AlgorithmType.Name,
					results[1].AlgorithmType.Name,
					(results[0].ProcessingRate - results[1].ProcessingRate),
					((results[0].ProcessingRate - results[1].ProcessingRate) / results[1].ProcessingRate) * 100
				);
			}
		}


		/// <summary>Executes throughput tests on a list of HashAlgorithms.</summary>
		/// <param name="suiteName">The name of list of HashAlgorithms being tested.</param>
		/// <param name="hashAlgorithmTypes">The list of HashAlgorithms to test.</param>
		/// <param name="input">The data to hash during each iteration.</param>
		/// <param name="iterations">The number of iterations to execute.</param>
		/// <returns>The results of all the test executions.</returns>
		static public List<TestResult> TestSuite(string suiteName, IEnumerable<Type> hashAlgorithmTypes, byte[] input, int iterations) {
			Console.Out.WriteLine();
			Console.Out.Write("Testing {0} Algorithms...", suiteName);

			TestResult temp;
			int longestName = 0;
			List<TestResult> resultSet = new List<TestResult>();
			foreach (Type hashAlgorithmType in hashAlgorithmTypes) {
				temp = TestAlgorithm(hashAlgorithmType, input, iterations);
				resultSet.Add(temp);
				if (temp.AlgorithmType.Name.Length > longestName) {
					longestName = temp.AlgorithmType.Name.Length;
				}
			}

			Console.Out.WriteLine(" complete.");

			resultSet.Sort();
			foreach (TestResult result in resultSet) {
				Console.Out.WriteLine(" {0}: {1} ({2:F3} MiB/s)",
					result.AlgorithmType.Name.PadLeft(longestName, ' '),
					result.ProcessingTime,
					result.ProcessingRate
				);
			}

			return resultSet;
		}


		/// <summary>Executes a throughput test for the specified HashAlgorithm.</summary>
		/// <param name="hashAlgorithmType">The HashAlgorithm to test.</param>
		/// <param name="input">The data to hash during each iteration.</param>
		/// <param name="iterations">The number of iterations to execute.</param>
		/// <returns>The results of the test execution.</returns>
		static public TestResult TestAlgorithm(Type hashAlgorithmType, byte[] input, int iterations) {
			System.Security.Cryptography.HashAlgorithm hashAlgorithm = Classless.Hasher.HashAlgorithm.Create(hashAlgorithmType.FullName);
			TestResult result = new TestResult(hashAlgorithmType, iterations, input.Length);

			byte[] output;
			DateTime start = DateTime.Now;
			for (int i = 0; i < iterations; i++) {
				output = hashAlgorithm.ComputeHash(input);
			}
			result.ProcessingTime = DateTime.Now - start;

			return result;
		}


		/// <summary>Prints intro information to the console.</summary>
		static public void PrintBanner() {
			Assembly current = Assembly.GetExecutingAssembly();

			Console.Out.WriteLine("{0} Version {1}", current.GetName().Name, current.GetName().Version);
			AssemblyCopyrightAttribute attr = (AssemblyCopyrightAttribute)AssemblyCopyrightAttribute.GetCustomAttribute(current, typeof(AssemblyCopyrightAttribute));
			Console.Out.WriteLine(attr.Copyright);
			Console.Out.WriteLine("http://code.google.com/p/classless-hasher/");

			Console.Out.WriteLine();
			Console.Out.WriteLine("Runtime Environment:");
			Console.Out.WriteLine("               OS Version: {0}", Environment.OSVersion.VersionString);
			string framework = (Type.GetType("Mono.Runtime") == null) ? ".NET" : "Mono";
			Console.Out.WriteLine("              CLR Version: v{0} ({1})", Environment.Version, framework);

			Console.Out.WriteLine(" Classless.Hasher Version: v{0}", Assembly.GetAssembly(typeof(Classless.Hasher.HashAlgorithm)).GetName().Version);
		}


		/// <summary>Determines whether or not one type is derived from another.</summary>
		/// <param name="type">The type to check.</param>
		/// <param name="ancestor">The suspected ancestor type.</param>
		/// <returns>true if the type is derived from the ancestor; false otherwise</returns>
		static public bool IsDescendant(Type type, Type ancestor) {
			if (type.BaseType == null) {
				return false;
			} else if (type.BaseType == ancestor) {
				return true;
			} else {
				return IsDescendant(type.BaseType, ancestor);
			}
		}
	}
}
