using CommonLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class StartApp
    {
        public async Task RunApp()
        {        
            await CallWithLoadingProgress(AddUser());           
        }

        private async Task CallWithLoadingProgress(Task task)
        {
            using (var cts = new CancellationTokenSource())
            {
                try
                {
                    var t = Task.Run(() => ShowLoadingDots(cts.Token), cts.Token);
                    await Task.Run(() => task, cts.Token);
                    cts.Cancel();
                    await t;
                }
                catch (TaskCanceledException e)
                {
                   return;                  
                }              
            }
        }

        private async Task<IEnumerable<Group>> GetAllGroups()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var address = "group";
                var baseUri = new Uri(ConfigurationManager.AppSettings["endPoint"]); // wyciagnac jako stale pole?
                var uri = new Uri(baseUri, address);

                httpClient.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var getResponse = await httpClient.GetAsync(uri);
                getResponse.EnsureSuccessStatusCode();
                var groupsString = await getResponse.Content.ReadAsStringAsync();

                var groups = JsonConvert.DeserializeObject<Group[]>(groupsString);

                return groups;
            }
        }

        private async Task<IEnumerable<User>> GetAllUsers()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var address = "user";
                var baseUri = new Uri(ConfigurationManager.AppSettings["endPoint"]);
                var uri = new Uri(baseUri, address);

                httpClient.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var getResponse = await httpClient.GetAsync(uri);
                getResponse.EnsureSuccessStatusCode();
                var usersString = await getResponse.Content.ReadAsStringAsync();

                var users = JsonConvert.DeserializeObject<User[]>(usersString);

                return users;
            }
        }

        private async Task<IEnumerable<Message>> GetAllPosts(int groupId)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var address = $"message/{groupId}";
                var baseUri = new Uri(ConfigurationManager.AppSettings["endPoint"]);
                var uri = new Uri(baseUri, address);

                httpClient.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var getResponse = await httpClient.GetAsync(uri);
                getResponse.EnsureSuccessStatusCode();
                var messagesString = await getResponse.Content.ReadAsStringAsync();

                var messages = JsonConvert.DeserializeObject<Message[]>(messagesString);

                return messages;
            }
        }

        private async Task AddUser()
        {    
                using (HttpClient httpClient = new HttpClient())
                {
                    var address = "user";
                    var baseUri = new Uri(ConfigurationManager.AppSettings["endPoint"]);
                    var uri = new Uri(baseUri, address);

                    var user = new User
                    {
                        Name = "Adam 2",
                        Email = "adam2@Malysz@pl.pl"
                    };

                    var userSerialized = JsonConvert.SerializeObject(user);

                    await httpClient.PostAsync(uri, new StringContent(userSerialized, Encoding.UTF8, "application/json"));
                }                                           
        }

        private async Task AddGroup()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var address = "group";
                var baseUri = new Uri(ConfigurationManager.AppSettings["endPoint"]);
                var uri = new Uri(baseUri, address);

                var group = new Group
                {
                    Name = "Moja grupa",
                };

                var groupSerialized = JsonConvert.SerializeObject(group);

                await httpClient.PostAsync(uri, new StringContent(groupSerialized, Encoding.UTF8, "application/json"));

            }
        }

        private async Task DeleteUser(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var address = $"user/{id}";
                var baseUri = new Uri(ConfigurationManager.AppSettings["endPoint"]);
                var uri = new Uri(baseUri, address);

                await httpClient.DeleteAsync(uri);
            }
        }

        private async Task DeleteGroup(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var address = $"group/{id}";
                var baseUri = new Uri(ConfigurationManager.AppSettings["endPoint"]);
                var uri = new Uri(baseUri, address);

                await httpClient.DeleteAsync(uri);
            }
        }

        private async Task PostMessage(int groupid, string text)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var address = $"message/{groupid}";
                var baseUri = new Uri(ConfigurationManager.AppSettings["endPoint"]);
                var uri = new Uri(baseUri, address);

                var message = new Message
                {
                    Text = text
                };

                var messageSerialized = JsonConvert.SerializeObject(message);

                await httpClient.PostAsync(uri,
                    new StringContent(messageSerialized, Encoding.UTF8, "application/json"));
            }

        }

        private async Task DeletePost(int groupId, int postId)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var address = $"message/{groupId}/{postId}";
                var baseUri = new Uri(ConfigurationManager.AppSettings["endPoint"]);
                var uri = new Uri(baseUri, address);

                await httpClient.DeleteAsync(uri);
            }
        }

        private void ShowLoadingDots(CancellationToken ct)
        {
            do
            {
                Console.Write("Trwa ładowanie");
                for (int i = 0; i < 3; i++)
                {

                    Console.Write(". ");
                    Thread.Sleep(1000);
                }
                Console.Clear();
            } while (ct.IsCancellationRequested);
        }

    }
}



