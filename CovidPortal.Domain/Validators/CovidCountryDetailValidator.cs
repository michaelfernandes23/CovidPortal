using CovidPortal.Domain.Entity;
using CovidPortal.Domain.Interfaces;
using FluentValidation;

namespace CovidPortal.Domain
{
    public class CovidCountryDetailValidator : AbstractValidator<CovidCountryDetail>
    {
        public CovidCountryDetailValidator(ICovidCountryDetailSqlRepository repository, OperationType operationType)
        {
            When(_ => operationType == OperationType.Add,
                 () =>
                 {
                     RuleFor(e => e.Name)
                         .NotEmpty()
                         .WithMessage($"{nameof(CovidCountryDetail.Name)} element cannot be empty");
                     RuleFor(e => e.CountryCode)
                        .NotEmpty()
                        .WithMessage($"{nameof(CovidCountryDetail.CountryCode)} element cannot be empty");
                 });
            When(_ => operationType == OperationType.Update,
                 () =>
                 {
                     RuleFor(e => e.Id)
                         .MustAsync(async (f, _) => await repository.HasDataAsync(x => x.Id == f))
                         .WithMessage("Id does not exist.");
                     RuleFor(e => e.Name)
                         .NotEmpty()
                         .WithMessage($"{nameof(CovidCountryDetail.Name)} element cannot be empty");
                     RuleFor(e => e.CountryCode)
                        .NotEmpty()
                        .WithMessage($"{nameof(CovidCountryDetail.CountryCode)} element cannot be empty");
                 });
            When(_ => operationType == OperationType.Delete,
                 () =>
                 {
                     RuleFor(e => e.Id)
                         .MustAsync(async (f, _) => await repository.HasDataAsync(x => x.Id == f))
                         .WithMessage("Id does not exist.");
                 });
        }
    }
}
