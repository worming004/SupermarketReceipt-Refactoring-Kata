namespace SupermarketReceipt
{
    public class TenPercentOffer : Offer
    {
        private double _amount;

        public TenPercentOffer(SupermarketCatalog catalog, Product product, double argument) : base(SpecialOfferType.TenPercentDiscount, catalog, product)
        {
            _amount = argument;
        }

        public override Discount ApplyDiscount(double quantity)
        {
            var unitPrice = _catalog.GetUnitPrice(_product);
            var quantityAsInt = (int)quantity;
            return new Discount(_product, this._amount + "% off", -quantityAsInt * unitPrice * this._amount / 100.0);
        }
    }
}
