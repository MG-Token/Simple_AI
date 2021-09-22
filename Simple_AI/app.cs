
using Ini;

using System;
using System.Drawing;
using System.Threading;

public static class app
{
    public static string databasedir = AppDomain.CurrentDomain.BaseDirectory + "Database.AI";
    public static string pubkey = "AI";
    public static Random rnd = new Random();
    public static bool ist = true;
    public static string botname=null;
    public static string name = null;

    public static void istype(string n= null)
    {
        new Thread(() => 
        {
            string p="";
            while (ist)
            {
                for (int ii = 0; ii < 4; ii++)
                {
                    if (n != null)
                        Console.Title = n+" Is Typing" + p;
                    else
                        Console.Title =  "Is Typing" + p;

                    p = p + ".";
                    Thread.Sleep(250);

                }
                p = "";
            }
            if (n != null)
                Console.Title = n;
            else
                Console.Title = "";

        }) { IsBackground = true }.Start();
    }
    public static void write(string inp,string botname= "AI",int speed = 100)
    {
        char[] o = inp.ToCharArray();
        ist = true;
        istype(botname);
        foreach (var i in o)
        {
            Console.Write(i);
            Thread.Sleep(speed);

        }
        ist = false;

        Console.Write("\n");

    }
    public static void ini_write(string dir, string section, string key, string value)
    {
        section = Crypto.Encrypt(section);
        key = Crypto.Encrypt(key);
        value = Crypto.Encrypt(value);

        IniFile ini = new IniFile(dir);
        ini.IniWriteValue(section, key, value);
    }
    public static string ini_read(string dir, string section, string key)
    {
        section = Crypto.Encrypt(section);
        key = Crypto.Encrypt(key);
        IniFile ini = new IniFile(dir);
        var y = ini.IniReadValue(section, key);
        return Crypto.Decrypt(y); 
    }
}

