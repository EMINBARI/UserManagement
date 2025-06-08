namespace UserManagement.Infrastructure;

public interface IPasswordHasher
{
    string Generate(string password);
    bool Verify( string providedPassword, string hashedPassword);
}