using AutoMapper;
using gRPC.Discount.Entities;
using gRPC.Discount.Protos;

namespace gRPC.Discount.Mapper
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
