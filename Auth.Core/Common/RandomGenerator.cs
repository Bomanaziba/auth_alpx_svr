

using System;
using System.Linq;
using System.Text;

namespace Auth.Core.Common
{

    public class RandomGenerator
    {
        private static Random random = new Random();

        private static int RandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }
        public static string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; 

            for (var i = 0; i < size; i++)
            {
                var @char = (char)new Random().Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public static string RandomAlphaString(int length)
        {
            string upperCaseLetter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowerCaseLetter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower();

            string chars = upperCaseLetter + lowerCaseLetter;

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomAlphaNumericString(int length)
        {
            string numbers = "0123456789";
            string upperCaseLetter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowerCaseLetter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower();

            string chars = upperCaseLetter + lowerCaseLetter + numbers;

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        
        public static string GenerateCleintId(int Id)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(Constant.AlphaX.ToUpper());

            stringBuilder.Append(RandomAlphaNumericString(8));

            stringBuilder.Append(Id);

            return stringBuilder.ToString();
        }

        public static string GenerateCleintSecret()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(RandomAlphaNumericString(19));

            return stringBuilder.ToString();
        }

    }

}