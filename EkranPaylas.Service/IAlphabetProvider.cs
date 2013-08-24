using System.Collections.Generic;

namespace EkranPaylas.Service
{
    public interface IAlphabetProvider
    {
        IEnumerable<char> GetAlphabet();
    }
}