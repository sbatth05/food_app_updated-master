using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
namespace food_app
{
    public class FoodIngredients
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string RecipeName { get; set; }
        public string imageurl { get; set; }
        public string ingredients { get; set; }
        public string Recipeurl { get; set; }
    }
}