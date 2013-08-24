using System;

namespace EkranPaylas.Service
{
    [Flags]
    public enum StringGenerateOptions
    {
        IncludeChars = 1,
        IncludeDigits = 2,
        IncludeCharAndDigits = 3
    }
}