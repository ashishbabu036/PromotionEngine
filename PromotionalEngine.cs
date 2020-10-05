using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine
{
    public class PromotionalEngine
    {
        private readonly List<Item> items = new List<Item>();
        private readonly List<PromotionRule> promotionRules = new List<PromotionRule>();
        private readonly PairPromotionalRule pairPromotionalRule;
        private readonly NPromotionalRule nPromotionalRule;

        public PromotionalEngine()
        {
            pairPromotionalRule = new PairPromotionalRule();
            nPromotionalRule = new NPromotionalRule();
        }

        /// <summary>
        /// Logic to Apply Relevant Promotional Rule.
        /// </summary>
        /// <returns></returns>
        public int CalculatePrice()
        {
            int total = 0;
            var itemGroups = items.GroupBy(g => g.SKU);
            List<char> skuProcessed = new List<char>();
            foreach (var itemGroup in itemGroups)
            {
                if (!skuProcessed.Contains(itemGroup.Key))
                {
                    var ruleForGroup = promotionRules.FirstOrDefault(r => r.SKU == itemGroup.Key);

                    if (ruleForGroup != null)
                    {
                        if (ruleForGroup.IsTwoSKUInvolved)
                        {
                            total = pairPromotionalRule.Calculate(total, items, ruleForGroup);
                            skuProcessed.Add((char)ruleForGroup.SKU2);
                        }
                        else
                        {
                            total = nPromotionalRule.Calculate(total,items, ruleForGroup);
                        }
                        skuProcessed.Add(ruleForGroup.SKU);
                    }
                    else
                    {
                        total += itemGroup.Sum(x => x.Price);
                    }
                }

            }
            return total;
        }       

        /// <summary>
        /// Adding SKU Items
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            items.Add(item);
        }

        /// <summary>
        /// Adding Promotional Rule.
        /// </summary>
        /// <param name="rule"></param>
        public void AddPricingRule(PromotionRule rule)
        {
            if (rule == null)
            {
                throw new ArgumentNullException();
            }

            promotionRules.Add(rule);
        }


    }
}
