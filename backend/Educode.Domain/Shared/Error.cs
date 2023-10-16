namespace Educode.Domain.Shared
{
    public sealed class Error : IEquatable<Error>
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "Result Value is Null");
        public static class Errors
        {
            public static class General
            {
                public static Error IsRequiredError()
                    => new Error("value.is.required", $"value is required");
                public static Error IsRequiredError(string value)
                    => new Error("value.is.required", $"{value} is required");

                public static Error LengthError()
                    => new Error("length.is.incorrect", "value length is incorrect");
                public static Error LengthError(string value, byte minLength)
                    => new Error("length.is.incorrect", $"{value} length should be at least ${minLength}");
                public static Error LengthError(string value, byte minLength, byte maxLength)
                    => new Error("length.is.incorrect", $"{value} length should be at least ${minLength} and ${maxLength} at most");
            }

            public static class Users
            {

            }
        }

        public string Code { get; }
        public string Message { get; }

        public Error(string code, string msg)
        {
            Code = code;
            Message = msg;
        }

        public static implicit operator string(Error error) => error.Code;

        public static bool operator ==(Error? a, Error? b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Code == b.Code;
        }

        public static bool operator !=(Error? a, Error? b)
        {
            if (a is null && b is null)
            {
                return false;
            }

            if (a is null || b is null)
            {
                return true;
            }

            return a.Code != b.Code;
        }

        public bool Equals(Error? other)
        {
            if (other is null || this is null) return false;

            if (other.GetType() != this.GetType()) return false;

            if (other.GetHashCode() != this.GetHashCode()) return false;

            return other.Code == other.Code;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Error);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}
