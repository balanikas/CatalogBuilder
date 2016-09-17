using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CatalogBuilder
{
    class NodesBuilder
    {
        public XElement XNodes { get; }
        public NodesBuilder()
        {
            XNodes = new XElement("Nodes");
        }

        public IEnumerable<XElement> CreateNodes(BuildContext context )
        {
            var xNodes = new List<XElement>();
            for (var i = 0; i < context.NrParentNodes; i++)
            {
                xNodes.Add(CreateParentNode(i));

                context.NodeRelations["nodecode" + i] = new List<string>();
                for (var j = 0; j < context.NrChildrenPerParentNode; j++)
                {
                    xNodes.Add(CreateChildNode(j, "nodecode" + i));
                   
                    context.NodeRelations["nodecode" + i].Add("childnodecode" + j);
                }
            }

            return xNodes;
        }
        
        private XElement CreateParentNode(int id)
        {
            return new XElement("Node",
                new XElement("Name", "nodename" + id),
                new XElement("StartDate", "2010-09-01 07:00:00Z"),
                new XElement("EndDate", "2020-10-01 07:00:00Z"),
                new XElement("IsActive", "true"),
                new XElement("SortOrder", id),
                new XElement("DisplayTemplate", "DisplayTemplate"),
                new XElement("Code", "nodecode" + id),
                new XElement("Guid", Guid.NewGuid()),
                new XElement("ParentNode"),
                new XElement("MetaData",
                    new XElement("MetaClass",
                        new XElement("Name", "CatalogNodeEx")),
                    new XElement("MetaFields")),
                    CreateNodeSeoInfo("nodename" + id, "en"));
        }

        private XElement CreateChildNode(int id, string parentId)
        {
            return 
                new XElement("Node",
                    new XElement("Name", "childnodename" + id),
                    new XElement("StartDate", "2010-09-01 07:00:00Z"),
                    new XElement("EndDate", "2020-10-01 07:00:00Z"),
                    new XElement("IsActive", "true"),
                    new XElement("SortOrder", id),
                    new XElement("DisplayTemplate", "DisplayTemplate"),
                    new XElement("Code", "childnodecode" + id),
                    new XElement("Guid", Guid.NewGuid()),
                    new XElement("ParentNode", parentId),
                    new XElement("MetaData",
                        new XElement("MetaClass",
                            new XElement("Name", "CatalogNodeEx")),
                        new XElement("MetaFields")),
                    CreateNodeSeoInfo("childnodename" + id, "en"));
        }

        private XElement CreateNodeSeoInfo(string uri, string languageCode)
        {
            return 
                new XElement("SeoInfo", 
                    new XElement("Seo", 
                        new XElement("LanguageCode", languageCode),
                        new XElement("Uri", uri + "-" + languageCode + ".aspx"),
                        new XElement("Title", "title"),
                        new XElement("Description", "description"),
                        new XElement("Keywords", "key,words"),
                        new XElement("UriSegment", uri)));
        }
    }
}