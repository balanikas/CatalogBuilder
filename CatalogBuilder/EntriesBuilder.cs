using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CatalogBuilder
{
    class EntriesBuilder
    {
        public XElement XNodes { get; }
        public EntriesBuilder()
        {
            XNodes = new XElement("Nodes");
        }

        public IEnumerable<XElement> CreateEntries(BuildContext context)
        {
            var productCounter = 0;
            var variationCounter = 0;
           
            var xEntries = new List<XElement>();
            foreach (var nodeRelation in context.NodeRelations)
            {
                foreach (var childCode in nodeRelation.Value)
                {
                    context.NodeEntryRelations[childCode] = new List<string>();

                    for (int i = 0; i < context.NrProductsPerNode; i++)
                    {
                        var xProduct =
                            new XElement("Entry",
                                new XElement("Name", "productname" + productCounter),
                                new XElement("StartDate", "2010-09-01 07:00:00Z"),
                                new XElement("EndDate", "2020-10-01 07:00:00Z"),
                                new XElement("IsActive", "True"),
                                new XElement("SortOrder", productCounter),
                                new XElement("DisplayTemplate", "DisplayTemplate" + productCounter),
                                new XElement("Code", "entrycode" + productCounter),
                                new XElement("EntryType", "EntryType"),
                                new XElement("Guid", Guid.NewGuid()),
                                new XElement("SeoInfo"),
                                new XElement("VariationInfo"),
                                new XElement("WarehouseInventories"),
                                new XElement("Prices"),
                                new XElement("MetaData",
                                    new XElement("MetaClass",
                                        new XElement("Name", "MetaClassName")),
                                    new XElement("MetaFields")),
                                CreateEntrySeoInfo("productname" + productCounter, "en"));

                        xEntries.Add(xProduct);
                        context.NodeEntryRelations[childCode].Add("entrycode" + productCounter);
                        productCounter++;
                        context.EntryRelations["product" + productCounter] = new List<string>();

                        for (int j = 0; j < context.NrVariationsPerProduct; j++)
                        {
                            var xVariation =
                                new XElement("Entry",
                                    new XElement("Name", "variation" + variationCounter),
                                    new XElement("StartDate", "2010-09-01 07:00:00Z"),
                                    new XElement("EndDate", "2020-10-01 07:00:00Z"),
                                    new XElement("IsActive", "True"),
                                    new XElement("SortOrder", productCounter),
                                    new XElement("DisplayTemplate", "DisplayTemplate"),
                                    new XElement("Code", "entrycode" + variationCounter),
                                    new XElement("EntryType", "EntryType"),
                                    new XElement("Guid", Guid.NewGuid()),
                                    new XElement("SeoInfo"),
                                    new XElement("VariationInfo"),
                                    new XElement("WarehouseInventories"),
                                    new XElement("Prices"),
                                    new XElement("MetaData",
                                        new XElement("MetaClass",
                                            new XElement("Name", "MetaClassName")),
                                        new XElement("MetaFields")),
                                CreateEntrySeoInfo("variation" + variationCounter, "en"));

                            xEntries.Add(xVariation);
                            context.EntryRelations["product" + productCounter].Add("variation" + variationCounter);

                            context.EntryIds.Add("variation" + variationCounter);
                            variationCounter++;
                        }
                    }
                }
            }

            return xEntries;
        }

        private XElement CreateEntrySeoInfo(string uri, string languageCode)
        {
            return
                new XElement("SeoInfo",
                    new XElement("Seo",
                        new XElement("LanguageCode", languageCode),
                        new XElement("Uri", uri + "-" + languageCode + ".aspx"),
                        new XElement("Title", "title"),
                        new XElement("Description", "description"),
                        new XElement("Keywords", "key,words"),
                        new XElement("UriSegment", uri)));
        }
    }
}