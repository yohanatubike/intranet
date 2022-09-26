using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class MeetingInvitation
    {
        public long MeetingInvitationId { get; set; }
        public long MeetingId { get; set; }
        public string Pfnumber { get; set; } = null!;
        public bool? IsFocalPerson { get; set; }
        public DateTime InvitedDate { get; set; }
        public string? InvitedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? AcceptanceStatus { get; set; }
        public DateTime? UpdateDate { get; set; }
        public virtual  Meeting Meetings { get; set; } = null!;
    }
}
