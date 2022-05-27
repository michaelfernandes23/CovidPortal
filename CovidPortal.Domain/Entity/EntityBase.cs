using CovidPortal.Domain.Interfaces;
using System;

namespace CovidPortal.Domain.Entity
{
    public class EntityBase: IEntityBase
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}