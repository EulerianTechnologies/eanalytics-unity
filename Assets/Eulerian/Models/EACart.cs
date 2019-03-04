using System;

namespace eulerian
{
    public class EACart : EAProperties
    {
        private readonly static string KEY_SCART = "scart";
        private readonly static string KEY_CUMUL = "scartcumul";
        private readonly static string KEY_AMOUNT = "amount";
        private readonly static string KEY_QUANTITY = "quantity";
        private readonly static string KEY_PRODUCTS = "products";

        internal readonly JSONArray products = new JSONArray();

        public EACart(string path) : base(path)
        {
            json[KEY_SCART] = "1";
        }

        public void SetCartCumul(bool cumul)
        {
            json[KEY_CUMUL] = cumul ? 1 : 0;
        }

        public void AddProduct(Product product, double amount, int quantity)
        {
            product.json[KEY_AMOUNT] = amount;
            product.json[KEY_QUANTITY] = quantity;
            products.Add(product.json);
            json[KEY_PRODUCTS] = products;  
        }
    }
}
