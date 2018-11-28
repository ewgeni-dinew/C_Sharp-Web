using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Eventures.Services.Contracts
{
    public interface IErrorExtractor
    {
        string ExtractErrors(ModelStateDictionary.ValueEnumerable list);
    }
}
