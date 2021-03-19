namespace SupermarketReceipt
{
    public class TenPercentOffer : Offer
    {

        public TenPercentOffer(SupermarketCatalog catalog, Product product, double argument) : base(SpecialOfferType.TenPercentDiscount, catalog, product, argument)
        {

        }

        public override Discount ApplyDiscount(double quantity)
        {
            var unitPrice = _catalog.GetUnitPrice(_product);
            var quantityAsInt = (int)quantity;
            return new Discount(_product, this.Argument + "% off", -quantityAsInt * unitPrice * this.Argument / 100.0);
        }
    }
}
