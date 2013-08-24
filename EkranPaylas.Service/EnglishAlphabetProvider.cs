using System.Collections.Generic;

namespace EkranPaylas.Service
{
    public class EnglishAlphabetProvider : IAlphabetProvider
    {
        public IEnumerable<char> GetAlphabet()
        {
            for (var c = 'A'; c <= 'Z'; c++)
                yield return c;
        }
    }
}