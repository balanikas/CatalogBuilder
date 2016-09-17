using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CatalogBuilder
{
    class CatalogBuilder
    {
        public XElement XCatalog { get; }

        public CatalogBuilder()
        {
            XCatalog =
                new XElement("Catalog",
                    new XAttribute("name", "name"),
                    new XAttribute("lastmodified", "2012-04-05 22:07:31Z"),
                    new XAttribute("startDate", "2012-01-16 05:51:00Z"),
                    new XAttribute("endDate", "2022-01-16 05:51:00Z"),
                    new XAttribute("defaultCurrency", "usd"),
                    new XAttribute("weightBase", "weightBase"),
                    new XAttribute("lengthBase", "lengthBase"),
                    new XAttribute("defaultLanguage", "en"),
                    new XAttribute("sortOrder", "1"),
                    new XAttribute("isActive", "true"),
                    new XAttribute("languages", "sv,fr"),
                    new XElement("Guid", Guid.NewGuid()),
                    new XElement("Languages",
                        new XElement("Language",
                        new XElement("LanguageCode", "en"),
                        new XElement("UriSegment", "UriSegment"))),
                    new XElement("Nodes"),
                    new XElement("Entries"),
                    new XElement("Relations"),
                    new XElement("Associations"));
        }

        public void AddCatalogLanguages(params XElement[] xCatalogLanguages)
        {
            XCatalog.Element("Languages").Add(xCatalogLanguages);
        }

        public void AddNodes(IEnumerable<XElement> xNodes)
        {
            XCatalog.Element("Nodes").Add(xNodes);
        }

        public void AddEntries(IEnumerable<XElement> xEntries)
        {
            XCatalog.Element("Entries").Add(xEntries);
        }

        public void AddRelations(IEnumerable<XElement> xRelations)
        {
            XCatalog.Element("Relations").Add(xRelations);
        }

        public void AddAssociations(IEnumerable<XElement> xAssociations)
        {
            XCatalog.Element("Associations").Add(xAssociations);
        }
        
    }
}