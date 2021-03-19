using System.Collections.Generic;

namespace SupermarketReceipt
{
    public class ShoppingCart
    {
        private readonly List<ProductQuantity> _items = new List<ProductQuantity>();
        private readonly Dictionary<Product, double> _productQuantities = new Dictionary<Product, double>();


        public List<ProductQuantity> GetItems()
        {
            return new List<ProductQuantity>(_items);
        }

        public void AddItem(Product product)
        {
            AddItemQuantity(product, 1.0);
        }


        public void AddItemQuantity(Product product, double quantity)
        {
            _items.Add(new ProductQuantity(product, quantity));
            if (_productQuantities.ContainsKey(product))
            {
                var newAmount = _productQuantities[product] + quantity;
                _productQuantities[product] = newAmount;
            }
            else
            {
                _productQuantities.Add(product, quantity);
            }
        }

        public void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers, SupermarketCatalog catalog)
        {
            foreach (var product in _productQuantities.Keys)
                HandleSingleOffer(receipt, offers, catalog, product);
        }

        private void HandleSingleOffer(Receipt receipt, Dictionary<Product, Offer> offers, SupermarketCatalog catalog, Product product)
        {
            var quantity = _productQuantities[product];
            var quantityAsInt = (int)quantity;
            if (offers.ContainsKey(product))
            {
                var offer = offers[product];
                var unitPrice = catalog.GetUnitPrice(product);
                Discount discount = null;
                var minimalQuantityForDiscount = 1;
                if (offer.OfferType == SpecialOfferType.ThreeForTwo)
                {
                    minimalQuantityForDiscount = 3;
                }
                else if (offer.OfferType == SpecialOfferType.TwoForAmount)
                {
                    minimalQuantityForDiscount = 2;
                    if (quantityAsInt >= 2)
                    {
                        var total = offer.Argument * (quantityAsInt / minimalQuantityForDiscount) + quantityAsInt % 2 * unitPrice;
                        var discountN = unitPrice * quantity - total;
                        discount = new Discount(product, "2 for " + offer.Argument, -discountN);
                    }
                }

                if (offer.OfferType == SpecialOfferType.FiveForAmount) minimalQuantityForDiscount = 5;
                var numberOfXs = quantityAsInt / minimalQuantityForDiscount;
                if (offer.OfferType == SpecialOfferType.ThreeForTwo && quantityAsInt > 2)
                {
                    var discountAmount = quantity * unitPrice - (numberOfXs * 2 * unitPrice + quantityAsInt % 3 * unitPrice);
                    discount = new Discount(product, "3 for 2", -discountAmount);
                }

                if (offer.OfferType == SpecialOfferType.TenPercentDiscount) discount = new Discount(product, offer.Argument + "% off", -quantity * unitPrice * offer.Argument / 100.0);
                if (offer.OfferType == SpecialOfferType.FiveForAmount && quantityAsInt >= 5)
                {
                    var discountTotal = unitPrice * quantity - (offer.Argument * numberOfXs + quantityAsInt % 5 * unitPrice);
                    discount = new Discount(product, minimalQuantityForDiscount + " for " + offer.Argument, -discountTotal);
                }

                if (discount != null)
                    receipt.AddDiscount(discount);
            }
        }
    }
}