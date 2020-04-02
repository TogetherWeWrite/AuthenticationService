using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Data
{
    public class AccountstoreDatabaseSettings: IAccountstoreDatabaseSettings
    {
        public string DefaultConnection { get; set; }
    }

    public interface IAccountstoreDatabaseSettings
    {
        string DefaultConnection { get; set; }
    }
}
