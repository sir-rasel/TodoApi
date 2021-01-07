using Cassandra;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace TodoApi.DataAccessLayer.Repositories
{
    public class BaseRepository
    {
        protected readonly TodoContext _context;
        public ICluster _cluster { get; set; }
        public ISession _session { get; set; }
        public IConfiguration Configuration { get; }

        public BaseRepository(TodoContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
            _ = CassandraInitialize();
        }

        public async Task CassandraInitialize()
        {
            string ConnectionString = Configuration.GetConnectionString("CassandraConnection");
            _cluster = Cluster.Builder().AddContactPoint(ConnectionString).Build();
            _session = await _cluster.ConnectAsync().ConfigureAwait(false);

            await _session.ExecuteAsync(new SimpleStatement("CREATE KEYSPACE IF NOT EXISTS " +
                "TodoDB WITH replication = { 'class': 'SimpleStrategy', 'replication_factor': '1' };"))
                .ConfigureAwait(false);
            await _session.ExecuteAsync(new SimpleStatement("USE TodoDB;"))
                .ConfigureAwait(false);
            await _session.ExecuteAsync(new SimpleStatement("CREATE TABLE IF NOT EXISTS " +
                "TodoItems(Id bigint, TaskTittle text, Date timestamp, PRIMARY KEY((Id), tasktittle, date));"))
                .ConfigureAwait(false);
            await _session.ExecuteAsync(new SimpleStatement("CREATE INDEX IF NOT EXISTS " +
                "FilerByDate ON tododb.todoitems(date);"))
                .ConfigureAwait(false);
        }
    }
}
