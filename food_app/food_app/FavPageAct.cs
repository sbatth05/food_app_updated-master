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

using Android.Content.PM;
using SQLite;
using System.IO;


namespace food_app
{
    // Set page orentation and no icon \\
    [Activity(Label = "FavPageAct", Theme = "@android:style/Theme.DeviceDefault.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait)]

    public class FavPageAct:Activity
    {
        string path = Path.Combine(Xamarin.Essentials.FileSystem.ToString(), "Food.db");
       // string path;
        ImageView imgbtnFavspg;
        ListView recipelist;

        TableQuery<FoodIngredients> table;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CutomList);

            imgbtnFavspg = FindViewById<ImageView>(Resource.Id.imgbtnFavspg);

            recipelist = FindViewById<ListView>(Resource.Id.lstFood);

            // setup connection to database \\
            var db = new SQLiteConnection(path);

            db.CreateTable<FoodIngredients>();

            var Recipes123 = db.Table<FoodIngredients>().ToList();

            table = db.Table<FoodIngredients>();

            recipelist.Adapter = new DataAdapter(this, Recipes123);

            recipelist.ItemClick += Recipelist_ItemClick;

            recipelist.ItemLongClick += Recipelist_ItemLongClick;
        }

        private void Recipelist_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            var db = new SQLiteConnection(path);
            var foodtable = table.ToList<FoodIngredients>();
            var RecipeName = foodtable[e.Position];

            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Delete Favourite");
            alert.SetMessage("Do you really want to delete " + RecipeName);
            alert.SetIcon(Resource.Drawable.icon);
            alert.SetButton("OK", (c, ev) =>
            {
                db.Delete(RecipeName);
                recipelist.Adapter = new DataAdapter(this, table.ToList<FoodIngredients>());
            });
            alert.SetButton2("CANCEL", (c, ev) => { });
            alert.Show();
        }

        private void Recipelist_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var db = new SQLiteConnection(path);
            var foodtable = table.ToList<FoodIngredients>();
            var RecipeName = foodtable[e.Position];

            var FoodActivities = new Intent(this, typeof(FavAct));

            FoodActivities.PutExtra("Name", RecipeName.RecipeName);
            FoodActivities.PutExtra("Recipe", RecipeName.Recipeurl);
            FoodActivities.PutExtra("Ingredients", RecipeName.ingredients);
            FoodActivities.PutExtra("Image", RecipeName.imageurl);

            StartActivity(FoodActivities);
        }

        private void ImgbtnFavspg_Click(object sender, EventArgs e)
        {
            Intent NextActivity = new Intent(this, typeof(FavAct));
            StartActivity(NextActivity);
        }
    }
}