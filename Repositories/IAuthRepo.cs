namespace MyShopApi.Repositories
{
    public interface IAuthRepo
    {
        KeyValuePair<bool, string> LoginUser(string userName, string password);
        KeyValuePair<bool, string> RegisterNewUser(string userName, string password);
    }
}

