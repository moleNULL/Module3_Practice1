namespace PhoneContactProject
{
    internal class Starter
    {
        public static void Run()
        {
            Config config = new Config();
            PhoneContact ph = new PhoneContact(config);
            ph.Add(new PhoneRecord("Alicon", "123"));
            ph.Add(new PhoneRecord("Abicon", "123"));
            ph.Add(new PhoneRecord("AAicon", "123"));
            ph.Add(new PhoneRecord("Azbicon", "123"));
            ph.Add(new PhoneRecord("ҐAlicon", "123"));
            ph.Add(new PhoneRecord("1licon", "123"));
            ph.Add(new PhoneRecord("Юlicon", "123"));
            ph.Add(new PhoneRecord("Ыlicon", "123"));
            ph.Add(new PhoneRecord("favicon", "12345"));

            // ph.Print();
            ph.Remove("favicon");

            foreach (var phone in ph)
            {
                if (phone.Value.Count > 0)
                {
                    Console.WriteLine($"{phone.Key}:");
                    phone.Value.Sort();

                    foreach (var record in phone.Value)
                    {
                        Console.WriteLine($"\t{record.Name}: {record.Number}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
