using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectListData.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectList.Controllers
{
    public class ProjectListController : Controller
    {
        readonly ProjectListContext _context;
            
        public ProjectListController(ProjectListContext context)
             => _context = context;
     
        [HttpGet]
        public IActionResult Index()
        {
            var list = _context.Project.ToList();
            return View(list);
        }

        [HttpPost]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                if (_context.Project.Any(proj => proj.Name.Equals(project.Name)))
                {
                    return Ok("Already in database...");
                }
                _context.Add(project);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            ViewData["ProjectId"] = id;
            return View();
            
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var project = _context.Project.Find(id);
            _context.Remove(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int id)
        {
            var project = _context.Project.Find(id);
            if (project == null)
            {
                return Ok("rip");
            }
            return View(project);
        }

        [HttpPost]
        public IActionResult Update(Project project)
        {
            if (ModelState.IsValid)
            {
                if (_context.Project.Any(proj => proj.Name.Equals(project.Name) && !proj.Id.Equals(project.Id)))
                {
                    return Ok("Already in database...");
                }
                _context.Update(project);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = _context.Project.Find(id);
            var name = project.Name;
            ViewData["Name"] = name;
            //this is an anonymous type aka a class without name that encapsulates read-only info
            var query = (from pm in _context.ProjectMembers 
                        join m in _context.Member on pm.Member_id equals m.Id
                        join p in _context.Project on pm.Project_id equals p.Id
                        where p.Id == id
                        select new
                        {
                            id = pm.Id,
                            first = m.First_name,
                            last = m.Last_name,
                            email = m.Email,
                        }).ToList();
            //.Select projects each element in query into a new form which in this case is an instance of the Member model
            var detailList = query.Select(x => new Member
            {
                Id = x.id,
                First_name = x.first,
                Last_name = x.last,
                Email = x.email,
            }).ToList();
            if(detailList is null)
            {
                return NotFound();
            }
            return View(detailList);
        }


        [HttpGet("Team")]
        public IActionResult Team()
        {
            var Richard = new Team()
            {
                Name = "Richard She",
                Position = "Application Development Intern",
                Description = "Rising Junior at the college of William & Mary. Always looking for fun stuff to do."
            };
            ViewData["Richard"] = Richard;

            var temp = new Team()
            {
                Name = "Maurice Blake",
                Position = "Lorem Ipsum",
                Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet"
            };
            ViewData["temp"] = temp;

            var temp2 = new Team()
            {
                Name = "Michael Ramirez",
                Position = "Lorem Ipsum",
                Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet"
            };
            ViewData["temp2"] = temp2;
            return View();
        }

    }
}
