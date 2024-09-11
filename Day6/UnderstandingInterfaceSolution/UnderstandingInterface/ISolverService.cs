using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingInterface
{
    internal interface ISolverService
    {
        public Issue[] GetAllIssues();
        public Issue AssignIssue(int eid, int issueId);
    }
}
