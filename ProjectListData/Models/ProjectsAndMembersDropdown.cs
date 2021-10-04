using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectListData.Models
{
    public class ProjectsAndMembersDropdown
    {
        public IEnumerable<Member> Members { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public int MemberId { get; set; }
        public int ProjectId { get; set; }
    }
}
