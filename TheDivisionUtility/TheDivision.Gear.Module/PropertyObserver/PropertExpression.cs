using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TheDivisionUtility.TheDivision.Gear.Module.PropertyObserver
{
    public class PropertyExpression<T>
        where T : class
    {
        private readonly WeakReference<T> _reference;

        private readonly MethodInfo _method;

        private readonly Type _delegateType;

        private readonly Type _eventType;

        public PropertyExpression(WeakReference<T> reference, Delegate @delegate)
        {
            _reference = reference;
            _eventType = typeof(T);
            _method = @delegate.GetMethodInfo();
            _delegateType = @delegate.GetType();
        }

        public Action<T> Method
        {
            get
            {
                return (Action<T>)Target;
            }
        }

        public Type EventType
        {
            get
            {
                return _eventType;
            }
        }

        public bool IsAlive
        {
            get
            {
                T target;
                return _reference.TryGetTarget(out target);
            }
        }

        public Delegate Target
        {
            get
            {
                return TryGetDelegate();
            }
        }

        internal virtual async Task FinalizedTask(T eventDataTask)
        {
            Method(eventDataTask);
            await Task.FromResult(0);
        }

        protected int DelegateGetHashCode(Delegate obj)
        {
            if (obj == null)
            {
                return 0;
            }

            int result = obj.Method.GetHashCode() ^ obj.GetType().GetHashCode();
            if (obj.Target != null)
            {
                result ^= RuntimeHelpers.GetHashCode(obj);
            }

            return result;
        }

        private Delegate TryGetDelegate()
        {
            if (_method.IsStatic)
            {
                return _method.CreateDelegate(_delegateType, null);
            }

            T target;

            if (_reference.TryGetTarget(out target))
            {
                return _method.CreateDelegate(_delegateType, target);
            }

            return null;
        }
    }
}
