using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CatalogBuilder
{
    class AssociationsBuilder
    {
        public IEnumerable<XElement> CreateAssociations(BuildContext context)
        {
            var counter = 0;
            var xCatalogAssociations = new List<XElement>();

            for (var i = 0; i < context.EntryIds.Count; i++)
            {
                var codes = context.EntryIds.Skip(i * context.NrAssociationsPerEntry).Take(context.NrAssociationsPerEntry).ToList();
                if (codes.Count < context.NrAssociationsPerEntry)
                {
                    continue;
                }

                var xAssociations = new List<XElement>();
                for (int j = 1; j < context.NrAssociationsPerEntry; j++)
                {
                    xAssociations.Add(
                        new XElement("Association",
                            new XElement("EntryCode", codes[j]),
                            new XElement("SortOrder", "1"),
                            new XElement("Type", "Type")));
                }

                var xCatalogAssociation =
                     new XElement("CatalogAssociation",
                        new XElement("Name", "CatalogAssociationName" + counter),
                        new XElement("Description", "Description"),
                        new XElement("SortOrder", "1"),
                        new XElement("EntryCode", codes[0]),
                        xAssociations);

                xCatalogAssociations.Add(xCatalogAssociation);
                counter++;

            }
          
            return xCatalogAssociations;
        }
    }
}