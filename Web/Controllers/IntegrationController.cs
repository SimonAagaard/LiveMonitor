using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Data.Handlers;
using System.Security.Claims;

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
            Guid userId = Guid.Parse(User?.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Get all integrations based on the userId
            List<Integration> integrations = await _integrationHandler.GetIntegrationsByUserId(userId);
            
            //If there is integrations in the DB pass them to the view
            if (integrations.Any())
            {
                return View(integrations);
            }
            return View();
        }

        // Method to get all integrations along with their settings based on a user Id
        public async Task<List<Integration>> IntegrationsAndSettingsByUserId(Guid userId)
        {
            string[] integrationSetting = new string[] { "IntegrationSetting" };

            // Get integrations including the Setting object
            List<Integration> integrations = await _integrationHandler.GetIntegrationsAndSettingsByUserId(userId, integrationSetting);

            if (integrations.Any())
            {
                return integrations;
            }

            return new List<Integration>();
        }

        // GET: Integration/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Integration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IntegrationName")] Integration integration)
        {
            if (ModelState.IsValid)
            {
                Guid userId = Guid.Parse(User?.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (userId != Guid.Empty)
                {
                    integration.UserId = userId;
                    integration.IntegrationSettingId = Guid.NewGuid();
                    integration.IntegrationId = Guid.NewGuid();
                    IntegrationSetting integrationSetting = new IntegrationSetting
                    {
                        IntegrationSettingId = integration.IntegrationSettingId,
                        IntegrationId = integration.IntegrationId,
                    };
                    integration.IntegrationSetting = integrationSetting;

                    // Create the integration object with the added integrationSetting
                    await _integrationHandler.CreateIntegration(integration);

                    //Passes the Ids needed by the IntegrationSetting view
                    return RedirectToAction(nameof(IntegrationSetting), new { integrationSettingId = integrationSetting.IntegrationSettingId });
                }
            }

            return View(integration);
        }

        public async Task<IActionResult> IntegrationSetting(Guid integrationSettingId)
        {
            if (integrationSettingId != Guid.Empty)
            {
                IntegrationSetting integrationSetting = await _integrationSettingHandler.GetIntegrationSetting(integrationSettingId);

                if (integrationSetting != null)
                {
                    //Sets viewbag to display the name of the integration linked to the IntegrationSetting page
                    Integration integration = await _integrationHandler.GetIntegration(integrationSetting.IntegrationId);
                    ViewBag.IntegrationName = integration.IntegrationName;

                    return View(integrationSetting);
                }
            }

            return NotFound();
        }

        // Update an integrationsetting
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateIntegrationSetting([Bind("IntegrationSettingId","IntegrationId","ClientId","ClientSecret","TenantId",
            "ResourceId","ResourceUrl","IsActive","MetricName","Aggregation","Interval","MinutesOffset")] IntegrationSetting integrationSetting)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _integrationSettingHandler.UpdateIntegrationSetting(integrationSetting);

                    return View("IntegrationSetting", integrationSetting);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw e;
                }
            }

            return BadRequest(ModelState);
        }

        // GET: Integration/Edit/5
        public async Task<IActionResult> Edit(Guid integrationId)
        {
            if (integrationId != null)
            {
                Integration integration = await _integrationHandler.GetIntegration(integrationId);

                if (integration != null)
                {
                    return View(integration);
                }
            }

            return NotFound();
        }

        // POST: Integration/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IntegrationId,UserId,IntegrationSettingId,IntegrationName")] Integration integration)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _integrationHandler.UpdateIntegration(integration);

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw e;
                }
            }

            return View(integration);
        }

        // POST: Integration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid integrationId)
        {
            Integration integration = await _integrationHandler.GetIntegration(integrationId);

            if (integration != null)
            {
                await _integrationHandler.DeleteIntegration(integration);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}