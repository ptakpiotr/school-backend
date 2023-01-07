namespace School.DataAccess.Services.Contracts
{
    public interface IMiscService
    {
        Task<List<PaymentTypeModel>> GetPaymentTypes();
        Task<List<RoomModel>> GetRooms();
        Task AddPaymentType(PaymentTypeDTO paymentType);
        Task AddRoom(RoomDTO room);
        Task DeletePaymentType(int id);
        Task DeleteRoom(int id);
    }
}
