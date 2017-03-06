using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CatalogBuilder
{
    static class AssociationsBuilder
    {
        private static readonly Random _random = new Random();

        public static IEnumerable<XElement> CreateAssociations(BuildContext context)
        {
            var xCatalogAssociations = new List<XElement>();

            var entryCodes = context.Entries.EntryIds.ToArray();
            foreach (var currentCode in entryCodes)
            {
                var associatedCodes = PickRandomAssociations(entryCodes, currentCode, context.Entries.AssociationsPerEntryCount);

                var xAssociations = associatedCodes.Select(x => 
                    new XElement("Association", 
                        new XElement("EntryCode", x), 
                        new XElement("SortOrder", 0), 
                        new XElement("Type", context.Dictionaries.AssociationTypes.First())));

                var xCatalogAssociation =
                    new XElement("CatalogAssociation",
                        new XElement("Name", "CrossSell"),
                        new XElement("Description", "Description"),
                        new XElement("SortOrder", 0),
                        new XElement("EntryCode", currentCode),
                        xAssociations);

                xCatalogAssociations.Add(xCatalogAssociation);
            }

            return xCatalogAssociations;
        }

        private static IEnumerable<string> PickRandomAssociations(string[] entryCodes, string codeToExclude, int numberOfAssociations)
        {
            var associatedCodes = new List<string>();

            for (var i = 0; i < numberOfAssociations; i++)
            {
                string associationCode;
                do
                {
                    associationCode = entryCodes[_random.Next(entryCodes.Length)];

                } while (codeToExclude == associationCode);
                associatedCodes.Add(associationCode);
            }

            return associatedCodes.Distinct();
        }
    }
}