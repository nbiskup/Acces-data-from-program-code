using Azure.Storage.Blobs.Models;
using BlobStorage.Dao;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorage.ViewModel
{
    internal class ItemsViewModel
    {
        private const string directory = "images";
        public const string ForwardSlash = "/";
        public ObservableCollection<BlobItem> Items { get; }

        public ItemsViewModel()
        {
            Items = new ObservableCollection<BlobItem>();
            Refresh();
        }        

        

        private void Refresh()
        {
            Items.Clear();
            Repository.Container.GetBlobs().ToList().ForEach(item =>
            {
                if (item.Name.Contains(ForwardSlash))
                {
                    string dir = item.Name.Substring(0, item.Name.LastIndexOf(ForwardSlash));                    
                }

                if (!item.Name.Contains(ForwardSlash))
                    Items.Add(item);
                
                else if(item.Name.Contains($"{directory}{ForwardSlash}"))
                    Items.Add(item);

            });
        }

        public async Task UploadAsync(string path)
        {
            string filename = path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar)+1);
            filename = string.IsNullOrEmpty(directory?.Trim()) ? filename : $"{directory}{Path.DirectorySeparatorChar}{filename}";
                       
            using (var fs = File.OpenRead(path))
                await Repository.Container.GetBlobClient(filename).UploadAsync(fs, true);
            
            Refresh();
        }


        public async Task DownloadAsync(BlobItem item, string path)
        {
            using (var fs = File.OpenWrite(path))
                await Repository.Container.GetBlobClient(item.Name).DownloadToAsync(fs);            
        }

        public async Task DeleteAsync(BlobItem item)
        {
            await Repository.Container.GetBlobClient(item.Name).DeleteAsync();
            Refresh();
        }

    }
}
