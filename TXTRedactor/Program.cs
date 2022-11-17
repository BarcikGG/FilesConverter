using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TXTRedactor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path_new;
            string format;
            string[] lines;
            string path_take;
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            List<open_text> listOrd = new();

            Console.WriteLine("Текстовый редактор | TXTRedactor");
            Console.WriteLine("--------------------------------");

            Console.WriteLine("Перетащите файл или введите путь до него: ");
            path_take = Console.ReadLine();
            if (!File.Exists(path_take))
            {
                Console.Clear();
                Main(args);
            }
            string format_old = path_take.Split('.').Last();
            Console.Clear();

            Console.WriteLine("Текстовый редактор (F1 для сохранения)| TXTRedactor");
            Console.WriteLine("---------------------------------------------------");
            if (format_old == "txt") //txt
            {
                lines = File.ReadAllLines(path_take);
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }
                for (int i = 0; i < lines.Length; i += 3)
                {
                    open_text ord = new open_text();
                    ord.date = lines[i];
                    ord.text = lines[i + 1];
                    ord.amount = lines[i + 2];
                    listOrd.Add(ord);
                }
            }
            else if (format_old == "xml") //xml
            {
                List<open_text> i;
                XmlSerializer xml_to_string = new XmlSerializer(typeof(List<open_text>));
                using (FileStream fs = new FileStream(path_take, FileMode.Open))
                {
                    i = (List<open_text>)xml_to_string.Deserialize(fs);
                }
                foreach (var elem in i)
                {
                    Console.WriteLine(elem.date);
                    Console.WriteLine(elem.text);
                    Console.WriteLine(elem.amount);
                    listOrd.Add(elem);
                }
            }
            else //json
            {
                List<open_text> result = JsonConvert.DeserializeObject<List<open_text>>(File.ReadAllText(path_take));
                foreach (var el in result)
                {
                    Console.WriteLine(el.date);
                    Console.WriteLine(el.text);
                    Console.WriteLine(el.amount);
                    listOrd.Add(el);
                }
            }

            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.F1:
                    Console.Clear();
                    Console.WriteLine("Сохранить на рабочий стол?\n1 - Да\n2 - Другой путь");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Введите название (пример: newfile.json):");
                        path_new = desktop + "\\" + Console.ReadLine();
                    }
                    else if (choice == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Введите путь до нового файла вместе с названием: ");
                        path_new = Console.ReadLine();
                    }
                    else
                    {
                        Random rnd = new Random();
                        int name = rnd.Next(10000);
                        path_new = desktop + "\\" + name + ".json";
                        Console.WriteLine($"Вы написали цифру не указанную выше." +
                            $"\nФайл будет сохранен на рабочий стол в формате json\n{name}.json");
                    }
                    format = path_new.Split('.').Last();
                    saver.save(format, path_new, listOrd);

                    Console.WriteLine("Файл успешно сохранен!\nОбновляюсь...");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Main(args);
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    Console.WriteLine("Завершаю работа...");
                    Thread.Sleep(1500);
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("на другие кнопки биндов нет\nделаю рестарт");
                    Thread.Sleep(3000);
                    Console.Clear();
                    Main(args);
                    break;
            }

        }

    }
}