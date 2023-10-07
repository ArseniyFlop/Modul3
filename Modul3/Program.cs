using System;
using System.Drawing;

class Program
{
    // Делегат для методов сортировки
    delegate void SortingMethod(int[] array);
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите задачу:");
            Console.WriteLine("1. Площадь фигур");
            Console.WriteLine("2. Класс \"Уведомление\" с событиями для отправки уведомлений");
            Console.WriteLine("3. Приложение для управления задачами с использованием делегатов");
            Console.WriteLine("4. Система фильтрации данных с использованием делегатов");
            Console.WriteLine("5. Приложение для сортировки числовых данных");
            Console.WriteLine("0. Выйти из программы");

            int choice0 = Convert.ToInt32(Console.ReadLine());

            switch (choice0)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Вы выбрали задачу 1.");
                    while (true)
                    {
                        Console.WriteLine("Выберите фигуру для вычисления площади:");
                        Console.WriteLine("1. Круг");
                        Console.WriteLine("2. Прямоугольник");
                        Console.WriteLine("3. Треугольник");
                        Console.WriteLine("0. Выйти");

                        int choice = Convert.ToInt32(Console.ReadLine());

                        if (choice == 0)
                            break;

                        double area = 0.0;

                        switch (choice)
                        {
                            case 1:
                                Console.Write("Введите радиус круга: ");
                                double radius = Convert.ToDouble(Console.ReadLine());
                                Circle circle = new Circle(radius);
                                area = circle.CalculateArea();
                                break;
                            case 2:
                                Console.Write("Введите ширину прямоугольника: ");
                                double width = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Введите высоту прямоугольника: ");
                                double height = Convert.ToDouble(Console.ReadLine());
                                Rectangle rectangle = new Rectangle(width, height);
                                area = rectangle.CalculateArea();
                                break;
                            case 3:
                                Console.Write("Введите длину основания треугольника: ");
                                double baseLength = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Введите высоту треугольника: ");
                                double triangleHeight = Convert.ToDouble(Console.ReadLine());
                                Triangle triangle = new Triangle(baseLength, triangleHeight);
                                area = triangle.CalculateArea();
                                break;
                            case 0:
                                Console.WriteLine("Программа завершена.");
                                return;
                            default:
                                Console.WriteLine("Некорректный выбор.");
                                continue;
                        }
                        Console.WriteLine($"Площадь выбранной фигуры: {area}");
                    }
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Вы выбрали задачу 2.");
                    Notification notifier = new Notification();

                    // Регистрация обработчиков событий
                    notifier.MessageSent += (sender, e) =>
                    {
                        Console.WriteLine($"Сообщение отправлено на {e.Recipient}: {e.Message}");
                    };

                    notifier.CallMade += (sender, e) =>
                    {
                        Console.WriteLine($"Звонок совершен на {e.Recipient}");
                    };

                    notifier.EmailSent += (sender, e) =>
                    {
                        Console.WriteLine($"Отправлено электронное письмо на {e.Recipient}, Тема: {e.Subject}, Текст: {e.Body}");
                    };

                    while (true)
                    {
                        Console.WriteLine("Выберите тип уведомления:");
                        Console.WriteLine("1. Отправить сообщение");
                        Console.WriteLine("2. Совершить звонок");
                        Console.WriteLine("3. Отправить электронное письмо");
                        Console.WriteLine("0. Выйти");

                        int choice1 = Convert.ToInt32(Console.ReadLine());

                        if (choice1 == 0)
                            break;

                        switch (choice1)
                        {
                            case 1:
                                Console.Write("Введите получателя сообщения: ");
                                string messageRecipient = Console.ReadLine();
                                Console.Write("Введите текст сообщения: ");
                                string messageText = Console.ReadLine();
                                notifier.SendTextMessage(messageRecipient, messageText);
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 2:
                                Console.Write("Введите получателя звонка: ");
                                string callRecipient = Console.ReadLine();
                                notifier.MakeCall(callRecipient);
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 3:
                                Console.Write("Введите получателя электронного письма: ");
                                string emailRecipient = Console.ReadLine();
                                Console.Write("Введите тему электронного письма: ");
                                string emailSubject = Console.ReadLine();
                                Console.Write("Введите текст электронного письма: ");
                                string emailBody = Console.ReadLine();
                                notifier.SendEmail(emailRecipient, emailSubject, emailBody);
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 0:
                                Console.WriteLine("Программа завершена.");
                                return;
                            default:
                                Console.WriteLine("Некорректный выбор.");
                                continue;
                        }
                    }
                    Console.Clear();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Вы выбрали задачу 3.");
                    TaskManager taskManager = new TaskManager();

                    // Регистрация делегатов для выполнения задач
                    taskManager.ExecuteTask += SendNotification;
                    taskManager.ExecuteTask += WriteToLog;

