﻿using System;
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
            string url = "https://maps.googleapis.com/maps/api/place/textsearch/json?query="+searchString+ "&location=41.4993,-81.69442&radius=32187&type=restaurant&key=AIzaSyBk0uA5zKZMDJmD5WUVLJq2gosxwV5GM_c";
            var client = new WebClient();
            var content = client.DownloadString(url);
            var serializer = new JavaScriptSerializer();
            var jsonContent = serializer.Deserialize<Object>(content);
            return jsonContent;
        }

        public Object getDetailsFromPlaceID(string placeIdString)
        {
            string url = "https://maps.googleapis.com/maps/api/place/details/json?placeid="+placeIdString+"&key=AIzaSyBk0uA5zKZMDJmD5WUVLJq2gosxwV5GM_c";
            var client = new WebClient();
            var content = client.DownloadString(url);
            var serializer = new JavaScriptSerializer();
            var jsonContent = serializer.Deserialize<Object>(content);
            return jsonContent;
        }
        
    }
}