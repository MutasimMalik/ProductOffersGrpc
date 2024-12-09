using OfferGrpcService.Models;

namespace OfferGrpcService.Repositories
{
    public interface IOfferRepository
    {
        Task<List<Offer>> GetAllAsync();
        Task<Offer> GetByIdAsync(int id);
        Task<Offer> AddAsync(Offer offer);
        Task<Offer> UpdateAsync(Offer offer);
        Task<bool> DeleteAsync(int id);
    }
}
