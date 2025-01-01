using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Services;

public class DepositValidator : BaseValidator<Deposit>
{
    protected override void ValidateGeneralState(Deposit entity, List<string> errors)
    {
        throw new NotImplementedException();
    }
    protected override void ValidateSaveState(Deposit entity, List<string> errors)
    {
        throw new NotImplementedException();
    }
    protected override void ValidateUpdateState(Deposit entity, List<string> errors)
    {
        throw new NotImplementedException();
    }
    protected override void ValidateDeleteState(Deposit entity)
    {
        throw new NotImplementedException();
    }
}
