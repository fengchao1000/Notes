using System.Threading.Tasks;

namespace FC.Notes.Data
{
    public interface INotesDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
