using System;

namespace CovidPortal.Domain.Interfaces
{
    public interface IEntityBase
    {
        string Id { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string UpdatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
