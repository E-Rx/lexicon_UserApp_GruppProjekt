namespace UsersApp.Domain.Enums.Entities;

public enum BookGenre
{
    Fiction,
    Crime,
    Fantasy,
    Science_Fiction,
    Romance,
    Thriller,
    Historical,
    Biography,
    Horror,
    Non_Fiction
}

public static class BookGenreExtensions
{
    public static string GetSwedishName(BookGenre genre)
    {
        return genre switch
        {
            BookGenre.Fiction => "Skönlitteratur",
            BookGenre.Crime => "Kriminalroman",
            BookGenre.Fantasy => "Fantasy",
            BookGenre.Science_Fiction => "Science fiction",
            BookGenre.Romance => "Romantik",
            BookGenre.Thriller => "Thriller",
            BookGenre.Historical => "Historia",
            BookGenre.Biography => "Biografi",
            BookGenre.Horror => "Skräck",
            BookGenre.Non_Fiction => "Facklitteratur",
            _ => "Okänt"
        };
    }
}
