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
                if (offer is null)
                    return;
               
                var discount = offer.ApplyDiscount(quantity);

                if (discount != null)
                    receipt.AddDiscount(discount);
            }
        }
    }
}