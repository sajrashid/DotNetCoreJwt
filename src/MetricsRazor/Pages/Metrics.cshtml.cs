using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetricsRazor.Models;
using MetricsRazor.Interfaces;

namespace MetricsRazor.Pages
{
    public class MetricsModel : PageModel
    {

        private IMetricsRepository _metricsRepo;
      

        public MetricsModel(IMetricsRepository metricsRepo)
        {
            _metricsRepo = metricsRepo;
        }

        public  void OnGet()
        {
            var mbox =  _metricsRepo.GetAll().GetAwaiter().GetResult();
            
        }
    }
}