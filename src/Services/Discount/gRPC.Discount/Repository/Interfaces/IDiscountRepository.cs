using gRPC.Discount.Entities;

namespace gRPC.Discount.Repository.Interfaces
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);
        Task<IEnumerable<Coupon>> GetAllDiscounts();

        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);
    }
}
