using System.Runtime.CompilerServices;

namespace UserManagement.Core.ValueObjects;

public class Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty");
        }

        if (!IsValidEmail(email))
        {
            throw new ArgumentException("Invalid email address");
        }

        return new Email(email);
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
           var mail = new System.Net.Mail.MailAddress(email);
           return mail.Address == email;
        }
        catch
        {
            return false;
        }
    }
}