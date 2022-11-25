namespace PhoneContactProject
{
    internal class Starter
    {
        public static void Run()
        {
            Config config = new Config(); // read CultureInfo from config.json
            PhoneContact phoneContacts = new PhoneContact(config);
            phoneContacts.FillWithInitialData(); // add 10 contacts by default

            Console.WriteLine("\t\t\t\t\t\t.::Phone Contact Manager::.\n");
            Console.WriteLine("  -> Initial contacts:\n");
            PrintPhoneContacts(phoneContacts);

            while (true)
            {
                int userChoice = GetUserChoice();

                switch (userChoice)
                {
                    case 1:
                        AddContactChoice(phoneContacts);
                        break;
                    case 2:
                        RemoveContactChoice(phoneContacts);
                        break;
                }
            }
        }

        // Print all contacts in the phone contact manager to console
        private static void PrintPhoneContacts(PhoneContact phoneContacts)
        {
            // KeyValuePair<char, List<IRecord>>
            foreach (var phoneContact in phoneContacts)
            {
                if (phoneContact.Value.Count > 0)
                {
                    Console.WriteLine($"{phoneContact.Key}:");
                    phoneContact.Value.Sort();

                    // phoneContact.Value: List<PhoneRecord>
                    foreach (var record in phoneContact.Value)
                    {
                        Console.WriteLine($"\t{record.Name}: {record.Number}");
                    }

                    Console.WriteLine();
                }
            }
        }

        // Clears console and print initial message and updated contact list to the user
        private static void ReloadOutput(PhoneContact phoneContacts)
        {
            Console.Clear();

            // always show program description to the user
            Console.WriteLine("\t\t\t\t\t\t.::Phone Contact Manager::.\n");

            PrintPhoneContacts(phoneContacts);
        }

        // Get user choice: add contact or remove contact
        private static int GetUserChoice()
        {
            while (true)
            {
                Console.Write("\nAdd contact(1) or remove(2)?. Choice: ");
                string? sortChoice = Console.ReadLine();

                switch (sortChoice)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    default:
                        Console.WriteLine(" Error! Only 1 or 2 is a valid answer");
                        continue;
                }

                return int.Parse(sortChoice);
            }
        }

        // Logic if user chose to add a new contact
        private static void AddContactChoice(PhoneContact phoneContacts)
        {
            while (true)
            {
                Console.Write("\tNew contact (example: Donald, +380503117920): ");
                var contact = Console.ReadLine()?.Split(',');

                if (contact is null)
                {
                    Console.WriteLine($"Error! Provide correct contact");
                    continue;
                }

                if (contact?.Length != 2)
                {
                    Console.WriteLine($"Error! Provide correct contact");
                    continue;
                }

                bool added = phoneContacts.Add(new PhoneRecord(contact[0], contact[1]));

                if (added)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error! Incorrect number provided");
                }
            }

            ReloadOutput(phoneContacts);
        }

        // Logic if user chose to remove a contact by name
        private static void RemoveContactChoice(PhoneContact phoneContacts)
        {
            Console.Write("\tRemove contact by name: ");
            string? name = Console.ReadLine();

            if (name is not null)
            {
                bool removed = phoneContacts.Remove(name);

                if (removed)
                {
                    ReloadOutput(phoneContacts);
                }
                else
                {
                    Console.WriteLine($"{name} hasn't been found. Not removed");
                }
            }
        }
    }
}
