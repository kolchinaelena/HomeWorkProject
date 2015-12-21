using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayerLibrary.DAL.Repositories;
using BusinessLayerLibrary.Domain.Model;

namespace BusinessLayerLibrary.DAL.EntityFramework.Repositories
{
    class OfferRepository: RepositoryBase ,IOfferRepository
    {
        public OfferRepository(ContextModel context, DbContextTransaction transaction)
            : base(context, transaction)
        {
        }

        public Offer Save(Offer offer)
        {
            int numEntr = -1;
            if (offer.IdOffer != 0)
            {
                Offer tempOffer = this.mContext.Offers.Where(c => c.IdOffer == offer.IdOffer).FirstOrDefault<Offer>();
                tempOffer.IdUser = offer.IdUser;
                tempOffer.NameOffer = offer.NameOffer;
                tempOffer.State = offer.State;
                tempOffer.Date = offer.Date;
                tempOffer.Description = offer.Description;
                tempOffer.Type = offer.Type;
                tempOffer.User = offer.User;
            }
            else
            {
                this.mContext.Offers.Add(offer);
            }
            numEntr = this.mContext.SaveChanges();
            if (numEntr > 0) return offer;
            return null;
        }
        public void Delete(Offer offer)
        {
            int numEntr = -1;
            this.mContext.Offers.Remove(offer);
            numEntr = this.mContext.SaveChanges();
        }

    }
}
