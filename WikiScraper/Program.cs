using System;
using System.Net.Http;
using WikiClientLibrary.Client;
using WikiClientLibrary.Sites;

namespace WikiScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();
            client.ProcessPages("./scrappedData.json",50);
            Console.ReadLine();
        }
    }
}