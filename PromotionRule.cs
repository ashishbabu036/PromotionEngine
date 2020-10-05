namespace PromotionEngine
{
    public class PromotionRule
    {
        private char sKU;
        private int itemCount;
        private int price;
        private char? sKU2;       

        public char SKU { get => sKU; set => sKU = value; }
        public int ItemCount { get => itemCount; set => itemCount = value; }
        public int Price { get => price; set => price = value; }
        public bool IsTwoSKUInvolved { get => sKU2 != null; }
        public char? SKU2 { get => sKU2; set => sKU2 = value; }
    }
}
