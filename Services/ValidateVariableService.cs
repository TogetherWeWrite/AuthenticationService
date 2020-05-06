using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
namespace AuthenticationService.Services
{
    public static class ValidateVariableService
    {
        #region NullOrEmptyChecks
        /// <summary>
        /// General static method that is used for giving the format exception that a string is empty or not
        /// </summary>
        /// <param name="param">string you want to check if it is empty or not</param>
        /// <param name="paramname">the parameter name that you want to include in the exception message</param>
        /// <exception cref="FormatException">when string param is empty or null</exception>
        /// <returns></returns>
        public static bool IsStringNotNullOrEmpty(this string param, string paramname)
        {
            if (param.IsNullOrEmpty())
            {
                throw new FormatException(paramname + " is empty or null it is required to fill this");
            }
            return true;
        }
        #endregion
    }
}
