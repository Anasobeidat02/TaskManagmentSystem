namespace Infrastractures;

public class Hash
{
    public static string HashText(string str)
    {
        if (string.IsNullOrEmpty(str))
            throw new ArgumentNullException("hash is required");

        var result = BCrypt.Net.BCrypt.HashPassword(str);  
        return result;
    }


    public static bool VerifyHash(string str, string hash)
    {
        if (string.IsNullOrEmpty(str))
            throw new ArgumentNullException("string is required");
        if (string.IsNullOrEmpty(hash))
            throw new ArgumentNullException("hash is required");


        var result = BCrypt.Net.BCrypt.Verify(str, hash);
        return result;
    }
}
