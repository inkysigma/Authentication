namespace Authentication.Frame.Security
{
    public interface IUserTokenProvider<in TUser>
    {
        byte[] CreateToken(TUser user, string id, TokenField field);
    }

    public enum TokenField
    {
        Password,
        Email,
        Unlock,
        Activation
    }
}
