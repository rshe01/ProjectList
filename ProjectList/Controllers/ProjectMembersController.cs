using Microsoft.AspNetCore.Mvc;
using ProjectListData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectList.Controllers
{
    public class ProjectMembersController : Controller
    {
        readonly ProjectListContext _context;

        public ProjectMembersController(ProjectListContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var query = (from pm in _context.ProjectMembers
                         join m in _context.Member on pm.Member_id equals m.Id
                         join p in _context.Project on pm.Project_id equals p.Id
                         orderby p.Id
                         select new
                         {
                             id = pm.Id,
                             p_id = p.Id,
                             m_id = m.Id,
                             p_name = p.Name,
                             m_first = m.First_name,
                             m_last = m.Last_name,
                             m_email = m.Email,
                         }).ToList();
            var readableList = query.Select(x => new ReadableProjectMember
            {
                Id = x.id,
                Project_id = x.p_id,
                Member_id = x.m_id,
                Project_name = x.p_name,
                Member_firstname = x.m_first,
                Member_lastname = x.m_last,
                Member_email = x.m_email
            }).ToList();
            return View(readableList);
        }

        [HttpGet]
        public IActionResult Add()
        {

            var model = new ProjectsAndMembersDropdown();
            model.Members = _context.Member.ToList();
            model.Projects = _context.Project.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Added(ProjectsAndMembersDropdown pam)
        {
            var projectMember = new ProjectMembers();
            projectMember.Member_id = pam.MemberId;
            projectMember.Project_id = pam.ProjectId;
            try
            {
                _context.ProjectMembers.Add(projectMember);
                _context.SaveChanges();
            }
            catch
            {
                return View("./AlreadyExists");
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            ViewData["ProjectMember"] = id;
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var projectMember = _context.ProjectMembers.Find(id);
            _context.Remove(projectMember);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
