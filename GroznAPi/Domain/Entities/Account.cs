﻿namespace Domain.Entities;

public class Account
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public UserInfo UserInfo { get; set; }
}