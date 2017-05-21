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

        public void Clear()
        {
            LinesOfCode = -1;
            NodesCount = -1;
            ProductsCount = -1;
            VariationsCount = -1;
        }

        public void Update(string filePath, int linesOfCode)
        {
            var doc = XDocument.Load(filePath);
            var entries = doc.Root.Element("Catalog").Element("Entries").Elements();

            LinesOfCode = linesOfCode;
            NodesCount = doc.Root.Element("Catalog").Element("Nodes").Elements().Count(); 
            ProductsCount = entries.Count(x => x.Element("EntryType").Value.Equals("Product", StringComparison.InvariantCultureIgnoreCase));
            VariationsCount = entries.Count(x => x.Element("EntryType").Value.Equals("Variation", StringComparison.InvariantCultureIgnoreCase));
        }

        public void Update(TreeNode node, int linesOfCode)
        {
            LinesOfCode = linesOfCode;
            NodesCount = TreeUtils.CountNodes(node);
            ProductsCount = TreeUtils.CountProducts(node);
            VariationsCount = TreeUtils.CountVariations(node);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}