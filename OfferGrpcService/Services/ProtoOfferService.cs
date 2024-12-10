using Grpc.Core;
using OfferGrpcService.DomainServices;
using OfferGrpcService.Models;
using OfferGrpcService.Protos;

namespace OfferGrpcService.Services
{
    public class ProtoOfferService : OfferGrpcService.Protos.OfferGrpcService.OfferGrpcServiceBase
    {
        /// <summary>
        /// This service is related to and implements the methods of .proto file
        /// The model we created in .proto files, should be the models we are returning
        /// That is the reason I converted the models to the models of .proto and returned them
        /// </summary>
        private readonly IOfferService _offerService;

        public ProtoOfferService(IOfferService offerService)
        {
            _offerService = offerService;
        }

        public override async Task<Offers> GetOfferList(Empty request, ServerCallContext context)
        {
            var offerDetail = new OfferDetail();

            var offersData = await _offerService.GetAllAsync();

            Offers response = new Offers();
            
            foreach (var offer in offersData)
            {
                offerDetail.Id = offer.Id;
                offerDetail.ProductName = offer.ProductName;
                offerDetail.OfferDescription = offer.OfferDescription;

                response.Items.Add(offerDetail);
            }

            return response;
        }

        public override async Task<OfferDetail> GetOffer(GetOfferDetailRequest request, ServerCallContext context)
        {
            var offerData = await _offerService.GetByIdAsync(request.ProductId);

            var offerDetail = new OfferDetail();
            offerDetail.Id = offerData.Id;
            offerDetail.ProductName = offerData.ProductName;
            offerDetail.OfferDescription = offerData.OfferDescription;

            return offerDetail;
        }

        public override async Task<OfferDetail> AddOffer(AddOfferDetailRequest request, ServerCallContext context)
        {
            var offer = new Offer(){ Id = request.Offer.Id, ProductName = request.Offer.ProductName, OfferDescription = request.Offer.OfferDescription};

            var response = await _offerService.AddAsync(offer);

            var offerDetail = new OfferDetail();
            offerDetail.Id = response.Id;
            offerDetail.ProductName = response.ProductName;
            offerDetail.OfferDescription = response.OfferDescription;

            return offerDetail;
        }

        public override async Task<OfferDetail> UpdateOffer(UpdateOfferDetailRequest request, ServerCallContext context)
        {
            var offer = new Offer() { Id = request.Offer.Id, ProductName = request.Offer.ProductName, OfferDescription = request.Offer.OfferDescription };

            var response = await _offerService.UpdateAsync(offer);

            var offerDetail = new OfferDetail();
            offerDetail.Id = response.Id;
            offerDetail.ProductName = response.ProductName;
            offerDetail.OfferDescription = response.OfferDescription;

            return offerDetail;
        }

        public override async Task<DeleteOfferResponse> DeleteOffer(DeleteOfferRequest request, ServerCallContext context)
        {
            var response = await _offerService.DeleteAsync(request.ProductId);

            var deleteResponse = new DeleteOfferResponse();

            if(response)
                deleteResponse.IsDeleted = true;
            else
                deleteResponse.IsDeleted = false;

            return deleteResponse;
        }
    }
}
