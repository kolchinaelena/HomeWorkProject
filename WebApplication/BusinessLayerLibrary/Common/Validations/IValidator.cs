using System;

namespace BusinessLayerLibrary.Logic.Common.Validations
{
    /// <summary> Интерфейс валидаторов </summary>
    public interface IValidator
    {
        /// <summary> Событие регистрации нового issue </summary>
        event Action<ValidationIssue> Issue;
    }
}