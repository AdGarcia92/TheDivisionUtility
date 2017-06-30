using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TheDivisionUtility.TheDivision.Gear.Module.PropertyObserver
{
    public class ObserverBuilder<T>
       where T : class
    {
        private readonly WeakReference<T> _observed;

        private readonly List<PropertyObserverBuilderBase<T>> _properties;

        public ObserverBuilder(T observed)
        {
            _observed = new WeakReference<T>(observed);
            _properties = new List<PropertyObserverBuilderBase<T>>();
        }

        public PropertyObserverBuilder<T, TProperty> Property<TProperty>(
            Expression<Func<T, TProperty>> propertyExpression)
        {
            var prop = new PropertyObserverBuilder<T, TProperty>(propertyExpression, _observed);
            AttachPropertyObserve(prop);
            return prop;
        }

        public void CompileQuery()
        {
            T target;
            if (_observed.TryGetTarget(out target))
            {
                var inpc = target as INotifyPropertyChanged;
                if (inpc != null)
                {
                    inpc.PropertyChanged -= OnInpcOnPropertyChanged;
                    inpc.PropertyChanged += OnInpcOnPropertyChanged;
                }
            }
        }

        private async void OnInpcOnPropertyChanged(object s, PropertyChangedEventArgs ea)
        {
            foreach (
                PropertyObserverBuilderBase<T> source in
                    _properties.Where(x => x.Name == ea.PropertyName && x.OnPropertyChangedExpression.IsAlive))
            {
                T target;
                if (_observed.TryGetTarget(out target))
                {
                    await source.OnPropertyChangedExpression.FinalizedTask(target);
                }
            }
        }

        private void AttachPropertyObserve(PropertyObserverBuilderBase<T> prop)
        {
            _properties.Add(prop);
        }
    }
}
