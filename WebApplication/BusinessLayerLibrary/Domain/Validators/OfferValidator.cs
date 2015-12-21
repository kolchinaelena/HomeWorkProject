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
    class OfferValidator: Validator<Offer>
    {
        public OfferValidator(Offer offer)
          : base(offer)
        { }

        #region Required
        public ValidationIssue RequiredNameOffer()
        {
            if (!String.IsNullOrWhiteSpace(Entity.NameOffer))
                return ValidationIssue.Valid;

            return RegisterPropertyIssue(o => o.NameOffer, ValidationMessages.Required);
        }

        public ValidationIssue RequiredTypeOffer()
        {
            if (Entity.Type!=0)
                return ValidationIssue.Valid;

            return RegisterPropertyIssue(o => o.Type, ValidationMessages.Required);
        }
        public ValidationIssue RequiredOfferState()
        {
            if (Entity.State != null)
                return ValidationIssue.Valid;

            return RegisterPropertyIssue(o => o.State, ValidationMessages.Required);
        }
        public ValidationIssue RequiredValidOfferDate()
        {
            if (Entity.Date<=DateTime.Now)
                return ValidationIssue.Valid;
            
            return RegisterPropertyIssue(o => o.Date, ValidationMessages.WrongDate);
        }

        public override ICollection<ValidationIssue> Validate()
        {
            using (var container = new IssueContainer(this))
            {
                RequiredNameOffer();
                RequiredTypeOffer();
                RequiredValidOfferDate();

                return container.ToList();
            }
        }


        #endregion
    }
}
