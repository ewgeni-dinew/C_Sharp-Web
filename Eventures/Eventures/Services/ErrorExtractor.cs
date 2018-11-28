using Eventures.Services.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;

namespace Eventures.Services
{
    public class ErrorExtractor : IErrorExtractor
    {
        public string ExtractErrors(ModelStateDictionary.ValueEnumerable list)
        {
            var errors = new StringBuilder();

            foreach (var modelState in list)
            {
                foreach (var error in modelState.Errors)
                {
                    errors.AppendLine(error.ErrorMessage);
                }
            }

            return errors.ToString();
        }
    }
}
