namespace MusicStore.Services;

public static class Utils
{
    public static int GetTotalPages(int total, int rows)
    {
        var totalPages = total / rows;
        if (total % rows > 0)
            totalPages++;

        return totalPages;
    }
}