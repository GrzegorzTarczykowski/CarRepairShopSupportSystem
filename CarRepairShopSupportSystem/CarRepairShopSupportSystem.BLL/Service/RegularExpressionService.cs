using CarRepairShopSupportSystem.BLL.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Service
{
    public class RegularExpressionService : IRegularExpressionService
    {
        public bool IsMatchOnlyAlphabeticCharacters(string input)
        {
            return new Regex(@"^[A-Za-z]+$").IsMatch(input);
        }
    }
}
