using System.Text;

namespace ComputerStoreApplication
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //For all possible swedish stuff
            Console.OutputEncoding = Encoding.UTF8;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");

            //Main loop
            await StoreSimulation.Run();
        }
    }
}
