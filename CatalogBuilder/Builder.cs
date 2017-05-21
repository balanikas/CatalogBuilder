using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace CatalogBuilder
{
    public class Builder
    {
        public static XDocument Build(BuildContext context)
        {
            var documentBuilder = new DocumentBuilder();

            var dictionariesBuilder = new DictionariesBuilder(context.Dictionaries);
            documentBuilder.AddPackages(dictionariesBuilder.CreatePackages());
            documentBuilder.AddWarehouses(dictionariesBuilder.CreateWarehouses());
            documentBuilder.AddMarkets(dictionariesBuilder.CreateMarkets());
            documentBuilder.AddTaxCategories(dictionariesBuilder.CreateTaxCategories());
            documentBuilder.AddAssociationTypes(dictionariesBuilder.CreateAssociationTypes());
            documentBuilder.AddMerchants(dictionariesBuilder.CreateMerchants());

            context.MetaData = MetaDataBuilder.CreateScheme(context);
            documentBuilder.AddMetaClasses(context.MetaData.MetaClasses.Values);
            documentBuilder.AddMetaClasses(context.MetaData.MetaFields.Values.SelectMany(x => x));

            documentBuilder.AddCatalog(BuildCatalog(context));

            return documentBuilder.XDocument;
        }

        private static XElement BuildCatalog(BuildContext context)
        {

            var catalogBuilder = new CatalogBuilder(context);
            catalogBuilder.AddNodes(NodesBuilder.CreateNodes(context.CatalogStructure, null, context));
            catalogBuilder.AddEntries(EntriesBuilder.CreateEntries(context.CatalogStructure, context));
            //catalogBuilder.AddRelations(RelationsBuilder.CreateNodeRelations(context.NodeRelations));
            catalogBuilder.AddRelations(RelationsBuilder.CreateNodeEntryRelations(context.NodeEntryRelations));
            catalogBuilder.AddRelations(RelationsBuilder.CreateEntryRelations(context.EntryRelations));
            catalogBuilder.AddAssociations(AssociationsBuilder.CreateAssociations(context));

            return catalogBuilder.XCatalog;
        }

        public static void ValidateXml(XDocument xDoc)
        {
            var schemas = new XmlSchemaSet();
            schemas.Add("", XmlReader.Create(Path.Combine(Environment.CurrentDirectory, "Catalog.xsd")));

            xDoc.Validate(schemas, (o, e) =>
            {
                Console.WriteLine("{0}", e.Message);
            });
        }
    }
}
