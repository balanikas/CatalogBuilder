using System.Collections.Generic;

namespace CatalogBuilder
{
    class BuildContext
    {
        public Dictionary<string, List<string>> NodeRelations { get; }
        public Dictionary<string, List<string>> NodeEntryRelations { get; }
        public Dictionary<string, List<string>> EntryRelations { get; }
        public HashSet<string> EntryIds { get;  }
        public HashSet<string> WarehouseIds { get; }
        public HashSet<string> PackageIds { get; }
        public HashSet<string> MetaClassIds { get;  }

        public int NrParentNodes { get; set; }
        public int NrChildrenPerParentNode { get; set; }
        public int NrProductsPerNode { get; set; }
        public int NrVariationsPerProduct { get; set; }
        public int NrAssociationsPerEntry { get; set; }

        public BuildContext()
        {
            NodeEntryRelations = new Dictionary<string, List<string>>();
            NodeRelations = new Dictionary<string, List<string>>();
            EntryRelations = new Dictionary<string, List<string>>();
            EntryIds = new HashSet<string>();
            PackageIds = new HashSet<string>();
            WarehouseIds = new HashSet<string>();
            MetaClassIds = new HashSet<string>();

        }

    }
}
