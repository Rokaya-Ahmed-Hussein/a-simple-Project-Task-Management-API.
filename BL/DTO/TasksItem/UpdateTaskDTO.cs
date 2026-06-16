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
        public string Status { get; set; } = "Pending";

    }
}
