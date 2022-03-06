using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinSignalR.Resources.layout
{
    [Activity(Label = "Activity1")]
    public class Activity1 : Activity
    {
        HubConnection hubConnection;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Bloki_admin);

            List<string> catNames = new List<string> {};
            
            ListView listView = FindViewById<ListView>(Resource.Id.listViewAdmin);

            // используем адаптер данных
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,
                    Android.Resource.Layout.SimpleListItem1, catNames);


            listView.Adapter = adapter;


            // Потом можно использовать для реализации интерфейса жюри (Пометки для Саши)
            listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs args) =>
            {
                Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
            };



            Button addBlockButton = FindViewById<Button>(Resource.Id.btnAddBlock);
            EditText name = FindViewById<EditText>(Resource.Id.editTextNameOfBlock);

            addBlockButton.Click += (sender, e) =>
            {
                catNames.Add(name.Text);
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,
                    Android.Resource.Layout.SimpleListItem1, catNames);


                listView.Adapter = adapter;
            };

            Button btnSend = FindViewById<Button>(Resource.Id.btnSendBlocksToServer);

            btnSend.Click += (sender, e) =>
            {
                if (hubConnection.State == HubConnectionState.Connected)
                {
                    hubConnection.SendAsync("RecieveDataOnServer", catNames.ToArray());
                }
            };

            Button btn_Blocks_Jury = FindViewById<Button>(Resource.Id.btnAddBlocks_Jury);
            btn_Blocks_Jury.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(List_Jury_admin));
                this.StartActivity(intent);
            };
            Button btn_Blocks_Members = FindViewById<Button>(Resource.Id.btnAddBlocks_Members);
            btn_Blocks_Members.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(List_Members_admin));
                this.StartActivity(intent);
            };

            Button btn_Blocks_Notes = FindViewById<Button>(Resource.Id.btnAddInfo_Notes);
            btn_Blocks_Notes.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(List_Info_Notes));
                this.StartActivity(intent);
            };


            Button btn_From_File = FindViewById<Button>(Resource.Id.btnFromFile);
            btn_From_File.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(From_File));
                this.StartActivity(intent);
            };


            hubConnection = new HubConnectionBuilder().WithUrl("http://*****/movehub").Build();

            try
            {
                hubConnection.StartAsync();
            }
            catch (System.Exception) { }

            
            
        }
    }
}