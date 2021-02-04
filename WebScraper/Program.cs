using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {

            string input = String.Empty;

            do
            {
                Console.WriteLine("Pick a genre to get some suggestions. Try typing \"Fantasy\" or \"Horror\"");
                Console.WriteLine("Pick your genre: ");
                input = Console.ReadLine();
            } while (input == String.Empty);



            Console.WriteLine($"Here's some suggestions for {input} books: ");
            Console.WriteLine("____________________");
            Console.WriteLine();

            BookList(GetBooksByGenre(input.ToLower()));
        }

        public static List<HtmlNode> GetBooksByGenre(string genre)
        {
            var html = @$"https://www.goodreads.com/genres/new_releases/{genre}";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);

            List<HtmlNode> nodes = htmlDoc.DocumentNode.SelectNodes("//div").Where(x => x.Id.StartsWith("bookCover")).ToList();
            return nodes;

        }

        public static void BookList(List<HtmlNode> nodes)
        {
            var titles = new List<string>();


            foreach (HtmlNode divNode in nodes)
            {
                var imgNode = divNode.SelectSingleNode(".//img");
                if (imgNode != null)
                {
                    var alt = imgNode.GetAttributeValue("alt", null);
                    if (alt != null)
                    {
                        titles.Add(alt);
                    }
                }
            }

            //foreach (var title in titles)
            //{
            //    Console.WriteLine($"{title}");
            //    Console.WriteLine();
            //}
            for(int i = 0; i < titles.Count; i++)
            {
                Console.WriteLine($"{titles[i]}");
                Console.WriteLine();
                if(i % 9 == 0 && i != 0)
                {
                    Console.WriteLine("Press Enter to get more suggestions (up to 10).");
                    Console.ReadLine();
                    continue;
                }
            }
        }


    }
}
