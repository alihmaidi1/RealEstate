// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Infrastructure.BackgroundServer.Filter;
public class BasicAuthenticationTokens
{
    private readonly string[] _tokens;

    public string Username => this._tokens[0];

    public string Password => this._tokens[1];

    public BasicAuthenticationTokens(string[] tokens) => this._tokens = tokens;

    public bool Are_Invalid()
    {
        return this.Not_Contains_Two_Tokens() || this.Invalid_Token_Value(this.Username) || this.Invalid_Token_Value(this.Password);
    }

    public bool Credentials_Match(string user, string pass)
    {
        return this.Username.Equals(user) && this.Password.Equals(pass);
    }

    private bool Invalid_Token_Value(string token) => string.IsNullOrWhiteSpace(token);

    private bool Not_Contains_Two_Tokens() => this._tokens.Length != 2;
}
