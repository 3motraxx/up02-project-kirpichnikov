using System;
using System.Windows.Forms;

namespace ProductCard
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            CatalogTest.TestFields();

            Application.Run(new Form1());
        }
    }
}