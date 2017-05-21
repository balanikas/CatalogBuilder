using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using CatalogBuilder;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Search;
using Microsoft.Win32;

namespace DesktopClient
{
    public partial class MainWindow : Window
    {
        private Point _dragStart;

        private TreeNode _structure;
        private TreeViewItem _selectedTreeViewItem;
        private readonly MainWindowViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();

            TextEditor.TextArea.DefaultInputHandler.NestedInputHandlers.Add(new SearchInputHandler(TextEditor.TextArea));

            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
            
            SetDefaultValues();
        }

        private void TreeCatalog_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (TreeCatalog.SelectedItem == null)
            {
                return;
            }

            _selectedTreeViewItem = (TreeViewItem) TreeCatalog.SelectedItem;
            GrdStructureControls.IsEnabled = e.NewValue != null;

            if (TreeUtils.IsRoot(_selectedTreeViewItem))
            {
                GrdStructureControls.IsEnabled = false;
                return;
            }

            var treeItem = (TreeViewItem) (e.NewValue ?? e.OldValue);
            var node = (TreeNode) treeItem.Tag;

            if (node == null)
            {
                return;
            }

            NumProductCount.Value = node.Config.ProductCount;
            NumVariationsPerProductCount.Value = node.Config.VariationsInProductCount;
            NumVariationCount.Value = node.Config.VariationCount;
        }

        private void BtnCreateFromDefinition_Click(object sender, RoutedEventArgs e)
        {
            BtnDragSource.Tag = null;
            BtnDragSource.Content = null;
            BtnDragSource.IsEnabled = false;

            //TreeUtils.UpdateNodeNamingPattern(_structure, TxtNodeNamingPattern.Text);

            var context = CatalogDefinition.CreateCatalogDefinition(this, _structure);

            RunAsync(
                () =>
                {
                    var doc = Builder.Build(context);
                    FileSystem.Save(doc);
                },
                () =>
                {
                    TextEditor.Load(FileSystem.GetDocumentPath());
                    UIUtils.UpdateFolding(TextEditor);
                    _viewModel.StatusBar.Update(_structure, TextEditor.Document.LineCount);
                });
        }


        private void CmbCatalogStructure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _structure = CatalogDefinition.CreateCatalogStructureDefinition((CatalogStructure) e.AddedItems[0]);
            TreeUtils.CreateTree(TreeCatalog, _structure);

            ((TreeViewItem)TreeCatalog.Items[0]).IsSelected = true;

        }

        private void SetDefaultValues()
        {
            CmbCatalogStructure.ItemsSource = Enum.GetValues(typeof(CatalogStructure)).Cast<CatalogStructure>();
            CmbCatalogStructure.SelectedItem = CatalogStructure.Simple;

            TxtCatalogCountry.Text = "USA";
            TxtCatalogCurrency.Text = "USD";
            TxtCatalogDefaultLanguage.Text = "en";
            TxtCatalogName.Text = "testcatalog";

            DateCatalogStartDate.SelectedDate = DateTime.Now.AddDays(-1);
            DateCatalogEndDate.SelectedDate = DateTime.Now.AddDays(1);
            DateEntryPriceStartDate.SelectedDate = DateTime.Now.AddDays(-1);
            DateEntryPriceEndDate.SelectedDate = DateTime.Now.AddDays(1);
            DateEntryBackorderAvailabilityDate.SelectedDate = DateTime.Now.AddDays(-1);
            DateEntryPreorderAvailabilityDate.SelectedDate = DateTime.Now.AddDays(-1);

            CmbCatalogLanguages.ItemsSource = new List<string> {"en", "sv", "fr", "ru", "de", "es"};
            CmbCatalogLanguages.SelectedItem = "en";

           // TxtNodeNamingPattern.Text = TreeNode.NodeNamingPattern;
        }

        private void NumProductCount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var node = (TreeNode)_selectedTreeViewItem.Tag;
            node.Config.ProductCount = Convert.ToInt32(e.NewValue);
            TreeUtils.UpdateTreeViewItemHeader(_selectedTreeViewItem, node);
        }

        private void NumVariationsPerProductCount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var node = (TreeNode)_selectedTreeViewItem.Tag;
            node.Config.VariationsInProductCount = Convert.ToInt32(e.NewValue);
            TreeUtils.UpdateTreeViewItemHeader(_selectedTreeViewItem, node);
        }

        private void NumVariationCount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var node = (TreeNode)_selectedTreeViewItem.Tag;
            node.Config.VariationCount = Convert.ToInt32(e.NewValue);
            TreeUtils.UpdateTreeViewItemHeader(_selectedTreeViewItem, node);
        }

        private void DragSource_Click(object sender, RoutedEventArgs e)
        {
            string zipFilePath = "";
            var text = TextEditor.Text;
            if (text == "")
            {
                return;
            }

            RunAsync(
                () =>
                {
                    var doc = XDocument.Parse(text);
                    zipFilePath = FileSystem.ZipAndSave(doc);
                },
                () =>
                {
                    BtnDragSource.Tag = zipFilePath;
                    BtnDragSource.Content = $"drag me to commerce manager or file explorer\n ({zipFilePath})";
                    BtnDragSource.IsEnabled = true;
                });
        }

        private void BtnDragSource_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           _dragStart = e.GetPosition(null);
        }

        private void BtnDragSource_MouseMove(object sender, MouseEventArgs e)
        {
            var mpos = e.GetPosition(null);
            var diff = _dragStart - mpos;

            if (BtnDragSource.Tag != null &&
                Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                var files = new[] {(string)BtnDragSource.Tag};
                DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, files), DragDropEffects.Copy);
            }
        }

        private void BtnOpenExisting_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Catalogs (*.zip)|*.zip";
            var zipFilePath = "";
            if (openFileDialog.ShowDialog() == true)
            {
                zipFilePath = openFileDialog.FileName;
            }

            var extractedFilePath = FileSystem.ExtractAndSave(zipFilePath);
            TextEditor.Load(extractedFilePath);
            UIUtils.UpdateFolding(TextEditor);
            _viewModel.StatusBar.Update(extractedFilePath, TextEditor.Document.LineCount);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextEditor.Clear();
           _viewModel.StatusBar.Clear();

            BtnDragSource.Tag = null;
            BtnDragSource.Content = null;
            BtnDragSource.IsEnabled = false;
        }

        private void RunAsync(Action action, Action continuation)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (o, ea) =>
            {
                action();
            };
            worker.RunWorkerCompleted += (o, ea) =>
            {
                continuation();
                IsEnabled = true;
            };

            IsEnabled = false;
            worker.RunWorkerAsync();
        }

        private void TxtCatalogName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtNodeNamingPattern.Text = TxtCatalogName.Text;
            TxtEntryNamingPattern.Text = TxtCatalogName.Text;
        }
    }
}