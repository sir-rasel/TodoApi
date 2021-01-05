using Cassandra;
using System.Threading.Tasks;

namespace TodoApi.DataAccessLayer.Repositories
{
    public  class BaseRepository
    {
        protected readonly TodoContext _context;
        public ICluster _cluster { get; set; }
        public ISession _session { get; set; }

        public BaseRepository(TodoContext context)
        {
            _context = context;
            _ = CassandraInitialize();
        }

        public async Task CassandraInitialize()
        {
            _cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            _session = await _cluster.ConnectAsync().ConfigureAwait(false);

            await _session.ExecuteAsync(new SimpleStatement("CREATE KEYSPACE IF NOT EXISTS " +
                "TodoDB WITH replication = { 'class': 'SimpleStrategy', 'replication_factor': '1' };"))
                .ConfigureAwait(false);
            await _session.ExecuteAsync(new SimpleStatement("USE TodoDB;"))
                .ConfigureAwait(false);
            await _session.ExecuteAsync(new SimpleStatement("CREATE TABLE IF NOT EXISTS " +
                "TodoItems(Id bigint, TaskTittle text, Date timestamp, PRIMARY KEY(Id));"))
                .ConfigureAwait(false);
        }
    }
}
