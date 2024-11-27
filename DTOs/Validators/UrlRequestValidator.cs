using Azure.Core;
using FluentValidation;

namespace UrlShortener.DTOs.Validators
{
    public class UrlRequestValidator : AbstractValidator<UrlRequest>
    {
        public UrlRequestValidator()
        {
            RuleFor(x => x.Url).Must(x => Uri.TryCreate(x, UriKind.Absolute, out _)).WithMessage("Incorrect URL");
        }
    }
}
