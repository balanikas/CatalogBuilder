using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CatalogBuilder
{
    class CatalogBuilder
    {
        public XElement XCatalog { get; }

        public CatalogBuilder(BuildContext context)
        {
            var xCatalogLanguages = context.CatalogLanguages.Select(x=>
                new XElement("Language",
                    new XElement("LanguageCode", x),
                    new XElement("UriSegment", context.CatalogName)));
            XCatalog =
                new XElement("Catalog",
                    new XAttribute("name", context.CatalogName),
                    new XAttribute("lastmodified", "2012-04-05 22:07:31Z"),
                    new XAttribute("startDate", context.CatalogStartDate),
                    new XAttribute("endDate", context.CatalogEndDate),
                    new XAttribute("defaultCurrency", context.CatalogCurrency),
                    new XAttribute("weightBase", "lbs"),
                    new XAttribute("lengthBase", "in"),
                    new XAttribute("defaultLanguage", context.DefaultCatalogLanguage),
                    new XAttribute("sortOrder", 0),
                    new XAttribute("isActive", "true"),
                    new XElement("Guid", Guid.NewGuid()),
                    new XElement("Languages", xCatalogLanguages),
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