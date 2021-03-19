namespace SupermarketReceipt
{
    public class FiveForAmountOffer : Offer
    {
        private readonly double amount;

        public FiveForAmountOffer(SupermarketCatalog catalog, Product product, double amount) : base(SpecialOfferType.FiveForAmount, catalog, product)
        {
            this.amount = amount;
        }

        const int minimalQuantityForDiscount = 5;

        public override Discount ApplyDiscount(double quantity)
        {

            if (quantity < minimalQuantityForDiscount)
                return null;

            var unitPrice = _catalog.GetUnitPrice(_product);
            var quantityAsInt = (int)quantity;

            var numberOfXs = quantityAsInt / minimalQuantityForDiscount;
            var discountTotal = unitPrice * quantity - (this.amount * numberOfXs + AmountExceedingTheOffer(quantityAsInt, unitPrice));
            return new Discount(_product, minimalQuantityForDiscount + " for " + this.amount, -discountTotal);
        }

        private double AmountExceedingTheOffer(int quantityAsInt, double unitPrice)
        {
            return quantityAsInt % minimalQuantityForDiscount * unitPrice;
        }
    }
}
