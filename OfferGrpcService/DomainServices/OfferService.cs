using OfferGrpcService.Models;
using OfferGrpcService.Repositories;

namespace OfferGrpcService.DomainServices
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;

        public OfferService(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<Offer> AddAsync(Offer offer)
        {
            return await _offerRepository.AddAsync(offer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _offerRepository.DeleteAsync(id);
        }

        public async Task<List<Offer>> GetAllAsync()
        {
            return await _offerRepository.GetAllAsync();
        }

        public async Task<Offer> GetByIdAsync(int id)
        {
            return await _offerRepository.GetByIdAsync(id);
        }

        public async Task<Offer> UpdateAsync(Offer offer)
        {
            return await _offerRepository.UpdateAsync(offer);
        }
    }
}
