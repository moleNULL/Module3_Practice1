global using System.Globalization;
global using System.Collections;
global using PhoneContactProject.Interfaces;
global using PhoneContactProject.Alphabets;

namespace PhoneContactProject
{
    internal class PhoneContact : IMyCollection, IEnumerable<KeyValuePair<char, List<IRecord>>>
    {
        private readonly List<CultureInfo> _cultures;
        private Dictionary<char, List<IRecord>> _phones;

        public PhoneContact(Config config)
        {
            _cultures = config.GetCultureInfo().ToList();

            _phones = FillPhoneContactsWithLetters();
        }

        // Add record and sort after IF it is not a duplicate (the same Name, the same Number)
        public bool Add(IRecord record)
        {
            // we compare letter in upper-case only
            // but save contact in any case
            char phoneLetter = char.ToUpper(record.Name[0]);

            bool constains = _phones.ContainsKey(phoneLetter);
            if (!constains)
            {
                // if char is not in letterList -> replace it with the default char: '#'
                phoneLetter = '#';
            }

            if (IsValid(_phones[phoneLetter], record))
            {
                _phones[phoneLetter].Add(record);
                _phones[phoneLetter].Sort();

                return true;
            }

            return false;
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

        // Add 10 contacts by default
        public void FillWithInitialData()
        {
            Add(new PhoneRecord("Alison",   "+108954604147"));
            Add(new PhoneRecord("Amory",    "+109522226520"));
            Add(new PhoneRecord("Карина",   "+380639546058"));
            Add(new PhoneRecord("Савелій",  "+380957806148"));
            Add(new PhoneRecord("Настя",    "+380985293461"));

            Add(new PhoneRecord("Aaron",    "+106156692340"));
            Add(new PhoneRecord("Ын Ким",   "+850649945045"));
            Add(new PhoneRecord("Крістіна", "+380965420453"));
            Add(new PhoneRecord("4_Єгор",    "+380661234559"));
            Add(new PhoneRecord("John",     "+380501298412"));
        }

        public IEnumerator<KeyValuePair<char, List<IRecord>>> GetEnumerator()
        {
            return _phones.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        // Fill dictionary: letterList[i] -> Dictionary.[Key]
        private Dictionary<char, List<IRecord>> FillPhoneContactsWithLetters()
        {
            List<char> letterList = GenerateLettersList();
            var phones = new Dictionary<char, List<IRecord>>(letterList.Count);

            for (int i = 0; i < letterList.Count; i++)
            {
                phones[letterList[i]] = new List<IRecord>();
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

        // Validate record -> correct number and check if not a duplicate
        private bool IsValid(List<IRecord> records, IRecord record)
        {
            // get rid of unnecessary spaces
            record.Name = record.Name.Trim();
            record.Number = record.Number.Trim();

            // +380 564 - not allowed
            if (record.Number.Contains(" "))
            {
                return false;
            }

            // +38050.. -> 38050..
            if (!decimal.TryParse(record.Number[1..], out var _))
            {
                return false;
            }

            if (IsRecordDuplicate(records, record))
            {
                return false;
            }

            return true;
        }

        // check current record is not a duplicate (the same Name + the same Number)
        private bool IsRecordDuplicate(List<IRecord> records, IRecord record)
        {
            foreach (var currentRecord in records)
            {
                if (record.CompareTo(currentRecord) == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
