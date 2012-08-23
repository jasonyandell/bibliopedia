using System;

namespace DrupalConnect.Commands
{
    public class BibliopediaUrls
    {
        public static Uri Domain { get { return new Uri("http://bibliopedia.org/"); } }

/*
        public static Uri RestEndpoint = new Uri(Domain, "crawled_data");
*/
        public static readonly Uri CreateNode = new Uri(Domain, "node/add/journal-article");    
    }
}