using System;
using System.Threading.Tasks;

namespace TheDivisionUtility.TheDivision.Gear.Module.PropertyObserver
{
    public class AsyncPropertyExpression<T> : PropertyExpression<T>
       where T : class
    {
        public AsyncPropertyExpression(WeakReference<T> reference, Delegate @delegate)
            : base(reference, @delegate)
        {
        }

        public new Func<T, Task> Method
        {
            get
            {
                return (Func<T, Task>)Target;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return Equals((AsyncPropertyExpression<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Method != null ? DelegateGetHashCode(Method) : 0;
            }
        }

        internal override async Task FinalizedTask(T eventDataTask)
        {
            await Method(eventDataTask);
        }

        private bool Equals(AsyncPropertyExpression<T> other)
        {
            return Method.Equals(other.Method);
        }
    }
}
