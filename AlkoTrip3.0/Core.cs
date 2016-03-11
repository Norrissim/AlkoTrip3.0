using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AlkoTrip3._0
{
    class Core
    {
        public static List<Component> allComponents = new List<Component>();
        public static List<Coctail> allCoctails = new List<Coctail>();
        public static List<Coctail> posibleCoctails = new List<Coctail>();
        public static List<Component> chooseComponents = new List<Component>();
        public static List<Component> chooseExistedComponents = new List<Component>();
        public static Coctail viewCoctail;
        public const String compPath = "storage/emulated/0/Android/data/Components.txt";
        public const String coctPath = "storage/emulated/0/Android/data/Coctails.txt";
        public static bool viewCoctailNow = false;
        public static bool init = false;
        public static int GlobalIdNumber = 0;

        public static String FirstCharToUpper(String input)
        {
            if (String.IsNullOrEmpty(input))
            { 
                throw new ArgumentException("ARGH!");
            }
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }

        public static void  readComponentsFromFile()
        {
            FileInfo file = new FileInfo(compPath);
            if(!file.Exists)
            {
                file.Create();
            }
            StreamReader reader = new StreamReader(file.FullName);
            while(!reader.EndOfStream)
            {
                String nameComp = reader.ReadLine();
                int degComp = Int32.Parse(reader.ReadLine());
                Component temp = new Component(nameComp, degComp);
                temp.setVolume(0);
                allComponents.Add(temp);
            }
            reader.Close();
        }

        public static void readCoctailsFromFile()
        {
            FileInfo file = new FileInfo(coctPath);
            if (!file.Exists)
            {
                file.Create();
            }
            StreamReader reader = new StreamReader(file.FullName);
            while (!reader.EndOfStream)
            {
                List<Component> list = new List<Component>();
                String nameCoct = reader.ReadLine();
                Android.Net.Uri tmpUri = Android.Net.Uri.Parse(reader.ReadLine());
                String discripCoct = reader.ReadLine();
                String temp;
                while((temp = reader.ReadLine()) != "" && !reader.EndOfStream)
                {
                    String nameComp = temp;
                    int degComp = Int32.Parse(reader.ReadLine());
                    Component tempComp = new Component(nameComp, degComp);
                    tempComp.setVolume(0);
                    list.Add(tempComp);
                }
                Coctail tempCoct = new Coctail(nameCoct, tmpUri, discripCoct, list);
                allCoctails.Add(tempCoct);
            }
            reader.Close();
        }

        public static void writeComponentInFile(Component i)
        {
            FileInfo file = new FileInfo(compPath);
            if (!file.Exists)
            {
                file.Create();
            }

            StreamWriter writer = file.AppendText();
            writer.WriteLine(i.getName());
            writer.WriteLine(i.getDegree());
            writer.Close();
        }

        public static void writeCoctailInFile(Coctail i)
        {
            FileInfo file = new FileInfo(coctPath);
            if (!file.Exists)
            {
                file.Create();
            }

            StreamWriter writer = file.AppendText();
            writer.WriteLine(i.getName());
            writer.WriteLine(i.getImageUri().ToString());
            writer.WriteLine(i.getDescription());
            foreach(Component j in i.getComponents())
            {
                writer.WriteLine(j.getName());
                writer.WriteLine(j.getDegree());
            }
            writer.WriteLine("");
            writer.Close();
        }
    }
}