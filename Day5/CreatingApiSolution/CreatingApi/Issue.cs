using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatingApi
{
    internal class Issue
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } = string.Empty ;
        public int ReportedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? ClosedDate { get; set; }

        public int? AssignedTo { get; set; }

    public Issue(int id, string title, string description, int reportedBy)
        {
            Id = id;
            Title = title;
            Description = description;
            ReportedBy = reportedBy;
            CreatedDate = DateTime.Now;
            Status = "Just Created";
        }
        public bool ChangeStatus(string newStatus)
        {
            if (Status == "Closed")
            {
                Console.WriteLine("Cannot change status as the issue is closed. Please raise a new Issue");
                return false;
            }
            Status = newStatus;
            if (newStatus == "Closed")
            {
                ClosedDate = DateTime.Now;
                Console.WriteLine($"Issue with {Id} is maked as Closed");
            }
            return true;
        }
        public bool AssignIssue(int assignedTo)
        {
            if (Status == "Closed")
            {
                Console.WriteLine("Cannot assign issue as the issue is closed. Please raise a new Issue");
                return false;
            }
            AssignedTo = assignedTo;
            Console.WriteLine($"Issue with {Id} successfully Assigned to {AssignedTo}");
            return true;
        }
        public void PrintIssueDetails()
        {
            var issueDetails = $"Issue Id: {Id}\nTitle: {Title}\nDescription: {Description}\nReported By: {ReportedBy}\nAssigned To: {AssignedTo}\nCreated Date: {CreatedDate}\nStatus: {Status}\nClosed Date: ";
            issueDetails += ClosedDate == null ? "-" : ClosedDate.ToString();
            Console.WriteLine(issueDetails);
        }

        public override string ToString()
        {
            var issueDetails = $"Issue Id: {Id}\nTitle: {Title}\nDescription: {Description}\nReported By: {ReportedBy}\nAssigned To: {AssignedTo}\nCreated Date: {CreatedDate}\nStatus: {Status}\nClosed Date: ";
            issueDetails += ClosedDate == null ? "-" : ClosedDate.ToString();
            return issueDetails;
        }
    }
}
