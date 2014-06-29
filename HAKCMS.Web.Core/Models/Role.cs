using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAKCMS.Web.Core.Models
{
    public class Roles
    {
        public const string Admin = "Admin";
        public const string Standart = "Standart";
        public const string SuperAdmin = "SuperAdmin";
    }

    //asp.net identityden alındı dikkat, membership valueları farklı
    public enum UserRoles
    {
        SuperAdmin = 3, //asp.net identityde 3 diğerinde farklı
        Admin = 4,
        Standart = 1
    }
}
