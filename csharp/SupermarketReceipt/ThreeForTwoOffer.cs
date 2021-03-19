namespace SupermarketReceipt
{
    public class ThreeForTwoOffer : Offer
    {

        public ThreeForTwoOffer(SupermarketCatalog catalog, Product product, double argument) : base(SpecialOfferType.ThreeForTwo, catalog, product, argument)
        {

        }

        public override Discount ApplyDiscount(double quantity)
        {
            const int minimalQuantityForDiscount = 3;
            var unitPrice = _catalog.GetUnitPrice(_product);
            var quantityAsInt = (int)quantity;
            var numberOfXs = quantityAsInt / minimalQuantityForDiscount;
            if (quantityAsInt > 2)
            {
                var discountAmount = quantity * unitPrice - (numberOfXs * 2 * unitPrice + quantityAsInt % 3 * unitPrice);
                return new Discount(_product, "3 for 2", -discountAmount);
            }

            return null;
        }
    }
}
