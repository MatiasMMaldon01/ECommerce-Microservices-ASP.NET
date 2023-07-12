using gRPC.Discount.Protos;

namespace Api.Basket.gRPCDiscountService
{
    public class gRPCDiscountService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public gRPCDiscountService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}
