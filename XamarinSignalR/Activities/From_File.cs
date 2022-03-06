using Android.App;
using Android.OS;
using Android.Widget;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XamarinSignalR.Resources.layout
{
    [Activity(Label = "From_File")]
    public class From_File : Activity
    {
        HubConnection hubConnection;
        List<string> catNames = new List<string> { };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.From_File);
            _ = PickAndShow(options: null);


            hubConnection = new HubConnectionBuilder().WithUrl("http://*****/movehub").Build();
            

            /*
            ListView TV_Content = FindViewById<ListView>(Resource.Id.textViewContent);
            */
            EditText Num_Table = FindViewById<EditText>(Resource.Id.NumTable);
            Button btn_Send_File = FindViewById<Button>(Resource.Id.btnSendFileToServer);
            

                btn_Send_File.Click += (sender, e) =>
            {
                if (hubConnection.State == HubConnectionState.Connected)
                {
                    hubConnection.SendAsync("RecieveParticipantsOnServer", int.Parse(Num_Table.Text) - 1, catNames.ToArray());
                }
            };
            //Здесь мог быть наш вывод из файла.

            try
            {
                hubConnection.StartAsync();
            }
            catch (System.Exception) { }

        }

        async Task<FileResult> PickAndShow(PickOptions options)
        {

            try
            {
                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {

                    TextView TV_Content = FindViewById<TextView>(Resource.Id.textViewContent);
                    FileStream fs = new FileStream(result.FullPath, FileMode.Open); //создаем файловый поток
                    StreamReader reader = new StreamReader(fs, System.Text.Encoding.UTF8); // создаем «потоковый читатель» и связываем его с файловым потоком
                    //TV_Content.Text = reader.ReadToEnd(); //считываем все данные с потока и выводим на экран
                    // или построчно ниже

                    var txtall = "";
                    while (reader.EndOfStream != true)
                    {
                        txtall += reader.ReadLine() + "\n\r"; // построчно
                        catNames.Add(reader.ReadLine());
                    }
                   //Надо как то вывести то, что мы читаем из файла, но оно не  видит ничего(
                    TV_Content.Text = txtall;
                    reader.Close(); //закрываем поток
                }
                return result;
            }
            catch (Exception)
            {
                // The user canceled or something went wrong
            }

            return null;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var file = await FilePicker.PickAsync();
                if (file == null)
                {
                    return;
                }
            }
            catch (Exception) { }

        }

       


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


    }
}