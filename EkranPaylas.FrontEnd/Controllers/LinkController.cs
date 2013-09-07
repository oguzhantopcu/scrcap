﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EkranPaylas.Service;

namespace EkranPaylas.FrontEnd.Controllers
{
    public class LinkController : Controller
    {
        private readonly IScreenShotService _screenShotService;

        public LinkController(IScreenShotService screenShotService)
        {
            _screenShotService = screenShotService;
        }

        public ActionResult Index(string id)
        {
            var links = _screenShotService.GetSources(id);

            return View(links);
        }
    }
}
