using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Handlers;
using Data.Entities;
using System.Security.Claims;
using Web.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly DashboardHandler _dashboardHandler;
        private readonly DashboardSettingHandler _dashboardSettingHandler;
        private readonly DashboardTypeHandler _dashboardTypeHandler;
        private readonly DataSetController _dataSetController;
        private readonly IntegrationController _integrationController;

        public DashboardController()
        {
            _dashboardHandler = new DashboardHandler();
            _dashboardSettingHandler = new DashboardSettingHandler();
            _dashboardTypeHandler = new DashboardTypeHandler();
            _dataSetController = new DataSetController();
            _integrationController = new IntegrationController();
        }

        // GET: Dashboards
        //Get all dashboards for the logged in user.
        public async Task<IActionResult> Index()
        {
            Guid userId = Guid.Parse(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            string[] children = new string[] { "DashboardSetting" };
            List<Dashboard> dashboards = new List<Dashboard>();
            var allDashboards = await _dashboardHandler.GetDashboardsByUserId(userId);
            if (allDashboards.Any())
            {
                 dashboards = await _dashboardHandler.GetDashboardsAndDashboardSettings(userId, children);
            }

            //If there is dashboards in the DB pass them to the view
            if (dashboards.Any())
            {
                return View(dashboards);
            }

            return View();
        }

        //Get a single dashboard along with its setting based on its dashboardId
        public async Task<IActionResult> Dashboard(Guid dashboardId)
        {
            string[] children = new string[] { "DashboardSetting" };
            Dashboard dashboardWithSetting = await _dashboardHandler.GetDashBoardAndDashboardSetting(dashboardId, children);

            if (dashboardWithSetting.DashboardSetting != null)
            {
                return View(dashboardWithSetting);
            }

            Dashboard dashboard = await _dashboardHandler.GetDashboard(dashboardId);

            if (dashboard != null)
            {
                return View(dashboard);
            }

            return NotFound();
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
                // Get current user
                Guid userId = Guid.Parse(User?.FindFirst(ClaimTypes.NameIdentifier).Value);

                // Checks if there is a current user to bind the dashboard to
                if (userId != Guid.Empty)
                {
                    // Set values on dashboard, and instantiate the dashboardsettings as well.
                    dashboard.UserId = userId;
                    dashboard.DashboardId = Guid.NewGuid();
                    dashboard.DateCreated = DateTime.UtcNow;
                    dashboard.DashboardSettingId = Guid.NewGuid();
                    DashboardSetting dashboardSetting = new DashboardSetting
                    {
                        DashboardSettingId = dashboard.DashboardSettingId,
                        DashboardId = dashboard.DashboardId,
                    };
                    dashboard.DashboardSetting = dashboardSetting;

                    // Create dashboard with the related dashboardsetting
                    await _dashboardHandler.CreateDashboard(dashboard);

                    // Passes the Ids needed by the Dashboardsetting view
                    return RedirectToAction(nameof(DashboardSetting), new { dashboardSettingId = dashboardSetting.DashboardSettingId });
                }
                else
                {
                    return NotFound("No current user available");
                }
            }

            return View(dashboard);
        }

        //Get DashboardSetting view
        public async Task<IActionResult> DashboardSetting(Guid dashboardSettingId)
        {
            if (dashboardSettingId != Guid.Empty)
            {
                DashboardSetting dashboardSetting = await _dashboardSettingHandler.GetDashboardSetting(dashboardSettingId);
                if (dashboardSetting != null)
                {
                    // Sets viewbag to display the name of the dashboard linked to the setting page
                    Dashboard dashboard = await _dashboardHandler.GetDashboard(dashboardSetting.DashboardId);
                    ViewBag.DashboardName = dashboard.DashboardName;

                    // Pass the userId to the view
                    ViewBag.UserId = dashboard.UserId;

                    // Get the DashboardType in case the user has already chosen one for the current DashboardSetting
                    DashboardType dashboardType = await _dashboardTypeHandler.GetDashboardType(dashboardSetting.DashboardTypeId);
                    ViewBag.DashboardTypeName = dashboardType?.DashboardTypeValue;

                    return View(dashboardSetting);
                }
            }

            return NotFound();
        }

        //POST - Update a setting
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDashboardSetting([Bind("DashboardSettingId,DashboardId,DashboardTypeId,RefreshRate,XLabel,YLabel,DashboardTypeValue,IntegrationSettingId")] DashboardSetting dashboardSetting)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _dashboardSettingHandler.UpdateDashboardSetting(dashboardSetting);
                    
                    Dashboard dashboard = await _dashboardHandler.GetDashboard(dashboardSetting.DashboardId);
                    ViewBag.DashboardName = dashboard.DashboardName;
                    ViewBag.UserId = dashboard.UserId;

                    return View("DashboardSetting", dashboardSetting);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw e;
                }
            }

            return BadRequest(ModelState);
        }

        // GET: Dashboards/Edit/5
        //Used for retrieveing the page to edit/update a dashboard
        public async Task<IActionResult> Edit(Guid dashboardId)
        {
            if (dashboardId != Guid.Empty)
            {
                Dashboard dashboard = await _dashboardHandler.GetDashboard(dashboardId);

                if (dashboard != null)
                {
                    return View(dashboard);
                }
            }

            return NotFound();
        }

        // POST: Dashboards/Edit/5
        //Used to post the changes made to the specified dashboard
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("DashboardName,DashboardId,DashboardSettingId,DateCreated,DateDeleted,UserId")] Dashboard dashboard)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dashboard.DateModified = DateTime.UtcNow;
                    await _dashboardHandler.UpdateDashboard(dashboard);

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw e;
                }
            }
            return View(dashboard);
        }

        // POST: Dashboards/Delete/5
        //Post the delete of the dashboard
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid dashboardId)
        {
            Dashboard dashboard = await _dashboardHandler.GetDashboard(dashboardId);

            if (dashboard != null)
            {
                await _dashboardHandler.DeleteDashboard(dashboard);
            }
            
            return RedirectToAction(nameof(Index));
        }

        //Helper method to determine if the dashboard exists in the DB before we try to update/delete it
        private async Task<bool> DashboardExists(Guid dashboardId)
        {
            bool dashBoardExists = false;

            Dashboard dashboard = await _dashboardHandler.GetDashboard(dashboardId);

            if (dashboard != null)
            {
                dashBoardExists = true;
            }

            return dashBoardExists;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<JsonResult> GetNewestDataSet(Guid integrationSettingId)
        {
            return await _dataSetController.GetDataSet(integrationSettingId);
        }

        public async Task<JsonResult> GetNewestDataSets(Guid integrationSettingId, int amountOfDataSets)
        {
            return await _dataSetController.GetAmountOfDataSets(integrationSettingId, amountOfDataSets);
        }

        public async Task<List<Integration>> GetIntegrationsAndSettings(Guid userId)
        {
            return await _integrationController.IntegrationsAndSettingsByUserId(userId);
        }
    }
}