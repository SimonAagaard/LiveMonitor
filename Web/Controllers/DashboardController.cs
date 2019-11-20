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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardHandler _dashboardHandler;
        private readonly DashboardSettingHandler _dashboardSettingHandler;

        public DashboardController()
        {
            _dashboardHandler = new DashboardHandler();
            _dashboardSettingHandler = new DashboardSettingHandler();

        }

        // GET: Dashboards
        public async Task<IActionResult> Index()
        {
            var dashboards = await _dashboardHandler.GetDashboards();
            //If there is dashboards in the DB pass them to the view
            if (dashboards.Any())
            {
                return View(dashboards);
            }
            return View();
        }

        Random rdn = new Random();

        public IActionResult RealTimeDashboard()
        {
            return View();
        }

        public JsonResult GetRealTimeData()
        {
            RealTimeData data = new RealTimeData
            {
                TimeStamp = DateTime.Now,
                DataValue = rdn.Next(0, 11)
            };
            return Json(data);
        }

        // GET: Dashboards/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
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
            return View();
        }

        // POST: Dashboards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DashboardName")] Dashboard dashboard)
        {
            if (ModelState.IsValid)
            {
                //Get current user
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                //Dashboard
                dashboard.DashboardId = Guid.NewGuid();
                dashboard.DateCreated = DateTime.Now;
                dashboard.DashboardSettingId = Guid.NewGuid();
                dashboard.UserId = Guid.Parse(userId);

                await _dashboardHandler.CreateDashboard(dashboard);

                //DashboardSetting - One-One relation
                DashboardSetting dashboardSetting = new DashboardSetting
                {
                    DashboardSettingId = dashboard.DashboardSettingId,
                    DashboardId = dashboard.DashboardId
                };

                await _dashboardSettingHandler.CreateDashboardSetting(dashboardSetting);

                return RedirectToAction(nameof(Index));
            }
            return View(dashboard);
        }

        // GET: Dashboards/Edit/5
        //Used for retrieveing the page to edit a dashboard
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
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

        // POST: Dashboards/Edit/5
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
            return View(dashboard);
        }

        // GET: Dashboards/Delete/5
        //Get the view for the dashboard to be deleted
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
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
    public class RealTimeData
    {
        public DateTime TimeStamp { get; set; }
        public double DataValue { get; set; }
    }
}
