namespace ArticlesAPI.Api.Data{

    public class SqlAppRepo : IAppRepo{
        private readonly AppDbContext _context;

        public SqlAppRepo(AppDbContext context){
            _context = context;
        }

        public async Task<bool> SaveChangesAsync(){
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}