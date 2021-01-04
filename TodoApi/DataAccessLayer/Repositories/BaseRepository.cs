namespace TodoApi.DataAccessLayer.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly TodoContext _context;

        public BaseRepository(TodoContext context)
        {
            _context = context;
        }
    }
}
