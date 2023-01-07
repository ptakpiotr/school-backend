namespace School.DataAccess.Services.Contracts
{
    public interface IPaymentService
    {
        Task<List<PaymentModel>> GetAllPayments();
        Task<PaymentModel> GetPayment(int id);
        Task AddPayment(PaymentDTO payment);
        Task DeletePayment(int id);
    }
}
