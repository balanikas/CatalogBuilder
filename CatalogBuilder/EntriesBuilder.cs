using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CatalogBuilder
{
    static class EntriesBuilder
    {
        private static int _productCounter;
        private static int _variationCounter;

        public static IEnumerable<XElement> CreateEntries(TreeNode node,BuildContext context)
        {
            var list = new List<XElement>();
            list.AddRange( CreateEntriesInternal(node,  context) );

            foreach (var child in node)
            {
                list.AddRange(CreateEntries(child, context));
            }

            return list;
        }

        private static IEnumerable<XElement> CreateEntriesInternal(TreeNode node,BuildContext context)
        {
            var xEntries = new List<XElement>();

            context.NodeEntryRelations[node.Id] = new List<string>();

            for (var i = 0; i < node.Config.ProductCount; i++)
            {
                var productId = context.EntryNamingPattern + "-product" + _productCounter;

                xEntries.Add(CreateProduct(productId, context));
                context.NodeEntryRelations[node.Id].Add(productId);
                context.EntryRelations[productId] = new List<string>();
                context.Entries.EntryIds.Add(productId);

                for (var j = 0; j < node.Config.VariationsInProductCount; j++)
                {
                    var variationId = productId + "-variation" + _variationCounter;

                    xEntries.Add(CreateVariation(variationId, context));
                    context.EntryRelations[productId].Add(variationId);
                    context.NodeEntryRelations[node.Id].Add(variationId);

                    context.Entries.EntryIds.Add(variationId);

                    _variationCounter++;
                }

                _productCounter++;
            }

            for (var i = 0; i < node.Config.VariationCount; i++)
            {
                var variationId = context.EntryNamingPattern + "-" + "variation" + _variationCounter;

                xEntries.Add(CreateVariation(variationId, context));
                context.NodeEntryRelations[node.Id].Add(variationId);

                context.Entries.EntryIds.Add(variationId);

                _variationCounter++;
            }

            return xEntries;

        }


        private static XElement CreateProduct(string productId, BuildContext context)
        {
            return 
                new XElement("Entry",
                    new XElement("Name", productId),
                    new XElement("StartDate", context.CatalogStartDate),
                    new XElement("EndDate", context.CatalogEndDate),
                    new XElement("IsActive", "True"),
                    new XElement("SortOrder", 0),
                    new XElement("DisplayTemplate", "DisplayTemplate" + _productCounter),
                    new XElement("Code", productId),
                    new XElement("EntryType", "Product"),
                    new XElement("Guid", Guid.NewGuid()),
                    new XElement("VariationInfo"),
                    new XElement("WarehouseInventories"),
                    new XElement("Prices"),
                    CreateProductMetaData(context),
                    CreateEntrySeoInfo(productId, context));
        }

        private static XElement CreateVariation(string variationId, BuildContext context)
        {
            return
                new XElement("Entry",
                    new XElement("Name", variationId),
                    new XElement("StartDate", context.CatalogStartDate),
                    new XElement("EndDate", context.CatalogEndDate),
                    new XElement("IsActive", "True"),
                    new XElement("SortOrder", 0),
                    new XElement("DisplayTemplate", "DisplayTemplate"),
                    new XElement("Code", variationId),
                    new XElement("EntryType", "Variation"),
                    new XElement("Guid", Guid.NewGuid()),
                    CreateVariationMetaData(context),
                    CreateVariationInfo(),
                    CreateWarehouseInventories(context),
                    CreatePrices(context),
                    CreateEntrySeoInfo(variationId, context));
        }

        private static XElement CreateProductMetaData(BuildContext context)
        {
            var xMetaFields = context.MetaData.MetaFields[MetaDataScheme.CatalogProductMetaField].Select(x =>
                new XElement("MetaField",
                    new XElement("Name", (string)x.Element("Name")),
                    new XElement("Type", (string)x.Element("DataType")),
                    context.CatalogLanguages.Select(y =>
                        new XElement("Data",
                            new XAttribute("language", y),
                            new XAttribute("value", (string)x.Element("Name") + "-value-" + y)))));

            return
                new XElement("MetaData",
                    new XElement("MetaClass",
                        new XElement("Name", MetaDataScheme.CatalogProductMetaClass)),
                    new XElement("MetaFields",
                        xMetaFields));
        }

        private static XElement CreateVariationMetaData(BuildContext context)
        {
            var xMetaFields = context.MetaData.MetaFields[MetaDataScheme.CatalogVariationMetaField].Select(x =>
                new XElement("MetaField",
                    new XElement("Name", (string)x.Element("Name")),
                    new XElement("Type", (string)x.Element("DataType")),
                    context.CatalogLanguages.Select(y =>
                        new XElement("Data",
                            new XAttribute("language", y),
                            new XAttribute("value", (string)x.Element("Name") + "-value-" + y)))));

            return
                new XElement("MetaData",
                    new XElement("MetaClass",
                        new XElement("Name", MetaDataScheme.CatalogVariationMetaClass)),
                    new XElement("MetaFields",
                        xMetaFields));
        }

        private static XElement CreateEntrySeoInfo(string entryId, BuildContext context)
        {
            return
                new XElement("SeoInfo", context.CatalogLanguages.Select(x =>
                    new XElement("Seo",
                        new XElement("LanguageCode", x),
                        new XElement("Uri", entryId + "-uri"),
                        new XElement("Title", entryId + "-title"),
                        new XElement("Description", entryId + "-description"),
                        new XElement("Keywords", entryId + "-keywords"),
                        new XElement("UriSegment", entryId + "-urisegment"))));
        }

        private static XElement CreateVariationInfo()
        {
            return
                new XElement("VariationInfo",
                    new XElement("Variation",
                        new XElement("ListPrice", 1),
                        new XElement("MaxQuantity",100),
                        new XElement("MinQuantity", 1),
                        new XElement("TrackInventory", "True"),
                        new XElement("TaxCategoryName", "taxcategory0"),
                        new XElement("Weight", 1),
                        new XElement("Height", 1),
                        new XElement("Width", 1),
                        new XElement("Length", 1)));
        }

        private static XElement CreateWarehouseInventories(BuildContext context)
        {
            var xWarehouseInventories = new XElement("WarehouseInventories");

            foreach (var warehouseId in context.Dictionaries.WarehouseIds)
            {
                xWarehouseInventories.Add(
                     new XElement("WarehouseInventory",
                        new XElement("WarehouseCode", warehouseId),
                        new XElement("AllowBackorder", false),
                        new XElement("AllowPreorder", true),
                        new XElement("BackorderAvailabilityDate", context.Entries.BackorderAvailabilityDate),
                        new XElement("BackorderQuantity", 0),
                        new XElement("InStockQuantity", 100),
                        new XElement("InventoryStatus", "Enabled"),
                        new XElement("PreorderAvailabilityDate", context.Entries.PreorderAvailabilityDate),
                        new XElement("PreorderQuantity", 0),
                        new XElement("ReorderMinQuantity", 10),
                        new XElement("ReservedQuantity", 10)
                        )
                    );
            }

            return xWarehouseInventories;
        }

        private static XElement CreatePrices(BuildContext context)
        {
            var xPrices = new XElement("Prices");
            foreach (var marketId in context.Dictionaries.MarketIds)
            {
                xPrices.Add(
                    new XElement("Price",
                        new XElement("MarketId", marketId),
                        new XElement("CurrencyCode", context.CatalogCurrency),
                        new XElement("PriceTypeId", 0),
                        new XElement("PriceCode"),
                        new XElement("ValidFrom", context.Entries.PriceStartDate),
                        new XElement("ValidUntil", context.Entries.PriceEndDate),
                        new XElement("MinQuantity", 0),
                        new XElement("UnitPrice", context.Entries.PriceSetter())
                        )
                    );
            }

            return xPrices;
        }
    }
}