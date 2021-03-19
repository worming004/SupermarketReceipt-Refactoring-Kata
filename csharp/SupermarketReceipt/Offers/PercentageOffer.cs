namespace SupermarketReceipt
{
    public class PercentageOffer : Offer
    {
        private double _percentage;

        public PercentageOffer(SupermarketCatalog catalog, Product product, double percentage) : base(SpecialOfferType.TenPercentDiscount, catalog, product)
        {
            _percentage = percentage;
        }

        public override Discount ApplyDiscount(double quantity)
        {
            var unitPrice = _catalog.GetUnitPrice(_product);
            var quantityAsInt = (int)quantity;
            return new Discount(_product, _percentage + "% off", -quantityAsInt * unitPrice * _percentage / 100.0);
        }
    }
}
