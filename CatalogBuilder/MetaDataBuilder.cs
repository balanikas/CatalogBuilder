using System.Xml.Linq;

namespace CatalogBuilder
{
    static class MetaDataBuilder
    {
        public static MetaDataScheme CreateScheme()
        {
            var metaDataScheme = new MetaDataScheme
            {
                MetaClasses =
                {
                    [MetaDataScheme.CatalogNodeMetaClass] =
                    CreateMetaClass(MetaDataScheme.CatalogNodeMetaClass, "CatalogNode"),
                    [MetaDataScheme.CatalogProductMetaClass] =
                    CreateMetaClass(MetaDataScheme.CatalogProductMetaClass, "CatalogEntry"),
                    [MetaDataScheme.CatalogVariationMetaClass] =
                    CreateMetaClass(MetaDataScheme.CatalogVariationMetaClass, "CatalogEntry")
                },
                MetaFields =
                {
                    [MetaDataScheme.CatalogNodeMetaField] =
                    new[] {CreateMetaField(MetaDataScheme.CatalogNodeMetaField, MetaDataScheme.CatalogNodeMetaClass)},
                    [MetaDataScheme.CatalogProductMetaField] =
                    new[] {CreateMetaField(MetaDataScheme.CatalogProductMetaField, MetaDataScheme.CatalogProductMetaClass)},
                    [MetaDataScheme.CatalogVariationMetaField] =
                    new[] {CreateMetaField(MetaDataScheme.CatalogVariationMetaField, MetaDataScheme.CatalogVariationMetaClass)},
                }
            };

            return metaDataScheme;
        }

        private static XElement CreateMetaField(string name, string ownerClass)
        {
            return
              new XElement("MetaField",
                  new XElement("Namespace", "Mediachase.Commerce.Catalog"),
                  new XElement("Name", name),
                  new XElement("FriendlyName", name),
                  new XElement("Description", name),
                  new XElement("DataType", "ShortString"),
                  new XElement("Length", 512),
                  new XElement("AllowNulls", true),
                  new XElement("SaveHistory", false),
                  new XElement("AllowSearch",false),
                  new XElement("MultiLanguageValue", true),
                  new XElement("IsSystem", false),
                  new XElement("Tag"),
                  new XElement("Attributes"),
                  new XElement("OwnerMetaClass", ownerClass));
        }

        private static XElement CreateMetaClass(string name, string parentClass)
        {
            return
               new XElement("MetaClass",
                   new XElement("Namespace", "Mediachase.Commerce.Catalog.User"),
                   new XElement("Name", name),
                   new XElement("FriendlyName", name),
                   new XElement("MetaClassType", "User"),
                   new XElement("ParentClass", parentClass),
                   new XElement("TableName", name + "_" + parentClass),
                   new XElement("Description", name),
                   new XElement("IsSystem", false),
                   new XElement("IsAbstract", false),
                   new XElement("FieldListChangedSqlScript"),
                   new XElement("Tag"),
                   new XElement("Attributes"));
        }
    }
}