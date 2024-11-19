using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HD.FireTracker.Common.Interfaces.DomainObjects;
using HD.FireTracker.Data.Common.Interfaces;

namespace HD.FireTracker.Data.Common.Models.FireTrackerDB
{
    public partial class RecurringProcess : IDomainRecurringProcess, IModelKey<int>
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

        [StringLength(256)]
        public string? RecurringJobName { get; set; }


        public DateTime? LogCleanupDate { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int PrimaryKeyId
        {
            get { return this.Id; }
            
        }



    }




}
