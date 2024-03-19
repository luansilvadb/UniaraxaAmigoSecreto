using System.Text.RegularExpressions;

namespace Uniaraxa.AmigoSecreto.Core.Validador
{
    public class ValidadorEmail
    {
        private readonly string _email;

        public ValidadorEmail(string email)
        {
            _email = email;
        }

        public bool EmailEValido()
        {
            if (string.IsNullOrWhiteSpace(_email))
            {
                return false;
            }

            string padrao = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var regex = new Regex(padrao);

            return regex.IsMatch(_email);
        }
    }
}
