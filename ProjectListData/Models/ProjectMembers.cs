using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectListData.Models
{
    public class ProjectMembers
    {
        public int Id { get; set; }
        public int Project_id { get; set; }
        public int Member_id { get; set; }
    }
}
