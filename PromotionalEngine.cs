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


        public int CalcualtePrice()
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
                            skuProcessed.Add((char)ruleForGroup.SKU2);
                        }
                        else
                        {
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

        public void AddItem(Item item)
        {
            items.Add(item);
        }

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
