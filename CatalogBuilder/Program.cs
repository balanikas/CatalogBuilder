using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
/*
 * TODO:
 * use DateTime, not strings
 * radnomize data ranges
 * support for bundles and packages
 * */
namespace CatalogBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc = Build(ConfigureCatalog_ComplexStructure());
            //ValidateXml(doc);
            ZipAndSave(doc);
        }

        static XDocument Build(params BuildContext[] contexts)
        {
            var documentBuilder = new DocumentBuilder();

            var dictionariesContext = new BuildContext.DictionariesBuildContext
            {
                AssociationTypesCount = 2,
                MarketsCount = 50,
                PackagesCount = 7,
                WarehousesCount = 20,
                TaxCategoriesCount = 5,
                MerchantsCount = 3
            };

            var dictionariesBuilder = new DictionariesBuilder(dictionariesContext);
            documentBuilder.AddPackages(dictionariesBuilder.CreatePackages());
            documentBuilder.AddWarehouses(dictionariesBuilder.CreateWarehouses());
            documentBuilder.AddMarkets(dictionariesBuilder.CreateMarkets());
            documentBuilder.AddTaxCategories(dictionariesBuilder.CreateTaxCategories());
            documentBuilder.AddAssociationTypes(dictionariesBuilder.CreateAssociationTypes());
            documentBuilder.AddMerchants(dictionariesBuilder.CreateMerchants());

            var metaDataScheme = MetaDataBuilder.CreateScheme();
            documentBuilder.AddMetaClasses(metaDataScheme.MetaClasses.Values);
            documentBuilder.AddMetaClasses(metaDataScheme.MetaFields.Values.SelectMany(x => x));

            foreach (var buildContext in contexts)
            {
                buildContext.Dictionaries = dictionariesContext;
                buildContext.MetaData = metaDataScheme;
                documentBuilder.AddCatalog(BuildCatalog(buildContext));
            }

            return documentBuilder.XDocument;
        }

        static XElement BuildCatalog(BuildContext context)
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

        static void ValidateXml(XDocument xDoc)
        {
            var schemas = new XmlSchemaSet();
            schemas.Add("", XmlReader.Create(Path.Combine(Environment.CurrentDirectory, "Catalog.xsd")));

            xDoc.Validate(schemas, (o, e) =>
            {
                Console.WriteLine("{0}", e.Message);
            });
        }

        static void ZipAndSave(XDocument xDoc)
        {
            var tempDirectoryPath = Path.Combine(Environment.CurrentDirectory, "Temp");
            var zipFilePath = Path.Combine(Environment.CurrentDirectory, "Catalog.zip");

            if (Directory.Exists(tempDirectoryPath))
            {
                Directory.Delete(tempDirectoryPath, true);
            }

            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }

            Directory.CreateDirectory(tempDirectoryPath);
            xDoc.Save(Path.Combine(tempDirectoryPath, "Catalog.xml"));

            ZipFile.CreateFromDirectory(tempDirectoryPath, zipFilePath);
        }
        
        static readonly Random _random = new Random();

        static BuildContext ConfigureBigCatalog_1M_Variations()
        {
            return new BuildContext
            {
                //catalog settings
                CatalogName = "manywarehousesandmarkets",
                CatalogStartDate = "2010-09-01 07:00:00Z",
                CatalogEndDate = "2020-10-01 07:00:00Z",
                DefaultCatalogLanguage = "en",
                CatalogLanguages = new List<string> { "en", "sv" },
                CatalogCurrency = "USD",
                CatalogCountry = "USA",
                //product and variations settings
                Entries = new BuildContext.EntriesBuildContext
                {
                    AssociationsPerEntryCount = 2,
                    PriceSetter = () => _random.Next(1, 10),
                    PriceStartDate = "2015-04-22 11:40:14Z",
                    PriceEndDate = "2035-04-22 11:40:14Z",
                    BackorderAvailabilityDate = "9999-12-31 23:59:59Z",
                    PreorderAvailabilityDate = "2015-04-22 11:39:31Z"
                },
                //defines catalog structure
                CatalogStructure = new TreeNode(NodeConfiguration.Root)
                {
                    
                    new TreeNode(NodeConfiguration.Empty)
                    {
                        new TreeNode(new NodeConfiguration {ProductCount = 10000, VariationsInProductCount = 100}),
                    },
                }
            };
        }

        static BuildContext ConfigureCatalog_ComplexStructure()
        {
            return new BuildContext
            {
                //catalog settings
                CatalogName = "complexstructure",
                CatalogStartDate = "2010-09-01 07:00:00Z",
                CatalogEndDate = "2020-10-01 07:00:00Z",
                DefaultCatalogLanguage = "en",
                CatalogLanguages = new List<string> { "en", "sv"},
                CatalogCurrency = "USD",
                CatalogCountry = "USA",
                //product and variations settings
                Entries = new BuildContext.EntriesBuildContext
                {
                    AssociationsPerEntryCount = 5,
                    PriceSetter = () => _random.Next(1, 10),
                    PriceStartDate = "2015-04-22 11:40:14Z",
                    PriceEndDate = "2035-04-22 11:40:14Z",
                    BackorderAvailabilityDate = "9999-12-31 23:59:59Z",
                    PreorderAvailabilityDate = "2015-04-22 11:39:31Z"
                },
                //defines catalog structure
                CatalogStructure = new TreeNode(NodeConfiguration.Root)
                {
                    new TreeNode(new NodeConfiguration {ProductCount = 1, VariationsInProductCount = 1}),
                    new TreeNode(new NodeConfiguration {VariationCount = 1}),
                    new TreeNode(NodeConfiguration.Empty)
                    {
                        new TreeNode(new NodeConfiguration {ProductCount = 2, VariationsInProductCount = 3, VariationCount = 1}),
                    },
                    new TreeNode(NodeConfiguration.Empty)
                    {
                        new TreeNode(new NodeConfiguration {ProductCount = 1, VariationsInProductCount = 1}),
                        new TreeNode(NodeConfiguration.Empty)
                        {
                            new TreeNode(new NodeConfiguration {ProductCount = 0, VariationsInProductCount = 3, VariationCount = 2}),
                        },
                        new TreeNode(new NodeConfiguration {ProductCount = 0, VariationsInProductCount = 3}),
                    },
                    new TreeNode(NodeConfiguration.Empty)
                    {
                        new TreeNode(NodeConfiguration.Empty)
                        {
                            new TreeNode(NodeConfiguration.Empty)
                            {
                                new TreeNode(NodeConfiguration.Empty)
                                {
                                    new TreeNode(NodeConfiguration.Empty)
                                    {
                                        new TreeNode(NodeConfiguration.Empty)
                                        {
                                            new TreeNode(NodeConfiguration.Empty)
                                            {
                                                new TreeNode(NodeConfiguration.Empty)
                                                {
                                                    new TreeNode(NodeConfiguration.Empty)
                                                    {
                                                        new TreeNode(NodeConfiguration.Empty)
                                                        {
                                                            new TreeNode(NodeConfiguration.Empty)
                                                            {
                                                                new TreeNode(new NodeConfiguration {ProductCount = 2, VariationsInProductCount = 3, VariationCount = 1}),
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                    },

                }
            };
        }
    }
}
