﻿using System.Security.Cryptography;
using System.Text;

namespace Helpers;

public class PasswordHasher
{
    public string HashPassword(string pass)
    {
        using var md5 = MD5.Create();
        byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(pass));

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            sb.Append(data[i].ToString("x2"));
        }

        return sb.ToString();
    }
}