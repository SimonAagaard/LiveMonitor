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
using System.Security.Claims;

namespace Web.Controllers
{
    public class DashboardsGroupController : Controller
    {
        private readonly DashboardGroupHandler _dashboardGroupHandler;
        private readonly DashboardHandler _dashboardHandler;

        public DashboardsGroupController()
        {
            _dashboardGroupHandler = new DashboardGroupHandler();
            _dashboardHandler = new DashboardHandler();
        }

        // GET: Dashboards
        public async Task<IActionResult> Index()
        {
            Guid userId = Guid.Parse(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            var dashboards = await _dashboardHandler.GetDashboardsByUserId(userId);

            //List<DashboardGroup> dashboardGroups = await _dashboardGroupHandler.GetDashboardGroupsByDashboardId();

            //if (dashboardGroups.Any())
            //{
            //    return View(dashboardGroups);
            //}
            return View();
        }

        // GET: Dashboards/Create
        public IActionResult Create()
        {
            //Tænker det her bliver the hard stuff
            return View();
        }

        // POST: Dashboards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DashboardName,UserId,DashboardId,DashboardSettingId,DateCreated,DateModified,DateDeleted")] Dashboard dashboard)
        {  //Tænker det her bliver the hard stuff
            return View();
        }

        // GET: Dashboards/Edit/5
        public async Task<IActionResult> Edit(Guid dashboardGroupId)
        {

            if (dashboardGroupId != null)
            {
                DashboardGroup dashboardGroup = await _dashboardGroupHandler.GetDashboardGroup(dashboardGroupId);

                if (dashboardGroup != null)
                {
                    return View(dashboardGroup);
                }
            }

            return NotFound();
        }

        // POST: Dashboards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("DashboardGroupName,UserId,DashboardId,GroupRefreshRate,DateCreated,DateModified,DateDeleted")] DashboardGroup dashboardGroup)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _dashboardGroupHandler.UpdateDashboardGroup(dashboardGroup);

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw e;
                }
            }

            return View(dashboardGroup);
        }

        // POST: Dashboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid dashboardGroupId)
        {
            DashboardGroup dashboardGroup = await _dashboardGroupHandler.GetDashboardGroup(dashboardGroupId);

            if (dashboardGroup != null)
            {
                await _dashboardGroupHandler.DeleteDashboardGroup(dashboardGroup);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
