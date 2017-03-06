using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CatalogBuilder
{
    class BuildContext
    {
        public string CatalogStartDate;
        public string CatalogEndDate;
        public string DefaultCatalogLanguage;
        public string CatalogCurrency;
        public string CatalogCountry;
        public string CatalogName;
        public Dictionary<string, List<string>> NodeRelations { get; }
        public Dictionary<string, List<string>> NodeEntryRelations { get; }
        public Dictionary<string, List<string>> EntryRelations { get; }
        public HashSet<string> MetaClassIds { get;  }
        public DictionariesBuildContext Dictionaries { get; set; }
        public EntriesBuildContext Entries { get; set; }
        public MetaDataScheme MetaData { get; set; }

        public TreeNode CatalogStructure { get; set; }
        public IEnumerable<string> CatalogLanguages { get; set; }

        public BuildContext()
        {
            NodeEntryRelations = new Dictionary<string, List<string>>();
            NodeRelations = new Dictionary<string, List<string>>();
            EntryRelations = new Dictionary<string, List<string>>();
            MetaClassIds = new HashSet<string>();
            Dictionaries = new DictionariesBuildContext();
            Entries = new EntriesBuildContext();
            MetaData = new MetaDataScheme();
        }


        public class DictionariesBuildContext
        {
            public HashSet<string> WarehouseIds { get; }
            public HashSet<string> PackageIds { get; }
            public HashSet<string> MarketIds { get; }
            public HashSet<string> AssociationTypes { get; }
            public int PackagesCount { get; set; }
            public int WarehousesCount { get; set; }
            public int MarketsCount { get; set; }
            public int TaxCategoriesCount { get; set; }
            public int AssociationTypesCount { get; set; }
            public int MerchantsCount { get; set; }
          
            public DictionariesBuildContext()
            {
                PackageIds = new HashSet<string>();
                WarehouseIds = new HashSet<string>();
                MarketIds = new HashSet<string>();
                AssociationTypes = new HashSet<string>();
            }
        }

        public class EntriesBuildContext
        {
            public int AssociationsPerEntryCount { get; set; }
            public HashSet<string> EntryIds { get; }
            public Func<decimal> PriceSetter { get; set; }
            public string PriceStartDate;
            public string PriceEndDate;
            public string BackorderAvailabilityDate;
            public string PreorderAvailabilityDate;

            public EntriesBuildContext()
            {
                EntryIds = new HashSet<string>();
                PriceSetter = () => 1;
            }
        }
    }


    public class MetaDataScheme
    {
        public const string CatalogNodeMetaClass = "CatalogNodeMetaClass";
        public const string CatalogProductMetaClass = "CatalogProductMetaClass";
        public const string CatalogVariationMetaClass = "CatalogVariationMetaClass";
        public const string CatalogNodeMetaField = "CatalogNodeMetaField";
        public const string CatalogProductMetaField = "CatalogProductMetaField";
        public const string CatalogVariationMetaField = "CatalogVariationMetaField";
        public IDictionary<string, XElement> MetaClasses;
        public IDictionary<string, IList<XElement>> MetaFields;

        public MetaDataScheme()
        {
            MetaClasses = new Dictionary<string, XElement>();
            MetaFields = new Dictionary<string, IList<XElement>>();
        }
    }
}
