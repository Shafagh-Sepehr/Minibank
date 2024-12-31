﻿using MiniBank.Entities.Enums;

namespace MiniBank.Entities.Classes;

public class Withdrawal : DataBaseEntity
{
    private readonly decimal _amount;
    
    public TransactionStatus Status     { get; init; } = TransactionStatus.Success;
    public required long              AccountRef { get; init; }
    
    public required decimal Amount
    {
        get => _amount;
        init
        {
            if (value < 0)
            {
                throw new ArgumentException("Amount cannot be negative");
            }
            
            _amount = value;
        }
    }
    
    public DateTime Date { get; init; } = DateTime.Now;
}