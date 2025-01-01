﻿using System.ComponentModel.DataAnnotations;
using MiniBank.Attributes;
using MiniBank.Validators.Services;

namespace MiniBank.Entities.Classes;

[Validator(typeof(UserValidator))]
public class User : DataBaseEntity
{
    [StringLength(50, MinimumLength = 5)]
    public required string Username { get; set; }
    
    [StringLength(64, MinimumLength = 64, ErrorMessage = "sha256 hash must be 64 characters.")]
    public required string PasswordHash { get; set; }
    
    [StringLength(70, MinimumLength = 2)]
    public required string FirstName { get; init; }
    
    [StringLength(70, MinimumLength = 2)]
    public required string LastName { get; init; }
    
    [StringLength(11, MinimumLength = 11)]
    public required string PhoneNumber { get; init; }
    
    [StringLength(10, MinimumLength = 10)]
    public required string NationalId { get; init; }
}
