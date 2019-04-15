namespace TodoList.Infrastructure.GistGateway
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;
    using Octokit;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;
    using TodoList.Core.Exceptions;

    public sealed class GistItemGateway : IItemGateway
    {
        private GitHubClient _githubClient;
        private string _gistToken;
        private string _gistId;

        private void CreateGitHubConnection()
        {
            _githubClient = new GitHubClient(new ProductHeaderValue("todo"));
            _githubClient.Credentials = new Credentials(_gistToken);
        }

        private List<Item> EnsureItemsAreLoaded()
        {
            if (string.IsNullOrWhiteSpace(_gistId))
            {
                List<Item> emptyItems = new List<Item>();
                var gist = CreateGist(emptyItems);
                return emptyItems;
            }

            try
            {
                Gist gist = LoadGist();
                string jsonContents = gist.Files["todolist.json"].Content;
                var items = JsonConvert.DeserializeObject<List<JsonItem>>(jsonContents);
                return items.Cast<Item>().ToList();
            }
            catch (Octokit.NotFoundException)
            {
                List<Item> emptyItems = new List<Item>();
                var gist = CreateGist(emptyItems);
                return emptyItems;
            }
        }

        private Gist LoadGist()
        {
            var gist = _githubClient
                .Gist
                .Get(_gistId).GetAwaiter()
                .GetResult();

            return gist;
        }

        private Gist CreateGist(List<Item> items)
        {
            var jsonContents = JsonConvert.SerializeObject(items, Formatting.Indented);
            var newGist = new NewGist();
            newGist.Description = "todolist";
            newGist.Files.Add("todolist.json", jsonContents);

            var gist = _githubClient.Gist
                .Create(newGist)
                .GetAwaiter()
                .GetResult();

            _gistId = gist.Id;

            WriteGistIdToAppSettings();

            return gist;
        }

        private void UpdateGist(List<Item> items)
        {
            var jsonContents = JsonConvert.SerializeObject(items, Formatting.Indented);
            var gistFileUpdate = new GistFileUpdate();
            gistFileUpdate.Content = jsonContents;
            gistFileUpdate.NewFileName = "todolist.json";
            var gistUpdate = new GistUpdate();
            gistUpdate.Description = "todolist";
            gistUpdate.Files.Add("todolist.json", gistFileUpdate);
            _githubClient.Gist
                .Edit(_gistId, gistUpdate)
                .GetAwaiter()
                .GetResult();
        }

        private void ReadGitHubCredentialsFromAppSettings()
        {
            var appsettingsPath = Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location) +
                Path.DirectorySeparatorChar +
                "appsettings.json";

            if (!File.Exists(appsettingsPath))
                throw new InfrastructureException($"The appsettings file `({appsettingsPath})` does not exit.");

            var contents = File.ReadAllText(appsettingsPath);
            var configuration = JsonConvert.DeserializeObject<JObject>(contents);

            _gistToken = configuration["GistToken"].Value<string>();
            _gistId = configuration["GistId"].Value<string>();

            if (string.IsNullOrWhiteSpace(_gistToken))
                throw new InfrastructureException($"The Gist Token was not setup. Create one at https://github.com/ivanpaulovich/todo#setup.");
        }

        private void WriteGistIdToAppSettings()
        {
            var appsettingsPath = Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location) +
                Path.DirectorySeparatorChar +
                "appsettings.json";

            if (!File.Exists(appsettingsPath))
                throw new InfrastructureException($"The appsettings file `({appsettingsPath})` does not exit.");
            
            var contents = File.ReadAllText(appsettingsPath);
            var configuration = JsonConvert.DeserializeObject<JObject>(contents);

            configuration["GistId"] = _gistId;

            contents = JsonConvert.SerializeObject(configuration, Formatting.Indented);
            File.WriteAllText(appsettingsPath, contents);
        }

        public void Add(IItem item)
        {
            ReadGitHubCredentialsFromAppSettings();
            CreateGitHubConnection();

            var items = EnsureItemsAreLoaded();
            items.Add((Item) item);
            UpdateGist(items);
        }

        public void Delete(string itemId)
        {
            ReadGitHubCredentialsFromAppSettings();
            CreateGitHubConnection();

            var items = EnsureItemsAreLoaded();

            Item item = items
                .SingleOrDefault(e => e.Id.ToString().StartsWith(itemId));

            if (item != null)
            {
                items.Remove(item);
                UpdateGist(items);
            }
        }

        public IItem Get(string itemId)
        {
            ReadGitHubCredentialsFromAppSettings();
            CreateGitHubConnection();

            var items = EnsureItemsAreLoaded();
            Item item = items
                .SingleOrDefault(e => e.Id.ToString().StartsWith(itemId));

            return item;
        }

        public IList<IItem> List()
        {
            ReadGitHubCredentialsFromAppSettings();
            CreateGitHubConnection();

            var items = EnsureItemsAreLoaded();
            return items.Cast<IItem>().ToList();
        }

        public void Update(IItem item)
        {
            ReadGitHubCredentialsFromAppSettings();
            CreateGitHubConnection();

            var items = EnsureItemsAreLoaded();

            Item currentItem = items
                .SingleOrDefault(e => e.Id.ToString()
                .StartsWith(item.Id.ToString()));

            if (currentItem != null)
            {
                items.Remove(currentItem);
                items.Add((Item) item);
                UpdateGist(items);
            }
        }
    }
}