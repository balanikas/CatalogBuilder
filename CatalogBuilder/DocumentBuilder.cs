using System.Collections.Generic;
using System.Xml.Linq;

namespace CatalogBuilder
{
    class DocumentBuilder
    {
        public XDocument XDocument { get; }

        public DocumentBuilder()
        {
            XDocument = new XDocument(new XDeclaration("1.0", "utf-8", "no"));
            XDocument.Add(new XElement("Catalogs", new XAttribute("Version", "1.0")));
            XDocument.Root.Add(
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
            XDocument.Root.Element("MetaDataScheme").Element("MetaDataPlusBackup").Add(xMetaClasses);
        }

        public void AddCatalog(XElement xCatalog)
        {
            XDocument.Root.Add(xCatalog);
        }

        public void AddPackages(IEnumerable<XElement> xPackages)
        {
            XDocument.Root.Element("Dictionaries").Element("Packages").Add(xPackages);
        }

        public void AddWarehouses(IEnumerable<XElement> xWarehouses)
        {
            XDocument.Root.Element("Dictionaries").Element("Warehouses").Add(xWarehouses);
        }

        public void AddMarkets(IEnumerable<XElement> xMarkets)
        {
            XDocument.Root.Element("Dictionaries").Element("Markets").Add(xMarkets);
        }

        public void AddTaxCategories(IEnumerable<XElement> xTaxCategories)
        {
            XDocument.Root.Element("Dictionaries").Element("TaxCategories").Add(xTaxCategories);
        }

        public void AddAssociationTypes(IEnumerable<XElement> xAssociationTypes)
        {
            XDocument.Root.Element("Dictionaries").Element("AssociationTypes").Add(xAssociationTypes);
        }

        public void AddMerchants(IEnumerable<XElement> xMerchants)
        {
            XDocument.Root.Element("Dictionaries").Element("Merchants").Add(xMerchants);
        }
    }
}
