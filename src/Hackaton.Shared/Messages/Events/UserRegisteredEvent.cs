﻿using System;
using System.Text.Json;

namespace Hackaton.Shared.Messages.Events;

public class UserRegisteredEvent : IEvent
{
    public Guid UserId { get; init; }
    public bool IsDeleted { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? UserName { get; init; }
    public string Email { get; init; }
    public string ValidateEmailToken { get; init; }
    public bool EmailConfirmed { get; init; }
    public bool LockoutEnabled { get; init; }
    public DateTimeOffset? LockoutEnd { get; init; }
    public string? PhoneNumber { get; init; }
    public bool PhoneNumberConfirmed { get; init; }
    public bool TwoFactorEnabled { get; init; }

    public UserRegisteredEvent
    (
        Guid userId,
        bool isDeleted,
        DateTime createdAt,
        string? userName,
        string email,
        string validateEmailToken,
        bool emailConfirmed,
        bool lockoutEnabled,
        DateTimeOffset? lockoutEnd,
        string? phoneNumber,
        bool phoneNumberConfirmed,
        bool twoFactorEnabled
    )
    {
        UserId = userId;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
        UserName = userName;
        Email = email;
        ValidateEmailToken = validateEmailToken;
        EmailConfirmed = emailConfirmed;
        LockoutEnabled = lockoutEnabled;
        LockoutEnd = lockoutEnd;
        PhoneNumber = phoneNumber;
        PhoneNumberConfirmed = phoneNumberConfirmed;
        TwoFactorEnabled = twoFactorEnabled;
    }

    public string GetStreamName()
    {
        return $"User-{UserId}";
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}