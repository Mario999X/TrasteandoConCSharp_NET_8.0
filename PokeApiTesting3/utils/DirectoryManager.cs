using PokeApiTesting2.mappers;
using Testing1.models.pokeApi;

namespace PokeApiTesting3.utils
{
    internal class DirectoryManager
    {
        readonly string header = Config.Instance.Configuration["Backup_csv_Header"];

        public string CreateWriteBackupCsv(HashSet<Pokemon> pokemons)
        {
            string parent = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)
                .Parent.Parent.Parent.FullName;

            string directory = Path.Combine(parent, "data");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string path = Path.Combine(
                directory,
                $"backup_pokidex_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv"
            );

            var lines = pokemons
                .Select(p => p.ToDTO())   // Mapper
                .Select(dto => dto.Name);   // DTO // Multiple values example:  .Select(dto => $"{dto.Name};{dto.BaseExperience}");

            // .Prepend("Name"); //  Only one Header

            File.WriteAllLines(path, new[] { header }.Concat(lines));

            return path;
        }

        public string? GetLatestBackupPath()
        {
            string parent = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)
                .Parent.Parent.Parent.FullName;

            string directory = Path.Combine(parent, "data");

            if (!Directory.Exists(directory))
                return null;

            return Directory.GetFiles(directory, "backup_pokidex_*.csv")
                .OrderByDescending(f => f)
                .FirstOrDefault();
        }

    }
}
