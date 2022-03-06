using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Microsoft.AspNetCore.SignalR.Client;
using XamarinSignalR.Resources.layout;
using static Android.Views.View;


namespace XamarinSignalR
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]

    public class MainActivity : AppCompatActivity
    {

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            // Next code for connect button

            EditText Password = FindViewById<EditText>(Resource.Id.editText1);
            TextView Proverka = FindViewById<TextView>(Resource.Id.textView2);
            Button PasswordButton = FindViewById<Button>(Resource.Id.button1);
            PasswordButton.Click += (sender, e) =>
            {
                if (Password.Text == "jury")
                {
                    Proverka.Visibility = Android.Views.ViewStates.Invisible;

                    var intent = new Intent(this, typeof(UserActivity));
                    this.StartActivity(intent);
                }
                else if (Password.Text == "admin1675")
                {
                    Proverka.Visibility = Android.Views.ViewStates.Invisible;
                    var intent = new Intent(this, typeof(Activity1));
                    this.StartActivity(intent);
                }
                else
                {
                    Proverka.Visibility = Android.Views.ViewStates.Visible;
                }
            };





            /*
            hubConnection = new HubConnectionBuilder().WithUrl("https://signalrcoreserver20210630235847.azurewebsites.net/movehub").Build();

            hubConnection.On<string[]>("ReceiveNewPosition", str =>
            {
                view_move.SetX(x);
                view_move.SetY(y);
            });

            btn_start.Click += async delegate
            {
                if (btn_start.Text.ToLower().Equals("start"))
                {
                    if (hubConnection.State == HubConnectionState.Disconnected)
                    {
                        try
                        {
                            await hubConnection.StartAsync();
                            btn_start.Text = "stop";
                        }
                        catch (System.Exception) { }
                    }
                }
                else if (btn_start.Text.ToLower().Equals("stop"))
                {
                    if (hubConnection.State == HubConnectionState.Connected)
                    {
                        try
                        {
                            await hubConnection.StopAsync();
                            btn_start.Text = "start";
                        }
                        catch (System.Exception) { }
                    }
                }
            };

            view_move.SetOnTouchListener(this);
            */
        }
    }

}