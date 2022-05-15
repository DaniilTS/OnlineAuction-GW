using OnlineAuction.DBAL.Repositories;
using System;

namespace OnlineAuction.API.Services
{
    public class OfferService
    {
        private readonly OfferRepository offerRepository;
        public OfferService(IServiceProvider sp) 
        {
            //offerRepository = sp.GetService<OfferRepository>();
        }
    }
}
