namespace TodoList.Infrastructure.FileSystemGateway
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System;
    using Newtonsoft.Json;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class FileSystemItemGateway : IItemGateway
    {
        private readonly string TasksFileName;

        public FileSystemItemGateway()
        {
            string homePath;

            if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
                homePath = Environment.GetEnvironmentVariable("HOME");
            else
                homePath = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            TasksFileName = System.IO.Path.Combine(homePath, ".todolist");
        }

        private void Initialize()
        {
            List<Item> items = new List<Item>();
            SaveChanges(items);
        }

        private List<Item> LoadItems()
        {
            try
            {
                if (!File.Exists(TasksFileName))
                    Initialize();

                string jsonContents = File.ReadAllText(TasksFileName);
                var items = JsonConvert.DeserializeObject<List<JsonItem>>(jsonContents);
                return items.Cast<Item>().ToList();
            }
            catch(Exception ex)
            {
                throw new Exception($"The file `{ TasksFileName }` is not accessible to read. Try running `todo` with adminstrator rights.", ex);
            }
        }

        private void SaveChanges(List<Item> items)
        {
            try
            {
                var jsonContents = JsonConvert.SerializeObject(items, Formatting.Indented);
                File.WriteAllText(TasksFileName, jsonContents);
            }
            catch(Exception ex)
            {
                throw new Exception($"The file `{ TasksFileName }` is not accessible to write. Try running `todo` with adminstrator rights.", ex);
            }
        }

        public void Add(IItem item)
        {
            var items = LoadItems();
            items.Add((Item) item);
            SaveChanges(items);
        }

        public void Delete(string itemId)
        {
            var items = LoadItems();

            Item item = items
                .SingleOrDefault(e => e.Id.ToString().StartsWith(itemId));

            if (item != null)
            {
                items.Remove(item);
                SaveChanges(items);
            }
        }

        public IItem Get(string itemId)
        {
            var items = LoadItems();
            Item item = items
                .SingleOrDefault(e => e.Id.ToString().StartsWith(itemId));

            return item;
        }

        public IList<IItem> List()
        {
            var items = LoadItems();
            return items.Cast<IItem>().ToList();
        }

        public void Update(IItem item)
        {
            var items = LoadItems();

            Item currentItem = items
                .SingleOrDefault(e => e.Id.ToString().StartsWith(item.Id.ToString()));

            if (currentItem != null)
            {
                items.Remove(currentItem);
                items.Add((Item) item);
                SaveChanges(items);
            }
        }
    }
}