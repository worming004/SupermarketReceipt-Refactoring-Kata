using System.Collections.Generic;
using System.Linq;

namespace SupermarketReceipt
{
    public class Teller
    {
        private readonly SupermarketCatalog _catalog;
        private readonly Dictionary<Product, Offer> _offers = new Dictionary<Product, Offer>();

        public Teller(SupermarketCatalog catalog)
        {
            _catalog = catalog;
        }

        public void AddSpecialOffer(SpecialOfferType offerType, Product product, double argument)
        {
            SpecialOfferType[] managedSpecialOfferTypesWithNew = new SpecialOfferType[] { SpecialOfferType.FiveForAmount, SpecialOfferType.TwoForAmount };
            if (managedSpecialOfferTypesWithNew.Contains(offerType))
            {
                _offers[product] = Offer.New(offerType, _catalog, product, argument);
            }
            else
            {
                _offers[product] = new Offer(offerType, _catalog, product, argument);
            }
        }

        public Receipt ChecksOutArticlesFrom(ShoppingCart theCart)
        {
            var receipt = new Receipt();
            var productQuantities = theCart.GetItems();
            foreach (var pq in productQuantities)
            {
                var p = pq.Product;
                var quantity = pq.Quantity;
                var unitPrice = _catalog.GetUnitPrice(p);
                var price = quantity * unitPrice;
                receipt.AddProduct(p, quantity, unitPrice, price);
            }

            theCart.HandleOffers(receipt, _offers, _catalog);

            return receipt;
        }
    }
}