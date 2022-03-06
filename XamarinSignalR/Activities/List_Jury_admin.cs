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
    [Activity(Label = "Создание Жюри")]
    public class List_Jury_admin : Activity
    {

        HubConnection hubConnection;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.List_Jury);


            List<string> catNames = new List<string> { };

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,
                    Android.Resource.Layout.SimpleListItem1, catNames);

            ListView LV_Jury = FindViewById<ListView>(Resource.Id.listViewJury);

            LV_Jury.Adapter = adapter;

            LV_Jury.ItemClick += (object sender, AdapterView.ItemClickEventArgs args) =>
            {
                Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
            };

            EditText CountOfJury = FindViewById<EditText>(Resource.Id.CountOfJury);
            Button btnAddCountOfJury = FindViewById<Button>(Resource.Id.btnAddCountOfJury);
            EditText FIO_Jury = FindViewById<EditText>(Resource.Id.FIO_Jury);
            Button btnAddFIO_Jury = FindViewById<Button>(Resource.Id.btnAddFIO_Jury);
            EditText NumTable = FindViewById<EditText>(Resource.Id.NumTable);

            
            int n = 0;
            int i = 0;
            btnAddCountOfJury.Click += (sender, e) =>
            {
                if (int.TryParse(CountOfJury.Text, out n))
                {
                    btnAddFIO_Jury.Enabled = true;
                    i = 0;
                    //Нужно ввести очистку лист вью на фамилии при этой строчке.
                }
                else
                {
                    btnAddFIO_Jury.Enabled = false;
                }
            };


            btnAddFIO_Jury.Click += (sender, e) =>
            {

                if (FIO_Jury.Text == "")
                {
                    
                }
                else
                {
                   
                    if (i == n)
                    {
                        btnAddFIO_Jury.Enabled = false;
                    }
                    else
                    {
                        if (i+1 == n)
                        {
                            btnAddFIO_Jury.Enabled = false;
                        }
                        catNames.Add(FIO_Jury.Text);
                        ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,
                            Android.Resource.Layout.SimpleListItem1, catNames);


                        LV_Jury.Adapter = adapter;
                        i++;
                    }

                }
            };




            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://*****/movehub")
                .Build();

            try
            {
                hubConnection.StartAsync();
            }
            catch (System.Exception) { }

            Button btnSend = FindViewById<Button>(Resource.Id.btnSendJuryToServer);

            btnSend.Click += (sender, e) =>
            {
                if (hubConnection.State == HubConnectionState.Connected)
                {
                    hubConnection.SendAsync("RecieveJuryOnServer", int.Parse(NumTable.Text)-1, catNames.ToArray());
                }
            };

        }


    }



    }
