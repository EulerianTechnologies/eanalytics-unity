namespace eulerian
{
    public class EAEstimate : EAProperties
    {
        internal static readonly string KEY_IS_ESTIMATE = "estimate";
        private static readonly string KEY_REF = "ref";
        private static readonly string KEY_AMOUNT = "amount";
        private static readonly string KEY_CURRENCY = "currency";
        private static readonly string KEY_TYPE = "type";
        private static readonly string KEY_PRODUCTS = "products";
        private static readonly string KEY_PRODUCT_AMOUNT = "amount";
        private static readonly string KEY_QUANTITY = "quantity";

        internal readonly JSONArray products = new();

        public EAEstimate(string path, string reference) : base(path)
        {
            json[KEY_IS_ESTIMATE] = "1";
            json[KEY_REF] = reference;
        }

        public void SetAmount(double amount) => json[KEY_AMOUNT] = amount.ToString();
        public void SetType(string type) => json[KEY_TYPE] = type;

        /// <summary>
        /// Sets the currency.
        /// </summary>
        /// <param name="currency">Currency. Must be ISO 4217. Exemple: USD, GBP, EUR...</param>
        public void SetCurrency(string currency) => json[KEY_CURRENCY] = currency;

        public void AddProduct(Product product, double amount, int quantity)
        {
            product.json[KEY_PRODUCT_AMOUNT] = amount;
            product.json[KEY_QUANTITY] = quantity;
            products.Add(product.json);
            json[KEY_PRODUCTS] = products;
        }

    }
}
