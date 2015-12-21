using System;
using System.Linq.Expressions;

namespace BusinessLayerLibrary.Logic.Common.Validations
{
    public class PropertyIssue<T>: ValidationIssue
    {
        public Expression<Func<T, object>> PropertyFunc { get; private set; }
        public T Object { get; private set; }

        public PropertyIssue(T obj, Expression<Func<T, Object>> propertyFunc, String description)
            :base(description)
        {
            Object = obj;
            PropertyFunc = propertyFunc;
        }
    }
}