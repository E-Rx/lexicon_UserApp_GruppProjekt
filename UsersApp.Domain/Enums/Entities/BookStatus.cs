

namespace UsersApp.Domain.Enums.Entities;

public enum BookStatus
{
    Available,
    Reserved,
    Loaned,
    Missing
}

public static class BookStatusExtensions
{
    public static string GetSwedishName(BookStatus status)
    {
        return status switch
        {
            BookStatus.Available => "Tillgänglig",
            BookStatus.Reserved => "Reserverad",
            BookStatus.Loaned => "Utlånad",
            BookStatus.Missing => "Borttappad",
            _ => "Okänt"
        };
    }
}
