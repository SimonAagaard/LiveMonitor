using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Handlers;
using Data.Entities;

namespace Web.Controllers
{
    public class DashboardsController : Controller
    {
        private DashboardHandler _dashboardHandler;

        public DashboardsController()
        {
            _dashboardHandler = new DashboardHandler();
        }
        //Used to retrieve the standard scaffolded index
        //public IActionResult Index()
        //{
        //    return View();
        //}


        // GET: Dashboards
        public async Task<IActionResult> Index()
        {
            var dashboards = _dashboardHandler.GetDashboards();
            //If there is dashboards in the DB pass them to the view
            if(dashboards.Result.Any())
            {
                return View(dashboards);
            }
            return View();
        }

        // GET: Dashboards/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboard = await _dashboardHandler.GetDashboard(id);
               
            if (dashboard == null)
            {
                return NotFound();
            }

            return View(dashboard);
        }

        // GET: Dashboards/Create
        public IActionResult Create()
        {
            //ViewData["UserId"] = new SelectList(_context.MonitorUsers, "Id", "Id");
            return View();
        }

        // POST: Dashboards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DashboardName,UserId,DashboardId,DashboardSettingId,DateCreated,DateModified,DateDeleted,Id")] Dashboard dashboard)
        {
            if (ModelState.IsValid)
            {
                dashboard.DashboardId = Guid.NewGuid();
                await _dashboardHandler.CreateDashboard(dashboard);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.MonitorUsers, "Id", "Id", dashboard.UserId);
            return View(dashboard);
        }

        // GET: Dashboards/Edit/5
        //Used for retrieveing the page to edit a dashboard
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboard = await _dashboardHandler.GetDashboard(id);
            if (dashboard == null)
            {
                return NotFound();
            }
            //ViewData["UserId"] = new SelectList(_context.MonitorUsers, "Id", "Id", dashboard.UserId);
            return View(dashboard);
        }

        // POST: Dashboards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Used to post the changes made to the specified dashboard
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DashboardName,UserId,DashboardId,DashboardSettingId,DateCreated,DateModified,DateDeleted,Id")] Dashboard dashboard)
        {
            if (id != dashboard.DashboardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _dashboardHandler.UpdateDashboard(dashboard);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DashboardExists(dashboard.DashboardId))
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
            //ViewData["UserId"] = new SelectList(_context.MonitorUsers, "Id", "Id", dashboard.UserId);
            return View(dashboard);
        }

        // GET: Dashboards/Delete/5
        //Get the view for the dashboard to be deleted
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboard = await _dashboardHandler.GetDashboard(id);
            if (dashboard == null)
            {
                return NotFound();
            }

            return View(dashboard);
        }

        // POST: Dashboards/Delete/5
        //Post the delete of the dashboard
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _dashboardHandler.DeleteDashboard(id);
            return RedirectToAction(nameof(Index));
        }

        //Helper method to determine if the dashboard exists in the db
        private bool DashboardExists(Guid dashboardId)
        {
            var dashboard = _dashboardHandler.GetDashboard(dashboardId);
            bool dashboardExists = dashboard != null ? true : false;
            return dashboardExists;
        }
    }
}
