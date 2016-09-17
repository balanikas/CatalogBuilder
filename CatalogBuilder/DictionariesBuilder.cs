using System.Collections.Generic;
using System.Xml.Linq;

namespace CatalogBuilder
{
    class DictionariesBuilder
    {
        public IEnumerable<XElement> CreatePackages(int packages, BuildContext context) 
        {
            var xPackages = new List<XElement>();
            for (int i = 0; i < packages; i++)
            {
                xPackages.Add(
                    new XElement("Package",
                        new XElement("Name", "packagename" + i),
                        new XElement("Description", "description"),
                        new XElement("Width", "1"),
                        new XElement("Height", "1"),
                        new XElement("Length", "1")));

                context.PackageIds.Add("packagename" + i);
            }

            return xPackages;
        }

        public IEnumerable<XElement> CreateWarehouses(int warehouses, BuildContext context) 
        {
            var xPackages = new List<XElement>();
            for (var i = 0; i < warehouses; i++)
            {
                xPackages.Add(
                    new XElement("Warehouse",
                        new XElement("Name", "warehousename" + i),
                        new XElement("IsActive", "true"),
                        new XElement("IsPrimary", "true"),
                        new XElement("SortOrder", "1"),
                        new XElement("Code", "warehousecode" + i),
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
                        new XElement("DaytimePhoneNumber", "DaytimePhoneNumber"),
                        new XElement("EveningPhoneNumber", "EveningPhoneNumber"),
                        new XElement("FaxNumber", "FaxNumber"),
                        new XElement("Email", "Email")));

                context.WarehouseIds.Add("warehousecode" + i);
            }

            return xPackages;
        }


        public IEnumerable<XElement> CreateTaxCategories(int count, BuildContext context)
        {
            var xList = new List<XElement>();
            for (var i = 0; i < count; i++)
            {
                xList.Add(
                    new XElement("TaxCategory",
                        new XElement("Name", "taxcategoryname" + i)));
            }

            return xList;
        }

        public IEnumerable<XElement> CreateMerchants(int count, BuildContext context)
        {
            var xList = new List<XElement>();
            for (var i = 0; i < count; i++)
            {
                xList.Add(
                    new XElement("Merchant",
                        new XElement("Name", "merchantname" + i)));
            }

            return xList;
        }

        public IEnumerable<XElement> CreateMarkets(int count, BuildContext context)
        {
            var xList = new List<XElement>();
            for (int i = 0; i < count; i++)
            {
                xList.Add(
                    new XElement("Market",
                    new XElement("MarketId", "marketid"+ i),
                    new XElement("IsEnabled", "true"),
                    new XElement("MarketName", "marketname" + i),
                    new XElement("MarketDescription", "MarketDescription"),
                    new XElement("DefaultLanguage", "en"),
                    new XElement("DefaultCurrency", "USD"),
                    new XElement("Currencies",
                        new XElement("Currency", "USD")),
                    new XElement("Languages",
                        new XElement("Language", "en")),
                    new XElement("Countries",
                        new XElement("Country", "Country"))));
            }

            return xList;
        }

        public IEnumerable<XElement> CreateAssociationTypes(int count, BuildContext context)
        {
            var xList = new List<XElement>();
            for (int i = 0; i < count; i++)
            {
                xList.Add(
                     new XElement("AssociationType",
                        new XElement("TypeId", "associationtype"+ i),
                        new XElement("Description", "Description")));
            }

            return xList;
        }
    }
}