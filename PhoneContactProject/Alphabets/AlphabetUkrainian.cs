using PhoneContactProject.Interfaces;

namespace PhoneContactProject.Alphabets
{
    internal class AlphabetUkrainian : IAlphabet
    {
        public AlphabetUkrainian()
        {
            Letters = FillLetters();
        }

        public char[] Letters { get; }

        private char[] FillLetters()
        {
            char[] alphabet = new char[33]
            {
                'А', 'Б', 'В', 'Г', 'Ґ',
                'Д', 'Е', 'Є', 'Ж', 'З',
                'И', 'І', 'Ї', 'Й', 'К',
                'Л', 'М', 'Н', 'О', 'П',
                'Р', 'С', 'Т', 'У', 'Ф',
                'Х', 'Ц', 'Ч', 'Ш', 'Щ',
                'Ь', 'Ю', 'Я'
            };

            return alphabet;
        }
    }
}
