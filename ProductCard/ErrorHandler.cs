using System;
using System.IO;
using System.Windows.Forms;

namespace ProductCard
{
    public static class ErrorHandler
    {
        public static object? SafeCall(
            Func<object?> func)
        {
            try
            {
                return func();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Ошибка файла",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (System.Net.WebException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Ошибка соединения",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return null;
        }

        public static (
            bool IsValid,
            int Number,
            string Message
        ) ValidatePositiveInt(
            string value,
            string fieldName = "Значение")
        {
            try
            {
                int number = int.Parse(value);

                if (number <= 0)
                {
                    return (
                        false,
                        0,
                        $"{fieldName} должно быть больше нуля"
                    );
                }

                return (
                    true,
                    number,
                    ""
                );
            }
            catch (FormatException)
            {
                return (
                    false,
                    0,
                    $"{fieldName} должно быть целым числом"
                );
            }
            catch (OverflowException)
            {
                return (
                    false,
                    0,
                    $"{fieldName} должно быть целым числом"
                );
            }
        }
    }
}