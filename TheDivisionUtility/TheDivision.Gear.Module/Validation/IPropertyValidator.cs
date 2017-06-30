using System;
using System.Collections;
using System.ComponentModel;

namespace TheDivisionUtility.TheDivision.Gear.Module.Validation
{
    public interface IPropertyValidator<T>
    {
        bool HasErrors { get; }

        IEnumerable GetErrors(string propertyName);

        void Validate(T viewModel, string propertyName, EventHandler<DataErrorsChangedEventArgs> errorsChanged);
    }
}
