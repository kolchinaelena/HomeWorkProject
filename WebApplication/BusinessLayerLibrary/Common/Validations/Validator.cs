using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BusinessLayerLibrary.Logic.Common.Validations
{
    /// <summary> Базовый класс валидаторов </summary>
    public abstract class Validator<T> : IValidator where T : class
    {
        /// <summary> Событие регистрации нового issue </summary>
        public event Action<ValidationIssue> Issue;

        protected Validator(T entity = null)
        {
            Entity = entity;
        }

        /// <summary> Комплексная валидация сущности Entity </summary>
        public abstract ICollection<ValidationIssue> Validate();

        /// <summary> Валидируемая сущность </summary>
        public T Entity { get; private set; }

        /// <summary> Валидируемая сущность != null </summary>
        public void SetEntity(T entity)
        {
            if (entity == null)
                throw new NullReferenceException("Entity is null");

            Entity = entity;
        }

        /// <summary> Генерирование события Issue, возвращение того же issue </summary>
        public ValidationIssue RegisterIssue(ValidationIssue issue)
        {
            var handler = Issue;
            if (handler != null)
                handler(issue);

            return issue;
        }

        //TODO: в экстеншен
        public ValidationIssue RegisterPropertyIssue(Expression<Func<T, Object>> propertyFunc, String description)
        {
            return RegisterIssue(new PropertyIssue<T>(Entity, propertyFunc, description));
        }
    }
}