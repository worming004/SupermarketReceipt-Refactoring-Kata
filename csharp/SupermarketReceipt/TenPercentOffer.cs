namespace SupermarketReceipt
{
    public class TenPercentOffer : Offer
    {

        public TenPercentOffer(SupermarketCatalog catalog, Product product, double argument) : base(SpecialOfferType.TenPercentDiscount, catalog, product, argument)
        {

        }

        public override Discount ApplyDiscount(double quantity)
        {
            const int minimalQuantityForDiscount = 5;
            var unitPrice = _catalog.GetUnitPrice(_product);
            var quantityAsInt = (int)quantity;
            var numberOfXs = quantityAsInt / minimalQuantityForDiscount;
            var discountTotal = unitPrice * quantity - (this.Argument * numberOfXs + quantityAsInt % 5 * unitPrice);
            return new Discount(_product, minimalQuantityForDiscount + " for " + this.Argument, -discountTotal);
        }
    }
}
