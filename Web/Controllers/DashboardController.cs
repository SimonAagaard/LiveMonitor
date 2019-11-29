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
using System.Collections.Generic;

namespace Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly DashboardHandler _dashboardHandler;
        private readonly DashboardSettingHandler _dashboardSettingHandler;
        private readonly DashboardTypeHandler _dashboardTypeHandler;
        private readonly DataSetHandler _dataSetHandler;

        public DashboardController()
        {
            _dashboardHandler = new DashboardHandler();
            _dashboardSettingHandler = new DashboardSettingHandler();
            _dataSetHandler = new DataSetHandler();
            _dashboardTypeHandler = new DashboardTypeHandler();

        }

        // GET: Dashboards
        //Get all dashboards for the logged in user.
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            string[] children = new string[] { "DashboardSetting" };
            var dashboards = await _dashboardHandler.GetDashboardAndDashboardSetting(userId, children);
            //If there is dashboards in the DB pass them to the view
            if (dashboards.Any())
            {
                return View(dashboards);
            }
            return View();
        }

        public async Task<IActionResult> Dashboard(Guid id)
        {
            var dashboard = await _dashboardHandler.GetDashboard(id);
            return View(dashboard);
        }

        //Used by the POC realtime dashboard, can be removed or refactored when we get real data through integrations
        public JsonResult GetRealTimeData()
        {
            Random rdn = new Random();
            RealTimeData data = new RealTimeData
            {
                TimeStamp = DateTime.Now,
                DataValue = rdn.Next(0, 11)
            };
            return Json(data);
        }

        public async Task<JsonResult> GetDataSet(Guid integrationSettingId)
        {
            if (integrationSettingId == Guid.Empty)
            {
                throw new Exception();
            }

            var dataSet = await _dataSetHandler.GetNewestDataSetByIntegrationSettingIdFromDateTime(integrationSettingId, DateTime.Now.AddMinutes(-70));

            if (dataSet == null)
            {
                throw new Exception();
            }

            dataSet.XValue = dataSet.XValue.AddHours(1);

            return Json(dataSet);
        }

        public async Task<JsonResult> GetDataSets(Guid integrationSettingId)
        {
            if (integrationSettingId != Guid.Empty)
            {
                List<DataSet> dataSets = await _dataSetHandler.GetDataSetsFromAGivenTimePeriod(integrationSettingId, DateTime.Now.AddMinutes(-200), DateTime.Now.AddMinutes(60));

                dataSets = dataSets.OrderBy(x => x.XValue).TakeLast(100).ToList();

                if (dataSets.Count > 0)
                {
                    foreach (DataSet dataSet in dataSets)
                    {
                        dataSet.XValue = dataSet.XValue.AddHours(1);
                    }

                    return Json(dataSets);
                }
            }

            return Json("");
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
                    DashboardId = dashboard.DashboardId,
                };

                await _dashboardSettingHandler.CreateDashboardSetting(dashboardSetting);
                //Passes the Ids needed by the Dashboardsetting view
                return RedirectToAction(nameof(DashboardSetting), new { dashboardSettingId = dashboardSetting.DashboardSettingId });
            }
            return View(dashboard);
        }

        //Get DashboardSetting view
        public async Task<IActionResult> DashboardSetting(Guid dashboardSettingId)
        {
            if (dashboardSettingId == Guid.Empty)
            {
                return NotFound();
            }

            var dashboardSetting = await _dashboardSettingHandler.GetDashboardSetting(dashboardSettingId);
            if (dashboardSetting == null)
            {
                return NotFound();
            }

            //Sets viewbag to display the name of the dashboard linked to the setting page
            var dashboard = await _dashboardHandler.GetDashboard(dashboardSetting.DashboardId);
            ViewBag.DashboardName = dashboard.DashboardName;
            //Get the DashboardType in case the user has already chosen one for the current DashboardSetting
            var dashboardType = _dashboardTypeHandler.GetDashboardType(dashboardSetting.DashboardTypeId);
            ViewBag.DashboardTypeName = dashboardType.Result?.DashboardName;

            return View(dashboardSetting);
        }

        //POST - Update a setting
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDashboardSetting([Bind("DashboardSettingId,DashboardId,DashboardTypeId,RefreshRate,XLabel,YLabel")] DashboardSetting dashboardSetting)
        {
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
                var dashboard = await _dashboardHandler.GetDashboard(dashboardSetting.DashboardId);
                ViewBag.DashboardName = dashboard.DashboardName;
                return View("DashboardSetting", dashboardSetting);

            }
            return BadRequest(ModelState);
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
