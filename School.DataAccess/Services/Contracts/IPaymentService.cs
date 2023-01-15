namespace School.DataAccess.Services.Contracts;

/// <summary>
/// Serwis służący do interakcji z opłatami
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Metoda zwracająca listę wszystkich płatności
    /// </summary>
    /// <returns></returns>
    Task<List<PaymentModel>> GetAllPayments();

    /// <summary>
    /// Metoda pobierająca płatność po ID płatności
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<PaymentModel> GetPayment(int id);

    /// <summary>
    /// Metoda dodająca nową płatność
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    Task AddPayment(PaymentDTO payment);

    /// <summary>
    /// Metoda usuwająca istniejącą płatność
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeletePayment(int id);
}
