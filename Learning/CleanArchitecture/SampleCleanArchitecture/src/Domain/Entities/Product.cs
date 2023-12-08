using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleCleanArchitecture.Domain.Entities;
public class Product : BaseAuditableEntity
{
    public string? ProductName { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ProductPrice { get; set; }

}
