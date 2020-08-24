using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using PostSharp.Aspects;
using StoreSystem.Core.CrossCuttingConcerns.Postsharp.Validation.FluentValidation;

namespace StoreSystem.Core.Aspects.Postsharp.Validation
{
    public class FluentValidationAspect : OnMethodBoundaryAspect
    {
        Type _validatorType;

        public FluentValidationAspect(Type validatorType)
        {
            _validatorType = validatorType;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var validator = (IValidator) Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = args.Arguments.Where(t => t.GetType() == entityType);

            foreach (var entity in entities)
            {
                ValidatorToolPostsharp.FluentValidate(validator, entity);
            }
        }
    }
}
