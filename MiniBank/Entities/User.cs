﻿using System.ComponentModel.DataAnnotations;
using MiniBank.Data.Abstractions;

namespace MiniBank.Entities;

public class User : IDatabaseEntity
{
    [StringLength(50, MinimumLength = 5)]
    public required string Username { get; set; }
    
    public required string PasswordHash { get; set; }
    
    [StringLength(70, MinimumLength = 2)]
    public required string FirstName { get; init; }
    
    [StringLength(70, MinimumLength = 2)]
    public required string LastName { get; init; }
    
    [StringLength(11, MinimumLength = 11)]
    public required string PhoneNumber { get; set; }
    
    [StringLength(10, MinimumLength = 10)]
    public required string NationalId { get; init; }
    
    public long Id { get; set; }
}
