using System;

namespace ProductCard
{
    public static class IndicatorTest
    {
        public static void TestIndicator()
        {
            int[,] testCases =
            {
                { 10, 1 },
                { 6, 1 },
                { 5, 0 },
                { 4, 0 },
                { 1, 0 },
                { 0, 0 },
                { 100, 1 }
            };

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

            string[] comments =
            {
                "10 > 5",
                "6 > 5 (граница)",
                "5 ≤ 5 (граница)",
                "4 ≤ 5",
                "1 ≤ 5",
                "0 ≤ 5",
                "большое число"
            };

            Console.WriteLine(
                "============================================================"
            );

            Console.WriteLine(
                "ТЕСТИРОВАНИЕ ИНДИКАТОРА"
            );

            Console.WriteLine(
                "============================================================"
            );

            int passed = 0;

            for (int i = 0; i < testCases.GetLength(0); i++)
            {
                int qty = testCases[i, 0];

                string result =
                    qty > 5
                        ? "много"
                        : "мало";

                string status =
                    result == expected[i]
                        ? "✅"
                        : "❌";

                if (result == expected[i])
                {
                    passed++;
                }

                Console.WriteLine(
                    $"{status} qty={qty}: {result} " +
                    $"(ожидалось {expected[i]}) — {comments[i]}"
                );
            }

            Console.WriteLine(
                "============================================================"
            );

            Console.WriteLine(
                $"Пройдено: {passed} / {testCases.GetLength(0)}"
            );
        }
    }
}