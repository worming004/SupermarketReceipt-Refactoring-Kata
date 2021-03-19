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

        public SpecialOfferType OfferType { get; private set; }

        public Offer(SpecialOfferType offerType, SupermarketCatalog catalog, Product product)
        {
            OfferType = offerType;
            _catalog = catalog;
            _product = product;
        }

        public static Offer New(SpecialOfferType offerType, SupermarketCatalog catalog, Product product, double argument)
        {
            switch (offerType)
            {
                case SpecialOfferType.FiveForAmount:
                    return new FiveForAmountOffer(catalog, product, argument);
                case SpecialOfferType.TwoForAmount:
                    return new TwoForAmountOffer(catalog, product, argument);
                case SpecialOfferType.TenPercentDiscount:
                    return new TenPercentOffer(catalog, product, argument);
                case SpecialOfferType.ThreeForTwo:
                    return new ThreeForTwoOffer(catalog, product);
                default:
                    return null;
            }
        }

        public virtual Discount ApplyDiscount(double quantity)
        {
            return null;
        }
    }
}