using MGM.Blog.Domain.Extensions;
using System.Text;

namespace MGM.Blog.Domain.ValueObjects
{
    public struct Cpf
    {
        private readonly string? _value;

        public static implicit operator string?(Cpf cpf)
        {
            return cpf.ToString();
        }

        public static implicit operator Cpf(string? value)
        {
            return new Cpf(value);
        }

        public Cpf(string? value)
        {
            _value = value?.OnlyDigits();
        }

        public override string? ToString()
        {
            return _value;
        }

        public readonly string? Format()
        {
            if (_value == null)
            {
                return null;
            }

            return new StringBuilder().Append(_value.AsSpan(0, 3)).Append('.').Append(_value.AsSpan(3, 3))
                .Append('.')
                .Append(_value.AsSpan(6, 3))
                .Append('-')
                .Append(_value.AsSpan(9, 2))
                .ToString();
        }

        public static bool Validate(string? cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }

            int[] array = (from x in cpf.Where(char.IsDigit)
                           select int.Parse(x.ToString())).ToArray();
            if (array.Length != 11)
            {
                return false;
            }

            if (array.Distinct().Count() == 1)
            {
                return false;
            }

            return true;
        }
    }
}
