using Microsoft.EntityFrameworkCore;
using OfferGrpcService.Data;
using OfferGrpcService.Models;

namespace OfferGrpcService.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly OfferContext _context;

        public OfferRepository(OfferContext context)
        {
            _context = context;
        }

        public async Task<Offer> AddAsync(Offer offer)
        {
            var result = await _context.Offer.AddAsync(offer);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Offer.FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                throw new Exception("Object not found!!");

            _context.Offer.Remove(item);
            var result = await _context.SaveChangesAsync();

            if(result > 0)
                return true;

            return false;
        }

        public async Task<List<Offer>> GetAllAsync()
        {
            return await _context.Offer.ToListAsync();
        }

        public async Task<Offer> GetByIdAsync(int id)
        {
            var item = await _context.Offer.FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                throw new Exception("Object not found!!");

            return item;
        }

        public async Task<Offer> UpdateAsync(Offer offer)
        {
            var updatedOffer = _context.Offer.Update(offer);
            await _context.SaveChangesAsync();
            return updatedOffer.Entity;
        }
    }
}
