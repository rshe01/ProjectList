using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectListData.Models;

namespace ProjectList.Controllers
{
    public class MemberController : Controller
    {
        //creating instance variable of our database context which contains connections to our SQL tables
        readonly ProjectListContext _context;

        public MemberController(ProjectListContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var members = _context.Member.ToList();
            return View(members);
        }

        [HttpPost]
        public IActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                //checking whether a person already exists. Emails are unique while names are not
                if(_context.Member.Any(person => person.Email.Equals(member.Email)))
                {
                    return Ok("Already in database...");
                }
                _context.Add(member);
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
            //ViewData allows us to temporarily store values and feed it into our Views
            ViewData["MemberId"] = id;
            return View();

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var member = _context.Member.Find(id);
            _context.Remove(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int id)
        {
            var member = _context.Member.Find(id);
            if (member == null)
            {
                return Ok("Not supposed to happen :(");
            }
            return View(member);
        }

        [HttpPost]
        public IActionResult Update(Member member)
        {
            if (ModelState.IsValid)
            {
                //making sure we don't update an email to something that already exists
                if (_context.Member.Any(person => person.Email.Equals(member.Email)))
                {
                    return Ok("Already in database...");
                }
                _context.Update(member);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
