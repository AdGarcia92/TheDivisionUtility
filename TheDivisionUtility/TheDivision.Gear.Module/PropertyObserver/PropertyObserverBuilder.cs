using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TheDivisionUtility.TheDivision.Gear.Module.PropertyObserver
{
    public sealed class PropertyObserverBuilder<T, TProperty> : PropertyObserverBuilderBase<T>
            where T : class
    {
        private readonly Dictionary<ExpressionType, Func<Expression, string>> _fromExpressionType;

        public PropertyObserverBuilder(Expression<Func<T, TProperty>> propertyExpression, WeakReference<T> instance)
            : base(instance)
        {
            _fromExpressionType = new Dictionary<ExpressionType, Func<Expression, string>>
                                      {
                                          {
                                              ExpressionType.Parameter, x => ((ParameterExpression)x).Name
                                          },
                                          {
                                              ExpressionType.MemberAccess, x => ((MemberExpression)x).Member.Name
                                          },
                                          {
                                              ExpressionType.Call, x => ((MethodCallExpression)x).Method.Name
                                          }
                                      };

            Name = GetMemberName(propertyExpression);
        }

        public PropertyObserverBuilder<T, TProperty> OnPropertyChangedCall(Action<T> @delegate)
        {
            OnPropertyChangedExpression = new PropertyExpression<T>(Instance, @delegate);
            return this;
        }

        public PropertyObserverBuilder<T, TProperty> OnPropertyChangedCallAsync(Func<T, Task> @delegate)
        {
            OnPropertyChangedExpression = new AsyncPropertyExpression<T>(Instance, @delegate);
            return this;
        }

        public PropertyObserverBuilder<T, TProperty> OnPropertyChangingCall(Action<T> @delegate)
        {
            OnPropertyChangingExpression = new PropertyExpression<T>(Instance, @delegate);
            return this;
        }

        public PropertyObserverBuilder<T, TProperty> OnPropertyChangingCallAsync(Func<T, Task> @delegate)
        {
            OnPropertyChangingExpression = new AsyncPropertyExpression<T>(Instance, @delegate);
            return this;
        }

        private string GetMemberName(LambdaExpression memberSelector)
        {
            var currentExpression = memberSelector.Body;
            return _fromExpressionType[currentExpression.NodeType](currentExpression);
        }
    }
}
