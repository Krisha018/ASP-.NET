using APIDemo.Models;
using FluentValidation;


namespace APIDemo.Validation
{
    public class Validation : AbstractValidator<PersonModel>
    {
        public Validation() 
        {
            RuleFor(PersonModel=>PersonModel.Name).NotEmpty();
            RuleFor(PersonModel => PersonModel.Email).NotEmpty().EmailAddress();
            RuleFor(PersonModel => PersonModel.Contact).NotEmpty();
            RuleFor(PersonModel => PersonModel.Password).NotEmpty();
            RuleFor(PersonModel => PersonModel.ConfirmPassword).NotEmpty();
        }
    }
}
