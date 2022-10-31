﻿using DevExtreme.AspNet.Mvc.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models.Planning
{
    public class Target
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Code { get; set; }
        public string Description { get; set; }
        public int ServiceOutputId { get; set; }
        public Indicator Indicator { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public TargetStatus Status { get; set; }
        //public IEnumerable<Activity> Activities { get; set; }
    }
}