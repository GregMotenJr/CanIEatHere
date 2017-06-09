using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace CanIEatHere.Models
{
    public class PlacesAPI
    {
        public Object getPlaceID(string searchString)
        {
            //string searchString = "barrio";
            //string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=-33.8670522,151.1957362&radius=500&type=restaurant&keyword=cruise&key=AIzaSyBk0uA5zKZMDJmD5WUVLJq2gosxwV5GM_c";
            string url = "https://maps.googleapis.com/maps/api/place/textsearch/json?query="+searchString+ "&location=41.4993,-81.69442&radius=32187&type=restaurant&key=AIzaSyBk0uA5zKZMDJmD5WUVLJq2gosxwV5GM_c";
            var client = new WebClient();
            var content = client.DownloadString(url);
            var serializer = new JavaScriptSerializer();
            var jsonContent = serializer.Deserialize<Object>(content);
            return jsonContent;
        }
        
    }
}