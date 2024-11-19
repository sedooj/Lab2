namespace Lab2;

public static class CustomSetExtensions
{
    
    public static string WithCommas(this CustomSet<string> set)
    {
        return string.Join(", ", set);
    }
    
    public static CustomSet<T> Distinct<T>(this CustomSet<T> set)
    {
        return set.Copy();
    }
}