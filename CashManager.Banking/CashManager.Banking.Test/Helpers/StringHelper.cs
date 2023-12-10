using System.Text;

namespace CashManager.Banking.Test.Helpers;

public static class StringHelper
{
    public static string Random(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var stringBuilder = new StringBuilder(length);
        for (var i = 0; i < length; i++)
        {
            var index = random.Next(chars.Length);
            stringBuilder.Append(chars[index]);
        }

        return stringBuilder.ToString();
    }
}