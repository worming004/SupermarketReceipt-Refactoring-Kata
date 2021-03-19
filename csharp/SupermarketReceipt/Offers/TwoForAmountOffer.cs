namespace SupermarketReceipt
{
    public class TwoForAmountOffer : Offer
    {
        private double _amount;
        public TwoForAmountOffer(SupermarketCatalog catalog, Product product, double argument) : base(SpecialOfferType.TwoForAmount, catalog, product)
        {
            this._amount = argument;
        }
        public override Discount ApplyDiscount(double quantity)
        {
            const int minimalQuantityForDiscount = 2;
            var unitPrice = _catalog.GetUnitPrice(_product);
            var quantityAsInt = (int)quantity;
            if (quantityAsInt >= 2)
            {
                var total = this._amount * (quantityAsInt / minimalQuantityForDiscount) + quantityAsInt % 2 * unitPrice;
                var discountN = unitPrice * quantity - total;
                return new Discount(this._product, "2 for " + this._amount, -discountN);
            }
            return null;
        }
    }
}
