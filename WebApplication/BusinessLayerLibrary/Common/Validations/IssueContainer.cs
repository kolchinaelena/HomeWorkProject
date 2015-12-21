using System;
using System.Collections.ObjectModel;

namespace BusinessLayerLibrary.Logic.Common.Validations
{
    // Строго типизированная коллекция ValidationIssue
    public class IssueContainer : Collection<ValidationIssue>, IDisposable
    {
        public IValidator Validator { get; private set; }

        public IssueContainer(IValidator validator)
        {
            Validator = validator;
            Validator.Issue += OnIssue;
        }

        private void OnIssue(ValidationIssue issue)
        {
            Add(issue);
        }

        public void Dispose()
        {
            Validator.Issue -= OnIssue;
        }
    }
}