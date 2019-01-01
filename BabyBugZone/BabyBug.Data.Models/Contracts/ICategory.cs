using BabyBug.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models.Contracts
{
    public interface ICategory
    {
        int Id { get; set; }

        string Name { get; set; }

        string ImageUrl { get; set; }

        string ImageId { get; set; }
    }
}
