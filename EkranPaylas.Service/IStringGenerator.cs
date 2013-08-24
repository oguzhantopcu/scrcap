namespace EkranPaylas.Service
{
    public interface IStringGenerator
    {
        string GenerateString(StringGenerateOptions options, int len);
    }
}