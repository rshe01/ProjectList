using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectListData.Models
{
    public class ReadableProjectMember
    {
        public int Id { get; set; }
        public int Project_id { get; set; }
        public int Member_id { get; set; }
        public string Project_name { get; set; }
        public string Member_firstname { get; set; }
        public string Member_lastname { get; set; }
        public string Member_email { get; set; }
    }
}
