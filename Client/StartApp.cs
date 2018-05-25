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
            //            Console.WriteLine("Wcisnij cokolwiek by dodac usera");
            //            Console.ReadKey();
            //            await AddUser();
            //            Console.WriteLine("User dodany");
            //            Console.ReadKey();

            Console.WriteLine("Wcisnij cokolwiek by odczytac userow");
            Console.ReadKey();

            try
            {

                var users = await GetAllUsers();
                foreach (var user in users)
                {
                    Console.WriteLine(user);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Wcisnij cokolwiek by wyjsc");
            Console.ReadKey();
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

                await httpClient.PostAsync(uri, new StringContent(
                    userSerialized, Encoding.UTF8, "application/json"));
            }
        }

        private void DeleteUser()
        {
            using (HttpClient httpClient = new HttpClient())
            {

            }
        }
    }
}
