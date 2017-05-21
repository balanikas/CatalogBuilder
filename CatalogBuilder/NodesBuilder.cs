using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CatalogBuilder
{
    static class NodesBuilder
    {
        public static IEnumerable<XElement>  CreateNodes(TreeNode node, string parentId, BuildContext context)
        {
            node.Id = context.NodeNamingPattern + "-" + node.Id;
            var list = new List<XElement>();
            if (!node.Config.IsRoot)
            {
                list.Add(CreateNode(node, parentId, context));
            }
           
            foreach (var child in node)
            {
                list.AddRange( CreateNodes(child, node.Id, context)); 
            }

            return list;
        }

        private static XElement CreateNode(TreeNode node, string parentId, BuildContext context)
        {
            return
               new XElement("Node",
                   new XElement("Name", node.Id),
                   new XElement("StartDate", context.CatalogStartDate),
                   new XElement("EndDate", context.CatalogEndDate),
                   new XElement("IsActive", "true"),
                   new XElement("SortOrder", 0),
                   new XElement("DisplayTemplate", "DisplayTemplate"),
                   new XElement("Code", node.Id),
                   new XElement("Guid", Guid.NewGuid()),
                   CreateMetaData(context),
                   new XElement("ParentNode", parentId),
                   CreateNodeSeoInfo(node.Id, context));
        }

        private static XElement CreateMetaData(BuildContext context)
        {
            var xMetaFields = context.MetaData.MetaFields[MetaDataScheme.CatalogNodeMetaField].Select(x => 
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
                        new XElement("Name", MetaDataScheme.CatalogNodeMetaClass)),
                    new XElement("MetaFields",
                        xMetaFields));
        }

        private static XElement CreateNodeSeoInfo(string nodeid, BuildContext context)
        {
            return
                new XElement("SeoInfo", context.CatalogLanguages.Select(x =>
                    new XElement("Seo",
                        new XElement("LanguageCode", x),
                        new XElement("Uri", nodeid + "-uri"),
                        new XElement("Title", nodeid + "-title"),
                        new XElement("Description", nodeid + "-description"),
                        new XElement("Keywords", nodeid + "-keywords"),
                        new XElement("UriSegment", nodeid + "-urisegment"))));
        }
    }
}