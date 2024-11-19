using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HD.FireTracker.DB.FireTrackerDB.DBFirst;

public partial class RecurringProcessDetail
{
    [Key]
    public int Id { get; set; }

    public string? Message { get; set; }

    public string? MessageTemplate { get; set; }

    public string? Level { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string? Exception { get; set; }

    public string? Properties { get; set; }

    [StringLength(50)]
    public string? TaskManagerProcessId { get; set; }

    [StringLength(20)]
    public string? MessageType { get; set; }

    public string? FireTrackerMsg { get; set; }
}
