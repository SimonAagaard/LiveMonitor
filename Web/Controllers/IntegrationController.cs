﻿using System;
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
            //If there is integrations in the DB pass them to the view
            if (integrations.Any())
            {
                return View(integrations);
            }
            return View();
        }

        // GET: Integration/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Integration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IntegrationName")] Integration integration)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                integration.UserId = Guid.Parse(userId);
                integration.IntegrationSettingId = Guid.NewGuid();
                integration.IntegrationId = Guid.NewGuid();
                await _integrationHandler.CreateIntegration(integration);

                IntegrationSetting integrationSetting = new IntegrationSetting
                {
                    IntegrationSettingId = Guid.NewGuid(),
                    IntegrationId = integration.IntegrationId,
                };
                await _integrationSettingHandler.CreateIntegrationSetting(integrationSetting);

                //Passes the Ids needed by the IntegrationSetting view
                return RedirectToAction(nameof(IntegrationSetting), new { integrationSettingId = integrationSetting.IntegrationSettingId });
            }
            return View(integration);
        }
        public async Task<IActionResult> IntegrationSetting(Guid integrationSettingId)
        {
            if (integrationSettingId == Guid.Empty)
            {
                return NotFound();
            }

            var integrationSetting = await _integrationSettingHandler.GetIntegrationSetting(integrationSettingId);
            if (integrationSetting == null)
            {
                return NotFound();
            }

            //Sets viewbag to display the name of the integration linked to the IntegrationSetting page
            var integration = await _integrationHandler.GetIntegration(integrationSetting.IntegrationId);
            ViewBag.IntegrationId = integration.IntegrationName;

            return View(integrationSetting);
        }

        // GET: Integration/Edit/5
        public async Task<IActionResult> Edit(Guid integrationId)
        {
            if (integrationId == null)
            {
                return NotFound();
            }

            var integration = await _integrationHandler.GetIntegration(integrationId);
            if (integration == null)
            {
                return NotFound();
            }

            return View(integration);
        }

        // POST: Integration/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IntegrationId,UserId,IntegrationSettingId,IntegrationName")] Integration integration)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _integrationHandler.UpdateIntegration(integration);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw e;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(integration);
        }

        // POST: Integration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid integrationId)
        {
            var integration = await _integrationHandler.GetIntegration(integrationId);
            await _integrationHandler.DeleteIntegration(integration);
            return RedirectToAction(nameof(Index));
        }
    }
}
