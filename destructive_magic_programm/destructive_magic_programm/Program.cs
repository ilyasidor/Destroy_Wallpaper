
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using NAudio.Wave;

namespace destructive_magic_programm
{
    class Program
    {
        static void Main(string[] args)
        {
            StartSoundWithImages();     
        }

        public static void StartSoundWithImages()
        {
            wallpapers wallpapers = new wallpapers();
            StartSound();
            int count = 0;
            while (count<=2000)
            {
                foreach (string file in wallpapers)
                {
                    Wallpaper.SetWallpaper(file);
                    Thread.Sleep(500);
                    count++;
                }
            }
        }
        public static void StartSound()
        {
            SoundPlayer sp;
            sp = new SoundPlayer(@"C:\Users\Ilya\source\repos\destructive_magic_programm\destructive_magic_programm\audios\SUPER_SUS_-_Pubertat_64680041.wav");
            sp.Play();
        }
        
    }
    class wallpapers
    {
        string[] files = Directory.GetFiles("C:\\Users\\Ilya\\source\\repos\\destructive_magic_programm\\destructive_magic_programm\\photos");
        public List<string> path_files = new List<string>(); 
        public wallpapers()
        {
            Add_Files_in_list();
        }
        public int Lenght
        {
            get
            {
                return path_files.Count;
            }
        }
        public void Add_Files_in_list()
        {
            
            foreach (string file_name in files)
            {
                path_files.Add(file_name);
            }
        }
        public IEnumerator GetEnumerator()
        {
            return path_files.GetEnumerator();
        }
    }
    class Wallpaper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(
        UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;

        public static void SetWallpaper(String path)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path,
           SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
    }
}
