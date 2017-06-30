using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDivisionUtility.TheDivision.Gear.Module.PropertyObserver
{
    public interface IPropertyObserver<T>
         where T : class
    {
        void BuildObserver(ObserverBuilder<T> builder);
    }
}
