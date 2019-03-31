namespace TodoList.Infrastructure.FileSystemGateway
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class FileSystemItemGateway : IItemGateway
    {
        private readonly string TasksFileName;

        public FileSystemItemGateway()
        {
            TasksFileName = Directory.GetCurrentDirectory() + ".todolist";
            Console.WriteLine(TasksFileName);
        }

        private void Initialize()
        {
            List<Item> items = new List<Item>();
            SaveChanges(items);
        }

        private List<Item> LoadItems()
        {
            if (!File.Exists(TasksFileName))
                Initialize();
            
            string jsonContents = File.ReadAllText(TasksFileName);
            var items = JsonConvert.DeserializeObject<List<JsonItem>>(jsonContents);
            return items.Cast<Item>().ToList();
        }

        private void SaveChanges(List<Item> items)
        {
            var jsonContents = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(TasksFileName, jsonContents);
        }

        public void Add(IItem item)
        {
            var items = LoadItems();
            items.Add((Item)item);
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
                items.Add((Item)item);
                SaveChanges(items);
            }
        }
    }
}