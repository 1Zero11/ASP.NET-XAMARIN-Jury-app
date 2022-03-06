using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinSignalR.Resources.layout
{
    [Activity(Label = "UserActivity")]
    public class UserActivity : Activity
    {
        HubConnection hubConnection;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.UserButtonView);

            List<string> catNames = new List<string> { };

            ListView listView = FindViewById<ListView>(Resource.Id.listViewUser);

            // используем адаптер данных
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,
                    Android.Resource.Layout.SimpleListItem1, catNames);


            listView.Adapter = adapter;

            listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs args) =>
            {
                var intent = new Intent(this, typeof(TableViewActivity));
                intent.PutExtra("id", args.Position);
                this.StartActivity(intent);

                /*
                if (hubConnection.State == HubConnectionState.Connected)
                {
                    hubConnection.SendAsync("GetInfoAboutBlock", args.Position);
                }
                */
            };

            hubConnection = new HubConnectionBuilder().WithUrl("http://*****/movehub").Build();

            hubConnection.On<string[]>("SendDataToUser", str =>
            {
                catNames = new List<string>(str);

                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,
                    Android.Resource.Layout.SimpleListItem1, catNames);


                listView.Adapter = adapter;
            });

            hubConnection.On<string>("SendBlockInfoToUser", str =>
            {
                Toast.MakeText(Application, str, ToastLength.Long).Show();
                var intent = new Intent(this, typeof(Activity1));
                this.StartActivity(intent);
            });

            

            try
            {
                hubConnection.StartAsync();
            }
            catch (System.Exception) { }


        }
    }
}