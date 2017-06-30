using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TheDivisionUtility.TheDivision.Gear.Module.Validation
{
    public sealed class PropertyValidator<T> : IPropertyValidator<T>
    {
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        private readonly Lazy<List<IViewModelPropertyValidator<T>>> _validators;

        public PropertyValidator(IEnumerable<IViewModelPropertyValidator<T>> validators)
        {
            _validators = new Lazy<List<IViewModelPropertyValidator<T>>>(
                () =>
                {
                    var viewModelPropertyValidators = validators as IViewModelPropertyValidator<T>[]
                                                      ?? validators.ToArray();
                    return validators != null && viewModelPropertyValidators.Any()
                               ? viewModelPropertyValidators.ToList()
                               : new List<IViewModelPropertyValidator<T>>();
                });
        }

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <returns>
        /// true if the entity currently has validation errors; otherwise, false.
        /// </returns>
        public bool HasErrors
        {
            get
            {
                return _errors.Values.Where(list => list != null).Any(list => list.Any());
            }
        }

        /// <summary>
        ///     Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName"> The name of the property to retrieve validation errors for; or null or System.String.Empty, to retrieve entity-level errors. </param>
        /// <returns>The validation errors for the property or entity.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return null;
            }

            List<string> propErrors;
            _errors.TryGetValue(propertyName, out propErrors);
            return propErrors;
        }

        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        /// <param name="viewModel">The viewModel for the properties</param>
        /// <param name="propertyName"> The name of the property to retrieve validation errors for; or null or System.String.Empty, to retrieve entity-level errors. </param>
        /// <param name="errorsChanged">The validation errors event handler from INotifyDataErrorInfo that will run on all error-ed properties.</param>
        public void Validate(T viewModel, string propertyName, EventHandler<DataErrorsChangedEventArgs> errorsChanged)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }

            _errors[propertyName] =
                _validators.Value.SelectMany(val => val.GetErrorsForProperty(viewModel, propertyName)).ToList();

            var @event = errorsChanged;
            if (@event != null)
            {
                @event(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }
    }
}
