using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;
using Data.Handlers;

namespace Web.Controllers
{
    public class IntegrationController : Controller
    {
        private readonly IntegrationHandler _integrationHandler;
        private readonly IntegrationSettingHandler _integrationSettingHandler;

        public IntegrationController()
        {
            _integrationHandler = new IntegrationHandler();
            _integrationSettingHandler = new IntegrationSettingHandler();
        }

        // GET: Integration
        public async Task<IActionResult> Index()
        {
            var integrations = await _integrationHandler.GetIntegrations();
            //If there is dashboards in the DB pass them to the view
            if (integrations.Any())
            {
                return View(integrations);
            }
            return View();
        }

        // GET: Integration/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.MonitorUsers, "Id", "Id");
            return View();
        }

        // POST: Integration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IntegrationId,UserId,IntegrationSettingId,IntegrationName")] Integration integration)
        {
            if (ModelState.IsValid)
            {
                integration.IntegrationId = Guid.NewGuid();
                _context.Add(integration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.MonitorUsers, "Id", "Id", integration.UserId);
            return View(integration);
        }

        // GET: Integration/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var integration = await _context.Integrations.FindAsync(id);
            if (integration == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.MonitorUsers, "Id", "Id", integration.UserId);
            return View(integration);
        }

        // POST: Integration/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IntegrationId,UserId,IntegrationSettingId,IntegrationName")] Integration integration)
        {
            if (id != integration.IntegrationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(integration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntegrationExists(integration.IntegrationId))
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
            ViewData["UserId"] = new SelectList(_context.MonitorUsers, "Id", "Id", integration.UserId);
            return View(integration);
        }

        // GET: Integration/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var integration = await _context.Integrations
                .Include(i => i.MonitorUser)
                .FirstOrDefaultAsync(m => m.IntegrationId == id);
            if (integration == null)
            {
                return NotFound();
            }

            return View(integration);
        }

        // POST: Integration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var integration = await _context.Integrations.FindAsync(id);
            _context.Integrations.Remove(integration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntegrationExists(Guid id)
        {
            return _context.Integrations.Any(e => e.IntegrationId == id);
        }
    }
}
