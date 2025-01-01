using System.ComponentModel.DataAnnotations;
using DB.Data.Abstractions;

namespace MiniBank.Entities;

public abstract class DataBaseEntity : IDatabaseEntity
{
    [AllowedValues(0L, ErrorMessage = "Id must not be set")]
    public long Id { get; set; }
}
