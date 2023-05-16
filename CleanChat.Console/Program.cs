using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanChat.ConsoleApp
{
    public class ApiResponse
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public object? ResponseData { get; set; }
    }
    public class Topic
    {
        public int topicId { get; set; }
        public string topicName { get; set; } = string.Empty;
    }

    public class Program
    {
        static HttpClient client = new HttpClient();
        static void ShowTopic(Topic topic)
        {
            Console.WriteLine($"{topic.topicId}: {topic.topicName}");
        }

        //static async Task<Uri> CreateTopicAsync(Topic topic)
        //{
        //    HttpResponseMessage response = await client.PostAsJsonAsync("api/Topic", topic);
        //    response.EnsureSuccessStatusCode();

        //    return response.Headers.Location;
        //}

        //static async Task<Topic?> GetTopicAsync(string path)
        //{
        //    Topic? topic = null;
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var temp = await response.Content.ReadFromJsonAsync<Topic?>();
        //        Console.WriteLine(temp);
        //        topic = temp;
        //    }
        //    return topic;
        //}

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://localhost:7221/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                //Topic topic = new Topic
                //{
                //    TopicName = "test"
                //};

                //var url = await CreateTopicAsync(topic);
                //Console.WriteLine($"Created at {url}");

                //topic = await GetTopicAsync("api/Topic");
                //ShowTopic(topic);
                HttpResponseMessage response = await client.GetAsync("api/Topic");
                var temp = await response.Content.ReadFromJsonAsync<ApiResponse>();
                var data = (temp?.ResponseData)?.ToString();
                var topics = JsonSerializer.Deserialize<List<Topic>>(data!) ?? new List<Topic>();
                foreach (var topic in topics)
                {
                    Console.WriteLine($"{topic.topicId} : {topic.topicName}");
                }

                Topic newTopic = new Topic()
                {
                    topicName = "test"
                };

                HttpResponseMessage respost = await client.PostAsJsonAsync("api/Topic", newTopic);
                respost.EnsureSuccessStatusCode();

                Console.WriteLine($"post: {respost.Headers.Location}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Exception: {ex.Data}");
            }
        }
    }
}