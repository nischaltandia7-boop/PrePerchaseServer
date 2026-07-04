using System.Security.Cryptography;

namespace PrePerchaseServer.Modules.Auth.Helpers;

public static class PasswordGenerator
{
    private const string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string LowerCase = "abcdefghijklmnopqrstuvwxyz";
    private const string Numbers = "0123456789";
    private const string Symbols = "@#$%&*!";

    private static readonly string AllCharacters =
        UpperCase + LowerCase + Numbers + Symbols;

    public static string Generate(int length = 12)
    {
        if (length < 8)
            throw new ArgumentException("Password length must be at least 8.");

        var password = new List<char>
        {
            UpperCase[RandomNumberGenerator.GetInt32(UpperCase.Length)],
            LowerCase[RandomNumberGenerator.GetInt32(LowerCase.Length)],
            Numbers[RandomNumberGenerator.GetInt32(Numbers.Length)],
            Symbols[RandomNumberGenerator.GetInt32(Symbols.Length)]
        };

        while (password.Count < length)
        {
            password.Add(
                AllCharacters[
                    RandomNumberGenerator.GetInt32(AllCharacters.Length)
                ]);
        }

        return new string(password
            .OrderBy(_ => RandomNumberGenerator.GetInt32(int.MaxValue))
            .ToArray());
    }
}