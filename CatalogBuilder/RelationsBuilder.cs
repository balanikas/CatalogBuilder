using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CatalogBuilder
{
    static class RelationsBuilder
    {
        public static IEnumerable<XElement> CreateNodeRelations(Dictionary<string, List<string>> nodeRelations)
        {
            return nodeRelations.SelectMany(nodeRelation => nodeRelation.Value,
                (nodeRelation, childCode) =>
                    new XElement("NodeRelation", 
                        new XElement("ChildNodeCode", childCode),
                        new XElement("ParentNodeCode", nodeRelation.Key), 
                        new XElement("SortOrder", 0)));
        }

        public static IEnumerable<XElement> CreateNodeEntryRelations(Dictionary<string, List<string>> contextNodeEntryRelations)
        {
            return contextNodeEntryRelations.SelectMany(nodeRelation => nodeRelation.Value,
                (nodeRelation, childCode) =>
                    new XElement("NodeEntryRelation",
                        new XElement("EntryCode", childCode),
                        new XElement("NodeCode", nodeRelation.Key),
                        new XElement("SortOrder", 0)));
        }


        public static IEnumerable<XElement> CreateEntryRelations(Dictionary<string, List<string>> entryRelations)
        {
            return entryRelations.SelectMany(nodeRelation => nodeRelation.Value,
                (nodeRelation, childCode) =>
                    new XElement("EntryRelation",
                        new XElement("ParentEntryCode", nodeRelation.Key),
                        new XElement("ChildEntryCode", childCode),
                        new XElement("RelationType", "ProductVariation"),
                        new XElement("Quantity", 1),
                        new XElement("GroupName", "default"),
                        new XElement("SortOrder", 0)));
        }
    }
}