                    while (true)
                    {
                        Console.WriteLine("Выберите действие:");
                        Console.WriteLine("1. Добавить задачу");
                        Console.WriteLine("2. Выполнить задачи");
                        Console.WriteLine("3. Просмотреть задачи");
                        Console.WriteLine("4. Удалить задачу");
                        Console.WriteLine("5. Очистить список задач");
                        Console.WriteLine("0. Выход");

                        int choice2 = Convert.ToInt32(Console.ReadLine());

                        if (choice2 == 0)
                            break;
                        switch (choice2)
                        {
                            case 1:
                                Console.Write("Введите задачу: ");
                                string task = Console.ReadLine();
                                taskManager.AddTask(task);
                                Console.Clear();
                                break;
                            case 2:
                                Console.WriteLine("Выберите способ выполнения задач:");
                                Console.WriteLine("1. Отправить уведомление");
                                Console.WriteLine("2. Записать в журнал");

                                int executionChoice = Convert.ToInt32(Console.ReadLine());

                                // Выбор делегата в зависимости от выбора пользователя
                                TaskDelegate selectedDelegate = null;
                                if (executionChoice == 1)
                                {
                                    selectedDelegate = SendNotification;
                                }
                                else if (executionChoice == 2)
                                {
                                    selectedDelegate = WriteToLog;
                                }

                                // Выполнение задач с выбранным делегатом
                                taskManager.ExecuteTasks(selectedDelegate);
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 3:
                                taskManager.ViewTasks();
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 4:
                                Console.Write("Введите индекс задачи для удаления: ");
                                int indexToRemove = Convert.ToInt32(Console.ReadLine()) - 1;
                                taskManager.RemoveTask(indexToRemove);
                                Console.Clear();
                                break;
                            case 5:
                                taskManager.ClearTasks();
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 0:
                                Console.WriteLine("Программа завершена.");
                                return;
                            default:
                                Console.WriteLine("Некорректный выбор.");
                                break;
                        }
                    }
                    Console.Clear();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Вы выбрали задачу 4.");
                    DataFilteringSystem dataFilteringSystem = new DataFilteringSystem();

                    while (true)
                    {
                        Console.WriteLine("Выберите действие:");
                        Console.WriteLine("1. Добавить даты");
                        Console.WriteLine("2. Сортировать по убыванию");
                        Console.WriteLine("3. Фильтровать и сортировать по ключевому слову");
                        Console.WriteLine("4. Просмотреть данные");
                        Console.WriteLine("0. Выход");

                        int choice3 = Convert.ToInt32(Console.ReadLine());

                        if (choice3 == 0)
                            break;

                        switch (choice3)
                        {
                            case 1:
                                Console.Write("Введите дату для добавления (например, '2023-09-28'): ");
                                string dateToAdd = Console.ReadLine();
                                dataFilteringSystem.AddData(dateToAdd);
                                Console.Clear();
                                break;
                            case 2:
                                dataFilteringSystem.SortByDateDescending();
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 3:
                                Console.Write("Введите ключевое слово для фильтрации: ");
                                string keyword = Console.ReadLine();
                                dataFilteringSystem.FilterAndSortByKeyword(keyword);
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 4:
                                dataFilteringSystem.ViewData();
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 0:
                                Console.WriteLine("Программа завершена.");
                                return;
                            default:
                                Console.WriteLine("Некорректный выбор.");
                                break;
                        }
                    }
                    Console.Clear();
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("Вы выбрали задачу 5.");
                    Console.WriteLine("Введите числа для сортировки через пробел:");
                    string input = Console.ReadLine();
                    string[] inputArray = input.Split(' ');

                    int[] numbers = new int[inputArray.Length];
                    for (int i = 0; i < inputArray.Length; i++)
                    {
                        numbers[i] = int.Parse(inputArray[i]);
                    }

                    Console.WriteLine("Выберите метод сортировки:");
                    Console.WriteLine("1. Сортировка пузырьком");
                    Console.WriteLine("2. Быстрая сортировка");

                    int choice4 = int.Parse(Console.ReadLine());

                    SortingMethod sortingMethod = null;

                    switch (choice4)
                    {
                        case 1:
                            sortingMethod = BubbleSort;
                            break;
                        case 2:
                            sortingMethod = QuickSort;
                            break;
                        default:
                            Console.WriteLine("Некорректный выбор.");
                            return;
                    }

                    sortingMethod(numbers);

                    Console.WriteLine("Отсортированный массив:");
                    foreach (int num in numbers)
                    {
                        Console.Write(num + " ");
                    }
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 0:
                    Console.WriteLine("Программа завершена.");
                    return;
                default:
                    Console.WriteLine("Некорректный выбор. Пожалуйста, выберите задачу из списка.");
                    break;
            }
        }

        // Функция для отправки уведомления
        void SendNotification(string task)
        {
            Console.WriteLine($"Отправка уведомления: {task}");
        }

        // Функция для записи в журнал
        void WriteToLog(string task)
        {
            Console.WriteLine($"Запись в журнал: {task}");
        }
    }
    // Метод сортировки пузырьком
    static void BubbleSort(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }

    // Метод быстрой сортировки
    static void QuickSort(int[] array)
    {
        QuickSort(array, 0, array.Length - 1);
    }

    static void QuickSort(int[] array, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(array, low, high);
            QuickSort(array, low, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, high);
        }
    }

    static int Partition(int[] array, int low, int high)
    {
        int pivot = array[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (array[j] < pivot)
            {
                i++;
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        int temp2 = array[i + 1];
        array[i + 1] = array[high];
        array[high] = temp2;

        return i + 1;
    }
}