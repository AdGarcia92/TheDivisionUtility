using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDivisionUtility.TheDivision.Gear.Module.Validation
{
    public interface IViewModelPropertyValidator<in T>
    {
        IList<string> GetErrorsForProperty(T instance, string propertyName);
    }
}
