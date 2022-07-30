namespace ArticlesAPI.Api.Data{

    public interface IAppRepo{
        Task<bool> SaveChangesAsync();
    }
}