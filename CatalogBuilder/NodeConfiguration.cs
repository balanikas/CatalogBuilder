namespace CatalogBuilder
{
    public class NodeConfiguration
    {
        public int VariationCount { get; set; }
        public int VariationsInProductCount { get; set; }
        public int ProductCount { get; set; }
        public bool IsRoot { get; }
        public static NodeConfiguration Root = new NodeConfiguration(true);
        public static NodeConfiguration Empty = new NodeConfiguration();

        public NodeConfiguration(bool isRoot)
        {
            IsRoot = isRoot;
        }

        public NodeConfiguration()
        {
        }
    }
}