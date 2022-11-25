namespace PhoneContactProject
{
    internal class PhoneRecord : IRecord
    {
        public PhoneRecord(string name, string number)
        {
            Name = name;
            Number = number;
        }

        public string Name { get; set; }
        public string Number { get; set; }

        // for List<PhoneRecord>().Sort()
        // initially compare Name with other.Name
        // if equal then compare Number with other.Number
        public int CompareTo(IRecord? other)
        {
            if (other is PhoneRecord ph)
            {
                if (string.Compare(Name, ph.Name) > 0)
                {
                    return 1;
                }
                else if (ph.Name == Name)
                {
                    if (string.Compare(Number, ph.Number) > 0)
                    {
                        return 1;
                    }
                    else if (Number == ph.Number)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                throw new Exception($"Must compare with PhoneRecord object " +
                    $"and not with {other?.GetType()}");
            }
        }
    }
}
