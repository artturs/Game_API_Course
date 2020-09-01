using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assignment1
{
    class Program
    {

        static void Main(string[] args) { AsyncMain(args).GetAwaiter().GetResult(); }

        static async Task AsyncMain(string[] args)
        {

            bool useOffline = false;
        
            if (args != null && args.Length != 0)
            {
                if(args.Length > 1)
                {
                    if(args[1] == "offline") { useOffline = true; }
                    else if(args[1] != "realtime") { Console.WriteLine("Mode not specified! Defaulting to realtime."); }
                }
                else { Console.WriteLine("Mode not specified! Defaulting to realtime."); }
                try
                {
                    if (useOffline)
                    {
                        OfflineCityBikeDataFetcher offlineCityBikeDataFetcher = new OfflineCityBikeDataFetcher();
                        await offlineCityBikeDataFetcher.GetBikeCountInStation(args[0]);
                    }
                    else
                    {
                        RealTimeCityBikeDataFetcher realTimeCityBikeDataFetcher = new RealTimeCityBikeDataFetcher();
                        realTimeCityBikeDataFetcher.Deserial(await realTimeCityBikeDataFetcher.GetJson());
                        await realTimeCityBikeDataFetcher.GetBikeCountInStation(args[0]);
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Invalid argument: " + ex.Message);
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine("Not found: " + ex.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                }
            }
            else
            {
                Console.WriteLine("Error: No arguments");
            }
        }
    }


    public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {
        Uri Url = new Uri("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
        public System.Net.Http.HttpClient client;
        StationList list;

        public RealTimeCityBikeDataFetcher()
        {
            client = new System.Net.Http.HttpClient();


        }

        public async Task<string> GetJson()
        {
            return await client.GetStringAsync(Url);
        }

        public void Deserial(string responce)
        {
            list = JsonConvert.DeserializeObject<StationList>(responce);
        }

        public Task<int> GetBikeCountInStation(string stationName)
        {
            if (stationName.Any(c => char.IsDigit(c)))
            {
                throw new ArgumentException(stationName);
            }

            foreach (var station in list.Stations)
            {
                if (stationName == station.Name)
                {
                    Console.WriteLine("Bikes available: " + station.BikesAvailable);
                    return Task.FromResult<int>(station.BikesAvailable);
                }
            }
            throw new NotFoundException();
        }
    }


    public class StationList
    {
        public Station[] Stations { get; set; }
    }
    public class Station
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("bikesAvailable")]
        public int BikesAvailable { get; set; }
    }


    public class NotFoundException : Exception
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception inner) : base(message, inner) { }
        protected NotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    public class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
    {
        readonly string path = Path.Combine(Environment.CurrentDirectory, "bikedata.txt");
        
        public int CountLines(string filePath)
            => File.ReadLines(filePath).Count();

        public Station[] GetStationList()
        {
            Station[] list = new Station[CountLines(path)];
            
            string line;
            int i = 0;

            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while((line = file.ReadLine()) != null)
            {
                list[i] = new Station();

                string[] data = line.Split(':');
                list[i].Name = data[0].TrimEnd();
                int numOfBikes = Int32.Parse(data[1]);
                list[i].BikesAvailable = numOfBikes;
                i++;
            }

            return list;
        }

        public Task<int> GetBikeCountInStation(string stationName)
        {
            if (stationName.Any(c => char.IsDigit(c)))
            {
                throw new ArgumentException(stationName);
            }

            Station[] list = GetStationList();

            foreach (var station in list)
            {
                if (stationName == station.Name)
                {
                    Console.WriteLine("Bikes available: " + station.BikesAvailable);
                    return Task.FromResult<int>(station.BikesAvailable);
                }
            }
            throw new NotFoundException();
        }
    }
}