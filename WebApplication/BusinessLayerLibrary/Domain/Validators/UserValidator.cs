using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayerLibrary.Domain.Model;
using BusinessLayerLibrary.Resources;
using BusinessLayerLibrary.Logic.Common.Validations;

namespace BusinessLayerLibrary.Domain.Validators
{
    public class UserValidator: Validator<User>
    {
        public UserValidator(User user)
          : base(user)
        { }

        public ValidationIssue RequiredName()
        {
            if (!String.IsNullOrWhiteSpace(Entity.Login))
                return ValidationIssue.Valid;

            return RegisterPropertyIssue(o => o.Name, ValidationMessages.Required);
        }

        public ValidationIssue RequiredPasswordHash()
        {
            if ((Entity.PasswordHash.Length>5))
                return ValidationIssue.Valid;

            return RegisterPropertyIssue(o => o.Name, ValidationMessages.MoreSymbols);
        }
   
        #region Required

        public override ICollection<ValidationIssue> Validate()
        {
            using (var container = new IssueContainer(this))
            {
                RequiredName();
                RequiredPasswordHash();
                
                return container.ToList();
            }
        }


        #endregion
    }
}
