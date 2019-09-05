using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppEngine
{
    class Program
    {
        static void Main(string[] args)
        {

            IFileManager File = new FileManager();

           
            var data = File.ReadTextFromFileAsync("Texts", FileTypeExtension.TXT, "C:/Users/Alpha/Desktop", true).Result;
            var list = new List<string>();
            
            foreach(var item in data)
            {
                list.Add(item);   
            }

            list.ForEach(Console.WriteLine);

            for (var i =0; i < 10; i++)
                Console.WriteLine("Hello World!");

            Console.ReadLine();
        }
        


        public static async Task<string> ReadTextFromFileAsync()
        {
            var list = default(string);
            

            await Task.Run(() =>
            {
                using (var streamReader = (TextReader)new StreamReader(File.Open(@"C:/Users/Alpha/Desktop/Texts.txt", FileMode.Open)))
                {
                    while (streamReader.Peek() > -1)
                    {
                        list = streamReader.ReadLine().ToString();
                    }
                }

            });


            return list;



        }

        static async Task<int> GetLeisureHours()
        {
            // Task.FromResult is a placeholder for actual work that returns a string.  
            var today = await Task.FromResult<string>(DateTime.Now.DayOfWeek.ToString());

            // The method then can process the result in some way.  
            int leisureHours;
            if (today.First() == 'S')
                leisureHours = 16;
            else
                leisureHours = 5;

            return leisureHours;
        }

        public static string Read()
        {
            var list = default(string);

            using (var streamReader = (TextReader)new StreamReader(File.Open(@"C:/Users/Alpha/Desktop/Text.txt", FileMode.Open)))
            {
                while (streamReader.Peek() > -1)
                {
                    list = streamReader.ReadLine().ToString();
                }
            }

            return list;
        }
    }
}
