using PhoneContactProject.Interfaces;

namespace PhoneContactProject
{
    internal class PhoneRecord : IRecord, IComparable<PhoneRecord>
    {
        public PhoneRecord(string name, string number)
        {
            Name = name;
            Number = number;

            Letter = char.ToUpper(Name[0]);
        }

        public char Letter { get; }
        public string Name { get; }
        public string Number { get; }

        // for sort in List<PhoneRecord>
        public int CompareTo(PhoneRecord? other)
        {
            return Name.CompareTo(other?.Name);
        }
    }
}
