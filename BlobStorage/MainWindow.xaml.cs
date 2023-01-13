using Azure.Storage.Blobs.Models;
using BlobStorage.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlobStorage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ItemsViewModel itemsViewModel;
        private const string EXTENSIONS = "Files with|*.jpg;*.png;*.jpeg;*.gif;*.tif;*.svg";


        public MainWindow()
        {
            InitializeComponent();
            itemsViewModel = new ItemsViewModel();
            Init();
        }

        private void Init() => LbItems.ItemsSource = itemsViewModel.Items;        

        private void LbItems_SelectionChanged(object sender, SelectionChangedEventArgs e) => DataContext = LbItems.SelectedItem as BlobItem;
                      
        
        private async void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = EXTENSIONS;
            if (fileDialog.ShowDialog() == true)
                await itemsViewModel.UploadAsync(fileDialog.FileName);

            LbItems.ItemsSource = itemsViewModel.Items;
        }

        private async void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            if (!(LbItems.SelectedItem is BlobItem item))
                return;

            var fileDialog = new SaveFileDialog
            {
                Filter = EXTENSIONS,
                FileName = item.Name.Substring(item.Name.LastIndexOf(ItemsViewModel.ForwardSlash) + 1)
            };
            if (fileDialog.ShowDialog() == true)
                await itemsViewModel.DownloadAsync(item, fileDialog.FileName);
            
            
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!(LbItems.SelectedItem is BlobItem item))
                return;
            
            await itemsViewModel.DeleteAsync(item);
        }
    }
}
