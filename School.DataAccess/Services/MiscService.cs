using Microsoft.Extensions.Options;
using Npgsql;
using School.DataAccess.Services.Contracts;
using System.Data;

namespace School.DataAccess.Services
{
    public class MiscService : IMiscService
    {
        private readonly string _mainConn;

        public MiscService(IOptions<ConnectionStringOptions> options)
        {
            _mainConn = options.Value.MainConn;
        }

        public async Task AddPaymentType(PaymentTypeDTO paymentType)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.InsertAsync("rodzaje_oplat", paymentType);
            }
        }

        public async Task AddRoom(RoomDTO room)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.InsertAsync("sale", room);
            }
        }

        public async Task DeletePaymentType(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.DeleteAsync("rodzaje_oplat", id);
            }
        }

        public async Task DeleteRoom(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.DeleteAsync("sale", id);
            }
        }

        public async Task<List<PaymentTypeModel>> GetPaymentTypes()
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectAllAsync<PaymentTypeModel>("rodzaje_oplat");
            }
        }

        public async Task<List<RoomModel>> GetRooms()
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectAllAsync<RoomModel>("sale");
            }
        }
    }
}
