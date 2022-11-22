using System.Globalization;

namespace PhoneContactProject
{
    internal class Starter
    {
        public static void Run()
        {
            CultureInfo[] info = new Config().GetCultureInfo();

            foreach (var cultureInfo in info)
            {
                Console.WriteLine(cultureInfo);
            }
        }
    }
}
