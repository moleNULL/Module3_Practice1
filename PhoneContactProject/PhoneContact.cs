using System.Globalization;
using System.Collections;
using PhoneContactProject.Interfaces;
using PhoneContactProject.Alphabets;

namespace PhoneContactProject
{
    internal class PhoneContact : IMyCollection, IEnumerable<KeyValuePair<char, List<PhoneRecord>>>
    {
        private readonly List<CultureInfo> _cultures;
        private Dictionary<char, List<PhoneRecord>> _phones;

        public PhoneContact(Config config)
        {
            _cultures = config.GetCultureInfo().ToList();

            _phones = FillPhoneContactsWithLetters();
        }

        public void Add(IRecord record)
        {
            // we compare letter in upper-case only
            // but save contatc in any case
            char phoneLetter = char.ToUpper(record.Name[0]);

            bool constains = _phones.ContainsKey(phoneLetter);
            if (constains)
            {
                _phones[phoneLetter].Add((PhoneRecord)record);
            }
            else
            {
                _phones['#'].Add((PhoneRecord)record);
            }
        }

        public bool Remove(string name)
        {
            foreach (var phone in _phones)
            {
                foreach (var record in phone.Value)
                {
                    if (record.Name == name)
                    {
                        phone.Value.Remove(record);

                        return true;
                    }
                }
            }

            return false;
        }

        public void Print()
        {
            foreach (var phone in _phones)
            {
                // prevent printing letters that have no records
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

        public IEnumerator<KeyValuePair<char, List<PhoneRecord>>> GetEnumerator()
        {
            return _phones.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        // Fill dictionary: letterList[i] -> Dictionary.[Key]
        private Dictionary<char, List<PhoneRecord>> FillPhoneContactsWithLetters()
        {
            List<char> letterList = GenerateLettersList();
            var phones = new Dictionary<char, List<PhoneRecord>>(letterList.Count);

            for (int i = 0; i < letterList.Count; i++)
            {
                phones[letterList[i]] = new List<PhoneRecord>();
            }

            return phones;
        }

        // Concatenate English and Ukrainian alphabets + 0-9 range + #
        private List<char> GenerateLettersList()
        {
            List<char> letterList = new List<char>();

            // English alphabet is used by default
            letterList.AddRange(new AlphabetEnglish().Letters);

            // Add Ukrainian alphabet if exists right after English
            if (_cultures.Contains(new CultureInfo("uk-UA")))
            {
                letterList.AddRange(new AlphabetUkrainian().Letters);
                CultureInfo.CurrentCulture = new CultureInfo("uk-UA");
            }

            // US English is culture by default if no other is present
            if (_cultures.Count == 0)
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-US");
            }

            // Add numbers 0-9
            for (int i = '0'; i <= '9'; i++)
            {
                letterList.Add(Convert.ToChar(i));
            }

            // names other than English, Ukrainian or starting with 0-9 should be in '#' category
            letterList.Add('#');

            return letterList;
        }
    }
}
