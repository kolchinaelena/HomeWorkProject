using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayerLibrary.Domain.Model;

namespace BusinessLayerLibrary.DAL.Repositories
{
    public interface IOfferRepository:IRepository<Offer>
    {
      //  ICollection<Offer> Save(ICollection<Offer> offers);
    }

}
