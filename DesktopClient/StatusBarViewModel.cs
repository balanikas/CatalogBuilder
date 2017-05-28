using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using CatalogBuilder;
using ICSharpCode.AvalonEdit;

namespace DesktopClient
{
    public class StatusBarViewModel : INotifyPropertyChanged
    {
        private int _linesOfCode;
        private int _nodesCount;
        private int _productsCount;
        private int _variationsCount;
        private int _associationsCount;
        private int _warehousesCount;
        private int _packagesCount;
        private int _merchantsCount;
        private int _taxCategoriesCount;
        private int _associationTypesCount;
        private int _marketsCount;

        public int LinesOfCode
        {
            get => _linesOfCode;
            set
            {
                _linesOfCode = value;
                RaisePropertyChanged("LinesOfCode");
            }
        }

        public int NodesCount
        {
            get => _nodesCount;
            set
            {
                _nodesCount = value;
                RaisePropertyChanged("NodesCount");
            }
        }

        public int ProductsCount
        {
            get => _productsCount;
            set
            {
                _productsCount = value;
                RaisePropertyChanged("ProductsCount");
            }
        }

        public int VariationsCount
        {
            get => _variationsCount;
            set
            {
                _variationsCount = value;
                RaisePropertyChanged("VariationsCount");
            }
        }

        public int AssociationsCount
        {
            get => _associationsCount;
            set
            {
                _associationsCount = value;
                RaisePropertyChanged("AssociationsCount");
            }
        }

        public int WarehousesCount
        {
            get => _warehousesCount;
            set
            {
                _warehousesCount = value;
                RaisePropertyChanged("WarehousesCount");
            }
        }

        public int MarketsCount
        {
            get => _marketsCount;
            set
            {
                _marketsCount = value;
                RaisePropertyChanged("MarketsCount");
            }
        }

        public int MerchantsCount
        {
            get => _merchantsCount;
            set
            {
                _merchantsCount = value;
                RaisePropertyChanged("MerchantsCount");
            }
        }

        public int AssociationTypesCount
        {
            get => _associationTypesCount;
            set
            {
                _associationTypesCount = value;
                RaisePropertyChanged("AssociationTypesCount");
            }
        }

        public int PackagesCount
        {
            get => _packagesCount;
            set
            {
                _packagesCount = value;
                RaisePropertyChanged("PackagesCount");
            }
        }

        public int TaxCategoriesCount
        {
            get => _taxCategoriesCount;
            set
            {
                _taxCategoriesCount = value;
                RaisePropertyChanged("TaxCategoriesCount");
            }
        }

        public void Clear()
        {
            LinesOfCode = -1;
            NodesCount = -1;
            ProductsCount = -1;
            VariationsCount = -1;
            AssociationsCount = -1;
            WarehousesCount = -1;
            PackagesCount = -1;
            MarketsCount = -1;
            MerchantsCount = -1;
            TaxCategoriesCount = -1;
            AssociationTypesCount = -1;

        }

        public void Update(string filePath, int linesOfCode)
        {
            var doc = XDocument.Load(filePath);
            var entries = doc.Root.Element("Catalog").Element("Entries").Elements();

            LinesOfCode = linesOfCode;
            NodesCount = doc.Root.Element("Catalog").Element("Nodes").Elements().Count(); 
            ProductsCount = entries.Count(x => x.Element("EntryType").Value.Equals("Product", StringComparison.InvariantCultureIgnoreCase));
            VariationsCount = entries.Count(x => x.Element("EntryType").Value.Equals("Variation", StringComparison.InvariantCultureIgnoreCase));
            AssociationsCount = doc.Root.Element("Catalog").Element("Associations").Elements("CatalogAssociation").Count();
            WarehousesCount = doc.Root.Element("Dictionaries").Element("Warehouses").Elements("Warehouse").Count();
            PackagesCount = doc.Root.Element("Dictionaries").Element("Packages").Elements("Package").Count();
            MarketsCount = doc.Root.Element("Dictionaries").Element("Markets").Elements("Market").Count();
            MerchantsCount = doc.Root.Element("Dictionaries").Element("Merchants").Elements("Merchant").Count();
            TaxCategoriesCount = doc.Root.Element("Dictionaries").Element("TaxCategories").Elements("TaxCategory").Count();
            AssociationTypesCount = doc.Root.Element("Dictionaries").Element("AssociationTypes").Elements("AssociationType").Count();
        }

        public void Update(TreeNode node, int linesOfCode)
        {
            LinesOfCode = linesOfCode;
            NodesCount = TreeUtils.CountNodes(node);
            ProductsCount = TreeUtils.CountProducts(node);
            VariationsCount = TreeUtils.CountVariations(node);
            AssociationsCount = -1;
            WarehousesCount = -1;
            PackagesCount = -1;
            MarketsCount = -1;
            MerchantsCount = -1;
            TaxCategoriesCount = -1;
            AssociationTypesCount = -1;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}