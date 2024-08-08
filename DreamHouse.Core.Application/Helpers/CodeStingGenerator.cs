namespace DreamHouse.Core.Application.Helpers
{
    public static class CodeStingGenerator
    {
        private static Random random = new Random();
        public const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
        public const string Numbers = "0123456789";
        public const string SpecialCharacters = "!@#$%^&*()_+[]{}|;:,.<>?";

        public static string GeneratePassword()
        {
            string password =
                Convert.ToChar(GetRandomCharacter(Uppercase)).ToString() +
                Convert.ToChar(GetRandomCharacter(Lowercase)).ToString() +
                Convert.ToChar(GetRandomCharacter(Numbers)).ToString() +
                Convert.ToChar(GetRandomCharacter(SpecialCharacters)).ToString();

            string allCharacters = Uppercase + Lowercase + Numbers + SpecialCharacters;
            for (int i = password.Length; i < 12; i++)
            {
                password += GetRandomCharacter(allCharacters);
            }

            return new string(password.ToCharArray().OrderBy(c => random.Next()).ToArray());
        }

        public static string GenerateRandomLetters(int length, string letters)
        {
            string stringGenerated = string.Empty;

            for (int i = 0; i < length; i++)
            {
                stringGenerated += GetRandomCharacter(letters);
            }

            return stringGenerated;
        }

        private static char GetRandomCharacter(string characters)
        {
            int index = random.Next(characters.Length);
            return characters[index];
        }
    }
}
