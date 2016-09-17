using System.Collections.Generic;
using System.Xml.Linq;

namespace CatalogBuilder
{
    class DocumentBuilder
    {
        public XDocument XDoc { get; }

        public DocumentBuilder()
        {
            XDoc = new XDocument(new XDeclaration("1.0", "utf-8", "no"));
            XDoc.Add(new XElement("Catalogs", new XAttribute("Version", "1.0")));
            XDoc.Root.Add(
                new XElement("MetaDataScheme",
                    new XElement("MetaDataPlusBackup",
                        new XAttribute("version", "1.0"))),
                new XElement("Dictionaries",
                    new XElement("Merchants"),
                    new XElement("Packages"),
                    new XElement("Warehouses"),
                    new XElement("TaxCategories"),
                    new XElement("AssociationTypes"),
                    new XElement("Markets")));
        }

        public void AddMetaClasses(IEnumerable<XElement> xMetaClasses)
        {
            XDoc.Root.Element("MetaDataScheme").Element("MetaDataPlusBackup").Add(xMetaClasses);
        }

        public void AddCatalog(XElement xCatalog)
        {
            XDoc.Root.Add(xCatalog);
        }

        public void AddPackages(IEnumerable<XElement> xPackages)
        {
            XDoc.Root.Element("Dictionaries").Element("Packages").Add(xPackages);
        }

        public void AddWarehouses(IEnumerable<XElement> xWarehouses)
        {
            XDoc.Root.Element("Dictionaries").Element("Warehouses").Add(xWarehouses);
        }

        public void AddMarkets(IEnumerable<XElement> xMarkets)
        {
            XDoc.Root.Element("Dictionaries").Element("Markets").Add(xMarkets);
        }

        public void AddTaxCategories(IEnumerable<XElement> xTaxCategories)
        {
            XDoc.Root.Element("Dictionaries").Element("TaxCategories").Add(xTaxCategories);
        }

        public void AddAssociationTypes(IEnumerable<XElement> xAssociationTypes)
        {
            XDoc.Root.Element("Dictionaries").Element("AssociationTypes").Add(xAssociationTypes);
        }

        public void AddMerchants(IEnumerable<XElement> xMerchants)
        {
            XDoc.Root.Element("Dictionaries").Element("Merchants").Add(xMerchants);
        }
    }
}
