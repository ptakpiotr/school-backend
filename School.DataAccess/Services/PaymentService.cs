using Microsoft.Extensions.Options;
using Npgsql;
using School.DataAccess.Services.Contracts;
using System.Data;

namespace School.DataAccess.Services
{
    public class PaymentService : IPaymentService
    {

        private readonly string _mainConn;

        public PaymentService(IOptions<ConnectionStringOptions> options)
        {
            _mainConn = options.Value.MainConn;
        }

        public async Task AddPayment(PaymentDTO payment)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.InsertAsync("oplaty", payment);
            }
        }

        public async Task DeletePayment(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.InsertAsync("oplaty", id);
            }
        }

        public async Task<List<PaymentModel>> GetAllPayments()
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectAllAsync<PaymentModel>("v_all_payments");
            }
        }

        public async Task<PaymentModel> GetPayment(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectOneAsync<PaymentModel>("v_all_payments", id);
            }
        }
    }
}
