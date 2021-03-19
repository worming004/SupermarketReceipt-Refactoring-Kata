namespace SupermarketReceipt
{
    public enum SpecialOfferType
    {
        ThreeForTwo,
        TenPercentDiscount,
        TwoForAmount,
        FiveForAmount
    }

    public class Offer
    {
        protected Product _product;
        protected SupermarketCatalog _catalog;

        public Offer(SpecialOfferType offerType, SupermarketCatalog catalog, Product product, double argument)
        {
            OfferType = offerType;
            _catalog = catalog;
            Argument = argument;
            _product = product;
        }

        public SpecialOfferType OfferType { get; private set; }
        public double Argument { get; }

        public static Offer New(SpecialOfferType offerType, SupermarketCatalog catalog, Product product, double argument)
        {
            switch (offerType)
            {
                case SpecialOfferType.FiveForAmount:
                    return new FiveForAmountOffer(catalog, product, argument);
                case SpecialOfferType.TenPercentDiscount:
                case SpecialOfferType.TwoForAmount:
                case SpecialOfferType.ThreeForTwo:
                default:
                    return null;
            }
        }

        public virtual Discount ApplyDiscount(double quantity)
        {
            return null;
        }
    }

    public class FiveForAmountOffer : Offer {

        public FiveForAmountOffer(SupermarketCatalog catalog, Product product, double argument) : base(SpecialOfferType.FiveForAmount, catalog, product, argument)
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