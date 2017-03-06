using System.Collections.Generic;
using System.Xml.Linq;

namespace CatalogBuilder
{
    class DictionariesBuilder
    {
        private readonly BuildContext.DictionariesBuildContext _ctx;

        public DictionariesBuilder(BuildContext.DictionariesBuildContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<XElement> CreatePackages() 
        {
            var xPackages = new List<XElement>();
            for (int i = 0; i < _ctx.PackagesCount; i++)
            {
                xPackages.Add(
                    new XElement("Package",
                        new XElement("Name", "packagename" + i),
                        new XElement("Description", "description"),
                        new XElement("Width", 1),
                        new XElement("Height", 1),
                        new XElement("Length", 1)));

                _ctx.PackageIds.Add("packagename" + i);
            }

            return xPackages;
        }

        public IEnumerable<XElement> CreateWarehouses() 
        {
            var xPackages = new List<XElement>();
            for (var i = 0; i < _ctx.WarehousesCount; i++)
            {
                var warehouseId = "warehouse" + i;
                xPackages.Add(
                    new XElement("Warehouse",
                        new XElement("Name", warehouseId),
                        new XElement("IsActive", "true"),
                        new XElement("IsPrimary", "true"),
                        new XElement("SortOrder", 1),
                        new XElement("Code", warehouseId),
                        new XElement("FirstName", "FirstName"),
                        new XElement("LastName", "LastName"),
                        new XElement("Organization", "Organization"),
                        new XElement("Line1", "Line1"),
                        new XElement("Line2", "Line2"),
                        new XElement("City", "City"),
                        new XElement("State", "State"),
                        new XElement("CountryCode", "CountryCode"),
                        new XElement("CountryName", "CountryName"),
                        new XElement("PostalCode", "PostalCode"),
                        new XElement("RegionCode", "RegionCode"),
                        new XElement("RegionName", "RegionName"),
                        new XElement("DaytimePhoneNumber", "1234"),
                        new XElement("EveningPhoneNumber", "5678"),
                        new XElement("FaxNumber", "0000"),
                        new XElement("Email", "Email@company.com")));

                _ctx.WarehouseIds.Add(warehouseId);
            }

            return xPackages;
        }

        public IEnumerable<XElement> CreateTaxCategories()
        {
            var xList = new List<XElement>();
            for (var i = 0; i < _ctx.TaxCategoriesCount; i++)
            {
                xList.Add(
                    new XElement("TaxCategory",
                        new XElement("Name", "taxcategory" + i)));
            }

            return xList;
        }

        public IEnumerable<XElement> CreateMerchants()
        {
            var xList = new List<XElement>();
            for (var i = 0; i < _ctx.MarketsCount; i++)
            {
                xList.Add(
                    new XElement("Merchant",
                        new XElement("Name", "merchant" + i)));
            }

            return xList;
        }

        public IEnumerable<XElement> CreateMarkets()
        {
            var xList = new List<XElement>();
            for (var i = 0; i < _ctx.MarketsCount; i++)
            {
                var marketId = "market" + i;
                xList.Add(
                    new XElement("Market",
                    new XElement("MarketId", marketId),
                    new XElement("IsEnabled", "True"),
                    new XElement("MarketName", marketId),
                    new XElement("MarketDescription", marketId),
                    new XElement("DefaultLanguage", "en"),
                    new XElement("DefaultCurrency", "USD"),
                    new XElement("Currencies",
                        new XElement("Currency", "USD")),
                    new XElement("Languages",
                        new XElement("Language", "en")),
                    new XElement("Countries",
                        new XElement("Country", "USA"))));

                _ctx.MarketIds.Add(marketId);
            }

            return xList;
        }

        public IEnumerable<XElement> CreateAssociationTypes()
        {
            var xList = new List<XElement>();
            for (int i = 0; i < _ctx.AssociationTypesCount; i++)
            {
                var associationTypeId = "associationtype" + i;
                xList.Add(
                     new XElement("AssociationType",
                        new XElement("TypeId", associationTypeId),
                        new XElement("Description", "Description")));

                _ctx.AssociationTypes.Add(associationTypeId);

            }

            return xList;
        }
    }
}