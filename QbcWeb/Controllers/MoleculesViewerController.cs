using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QbcWeb.Models;

namespace QbcWeb.Controllers
{
    public class MoleculesViewerController : Controller
    {

        public IActionResult Index(int Id)
        {
            return View("Molecules3DView", new Molecules3DViewerModel()
            {
                MoleculeId =Id
            });
        }
    }
}