using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models.Contracts
{
    public interface IProduct
    {
        int Id { get; set; }

        string Name { get; set; }

        char Gender { get; set; }

        string Description { get; set; }
        
        decimal Price { get; set; }

        bool IsAvailable { get; set; }

        DateTime CreatedOn { get; set; }

        string ImageUrl { get; set; }

        string ImageId { get; set; }
    }
}
