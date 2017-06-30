using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TheDivisionUtility.TheDivision.Gear.Module.Validation
{
    public abstract class ViewModelPropertyValidatorBase<T> : IViewModelPropertyValidator<T>
    {
        private readonly Dictionary<string, List<Func<T, string>>> _properties;

        protected ViewModelPropertyValidatorBase()
        {
            _properties = new Dictionary<string, List<Func<T, string>>>();
            Initialize();
        }

        public IList<string> GetErrorsForProperty(T instance, string propertyName)
        {
            /////instance.Guard();

            if (propertyName == null || !_properties.ContainsKey(propertyName))
            {
                return new List<string>();
            }

            return
                _properties[propertyName].Select(test => test(instance))
                    .Where(err => !string.IsNullOrEmpty(err))
                    .ToList();
        }

        protected abstract void ApplyRule();

        protected void Rule(string propertyName, Func<T, bool> isWrong, string message)
        {
            _properties[Get(propertyName)].Add(rule => isWrong(rule) ? message : null);
        }

        protected void Rule(string propertyName, Func<T, bool> isWrong, Func<T, string> getMessage)
        {
            _properties[Get(propertyName)].Add(rule => isWrong(rule) ? getMessage(rule) : null);
        }

        protected void Rule(Expression<Func<T, object>> propertyName, Func<T, bool> isWrong, Func<T, string> getMessage)
        {
            _properties[Get(propertyName)].Add(rule => isWrong(rule) ? getMessage(rule) : null);
        }

        protected void Rule(Expression<Func<T, object>> propertyName, Func<T, bool> isWrong, string message)
        {
            _properties[Get(propertyName)].Add(rule => isWrong(rule) ? message : null);
        }

        private string Get(string propertyName)
        {
            ////propertyName.Guard();

            if (!_properties.ContainsKey(propertyName))
            {
                _properties.Add(propertyName, new List<Func<T, string>>());
            }

            return propertyName;
        }

        private string Get(Expression<Func<T, object>> propertyName)
        {
            var lambda = propertyName as LambdaExpression;
            var expression = lambda.Body as UnaryExpression;
            var memberExpression = expression != null
                                       ? expression.Operand as MemberExpression
                                       : lambda.Body as MemberExpression;

            if (memberExpression == null || memberExpression.Member == null)
            {
                throw new ArgumentException("The body must be a member or unary expression");
            }

            return Get(memberExpression.Member.Name);
        }

        private void Initialize()
        {
            ApplyRule();
        }
    }
}
