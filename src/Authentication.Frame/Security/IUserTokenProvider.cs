namespace Authentication.Frame.Security
{
    public interface IUserTokenProvider<in TUser>
    {
        byte[] CreateToken(TUser user, string id, TokenField field);
        bool VerifyToken(TUser user, string id, TokenField field, byte[] token);
    }

    public enum TokenField
    {
        Password,
        Email,
        Unlock,
        Activation
    }
}
