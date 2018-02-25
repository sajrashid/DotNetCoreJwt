using MetricsRazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsRazor.Interfaces
{
    public interface IMetricsRepository
    {
      Task<Metrics> GetAll();
    }
}
