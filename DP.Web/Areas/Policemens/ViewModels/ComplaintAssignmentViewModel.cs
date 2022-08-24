using DP.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace DP.Web.Areas.Policemens.ViewModels
{
    public class ComplaintAssignmentViewModel : ComplaintAssignment
    {
        [Display(Name = "Complainer's Name")]
        public override int ComplainerId
        {
            get => base.ComplainerId;
            set => base.ComplainerId = value;
        }

        [Display(Name = "Assigned to Policemen's Name")]
        public override int PolicemenId
        {
            get => base.PolicemenId;
            set => base.PolicemenId = value;
        }
        [Display(Name = "Assigned Date")]
        public override DateTime AssignedDate 
        { 
            get => base.AssignedDate;
            set => base.AssignedDate = value;
        }

        public override bool IsAssigned
        {
            get => base.IsAssigned;
            set => base.IsAssigned = value;
        }
        public override bool IsResolved
        {
            get => base.IsResolved;
            set => base.IsResolved = value;
        }

    }
}
