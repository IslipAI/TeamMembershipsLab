// I, Alexander Islip, student number 000786144, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamMembershipsLab.Data;
using TeamMembershipsLab.Models;

namespace TeamMembershipsLab.Controllers
{
    /// <summary>
    /// This class is a MVC controller for teams. 
    /// It inherits from the Controller class.
    /// </summary>
    [Authorize]
    public class TeamsController : Controller
    {
        /// <summary>
        /// Applications context.
        /// </summary>
        private readonly ApplicationDbContext _context;


        /// <summary>
        /// TeamsController Constructor. Populates teams table with data.
        /// </summary>
        /// <param name="context">Applications database context</param>
        public TeamsController(ApplicationDbContext context)
        {
            //create teams to populate database.
            Team TeamOne = new Team
            {
                TeamName = "Mercedes AMG Petronas",
                Email = "MercedesAMG@mail.com",
                EstablishedDate = new DateTime(1925, 8, 20, 0, 0, 0)
            };

            Team TeamTwo = new Team
            {
                TeamName = "Redbull Honda Racing",
                Email = "redbullracing@mail.com",
            };

            Team TeamThree = new Team
            {
                TeamName = "Ferrari",
                Email = "ferrari@mail.com",
                EstablishedDate = new DateTime(1947, 3, 3, 0, 0, 0)
            };

            _context = context;

            //Add them to database.
            if (_context.Teams.Count() == 0)
            {
                _context.Add(TeamOne);
                _context.Add(TeamTwo);
                _context.Add(TeamThree);
                _context.SaveChanges();
            }
        }


        /// <summary>
        /// Gets teams data and list it.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.ToListAsync());
        }


        /// <summary>
        /// Gets details about a team by id.
        /// </summary>
        /// <param name="id">Search Id</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }


        /// <summary>
        /// Returns Create View upon being called.
        /// </summary>
        /// <returns>Create new team view.</returns>
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// Creates a new team object in the database.
        /// </summary>
        /// <param name="team">team being added</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeamName,Email,EstablishedDate")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }


        /// <summary>
        /// Method returns the edit view based off id.
        /// </summary>
        /// <param name="id">Team id.</param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }


        /// <summary>
        /// Method edits team by the id.
        /// </summary>
        /// <param name="id">Teams id</param>
        /// <param name="team">The team instance.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeamName,Email,EstablishedDate")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }


        /// <summary>
        /// Method returns the delete view based off id.
        /// </summary>
        /// <param name="id">The teams id.</param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }


        /// <summary>
        /// Method deletes team by id.
        /// </summary>
        /// <param name="id">Teams id.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// Method checks if team still exists in the database.
        /// </summary>
        /// <param name="id">The teams id.</param>
        /// <returns></returns>
        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
