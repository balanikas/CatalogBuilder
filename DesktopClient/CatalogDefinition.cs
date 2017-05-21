using System;
using CatalogBuilder;

namespace DesktopClient
{
    class CatalogDefinition
    {
        public static BuildContext CreateCatalogDefinition(MainWindow ui, TreeNode structure)
        {
            return new BuildContext
            {
                Dictionaries= new BuildContext.DictionariesBuildContext
                {
                    AssociationTypesCount = ui.NumAssociationTypes.Value.Value,
                    MarketsCount = ui.NumMarkets.Value.Value,
                    PackagesCount = ui.NumPackages.Value.Value,
                    WarehousesCount = ui.NumWarehouses.Value.Value,
                    TaxCategoriesCount = ui.NumTaxCategories.Value.Value,
                    MerchantsCount = ui.NumMerchants.Value.Value,
                },
                CatalogName = ui.TxtCatalogName.Text,
                CatalogStartDate = $"{ui.DateCatalogStartDate.SelectedDate.Value:u}",
                CatalogEndDate = $"{ui.DateCatalogEndDate.SelectedDate.Value:u}",
                DefaultCatalogLanguage = ui.TxtCatalogDefaultLanguage.Text,
                CatalogLanguages = ui.CmbCatalogLanguages.SelectedValue.Split(','),
                CatalogCurrency = ui.TxtCatalogCurrency.Text,
                CatalogCountry = ui.TxtCatalogCountry.Text,
                NodeMetaFieldCount = ui.NumMetaFieldCountPerNode.Value.Value,
                ProductMetaFieldCount = ui.NumMetaFieldCountPerProduct.Value.Value,
                VariationMetaFieldCount = ui.NumMetaFieldCountPerVariation.Value.Value,
                NodeNamingPattern = ui.TxtNodeNamingPattern.Text,
                EntryNamingPattern = ui.TxtEntryNamingPattern.Text,
                Entries = new BuildContext.EntriesBuildContext
                {
                    AssociationsPerEntryCount = ui.NumAssociationsPerEntry.Value.Value,
                    PriceSetter = () => 1, 
                    PriceStartDate = $"{ui.DateEntryPriceStartDate.SelectedDate.Value:u}",
                    PriceEndDate = $"{ui.DateEntryPriceEndDate.SelectedDate.Value:u}",
                    BackorderAvailabilityDate = $"{ui.DateEntryBackorderAvailabilityDate.SelectedDate.Value:u}",
                    PreorderAvailabilityDate = $"{ui.DateEntryPreorderAvailabilityDate.SelectedDate.Value:u}",
                },
                CatalogStructure = structure
            };
        }

        public static TreeNode CreateCatalogStructureDefinition(CatalogStructure structure)
        {
            if (structure == CatalogStructure.Simple)
            {
                return new TreeNode(NodeConfiguration.Root)
                {
                    new TreeNode(NodeConfiguration.Empty)
                    {
                        new TreeNode(new NodeConfiguration {ProductCount = 1, VariationsInProductCount = 2}),
                    },
                };
            }


            if (structure == CatalogStructure.Flat)
            {
                return new TreeNode(NodeConfiguration.Root)
                {
                    new TreeNode(new NodeConfiguration { ProductCount = 1, VariationsInProductCount = 2, VariationCount = 1 })
                };
            }

            if (structure == CatalogStructure.Deep)
            {
                return new TreeNode(NodeConfiguration.Root)
                {
                    new TreeNode(NodeConfiguration.Empty)
                    {
                        new TreeNode(NodeConfiguration.Empty)
                        {
                            new TreeNode(NodeConfiguration.Empty)
                            {
                                new TreeNode(NodeConfiguration.Empty)
                                {
                                    new TreeNode(new NodeConfiguration {ProductCount = 1, VariationsInProductCount = 2})
                                }
                            }
                        }
                    }
                };
            }

            if (structure == CatalogStructure.Complex)
            {
                return new TreeNode(NodeConfiguration.Root)
                {
                    new TreeNode(NodeConfiguration.Empty)
                    {
                        new TreeNode(NodeConfiguration.Empty)
                        {
                            new TreeNode(new NodeConfiguration {ProductCount = 1000, VariationsInProductCount = 10, VariationCount = 100}),
                        },
                        new TreeNode(NodeConfiguration.Empty)
                        {
                            new TreeNode(new NodeConfiguration {ProductCount = 1000, VariationsInProductCount = 10, VariationCount = 100}),
                        },
                    },
                    new TreeNode(NodeConfiguration.Empty)
                    {
                        new TreeNode(NodeConfiguration.Empty)
                        {
                            new TreeNode(new NodeConfiguration {ProductCount = 1000, VariationsInProductCount = 10, VariationCount = 100}),
                        },
                        new TreeNode(NodeConfiguration.Empty)
                        {
                            new TreeNode(new NodeConfiguration {ProductCount = 1000, VariationsInProductCount = 10, VariationCount = 100}),
                        },
                    },
                };
            }

            throw new NotImplementedException();
        }
    }

    public enum CatalogStructure
    {
        Simple,
        Flat,
        Deep,
        Complex,
        
    }
}
