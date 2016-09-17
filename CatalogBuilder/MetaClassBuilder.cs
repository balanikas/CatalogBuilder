using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CatalogBuilder
{
    class MetaClassBuilder
    {
        public IEnumerable<XElement> CreateMetaClasses(BuildContext context)
        {
            context.MetaClassIds.Add("CatalogEntry");

            yield return
                 new XElement("MetaClass",
                     new XElement("Namespace", "Mediachase.Commerce.Catalog.System"),
                     new XElement("Name", "CatalogEntry"),
                     new XElement("FriendlyName", "Catalog Entry"),
                     new XElement("MetaClassType", "System"),
                     new XElement("ParentClass"),
                     new XElement("TableName", "CatalogEntry"),
                     new XElement("Description", "CatalogEntry Class"),
                     new XElement("IsSystem", "true"),
                     new XElement("IsAbstract", "false"),
                     new XElement("FieldListChangedSqlScript"),
                     new XElement("Tag"),
                     new XElement("Attributes"));

            yield return
               new XElement("MetaClass",
                   new XElement("Namespace", "Mediachase.Commerce.Catalog.User"),
                   new XElement("Name", "CatalogEntryEx"),
                   new XElement("FriendlyName", "Default Catalog Entry"),
                   new XElement("MetaClassType", "User"),
                   new XElement("ParentClass", "CatalogEntry"),
                   new XElement("TableName", "CatalogEntryEx"),
                   new XElement("Description", "Catalog Entry Extended Class"),
                   new XElement("IsSystem", "true"),
                   new XElement("IsAbstract", "false"),
                   new XElement("FieldListChangedSqlScript"),
                   new XElement("Tag"),
                   new XElement("Attributes"));

            yield return
                new XElement("MetaClass",
                    new XElement("Namespace", "Mediachase.Commerce.Catalog.System"),
                    new XElement("Name", "CatalogNode"),
                    new XElement("FriendlyName", "Catalog Node"),
                    new XElement("MetaClassType", "System"),
                    new XElement("ParentClass"),
                    new XElement("TableName", "CatalogNode"),
                    new XElement("Description", "CatalogNode Class"),
                    new XElement("IsSystem", "true"),
                    new XElement("IsAbstract", "false"),
                    new XElement("FieldListChangedSqlScript"),
                    new XElement("Tag"),
                    new XElement("Attributes"));

            yield return
               new XElement("MetaClass",
                   new XElement("Namespace", "Mediachase.Commerce.Catalog.User"),
                   new XElement("Name", "CatalogNodeEx"),
                   new XElement("FriendlyName", "Default Catalog Node"),
                   new XElement("MetaClassType", "User"),
                   new XElement("ParentClass", "CatalogNode"),
                   new XElement("TableName", "CatalogNodeEx"),
                   new XElement("Description", "Catalog Node Extended Class"),
                   new XElement("IsSystem", "true"),
                   new XElement("IsAbstract", "false"),
                   new XElement("FieldListChangedSqlScript"),
                   new XElement("Tag"),
                   new XElement("Attributes"));
        }

    }
}