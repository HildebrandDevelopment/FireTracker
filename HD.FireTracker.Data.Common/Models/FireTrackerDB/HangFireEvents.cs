using HD.FireTracker.Common.Interfaces.DomainObjects;
using HD.FireTracker.Data.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HD.FireTracker.Data.Common.Models.FireTrackerDB
{
    public partial class HangFireEvents : IDomainHangFireEvents, IModelKey<int>
    {
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int PrimaryKeyId
        {
            get { return this.Id; }

        }

        [Key]
        public int Id { get; set; }

        public string? Message { get; set; }

        public string? MessageTemplate { get; set; }

        public string? Level { get; set; }

        public DateTime? TimeStamp { get; set; }

        public string? Exception { get; set; }

        public string? Properties { get; set; }
    }
}
