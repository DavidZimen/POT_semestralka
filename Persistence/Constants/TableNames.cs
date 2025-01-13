namespace Persistence.Constants;

public static class TableNames
{
    // standard table names
    public const string User = "user";
    public const string Person = "person";
    public const string Actor = "actor";
    public const string Director = "director";
    public const string Film = "film";
    public const string Show = "show";
    public const string Season = "season";
    public const string Episode = "episode";
    public const string Rating = "rating";
    public const string Genre = "genre";
    public const string Character = "character";
    
    // M:N table names
    public const string ShowActor = $"{Show}_{Actor}";
    public const string FilmActor = $"{Film}_{Actor}";
    public const string ShowGenre = $"{Show}_{Genre}";
    public const string FilmGenre = $"{Film}_{Genre}";
}