using System;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace CatalogBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BuildContext
            {
                NrParentNodes = 1,
                NrChildrenPerParentNode = 1,
                NrProductsPerNode = 1,
                NrVariationsPerProduct = 3,
                NrAssociationsPerEntry = 3
            };

            var doc = Build(context);

            ValidateXml(doc);
            Console.WriteLine(doc);
            SaveAndZip(doc);
            Console.ReadKey();
        }

        static XDocument Build(BuildContext context)
        {
            var metaClassBuilder = new MetaClassBuilder();
            var xMetaClasses = metaClassBuilder.CreateMetaClasses(context);

            var nodesBuilder = new NodesBuilder();
            var xNodes = nodesBuilder.CreateNodes(context);

            var entriesBuilder = new EntriesBuilder();
            var xEntries = entriesBuilder.CreateEntries(context);

            var relationsBuilder = new RelationsBuilder();
            var xRelations = relationsBuilder.CreateNodeRelations(context.NodeRelations);
            var xNodeEntryRelations = relationsBuilder.CreateNodeEntryRelations(context.NodeEntryRelations);
            var xEntryRelations = relationsBuilder.CreateEntryRelations(context.EntryRelations);

            var associationsBuilder = new AssociationsBuilder();
            var xAssociations = associationsBuilder.CreateAssociations(context);

            var catalogBuilder = new CatalogBuilder();
            catalogBuilder.AddNodes(xNodes);
            catalogBuilder.AddEntries(xEntries);
            catalogBuilder.AddRelations(xRelations);
            catalogBuilder.AddRelations(xNodeEntryRelations);
            catalogBuilder.AddRelations(xEntryRelations);
            catalogBuilder.AddAssociations(xAssociations);

            var dictionariesBuilder = new DictionariesBuilder();
            var xPackages = dictionariesBuilder.CreatePackages(1, context);
            var xWarehouses = dictionariesBuilder.CreateWarehouses(1, context);
            var xTaxCategories = dictionariesBuilder.CreateTaxCategories(1, context);
            var xMerchants = dictionariesBuilder.CreateMerchants(1, context);
            var xAssociationTypes = dictionariesBuilder.CreateAssociationTypes(1, context);
            var xMarkets = dictionariesBuilder.CreateMarkets(1, context);

            var documentBuilder = new DocumentBuilder();
            documentBuilder.AddPackages(xPackages);
            documentBuilder.AddWarehouses(xWarehouses);
            documentBuilder.AddMarkets(xMarkets);
            documentBuilder.AddTaxCategories(xTaxCategories);
            documentBuilder.AddAssociationTypes(xAssociationTypes);
            documentBuilder.AddMerchants(xMerchants);
            documentBuilder.AddMetaClasses(xMetaClasses);
            documentBuilder.AddCatalog(catalogBuilder.XCatalog);

            return documentBuilder.XDoc;
        }

        static void ValidateXml(XDocument xDoc)
        {
            var schemas = new XmlSchemaSet();
            schemas.Add("", XmlReader.Create(Path.Combine(Environment.CurrentDirectory, "Catalog.xsd")));

            xDoc.Validate(schemas, (o, e) =>
            {
                Console.WriteLine("{0}", e.Message);
            });
        }

        static void SaveAndZip(XDocument xDoc)
        {
            var tempDirectoryPath = Path.Combine(Environment.CurrentDirectory, "Temp");
            var zipFilePath = Path.Combine(Environment.CurrentDirectory, "Catalog.zip");

            if (Directory.Exists(tempDirectoryPath))
            {
                Directory.Delete(tempDirectoryPath,true);
            }

            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }

            Directory.CreateDirectory(tempDirectoryPath);
            xDoc.Save(Path.Combine(tempDirectoryPath, "Catalog.xml"));

            ZipFile.CreateFromDirectory(tempDirectoryPath, zipFilePath);
        }
    }
}
