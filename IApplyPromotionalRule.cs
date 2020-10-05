using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public interface IApplyPromotionalRule
    {
        /// <summary>
        /// Common Method to Be Implemented for Both Promotional Rules
        /// </summary>
        /// <param name="total"></param>
        /// <param name="items"></param>
        /// <param name="ruleForGroup"></param>
        /// <returns></returns>
        int Calculate(int total, List<Item> items,  PromotionRule ruleForGroup);
    }
}
