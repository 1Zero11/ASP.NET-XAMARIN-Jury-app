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
using XamarinSignalR.Resources.layout;

namespace XamarinSignalR.Resources.layout
{
    [Activity(Label = "Создание Информации об оценках")]
    public class List_Info_Notes : Activity
    {
        HubConnection hubConnection;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.List_Notes);



            List<string> catNames = new List<string> { };

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,
                    Android.Resource.Layout.SimpleListItem1, catNames);

            ListView LV_Notes = FindViewById<ListView>(Resource.Id.listViewNote);

            LV_Notes.Adapter = adapter;

            LV_Notes.ItemClick += (object sender, AdapterView.ItemClickEventArgs args) =>
            {
                Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
            };

            
            EditText NoteEdit = FindViewById<EditText>(Resource.Id.NoteEdit);
            Button btnNote = FindViewById<Button>(Resource.Id.btnNote);
            EditText NumTable = FindViewById<EditText>(Resource.Id.NumTable);


            int n = 0;
            int i = 0;


            btnNote.Click += (sender, e) =>
            {

                if (NoteEdit.Text == "")
                {

                }
                else
                {
                    i++;
                    catNames.Add(NoteEdit.Text);
                    ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,
                    Android.Resource.Layout.SimpleListItem1, catNames);


                    LV_Notes.Adapter = adapter;

                    if (i == 3)
                    {
                        btnNote.Enabled = false;
                    }

                }
            };

            


            hubConnection = new HubConnectionBuilder().WithUrl("http://*****/movehub").Build();
            try
            {
                hubConnection.StartAsync();
            }
            catch (System.Exception) { }

            Button btnSend = FindViewById<Button>(Resource.Id.btnSendNote);

            btnSend.Click += (sender, e) =>
            {
                if (hubConnection.State == HubConnectionState.Connected)
                {
                    //Дописать отправку на сервер со всеми вытекающими
                    hubConnection.SendAsync("RecieveNotesOnServer", int.Parse(NumTable.Text) - 1, catNames.ToArray());
                }
            };

        }




    }
}