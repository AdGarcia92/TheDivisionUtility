using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDivisionUtility.TheDivision.Gear.Module.PropertyObserver
{
    public abstract class PropertyObserverBuilderBase<T>
        where T : class
    {
        protected readonly WeakReference<T> Instance;

        public PropertyObserverBuilderBase(WeakReference<T> instance)
        {
            Instance = instance;
        }

        public string Name { get; protected set; }

        public PropertyExpression<T> OnPropertyChangedExpression { get; protected set; }

        public PropertyExpression<T> OnPropertyChangingExpression { get; protected set; }
    }
}
