using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO.TasksItem
{
    public class GetTasksByProjectDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";
        public DateTime DueDate { get; set; }
        public string Priority { get; set; } = "Medium";
        public int ProjectId { get; set; }
    }
}
