using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;

namespace destructive_magic_programm
{
    class Program
    {
        delegate void Action();
        static void Main(string[] args)
        {
            Action magic = StartSound;
            magic += ChangeWallpaper;
            magic();
        }
        public static void ChangeWallpaper()
        {
            wallpapers wallpapers = new wallpapers();
            int count = 0;
            while (count <= 2000)
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
            Audios audios = new Audios();
            try
            {
                string path_to_audio = audios.path_files[0];
                SoundPlayer sp;
                sp = new SoundPlayer(@$"{path_to_audio}");
                sp.Play();
            }
            catch (Exception)
            {
                Console.WriteLine("В папке 'audios' нету файлов вообще или нету файлов wav");
            }
        }
        
    }
    class Audios
    {
        static string path_audios = GetPathAudio();
        static string GetPathAudio()
        {
            string[] DirsInCurrentDirectory = Directory.GetDirectories(
                AppDomain.CurrentDomain.BaseDirectory);
            string path_audios = null;

            foreach (string item in DirsInCurrentDirectory)
            {
                if (item.EndsWith("audios"))
                {
                    path_audios = item;
                }
            }
            return path_audios;
        }

        string[] files = Directory.GetFiles(path_audios);
        public int Lenght
        {
            get
            {
                return path_files.Count;
            }
        }

        public List<string> path_files = new List<string>();
        public Audios()
        {
            Add_Files_in_list();
        }
        void Add_Files_in_list()
        {
            foreach (string file_name in files)
            {
                if (file_name.EndsWith(".wav"))
                {
                    path_files.Add(file_name);
                }
                else
                {
                    System.IO.File.Delete($@"{file_name}");
                }
            }
        }
        public IEnumerator GetEnumerator()
        {
            return path_files.GetEnumerator();
        }
    }
    class wallpapers
    {
        public static string path_photo = GetPathPhotos();
        string[] files = Directory.GetFiles(path_photo);
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

        
        public static string GetPathPhotos()
        {
            string[] dirs = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory);
            string path_photos = null;
            foreach (string item in dirs)
            {
                if (item.EndsWith("photos"))
                {
                    path_photos = item;
                }
            }
            return path_photos;
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
