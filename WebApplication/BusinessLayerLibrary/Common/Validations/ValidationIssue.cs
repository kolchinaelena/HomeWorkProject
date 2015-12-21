using System;

namespace BusinessLayerLibrary.Logic.Common.Validations
{
    /// <summary> Ошибка валидации </summary>
    public class ValidationIssue
    {
        public static ValidationIssue Valid
        {
            get { return new ValidationIssue(); }
        }

        public ValidationIssue()
        {
            IsValid = true;
        }
        public ValidationIssue(String description)
        {
            IsValid = false;
            this.description = description;         
        }

        public Boolean IsValid { get; private set; }

        private readonly String description;
        public virtual String Description
        {
            get { return description; }
        }
    }
}