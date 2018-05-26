using JetBrains.Annotations;

namespace WebApp.Models
{
    public class Error
    {
        public string Code { get; }
        public string Description { get; }

        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public override string ToString()
        {
            return $"{Code}|{Description}";
        }

        public static Error[] Create(string code, string description) => Create(new Error(code, description));
        public static Error[] Create(Error error) => new[] {error};
        [CanBeNull]
        public static Error[] NoError => null;
    }
}