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
            Console.WriteLine("Wcisnij cokolwiek by dodac usera");
            Console.ReadKey();
            await CallWithLoadingProgress(AddUser());
            Console.WriteLine("User dodany");
            Console.ReadKey();

            #region

            //DODAWANIE USERA
            //            Console.WriteLine("Wcisnij cokolwiek by dodac usera");
            //Console.ReadKey();
            //await CallWithLoadingProgress(AddUser());
            //Console.WriteLine("User dodany");
            //Console.ReadKey();


            // ODCZYTYWANIE WSZYSTKICH USEROW
            //Console.WriteLine("Wcisnij cokolwiek by odczytac userow");
            //Console.ReadKey();

            //try
            //{

            //    var users = await GetAllUsers();
            //    foreach (var user in users)
            //    {
            //        Console.WriteLine(user);
            //    }
            //}
            //catch (HttpRequestException e)
            //{
            //    Console.WriteLine(e);
            //}

            //Console.WriteLine("Wcisnij cokolwiek by wyjsc");
            //Console.ReadKey();


            // USUWANIE USERA
            //Console.WriteLine("Podaj id usera do usuniecia");
            //int id = int.Parse(Console.ReadLine());

            //await DeleteUser(id);
            //Console.WriteLine("User został usuniety");
            //Console.ReadKey();


            //// DODAWANIE GRUPY
            //Console.WriteLine("Wcisnij cokolwiek by dodac grupę");
            //Console.ReadKey();
            //await AddGroup();
            //Console.WriteLine("Grupa dodana");
            //Console.ReadKey();

            ////USUWANIE GRUPY
            //Console.WriteLine("Podaj id grupy do usuniecia");
            //int id = int.Parse(Console.ReadLine());

            //await DeleteGroup(id);
            //Console.WriteLine("Grupa została usunieta");
            //Console.ReadKey();


            ///// ODCZYTYWANIE WSZYSTKICH Grup
            //Console.WriteLine("Wcisnij cokolwiek by odczytac grupy");
            //Console.ReadKey();

            //try
            //{
            //    var groups = await GetAllGroups();
            //    foreach (var group in groups)
            //    {
            //        Console.WriteLine(group);
            //    }
            //}
            //catch (HttpRequestException e)
            //{
            //    Console.WriteLine(e);
            //}

            //Console.WriteLine("Wcisnij cokolwiek by wyjsc");
            //Console.ReadKey();

            ////DODAWANIE POSTA
            //Console.WriteLine("Wcisnij podaj numer grupy do ktorej chcesz napisac post");
            //int id = int.Parse(Console.ReadLine());
            //Console.WriteLine("Podaj test posta");
            //string text = Console.ReadLine();
            //await PostMessage(id, text);
            //Console.WriteLine("Post automatyczny napisany");
            //Console.ReadKey();

            ////USUWANIE POSTA
            //Console.WriteLine("Podaj numer grupy z ktorej chcesz usunac post");
            //int groupid = int.Parse(Console.ReadLine());
            //Console.WriteLine("Podaj id posta do usuniecia");
            //int postid = int.Parse(Console.ReadLine());
            //await DeletePost(groupid, postid);
            //Console.WriteLine("Post usunięto");
            //Console.ReadKey();

            ////ODCZYTYWANIE WSZYSTKICH POSTOW Z GRUPY
            //Console.WriteLine("Podaj numer grupy z ktorej chcesz odczytac posty");
            //int groupid = int.Parse(Console.ReadLine());

            //try
            //{

            //    var messages = await GetAllPosts(groupid);
            //    foreach (var message in messages)
            //    {
            //        Console.WriteLine(message);
            //    }
            //    Console.ReadKey();
            //}
            //catch (HttpRequestException e)
            //{
            //    Console.WriteLine(e);
            //}

            #endregion
        }

        private async Task CallWithLoadingProgress(Task task)
        {
            using (var cts = new CancellationTokenSource())
            {
                try
                {
                    var t = Task.Run(() => ShowLoadingDots(cts.Token), cts.Token);
                    cts.Cancel();
                    await t;
                }
                catch (TaskCanceledException e)
                {
                   return;                  
                }
               
                //cts.Cancel();
                //await t;
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
    }
}



