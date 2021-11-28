using MyBank.Nameservice;

namespace MyBank.Interfaces
{
    public interface IUser
    {
        Privileges Privilege { get; }
        string Username { get; set; }
        string Password { get; set; }
        string Token { get; }

    }
}
