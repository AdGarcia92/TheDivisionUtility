using System;
using System.Collections.Generic;

using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;
using PostSharp.Patterns.Model;

namespace TheDivisionUtility.TheDivision.Gear.Module.Validation
{
    [AspectTypeDependency(AspectDependencyAction.Order, AspectDependencyPosition.After,
        typeof(NotifyPropertyChangedAttribute))]
    [Serializable]
    public sealed class NotifyPropertyValidatorAttribute : LocationInterceptionAspect
    {
        private readonly List<string> _linkedPropertyNames;

        public NotifyPropertyValidatorAttribute(params string[] linkedPropertyNames)
        {
            _linkedPropertyNames = new List<string>(linkedPropertyNames);
        }

        public override void OnSetValue(LocationInterceptionArgs args)
        {
            if (args.Value != args.GetCurrentValue())
            {
                args.ProceedSetValue();

                Validate(args.Instance as INotifyDataErrorInfoPropertyValidator, args.LocationName);

                foreach (var linkedPropertyName in _linkedPropertyNames)
                {
                    Validate(args.Instance as INotifyDataErrorInfoPropertyValidator, linkedPropertyName);
                }
            }
        }

        private void Validate(INotifyDataErrorInfoPropertyValidator viewModel, string propertyName)
        {
            if (viewModel != null && propertyName != null)
            {
                viewModel.Validate(propertyName);
            }
        }
    }
}

