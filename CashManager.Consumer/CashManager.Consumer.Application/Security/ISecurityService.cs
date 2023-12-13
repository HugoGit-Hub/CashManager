namespace CashManager.Consumer.Application.Security;

public interface ISecurityService
{
    public string EncryptPassword(string data, string uniqueUserKey);

    public string HashWithSalt<TEntity>(TEntity data);
}