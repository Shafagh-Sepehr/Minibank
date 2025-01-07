namespace InMemoryDataBase.Entities.Classes;

public class Reference
{
    public required Type   MasterType { get; set; }
    public required Type   SlaveType  { get; set; }
    public required string MasterId   { get; set; }
    public required string SlaveId    { get; set; }
}