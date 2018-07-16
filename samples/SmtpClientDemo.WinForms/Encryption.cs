#region Namespaces

using System;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

using Microsoft.Practices.EnterpriseLibrary.Common.Cryptography;

#endregion Namespaces

namespace SmtpClientDemo.WinForms
{
	public class Encryption
	{
		static Encryption()
		{
			m_ValidatorBase64 = new Regex(
				@"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$",
				RegexOptions.Compiled);
			m_StripWhitespace = new Regex(@"\s+", RegexOptions.Compiled);
		}

		/// <summary>
		/// Decrypts the specified STR in.
		/// </summary>
		/// <param name="strIn">The STR in.</param>
		/// <returns></returns>
		public static string DpapiDecrypt(string strIn)
		{
			//******************************************************************
			// NOTE: Special consideration will need to be taken before changing 
			//       method of separating the byte arrays since the encryption method
			//       will use the same algorithm.  Also if changes are deemed 
			//       necessary the encryption algorithm will also need to be changed.
			//******************************************************************

			// set the output sting equal to the input string, so if it does not meet the expectations
			// needed to decrypt the string we will return the string sent in;
			string strOut = strIn;

			// if the string coming in is either null, empty, or not a multiple of 4 (base64 format requirement)
			// there is no need to try to decrypt it
			if ((!string.IsNullOrEmpty(strIn)) &&
				((strIn.Length % 4) == 0) &&
				(IsValidBase64(strIn)))
			{
				int iEntropy = 8;
				byte[] bCombined = Convert.FromBase64String(strIn);

				// if the number of bytes after converting from the Base64 string is not greater 
				// than the entropy there is no need to try to decrypt it
				if (bCombined.Length > iEntropy)
				{
					byte[] bEntropy = new byte[iEntropy];
					byte[] bValue = new byte[bCombined.Length - iEntropy];
					int idxEntropy = 0;
					int idxValue = 0;
					for (int i = 0; i < bCombined.Length; i++)
					{
						if (((i % iEntropy == 0) && (idxEntropy < iEntropy)) || (idxValue + iEntropy >= bCombined.Length))
							bEntropy[idxEntropy++] = bCombined[i];
						else
							bValue[idxValue++] = bCombined[i];
					}
					byte[] bDecrypted = m_DpapiCryptographer.Decrypt(bValue, bEntropy);
					strOut = Encoding.ASCII.GetString(bDecrypted);
				}
			}
			return strOut;
		}

		/// <summary>
		/// Encrypts the specified STR in.
		/// </summary>
		/// <param name="input">The STR in.</param>
		/// <returns></returns>
		public static string DpapiEncrypt(string input)
		{
			if (input == null)
			{
				return "";
			}

			byte[] entropy = new byte[ENTROPY_LENGTH];

			// Generate a random entropy vector.
			Random enrtropyRandom = new Random();
			enrtropyRandom.NextBytes(entropy);

			byte[] encryptedData = ProtectedData.Protect(
				Encoding.ASCII.GetBytes(input),
				entropy,
				DataProtectionScope.CurrentUser);

			int combinedLength = ENTROPY_LENGTH + encryptedData.Length;
			byte[] combinedData = new byte[combinedLength];

			int dataEntropyIndex = 0;
			int dataOutIndex = 0;

			for (int i = 0; i < combinedLength; i++)
			{
				if (((i % ENTROPY_LENGTH == 0) && (dataEntropyIndex < ENTROPY_LENGTH)) ||
					(dataOutIndex + ENTROPY_LENGTH >= combinedLength))
				{
					combinedData[i] = entropy[dataEntropyIndex++];
				}
				else
				{
					combinedData[i] = encryptedData[dataOutIndex++];
				}
			}

			return Convert.ToBase64String(combinedData);
		}

		#region Private Constants

		private static readonly int ENTROPY_LENGTH = 8;

		#endregion Private Constants

		#region Private Fields

		private readonly static DpapiCryptographer m_DpapiCryptographer = new DpapiCryptographer();

		/// <summary>
		/// Contains a regular expression string for validating Base64-encoded strings.
		/// </summary>
		private static readonly Regex m_ValidatorBase64;

		/// <summary>
		/// Contains a regular expression that strips out all whitespace, including
		/// newlines, tabs, and spaces.
		/// </summary>
		private static readonly Regex m_StripWhitespace;

		#endregion Private Fields

		#region Private Methods

		/// <summary>
		/// Determines whether the given input is a valid base64 input.
		/// </summary>
		/// <param name="inputString">The input string.</param>
		/// <returns>
		/// 	<c>true</c> if [is valid base64] [the specified input string]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsValidBase64(
			string inputString)
		{
			// If we have a blank string, it will never be valid base64.
			if (string.IsNullOrEmpty(inputString))
			{
				return false;
			}

			// The RFC requires stripping out whitespace before processing. Once
			// we removed whitespace, we use regex to determine if this is a valid
			// base64 string.
			string strippedString = m_StripWhitespace.Replace(inputString, "");
			return m_ValidatorBase64.IsMatch(strippedString);
		}

		#endregion Private Methods
	}
}
