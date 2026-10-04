using System;

class Program
{
    static string Indicator(int qty)
    {
        return qty > 5 ? "много" : "мало";
    }

    static void Main()
    {
        int[] quantities = { 10, 6, 5, 4, 1, 0, 100 };

        string[] expected =
        {
            "много",
            "много",
            "мало",
            "мало",
            "мало",
            "мало",
            "много"
        };

        int passed = 0;

        Console.WriteLine("============================================================");
        Console.WriteLine("ТЕСТИРОВАНИЕ ИНДИКАТОРА");
        Console.WriteLine("============================================================");

        for (int i = 0; i < quantities.Length; i++)
        {
            int qty = quantities[i];

            string result = Indicator(qty);

            string status =
                result == expected[i]
                    ? "✅"
                    : "❌";

            if (result == expected[i])
            {
                passed++;
            }

            Console.WriteLine(
                $"{status} qty={qty}: {result} (ожидалось {expected[i]})"
            );
        }

        Console.WriteLine("============================================================");
        Console.WriteLine($"Пройдено: {passed} / {quantities.Length}");
    }
}