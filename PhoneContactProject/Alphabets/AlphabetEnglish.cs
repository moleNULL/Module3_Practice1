namespace PhoneContactProject.Alphabets
{
    internal class AlphabetEnglish : IAlphabet
    {
        public AlphabetEnglish()
        {
            Letters = FillLetters();
        }

        public char[] Letters { get; }

        private char[] FillLetters()
        {
            char[] alphabet = new char[26];

            // fill with letters: A-Z
            for (int i = 0, j = 'A'; i < alphabet.Length; i++)
            {
                alphabet[i] = Convert.ToChar(j++);
            }

            return alphabet;
        }
    }
}
