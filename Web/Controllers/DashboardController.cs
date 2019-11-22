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
using Data.Integrations;

namespace Web.Controllers
{
    [Authorize]
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
        //Get all dashboards for the logged in user.
        public async Task<IActionResult> Index()
        {
            IntegrationHandler integrationHandler = new IntegrationHandler();
            IntegrationSettingHandler integrationSettingHandler = new IntegrationSettingHandler();

            //var integration = new Integration
            //{
            //    IntegrationId = Guid.NewGuid(),
            //    IntegrationName = "Azure Test",
            //    IntegrationSettingId = Guid.NewGuid(),
            //    UserId = new Guid("88460F1B-7E0C-EA11-A6A2-C86000C03827"),
            //};

            //await integrationHandler.CreateIntegration(integration);
            //var integrationSetting = new IntegrationSetting
            //{
            //    ClientId = "2de6a64e-eb0c-4275-9952-4ce1d3f0d131",
            //    ClientSecret = "A1l3?[ReU3?L8eEhaYpcUPJG]jEX0_X5",
            //    IntegrationId = integration.IntegrationId,
            //    IntegrationSettingId = integration.IntegrationSettingId,
            //    TenantId = "92404485-d794-4fc2-8d0d-587d30cba2ad",
            //    ResourceId = "https://management.azure.com",
            //    ResourceUrl = @"/subscriptions/2c24d5f6-cb4d-4857-88f8-fe5c9a827f7c/resourceGroups/LiveMonitor/providers/Microsoft.Web/sites/LiveMonitorApp/"
            //};
            //await integrationSettingHandler.CreateIntegrationSetting(integrationSetting);

            var integrationSetting = await integrationSettingHandler.GetIntegration(new Guid("966d8d9c-6736-4da3-a48a-a9032f512a7a"));

            AzureConnector conn = new AzureConnector();
            await conn.GetAzureDataAsync(integrationSetting);

            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var dashboards = await _dashboardHandler.GetDashboardsByUserId(userId);
            //If there is dashboards in the DB pass them to the view
            if (dashboards.Any())
            {
                return View(dashboards);
            }
            return View();
        }

        Random rdn = new Random();

        public async Task<IActionResult> Dashboard(Guid id)
        {
            var dashboard = await _dashboardHandler.GetDashboard(id);
            return View(dashboard);
        }

        //Used by the POC realtime dashboard, can be removed or refactored when we get real data through integrations
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

                //Checks if there is a current user to bind the dashboard to
                if (userId == Guid.Empty.ToString())
                {
                    return NotFound("No current user available");
                }
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

                return RedirectToAction(nameof(DashboardSetting), new {id =dashboardSetting.DashboardSettingId });
            }
            return View(dashboard);
        }
        //Get DashboardSetting view
        public async Task<IActionResult> DashboardSetting(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var dashboardSetting = await _dashboardSettingHandler.GetDashboardSetting(id);
            if (dashboardSetting == null)
            {
                return NotFound();
            }

            //Sets viewbag to display the name of the dashboard linked to the setting page
            var dashboard = await _dashboardHandler.GetDashboard(dashboardSetting.DashboardId);
            ViewBag.DashboardName = dashboard.DashboardName;
            return View(dashboardSetting);
        }
        //POST - Update a setting
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDashboardSetting(Guid id, [Bind("DashboardSettingId,DashboardId,DashboardTypeId,RefreshRate,XLabel,YLabel")] DashboardSetting dashboardSetting)
        {
            if (id != dashboardSetting.DashboardSettingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _dashboardSettingHandler.UpdateDashboardSetting(dashboardSetting);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw e;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dashboardSetting);
        }

        // GET: Dashboards/Edit/5
        //Used for retrieveing the page to edit/update a dashboard
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
        public async Task<IActionResult> Edit([Bind("DashboardName,DashboardId,DashboardSettingId,DateCreated,DateDeleted,UserId")] Dashboard dashboard)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dashboard.DateModified = DateTime.Now;
                    await _dashboardHandler.UpdateDashboard(dashboard);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw e;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dashboard);
        }

        // POST: Dashboards/Delete/5
        //Post the delete of the dashboard
        [HttpPost]       
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dashboard = await _dashboardHandler.GetDashboard(id);
            await _dashboardHandler.DeleteDashboard(dashboard);
            return RedirectToAction(nameof(Index));
        }

        //Helper method to determine if the dashboard exists in the DB before we try to update/delete it
        private bool DashboardExists(Guid dashboardId)
        {
            var dashboard = _dashboardHandler.GetDashboard(dashboardId);
            bool dashboardExists = dashboard != null ? true : false;
            return dashboardExists;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public class RealTimeData
    {
        public DateTime TimeStamp { get; set; }
        public double DataValue { get; set; }
    }

}
