using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine
{
    public class NPromotionalRule : IApplyPromotionalRule
    {
        /// <summary>
        /// Logic to Calculate Price when Applying NPromotional Rule.
        /// </summary>
        /// <param name="total">Result of Calculations</param>
        /// <param name="items">Total No. of SKU Items </param>
        /// <param name="ruleForGroup">Promotional Rule to be applied</param>
        /// <returns></returns>
        public int Calculate(int total, List<Item> items, PromotionRule ruleForGroup)
        {
            var itemGroup = items.Where(g => g.SKU == ruleForGroup.SKU);
            var groupCount = itemGroup.Count();

            var extra = groupCount - ruleForGroup.ItemCount;
            if (extra < 0)
            {
                total += itemGroup.Sum(g => g.Price);
            }
            else
            {
                total += (groupCount / ruleForGroup.ItemCount) * ruleForGroup.Price;
                total += (groupCount % ruleForGroup.ItemCount) * itemGroup.First().Price;
            }

            return total;
        }
    }
}
