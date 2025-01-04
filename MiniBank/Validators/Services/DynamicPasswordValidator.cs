using System.ComponentModel.DataAnnotations;
using DB.Data.Abstractions;
using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Services;

public class DynamicPasswordValidator(IDataBase dataBase) : BaseValidator<DynamicPassword>
{
    protected override void ValidateSaveState(DynamicPassword entity, List<string> errors)
    {
        if (entity.Amount <= 0)
        {
            errors.Add("Withdrawal amount must be greater than 0");
        }
        
        var cards = dataBase.FetchAll<Card>().ToArray();
        if (OriginCardNotFound(cards, entity.OriginCardNumber))
        {
            errors.Add("no card found for this request's OriginCardNumber");
        }
        
        if (OriginCardNotFound(cards, entity.DestinationCardNumber))
        {
            errors.Add("no card found for this request's DestinationCardNumber");
        }
    }
    
    private static bool OriginCardNotFound(IEnumerable<Card> cards, string originCardNumber)
        => cards.All(x => x.CardNumber != originCardNumber);
    
    private static bool DestinationCardNotFound(IEnumerable<Card> cards, string destinationCardNumber)
        => cards.All(x => x.CardNumber != destinationCardNumber);
    
    protected override void ValidateUpdateState(DynamicPassword entity, List<string> errors)
    {
        throw new ValidationException("Transaction entities can't get updated");
    }
    
    protected override void ValidateDeleteState(DynamicPassword entity)
    {
        throw new ValidationException("Transaction entities can't be deleted");
    }
}
