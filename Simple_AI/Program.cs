using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Simple_AI
{
    class Program
    {
        static void Main(string[] args)
        {
            app.write("hi im Simple AI (Artificial intelligence)");
            app.write("I can talk with you!");
            app.write("in the first. choose name for me :)");
            app.write("Enter a name :");
            app.botname = Console.ReadLine();
            if (app.botname == "")
                app.botname = "AI";
            app.write("ok what is your name?", app.botname);
            app.name = Console.ReadLine();
            if (app.name == "")
                app.name = Environment.UserName;
            app.write($"Thanks for installing me! {app.name}\nHow are you?", app.botname);
            while (true)
            {
                try
                {

                    string inp = Console.ReadLine();
                    inp = inp.Replace(app.botname, "0x0");
                    inp = inp.Replace(app.name, "0x1");
                    if (inp == "0x0")
                    {
                        app.write($"bot name is {app.botname}\nWhat do you need now ?", app.botname);
                        continue;

                    }
                    if (inp == "0x1")
                    {
                        app.write($"your name is {app.name}\nWhat do you need now ?", app.botname);
                        continue;

                    }
                    if (inp == "0x3")
                    {
                        app.write($"Enter your question Mr {app.name} :", app.botname);
                        var inp1 = Console.ReadLine();

                        app.write($"Enter your answer Mr {app.name}:", app.botname);
                        var inp2 = Console.ReadLine();

                        if (app.ini_read(app.databasedir, inp1, "count") == null)
                        {
                            app.ini_write(app.databasedir, inp1, "count", "1");
                            app.ini_write(app.databasedir, inp1, "1", inp2);

                        }
                        else
                        {
                            int u = int.Parse(app.ini_read(app.databasedir, inp1, "count"));
                            u++;
                            app.ini_write(app.databasedir, inp1, "count", u.ToString());
                            app.ini_write(app.databasedir, inp1, u.ToString(), inp2);
                        }
                        app.write($"Mr {app.name} all you need was saved or updated!\nWhat do you need now?", app.botname);
                        continue;
                    }
                    if (inp == "0x2")
                    {
                        app.write($"Mr {app.name} are you sure to clear the Console?", app.botname);
                        var key = Console.ReadKey();

                        if (key.Key == ConsoleKey.Y)
                        {
                            Console.Clear();
                            app.write("What do you need now ?", app.botname);
                        }
                        else
                            app.write("\nWhat do you need now ?", app.botname);
                        continue;

                    }
                    if (inp == "help" || inp == "Help")
                    {
                        app.write($@"Mr {app.name} Welcome to my World i maded by Metal Ghost (GitHub.com/MG-Token)
Enter any word in this list to run AI Parameter:
0x0 is bot name
0x1 is user name
0x2 is clear the console
0x3 is add or update a question or answer
0x4 is to updateing database to the lastet version
1x0 to 14 is set the answer color (only one line)", app.botname, 90);
                        //2xX is set the type speed (example 2x100 is normal type speed) in the feature
                        app.write("What do you need now ?", app.botname);
                        continue;

                    }
                    if (inp == "0x4")
                    {
                       
                        app.write("Downloading AI database...", app.botname);
                        Task.Run(() =>
                        {
                            WebClient wb = new WebClient();
                            wb.DownloadProgressChanged += Wb_DownloadProgressChanged;
                            wb.DownloadFileCompleted += Wb_DownloadFileCompleted;
                            wb.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/MG-Token/Simple_AI/master/Database.AI"), app.databasedir);

                            void Wb_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
                            {
                                Console.Title = $"{app.botname} is Now Downloading Database.AI {e.ProgressPercentage}%";
                            }
                            void Wb_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
                            {
                                app.write("AI database Downloaded.", app.botname);

                            }
                        }).Wait();

                        continue;
                    }
                    if (app.ini_read(app.databasedir, inp, "count") != null)
                    {
                        settingupouts:
                        string outs = "";
                        if (int.Parse(app.ini_read(app.databasedir, inp, "count")) > 1)
                            outs = app.ini_read(app.databasedir, inp, app.rnd.Next(0, int.Parse(app.ini_read(app.databasedir, inp, "count")) + 1).ToString());
                        else
                            outs = app.ini_read(app.databasedir, inp, "1");
                        if (outs == null)
                            goto settingupouts;
                        int color = -1;
                        for (int i = 0; i < 15; i++)
                        {
                            if (outs.IndexOf("1x" + i.ToString()) > -1)
                            {
                                color = i;
                                outs = outs.Replace("1x" + i.ToString(), "");
                            }
                        }
                        outs = outs.Replace("0x0", app.botname);
                        outs = outs.Replace("0x1", app.name);
                        if (color != -1)
                            Console.ForegroundColor = (ConsoleColor)color;
                        app.write(outs, app.botname);


                        Console.ResetColor();

                    }
                    else
                        app.write("Uhhh... this not found my database try add this with enter help in text", app.botname,30);


                }
                catch (Exception ex)
                {
                    Console.Clear();
                    app.write(ex.ToString(), app.botname,25);
                    app.write($"Mr {app.name} do you want to continue?", app.botname);
                    var key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Y)
                    {
                        Console.Clear();
                        app.write("I return!");
                        continue;

                    }
                    break;
                }

            }


        }


    }
}
