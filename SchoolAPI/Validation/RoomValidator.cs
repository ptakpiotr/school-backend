namespace SchoolAPI.Validation
{
    /// <summary>
    /// Walidator dla sali
    /// </summary>
    public class RoomValidator : AbstractValidator<RoomDTO>
    {
        public RoomValidator()
        {
            RuleFor(x => x.Numer_Sali).NotEmpty();
        }
    }
}
