using CommonLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class StartApp
    {
        public async Task Start()
        {
            #region
            // DODAWANIE USERA
            //            Console.WriteLine("Wcisnij cokolwiek by dodac usera");
            //            Console.ReadKey();
            //            await AddUser();
            //            Console.WriteLine("User dodany");
            //            Console.ReadKey();


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

            #endregion
        }

        private async Task<IEnumerable<Group>> GetAllGroups()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var address = "group";
                var baseUri = new Uri(ConfigurationManager.AppSettings["endPoint"]);        // wyciagnac jako stale pole?
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
                    Name = "Adam",
                    Email = "adam@Malysz@pl.pl"
                };

                var userSerialized = JsonConvert.SerializeObject(user);

                await httpClient.PostAsync(uri, new StringContent(userSerialized, Encoding.UTF8, "application/json"));
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
    }
}
