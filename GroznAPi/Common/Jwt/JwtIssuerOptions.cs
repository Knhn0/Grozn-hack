﻿namespace Jwt;

public class JwtIssuerOptions
{
    public string Issuer { get; set; }

    public string Audience { get; set; }

    public string SecretKey { get; set; }

    public TimeSpan TokenExpiresAfter { get; set; }
}