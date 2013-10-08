using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Models
{
    public enum IPMonitorStatus
    {
        Pending = 0,
        Normal = 1,
        Unknown = 2,
        Exception = 3
    }

    public enum RoleType
    {
        SuperAdministrator = 0,
        Administrators = 1,
        ClientUsers = 2
    }
}
