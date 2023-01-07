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
            await CRUDHelper.Add<PaymentModel, PaymentDTO>(_mainConn, payment);
        }

        public async Task DeletePayment(int id)
        {
            await CRUDHelper.Delete<PaymentModel>(_mainConn, id);
        }

        public async Task<List<PaymentModel>> GetAllPayments()
        {
            return await CRUDHelper.GetAll<PaymentModel>(_mainConn);
        }

        public async Task<PaymentModel> GetPayment(int id)
        {
            return await CRUDHelper.GetOne<PaymentModel>(_mainConn, id);
        }
    }
}
