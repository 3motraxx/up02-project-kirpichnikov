using System;

class Program
{
    static string GetCardColor(int qty)
    {
        return qty <= 3
            ? "#ff8080"
            : "#FFFFFF";
    }

    static void Test(int qty, string expected)
    {
        string actual = GetCardColor(qty);

        if (actual == expected)
            Console.WriteLine($"PASS: qty={qty} -> {actual}");
        else
            Console.WriteLine($"FAIL: qty={qty} -> {actual}, ожидалось {expected}");
    }

    static void Main()
    {
        Test(1000, "#FFFFFF");
        Test(-1, "#ff8080");
        Test(3, "#ff8080");
    }
}