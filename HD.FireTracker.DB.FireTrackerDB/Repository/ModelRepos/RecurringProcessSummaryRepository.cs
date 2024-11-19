using HD.FireTracker.Data.Common.IRepositories.FireTrackerDB.ModelRepo;
using HD.FireTracker.Data.Common.Models.FireTrackerDB;
using HD.FireTracker.DB.FireTrackerDB.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HD.FireTracker.DB.FireTrackerDB.Repository.ModelRepos
{
    public class RecurringProcessSummaryRepository : RepositoryReadOnly<RecurringProcessSummary>, IRecurringProcessSummaryRepository
    {
        public RecurringProcessSummaryRepository(FireTrackerDbContext dbContext) : base(dbContext)
        {
        }



        public IEnumerable<RecurringProcessSummary> GetAllRecurringProcessSummaries()
        {


            System.FormattableString sqlStmt = $@"
                    --process summary
                    SELECT
                        a.LastId,
                        a.RecurringJobName,
                        rpLast.Level,
                        rpLast.TimeStamp [LastRunTime],
                        rpLast.Exception,
                        rpLast.TaskManagerProcessId
                    FROM
                    (
                        SELECT
                        MAX(rp.Id) LastId
                        , rp.RecurringJobName
                        FROM dbo.RecurringProcess rp
                        WHERE
                        rp.RecurringJobName IS NOT NULL
                        GROUP BY rp.RecurringJobName
                    ) a
                    INNER JOIN dbo.RecurringProcess rpLast
                      ON a.LastId = rpLast.Id
                    ";

            var query = _dbContext.Database.SqlQuery<RecurringProcessSummary>(sqlStmt);

            var q = from x in query
                    select new RecurringProcessSummary
                    {
                        LastId = x.LastId,
                        Exception = x.Exception,
                        LastRunTime = x.LastRunTime,
                        Level = x.Level,
                        RecurringJobName = x.RecurringJobName,
                        TaskManagerProcessId = x.TaskManagerProcessId
                    };

            return q;

        }



    }
}
