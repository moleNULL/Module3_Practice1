using System.Collections;
using PhoneContactProject.Interfaces;

namespace PhoneContactProject
{
    internal class PhoneContactEnumerator : IEnumerator<PhoneRecord>
    {
        private List<PhoneRecord> _phones;
        private int _position = -1;

        public PhoneContactEnumerator(Dictionary<char, List<PhoneRecord>> dictPhones)
        {
            _phones = ConvertDictionaryToList(dictPhones);
        }

        public PhoneRecord Current
        {
            get
            {
                if (_position == -1 || _position >= _phones.Count)
                {
                    throw new IndexOutOfRangeException();
                }

                return _phones[_position];
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public bool MoveNext()
        {
            _position++;

            if (_position < _phones.Count)
            {
                return true;
            }

            return false;
        }

        public void Reset() => _position = -1;

        public void Dispose()
        {
        }

        private List<PhoneRecord> ConvertDictionaryToList(Dictionary<char, List<PhoneRecord>> dictPhones)
        {
            List<PhoneRecord> phones = new List<PhoneRecord>(dictPhones.Count);

            foreach (var dictPhone in dictPhones)
            {
                foreach (var phone in dictPhone.Value)
                {
                    phones.Add(phone);
                }
            }

            return phones;
        }
    }
}
