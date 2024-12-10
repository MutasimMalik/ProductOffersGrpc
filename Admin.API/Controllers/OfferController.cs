using Admin.API.Models;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using OfferGrpcService.Protos;

namespace Admin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly GrpcChannel _channel;
        private readonly OfferGrpcService.Protos.OfferGrpcService.OfferGrpcServiceClient _client;
        private readonly IConfiguration _configuration;

        public OfferController(IConfiguration configuration)
        {
            _configuration = configuration;
            _channel = GrpcChannel.ForAddress(_configuration.GetValue<string>("GrpcSettings:OfferServiceUrl")!);
            _client = new OfferGrpcService.Protos.OfferGrpcService.OfferGrpcServiceClient(_channel);
        }

        [HttpGet("GetOfferList")]
        public async Task<IActionResult> GetOfferListAsync()
        {
            try
            {
                var result = await _client.GetOfferListAsync(new Empty { });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetOfferById")]
        public async Task<IActionResult> GetOfferByIdAsync(int id)
        {
            try
            {
                var request = new GetOfferDetailRequest
                {
                    ProductId = id
                };

                var result = await _client.GetOfferAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddOffer")]
        public async Task<IActionResult> AddOfferAsync(Offer offer)
        {
            try
            {
                var offerRequest = new OfferDetail
                {
                    Id = offer.Id,
                    ProductName = offer.ProductName,
                    OfferDescription = offer.OfferDescription
                };

                var request = new AddOfferDetailRequest
                {
                    Offer = offerRequest
                };

                var response = await _client.AddOfferAsync(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateOffer")]
        public async Task<IActionResult> UpdateOfferAsync(Offer offer)
        {
            try
            {
                var offerRequest = new OfferDetail
                {
                    Id = offer.Id,
                    ProductName = offer.ProductName,
                    OfferDescription = offer.OfferDescription
                };

                var request = new UpdateOfferDetailRequest
                {
                    Offer = offerRequest
                };

                var response = await _client.UpdateOfferAsync(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteOffer")]
        public async Task<IActionResult> DeleteOfferAsync(int id)
        {
            try
            {
                var deleteRequest = new DeleteOfferRequest
                {
                    ProductId = id
                };

                var response = await _client.DeleteOfferAsync(deleteRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
