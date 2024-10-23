using System.Text;
using Lab2;

class Program
{
    public static void Main(string[] args)
    {
        var a = new CustomSet<int> { 2 };
        Console.WriteLine(a.ToString());
    }
}