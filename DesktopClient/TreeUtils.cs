using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using CatalogBuilder;

namespace DesktopClient
{
    class TreeUtils
    {
        public static int CountNodes(TreeNode node)
        {
            return 1 + node.Sum(CountNodes);
        }

        public static int CountProducts(TreeNode node)
        {
            return node.Config.ProductCount + node.Sum(CountProducts);
        }

        public static int CountVariations(TreeNode node)
        {
            return node.Config.VariationCount + (node.Config.ProductCount * node.Config.VariationsInProductCount) + node.Sum(CountVariations);
        }

        public static void UpdateNodeNamingPattern(TreeNode node, string pattern)
        {
            if (!node.Config.IsRoot)
            {
                var onlyLetters = new String(node.Id.Where(Char.IsLetter).ToArray());
                node.Id = node.Id.Replace(onlyLetters, pattern);
            }

            foreach (var child in node)
            {
                UpdateNodeNamingPattern(child, pattern);
            }
        }

        public static void CreateTree(TreeView treeView, TreeNode node)
        {
            treeView.Items.Clear();
            var treeItem = new TreeViewItem { Header = "Root", IsExpanded = true};
            treeView.Items.Add(treeItem);

            CreateTree(node, treeItem);
        }

        public static void UpdateTreeViewItemHeader(TreeViewItem treeViewItem, TreeNode node)
        {
            treeViewItem.Header = $"prod: {node.Config.ProductCount} var/prod: {node.Config.VariationsInProductCount} var: {node.Config.VariationCount}";
        }

        public static bool IsRoot(TreeViewItem treeViewItem)
        {
            return treeViewItem.Tag == null;
        }

        private static void CreateTree(TreeNode node, TreeViewItem treeItem)
        {
            if (!node.Config.IsRoot)
            {
                var childTreeItem = new TreeViewItem
                {
                    Tag = node,
                    IsExpanded = true,
                };

                UpdateTreeViewItemHeader(childTreeItem, node);
                treeItem.Items.Add(childTreeItem);

                if (node.Any())
                {
                    treeItem = childTreeItem;
                }
            }

            foreach (var child in node)
            {
                CreateTree(child, treeItem);
            }
        }
    }
}
