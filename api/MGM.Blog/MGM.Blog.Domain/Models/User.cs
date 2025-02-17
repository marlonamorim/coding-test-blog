using MGM.Blog.Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace MGM.Blog.Domain.Models
{
    public sealed class User : Entity
    {
        public string FirstName { get; private set; } = null!;

        public string LastName { get; private set; } = null!;

        public DateTime BirthDate { get; private set; }

        public string Email { get; private set; } = null!;

        public string Password { get; set; } = null!;

        public string TaxDocument { get; private set; } = null!;

        public IEnumerable<Post>? Posts { get; private set; }

        public User()
        {}

        public User(string firstName, string lastName, DateTime birthDate, string email, string password, string taxDocument, IEnumerable<Post>? posts)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Email = email;
            TaxDocument = taxDocument;
            Posts = posts;
            Password = password;
        }

        public static User Create(string firstName, string lastName, DateTime birthDate, string email, string password, string taxDocument)
        {
            ValidateInputs(firstName, lastName, email, password, taxDocument);
            return new User(firstName, lastName, birthDate, email, password, taxDocument, null);
        }

        public override string ToString()
            => $"{FirstName} {LastName}";

        public static bool IsValidEmail(string email) => Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

        public static bool IsValidTaxDocument(Cpf taxDocument) => Cpf.Validate(taxDocument);

        private static void ValidateInputs(string firstName, string lastName, string email, string password, Cpf taxDocument)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("FirstName cannot be null or empty.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("LastName cannot be null or empty.", nameof(lastName));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            if (!IsValidEmail(email))
                throw new ArgumentException("Email is not valid.", nameof(email));

            if (!IsValidTaxDocument(taxDocument))
                throw new ArgumentException("Cpf is not valid.", nameof(taxDocument));
        }
    }
}
