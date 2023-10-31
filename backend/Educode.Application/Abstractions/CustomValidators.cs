using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Educode.Domain.Shared.Error;

namespace Educode.Application.Abstractions
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, TProperty> NotEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string fieldName)
        {
            return DefaultValidatorExtensions.NotEmpty(ruleBuilder).WithMessage(Errors.General.IsRequiredError(fieldName).Serialize());
        }
    }
}
