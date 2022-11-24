namespace PhoneContactProject
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\t\t\tPhone Contact Manager\n");

            try
            {
                Starter.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception. {ex.Message}");
            }

            Console.Write("\nPress any key to continue . . .");
            Console.ReadKey();
        }
    }
}