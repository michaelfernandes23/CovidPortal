using CovidPortal.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CovidPortal.Domain.Entity
{
    public class EntityBase: IEntityBase
    {
        [Column(nameof(Id), TypeName = "varchar(36)")]
        public string Id { get; set; }
        [Column(nameof(CreatedBy), TypeName = "varchar(100)")]
        public string CreatedBy { get; set; }
        [Column(nameof(CreatedDate), TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(nameof(UpdatedBy), TypeName = "varchar(100)")]
        public string? UpdatedBy { get; set; }
        [Column(nameof(UpdatedDate), TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}