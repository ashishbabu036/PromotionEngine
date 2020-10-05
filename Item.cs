using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
 
    /// <summary>
    /// Item Definition
    /// </summary>
    public class Item
    {
        private char sKU;       
        private int price;

        public char SKU { get => sKU; set => sKU = value; }      
        public int Price { get => price; set => price = value; }
    }
}
