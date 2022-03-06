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
using static Android.Views.ViewGroup;
using ClassLibrary2.Classes;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XamarinSignalR.Resources.layout
{
    [Activity(Label = "TableViewActivity")]
    public class TableViewActivity : Activity
    {
        HubConnection hubConnection;
        EditText[][] cells;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.TableView);
            TableLayout _table = FindViewById<TableLayout>(Resource.Id.table);
            Button but1 = FindViewById<Button>(Resource.Id.button1);
            TableRow tableRow = new TableRow(this);

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;


            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://*****/tablehub")
                 .ConfigureLogging(logging => {
                     logging.AddDebug();
                     logging.SetMinimumLevel(LogLevel.Debug);

                 })
                .Build();

            hubConnection.On<Block>("SendBlockToUser", block =>
            {

                tableRow = new TableRow(this);

                TextView textView = new TextView(this);
                textView.Text = "Участники";
                tableRow.AddView(textView);

                foreach (string str in block.jury)
                {
                    textView = new TextView(this);
                    textView.Text = str;
                    textView.SetWidth(170);
                    textView.SetMaxWidth(170);
                    tableRow.AddView(textView);

                    textView = new TextView(this);
                    textView.SetWidth(170);
                    textView.SetMaxWidth(170);
                    tableRow.AddView(textView);

                    textView = new TextView(this);
                    textView.SetWidth(170);
                    textView.SetMaxWidth(170);
                    tableRow.AddView(textView);
                }

                textView = new TextView(this);
                textView.Text = "Всего";
                tableRow.AddView(textView);

                textView = new TextView(this);
                textView.Text = "Место";
                tableRow.AddView(textView);

                _table.AddView(tableRow);


                tableRow = new TableRow(this);
                textView = new TextView(this);
                tableRow.AddView(textView);
                for (int i = 0; i < block.jury.Length; i++)
                {


                    foreach (string str in block.notes)
                    {
                        textView = new TextView(this);
                        textView.Text = str;
                        textView.SetMaxWidth(170);
                        tableRow.AddView(textView);
                    }

                }




                textView = new TextView(this);
                tableRow.AddView(textView);

                textView = new TextView(this);
                tableRow.AddView(textView);


                _table.AddView(tableRow);

                cells = new EditText[block.rows.Length][];

                for (int i = 0; i < block.rows.Length; i++)
                {
                    tableRow = new TableRow(this);

                    textView = new TextView(this);
                    textView.Text = block.rows[i].name;
                    tableRow.AddView(textView);
                    EditText t;

                    List<EditText> editTexts = new List<EditText>();
                    EventHandler<View.FocusChangeEventArgs> eventHandler = new EventHandler<View.FocusChangeEventArgs>(OnFocusChanged);

                    foreach(JuryScore score in block.rows[i].scores)
                    {
                        foreach(float f in score.score)
                        {
                            t = new EditText(this);
                            t.Hint = "0";
                            if (f != 0f)
                                t.Text = f.ToString();
                            t.SetMaxWidth(170);

                            t.FocusChange += eventHandler;
                            editTexts.Add(t);
                            tableRow.AddView(t);
                        }
                    }

                    /*
                    for (int j = 0; j < block.jury.Length; j++)
                    {
                        t = new EditText(this);
                        t.Hint = "0";
                        t.SetMaxWidth(170);

                        t.FocusChange += eventHandler;
                        editTexts.Add(t);
                        tableRow.AddView(t);

                        t = new EditText(this);                                                      
                        t.Hint = "0";                                                                
                        t.SetMaxWidth(170);
                        t.FocusChange += eventHandler;
                        editTexts.Add(t);
                        tableRow.AddView(t);
                        
                        t = new EditText(this);
                        t.Hint = "0";
                        t.SetMaxWidth(170);
                        t.FocusChange += eventHandler;
                        editTexts.Add(t);
                        tableRow.AddView(t);
                    }
                    */

                    t = new EditText(this);
                    t.Text = block.rows[i].finalScore.ToString();
                    t.FocusChange += eventHandler;
                    editTexts.Add(t);
                    tableRow.AddView(t);

                    t = new EditText(this);
                    t.Text = block.rows[i].prize;
                    t.FocusChange += eventHandler;
                    editTexts.Add(t);
                    tableRow.AddView(t);

                    cells[i] = editTexts.ToArray();

                    _table.AddView(tableRow);
                }
            });

            hubConnection.On("ServerIsReady", () =>
            {
                hubConnection.SendAsync("GetBlock", Intent.GetIntExtra("id", 0));
            });

            hubConnection.On<int, int, int, string>("ChangeScore", (blockId, row, column, value) =>
             {
                 Console.WriteLine($"{row} {column} {value}");
                 if (Intent.GetIntExtra("id", 0) == blockId)
                    cells[row][column].Text = value;
                 
             });

            try
            {
                hubConnection.StartAsync();
            }
            catch (System.Exception) { }




            //EditText t = new EditText(this);

            // Create your application here
        }

        Task SendDataToServer(int blockId, int row, int column, string value)
        {
            if (hubConnection.State == HubConnectionState.Connected)
            {
                return hubConnection.SendAsync("ChangeScoreOnServer", blockId, row, column, value);
            }
            return null;
        }

        async void OnFocusChanged(object s, View.FocusChangeEventArgs e)
        {
            if (!e.HasFocus)
            {
                //Отправляем на сервак
                for (int i = 0; i < cells.Length; i++)
                {
                    for (int j = 0; j < cells[i].Length; j++)
                    {
                        if (cells[i][j] == s)
                        {
                            string str;
                            if (((EditText)s).Text == "")
                                str = "0";
                            else
                                str = ((EditText)s).Text;
                            await SendDataToServer(Intent.GetIntExtra("id", 0), i, j, str);
                        }
                    }
                }

            }
        }
    }
}