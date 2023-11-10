using PassOfficeLibrary;

Console.WriteLine("БЮРО ПРОПУСКОВ");

List<PassItem> initialData = new()
{
    new PassItem { PassNumber = 1, Name = "Данилов М. А.", PassType = 3, IssueDate = DateTime.Parse("03.11.2023") },
    new PassItem { PassNumber = 2, Name = "Данилов Н. Н.", PassType = 2, IssueDate = DateTime.Parse("04.11.2023") },
    new PassItem { PassNumber = 3, Name = "Данилов А. А.", PassType = 1, IssueDate = DateTime.Parse("05.11.2023") }
};

PassOffice passOffice = new(initialData);

do
{
    Console.WriteLine("Введите действие: добавить / удалить / просмотр / поиск");
    string? input = Console.ReadLine();

    switch (input)
    {
        case "добавить":
            passOffice.Add();
            break;
        case "удалить":
            passOffice.Delete();
            break;
        case "просмотр":
            passOffice.View();
            break;
        case "поиск":
            passOffice.Search();
            break;
        default:
            Console.WriteLine("неизвестная команда");
            break;
    }
    Console.WriteLine();
} while (true);