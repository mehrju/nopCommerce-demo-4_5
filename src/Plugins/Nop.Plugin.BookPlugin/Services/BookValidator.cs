using FluentValidation;
using Nop.Plugin.BookPlugin.Domains;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.BookPlugin.Service
{
    public partial class BookValidator : BaseNopValidator<Book>
    {
        public BookValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessageAwait(localizationService.GetResourceAsync("Name Required"));

            RuleFor(x => x.Author)
                .NotEmpty()
                .WithMessageAwait(localizationService.GetResourceAsync("Author Required"));

            RuleFor(x => x.PublishDate)
                .NotEmpty()
                .WithMessageAwait(localizationService.GetResourceAsync("Publish Date Required"));
        }
    }
}
