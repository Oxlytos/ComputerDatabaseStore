using System.Text;

namespace ComputerStoreApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");
            StoreSimulation.Run();

        }
    }
}
