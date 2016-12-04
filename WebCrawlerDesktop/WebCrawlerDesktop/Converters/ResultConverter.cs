using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using WebCrawlerModel;
using WebCrawlerModel.Types;

namespace WebCrawlerDesktop.Converters
{
    public class ResultConverter: IValueConverter
    {

        private Dispatcher currentDispatcher = Dispatcher.CurrentDispatcher;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = value as CrawlerResultType;
            if (value == null)
            {
                return null;
            }
            return new List<TreeViewItem>()
            {
                currentDispatcher.Invoke(() => ConvertToBuildTree(result), DispatcherPriority.Background)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }


        private TreeViewItem ConvertToBuildTree(CrawlerResultType crawlerNode, TreeViewItem treeViewItem = null)
        {
            var treeView = new TreeViewItem
            {
                Header = crawlerNode.Url
            };
            foreach (var result in crawlerNode.NodeList)
            {
                treeView.Items.Add(currentDispatcher.Invoke(()=> ConvertToBuildTree(result, treeView), DispatcherPriority.Background));
            }
            return treeView;
        }
    }
}