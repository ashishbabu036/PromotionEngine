using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine
{
    public class PairPromotionalRule : IApplyPromotionalRule
    {
        /// <summary>
        /// Logic to Calculate Price when Applying ]Pair-Promotional Rule(1A and 1B costs N Rs).
        /// </summary>
        /// <param name="total">Result of Calculations</param>
        /// <param name="items">Total No. of SKU Items </param>
        /// <param name="ruleForGroup">Promotional Rule to be applied</param>
        /// <returns></returns>
        public int Calculate(int total, List<Item> items, PromotionRule ruleForGroup)
        {
            var groupCount1 = items.Where(g => g.SKU == ruleForGroup.SKU).Count();
            var groupCount2 = items.Where(g => g.SKU == ruleForGroup.SKU2).Count();
            int count = 0;

            while (groupCount1 > 0 && groupCount2 > 0)
            {
                count++;
                groupCount1--;
                groupCount2--;
            }

            total += count * ruleForGroup.Price;
            total += groupCount1 * items.Where(x => x.SKU == ruleForGroup.SKU).Select(x => x.Price).FirstOrDefault();
            total += groupCount2 * items.Where(x => x.SKU == ruleForGroup.SKU2).Select(x => x.Price).FirstOrDefault();
            return total;
        }
    }
}
