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
    [Activity(Label = "Создание Участников")]
    public class List_Members_admin : Activity
    {
        HubConnection hubConnection;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.list_Members);



            List<string> catNames = new List<string> { };

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,
                    Android.Resource.Layout.SimpleListItem1, catNames);

            ListView LV_Members = FindViewById<ListView>(Resource.Id.listViewMembers);

            LV_Members.Adapter = adapter;

            LV_Members.ItemClick += (object sender, AdapterView.ItemClickEventArgs args) =>
            {
                Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
            };

            EditText DeleteMembers = FindViewById<EditText>(Resource.Id.DeleteMembers);
            Button btnDeleteMembers = FindViewById<Button>(Resource.Id.btnDeleteMembers);
            EditText FIO_Members = FindViewById<EditText>(Resource.Id.FIO_Members);
            Button btnAddFIO_Members = FindViewById<Button>(Resource.Id.btnAddFIO_Members);
            EditText NumTable1 = FindViewById<EditText>(Resource.Id.NumTable1);


            int n = 0;
            int i = 0;


            btnAddFIO_Members.Click += (sender, e) =>
            {

                if (FIO_Members.Text == "")
                {

                }
                else
                {
                    i++;
                    btnDeleteMembers.Enabled = true;
                    catNames.Add(i+"  "+FIO_Members.Text);
                        ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,
                        Android.Resource.Layout.SimpleListItem1, catNames);


                    LV_Members.Adapter = adapter;

                    

                }
            };

            btnDeleteMembers.Click += (sender, e) =>
            {
                if (int.TryParse(DeleteMembers.Text, out n) && (n<=i))
                {
                    // Здесь нужно удалить строку под номером н и сместить все вниз на одну строку. но я хз как

                    i--;
                    if (i==0)
                    {
                        btnDeleteMembers.Enabled = false;
                    }
                }
            };


            hubConnection = new HubConnectionBuilder().WithUrl("http://*****/movehub").Build();
              try
             {
                 hubConnection.StartAsync();
             }
              catch (System.Exception) { }

            Button btnSend = FindViewById<Button>(Resource.Id.btnSendMembersToServer);

            btnSend.Click += (sender, e) =>
            {
                if (hubConnection.State == HubConnectionState.Connected)
                {
                    hubConnection.SendAsync("RecieveParticipantsOnServer", int.Parse(NumTable1.Text)-1, catNames.ToArray());
                }
            };

        }




    }
}