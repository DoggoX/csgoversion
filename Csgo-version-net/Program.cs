using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace Csgo_version_net
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.Title = "CS:GO";

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("Ez a rendszer nem támogatott.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Verzió átírása...");
            RegistryKey steamrk = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam", false);
            if (steamrk == null)
            {
                Console.WriteLine("Valami hiba lépett fel.");
                Console.ReadKey();
                return;
            }
            string steamPath = (string)steamrk.GetValue("SteamPath");
            if (steamPath == null)
            {
                Console.WriteLine("Valami hiba lépett fel.");
                Console.ReadKey();
                return;
            }
            string csFolder = steamPath + "/steamapps/common/Counter-Strike Global Offensive/csgo";
            string steamInf = csFolder + "/steam.inf";
            Console.WriteLine("# " + steamInf);
            Console.WriteLine("");
            try
            {
                var allLines = File.ReadAllLines(steamInf);
                Console.WriteLine("(1) " + allLines[0] + " --> " + "ClientVersion=2000258");
                Console.WriteLine("(2) " + allLines[1] + " --> " + "ServerVersion=2000258");
                allLines[0] = "ClientVersion=2000258";
                allLines[1] = "ServerVersion=2000258";
                File.WriteAllLines(steamInf, allLines);
                Console.WriteLine("\nVerzió átírva.\n\n -> Kérlek válaszd ki a \"Play Legacy Version of CS:GO\"-t!");
                Process.Start("steam://launch/730/dialog");
                Thread.Sleep(850);
                MessageBox.Show("Kérlek válaszd ki a CS:GO-t és inditsd el!", "cshungary.fun");
                Thread.Sleep(200);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Valami hiba lépett fel. ");
                Console.Write(" -> " + ex.Message);
                Console.ReadKey();
            }
        }

    }
}
