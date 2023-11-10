using System.Text.RegularExpressions;

namespace PassOfficeLibrary
{
    public class PassItem
    {
        public uint PassNumber;
        public string Name = "";
        public DateTime IssueDate;
        public byte PassType;
    }

    public class PassOffice
    {
        internal readonly string[] _passTypeNames = { "обычный", "срочный", "транзит" };
        
        public List<PassItem> PassList { get; set; }

        public PassOffice(List<PassItem> initialData)
        {
            PassList = initialData;
        }
    }

    public static class PassOfficeExtensions
    {
        public static void Add(this PassOffice value)
        {
            Console.Write("Введите ФИО: ");
            string newName = Console.ReadLine() ?? "";

            Console.Write("Введите тип пропуска (1 - обычный, 2 - срочный, 3 - транзитный): ");
            string newPassType = Console.ReadLine() ?? "";
            Regex r = new Regex("[1|2|3]");
            if (!r.IsMatch(newPassType))
            {
                Console.WriteLine("недопустимый тип пропуска");
                return;
            }

            uint highestId = value.PassList.Any() ? value.PassList.Max(x => x.PassNumber) : 0;
            value.PassList.Add(new PassItem
            {
                PassNumber = highestId + 1, 
                Name = newName, 
                PassType = Convert.ToByte(newPassType), 
                IssueDate = DateTime.Now 
            });
            Console.WriteLine("пропуск добавлен");
        }

        public static void Delete(this PassOffice value)
        {
            try
            {
                Console.Write("Введите номер: ");
                string? findNumber = Console.ReadLine();
                int index = value.PassList.FindIndex(x => x.PassNumber == Convert.ToInt32(findNumber));
                value.PassList.RemoveAt(index);
                Console.WriteLine("пропуск удалён");
            }
            catch
            {
                Console.WriteLine("пропуск не найден");
            }

        }

        public static void View(this PassOffice value)
        {
            foreach (PassItem c in value.PassList)
            {
                Console.WriteLine(
                    "{0,2}\t{1,10}\t{2,10}\t{3,10}",
                    c.PassNumber,
                    c.Name,
                    value._passTypeNames[c.PassType - 1],
                    c.IssueDate.ToString("dd.MM.yyyy")
                );
            }
        }

        public static void Search(this PassOffice value)
        {
            Console.Write("Поиск с даты (дд.мм.гггг): ");
            string startOfRange = Console.ReadLine() ?? "";
            Console.Write("Поиск до даты (дд.мм.гггг): ");
            string endOfRange = Console.ReadLine() ?? "";

            var selectedItems = value.PassList.Where(p => 
                p.IssueDate >= DateTime.Parse(startOfRange) && p.IssueDate <= DateTime.Parse(endOfRange)
            );

            foreach (PassItem c in selectedItems)
            {
                Console.WriteLine(
                    "{0,2}\t{1,10}\t{2,10}\t{3,10}",
                    c.PassNumber,
                    c.Name,
                    value._passTypeNames[c.PassType - 1],
                    c.IssueDate.ToString("dd.MM.yyyy")
                );
            }
        }
    }
}