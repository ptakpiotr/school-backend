namespace School.DataAccess.Exceptions;

/// <summary>
/// Wyjątek wyrzucany dla nieprawidłowej (z punktu widzenia bazy danych) operacji
/// </summary>
public class InvalidDatabaseOperationException : Exception
{
    public InvalidDatabaseOperationException(string message = "Tabela bądź mapowanie nie istnieje") : base(message)
    {

    }
}
