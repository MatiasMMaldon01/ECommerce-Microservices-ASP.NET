using Api.Discount.Entities;
using Api.Discount.Repository.Interfaces;
using Dapper;
using Npgsql;

namespace Api.Discount.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly NpgsqlConnection _connection;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = new NpgsqlConnection(_configuration.GetValue<string>("PostgresSettings:ConnectionString"));
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var coupon = await _connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE POSITION(@ProductName in ProductName) > 0", new { ProductName = productName });

            if (coupon == null)
                return new Coupon
                { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

            return coupon;
        }

        public async Task<IEnumerable<Coupon>> GetAllDiscounts()
        {
            var coupon = await _connection.QueryAsync<Coupon>
                ("SELECT * FROM Coupon");

            if (coupon == null)
                return new List<Coupon>() { new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" } };
         

            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {

            var affected =
                await _connection.ExecuteAsync("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {

            var affected = await _connection.ExecuteAsync("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {

            var affected = await _connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
