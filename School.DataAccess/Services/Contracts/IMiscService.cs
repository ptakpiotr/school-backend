namespace School.DataAccess.Services.Contracts;

/// <summary>
/// Serwis do interakcji z rodzajami opłat oraz z salami
/// </summary>
public interface IMiscService
{
    /// <summary>
    /// Metoda zwracająca rodzaje opłat
    /// </summary>
    /// <returns></returns>
    Task<List<PaymentTypeModel>> GetPaymentTypes();

    /// <summary>
    /// Metoda zwracająca listę sal
    /// </summary>
    /// <returns></returns>
    Task<List<RoomModel>> GetRooms();

    /// <summary>
    /// Metoda dodająca nowy rodzaj opłat
    /// </summary>
    /// <param name="paymentType"></param>
    /// <returns></returns>
    Task AddPaymentType(PaymentTypeDTO paymentType);

    /// <summary>
    /// Metoda dodająca nową salę
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    Task AddRoom(RoomDTO room);

    /// <summary>
    /// Metoda usuwająca istniejący rodzaj opłat
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeletePaymentType(int id);

    /// <summary>
    /// Metoda usuwająca istniejącą salę
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteRoom(int id);

    /// <summary>
    /// Metoda uaktualniająca istniejący rodzaj opłat
    /// </summary>
    /// <param name="id"></param>
    /// <param name="paymentType"></param>
    /// <returns></returns>
    Task UpdatePaymentType(int id, PaymentTypeDTO paymentType);

    /// <summary>
    /// Metoda uaktualniająca istniejącą salę
    /// </summary>
    /// <param name="id"></param>
    /// <param name="room"></param>
    /// <returns></returns>
    Task UpdateRoom(int id, RoomDTO room);
}
