namespace CarShowRegistration.Infrastructure.Persistence;

public static class DatabaseLocation
{
    public static string GetDatabasePath(string? contentRoot = null)
    {
        var backendDirectory = FindBackendDirectory(contentRoot) ?? FindBackendDirectory(AppContext.BaseDirectory);
        var databaseDirectory = backendDirectory ?? AppContext.BaseDirectory;
        return Path.Combine(databaseDirectory, "carshow.db");
    }

    private static string? FindBackendDirectory(string? startPath)
    {
        if (string.IsNullOrWhiteSpace(startPath))
            return null;

        var directory = new DirectoryInfo(startPath);
        while (directory is not null)
        {
            var hasSolution = Directory.EnumerateFiles(directory.FullName, "*.sln", SearchOption.TopDirectoryOnly)
                .Any(path => string.Equals(Path.GetFileName(path), "VerticalSlice.sln", StringComparison.OrdinalIgnoreCase));
            if (hasSolution)
                return directory.FullName;

            directory = directory.Parent;
        }

        return null;
    }
}
