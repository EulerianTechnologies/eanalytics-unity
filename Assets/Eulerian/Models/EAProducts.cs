namespace eulerian
{
    public class EAProducts : EAProperties
    {
        private static readonly string KEY_PRODUCTS = "products";

        private readonly JSONArray products = new();

        public EAProducts(string path) : base(path)
        {
        }

        public void AddProduct(Product product)
        {
            products.Add(product.json);
            json.Add(KEY_PRODUCTS, products);
        }
    }
}
