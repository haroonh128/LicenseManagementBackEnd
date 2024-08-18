using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Models
{
    public class JwtConfig
    {
        public string Key { get; set; } = string.Empty;
        public string CLient_URL { get; set; } = string.Empty;
    }
}
