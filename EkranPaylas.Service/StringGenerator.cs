using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;

namespace EkranPaylas.Service
{
    public class StringGenerator : IStringGenerator
    {
        private readonly IAlphabetProvider _alphabetProvider;
        private readonly char[] _digits = Enumerable.Range(0, 10).Select(i => Convert.ToString(i)).Select(Convert.ToChar).ToArray();

        public StringGenerator(IAlphabetProvider alphabetProvider)
        {
            _alphabetProvider = alphabetProvider;
        }

        public string GenerateString(StringGenerateOptions options, int len)
        {
            var data = new HashSet<char>();
            switch (options)
            {
                case StringGenerateOptions.IncludeChars:
                    _alphabetProvider.GetAlphabet().ToList().ForEach(i => data.Add(i));
                    break;
                case StringGenerateOptions.IncludeDigits:
                    _digits.ForEach(i => data.Add(i));
                    break;
                case StringGenerateOptions.IncludeCharAndDigits:
                    var alphabet = _alphabetProvider.GetAlphabet().ToArray();

                    alphabet.ForEach(i => data.Add(char.ToUpper(i)));
                    alphabet.ForEach(i => data.Add(char.ToLower(i)));

                    _digits.ToList().ForEach(i => data.Add(i));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("options");
            }

            return GenerateString(data.ToArray(), len);
        }

        public string GenerateString(char[] charList, int len)
        {
            return new string(Enumerable.Range(1, len).Select(f => charList.OrderBy(t => Guid.NewGuid()).First()).ToArray());
        }
    }
}