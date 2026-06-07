using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO.TasksItem
{
    public class UpdateTaskDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";
        public DateTime DueDate { get; set; }
        public string Priority { get; set; } = "Medium";
    }
}
