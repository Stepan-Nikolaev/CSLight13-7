using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLight13_7
{
    class Program
    {
        static void Main(string[] args)
        {
            Direction direction = new Direction();
            Pasangers pasangers = new Pasangers();
            ConstructorTrain constructor = new ConstructorTrain();
            Train train = new Train();
            Display display = new Display(direction, pasangers, train);

            bool step1 = false;
            bool step2 = false;
            bool step3 = false;
            bool step4 = false;
            int userInput = -1;

            Console.WriteLine("Добро пожаловать в планировщик поездов. Нажмите любую клавищу, чтобы перейти к созданию плана поезда.");
            Console.ReadKey();

            while (userInput != 0)
            {
                Console.Clear();
                display.DisplayProgress();

                if (!step1)
                {
                    Console.WriteLine("1. Перейти к созданию направления поезда.");
                }

                if (step1 && !step2)
                {
                    Console.WriteLine("2. Начать продажу билетов.");
                }

                if (step2 && !step3)
                {
                    Console.WriteLine("3. Приступить к формированию поезда.");
                }

                if (step3 && !step4)
                {
                    Console.WriteLine("4. Поезд готов к отправлению. Отправить поезд.");
                }

                Console.WriteLine("0. Выход.");

                userInput = Convert.ToInt32(Console.ReadLine());

                switch (userInput)
                {
                    case 1:
                        if (!step1)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите, пожалуйста, откуда будет поезд?");
                            string startingPoint = Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("Введите, пожалуйста, куда направдяется поезд?");
                            string destinationPoint = Console.ReadLine();
                            direction.CreateDirection(startingPoint, destinationPoint);
                            step1 = true;
                        }
                        break;
                    case 2:
                        if (step1 && !step2)
                        {
                            Console.Clear();
                            Console.WriteLine("Количество проданных билетов - " + pasangers.SellingTicket());
                            Console.ReadKey();
                            step2 = true;
                        }
                        break;
                    case 3:
                        if (step2 && !step3)
                        {
                            train.CreateTrain(constructor.CreateTrain(pasangers.countPasangers));
                            step3 = true;
                        }
                        break;
                    case 4:
                        if (step3 && !step4)
                        {
                            Console.Clear();
                            Console.WriteLine("Поезд отправлен. Хотите спланировать новую поездку?");
                            Console.WriteLine("1. Да, хочу!");
                            Console.WriteLine("2. Нет, выход.");
                            string userChoice = Console.ReadLine();

                            if (userChoice == "1")
                            {
                                step1 = false;
                                step2 = false;
                                step3 = false;
                                step4 = false;
                                direction = new Direction();
                                pasangers = new Pasangers();
                                constructor = new ConstructorTrain();
                                train = new Train();
                                display = new Display(direction, pasangers, train);
                            }
                            else if(userChoice == "2")
                            {
                                userInput = 0;
                            }
                            else
                            {
                                Console.WriteLine("Неверная команда");
                            }
                        }
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Всего доброго.");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Неверная команда.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }

    class Display
    {
        private Direction _direction;
        private Pasangers _pasangers;
        private Train _train;
        public Display(Direction direction, Pasangers pasangers, Train train)
        {
            _direction = direction;
            _pasangers = pasangers;
            _train = train;
        }

        public void DisplayProgress()
        {
            if (_direction.StartingPoint != null && _direction.DestinationPoint != null)
            {
                Console.WriteLine($"Направление: {_direction.StartingPoint} - {_direction.DestinationPoint}");
                Console.WriteLine();
            }

            if (_pasangers.countPasangers != 0)
            {
                Console.WriteLine("Количество проданных билетов - " + _pasangers.countPasangers);
                Console.WriteLine();
            }

            if (_train.CountPlaceOfTrain != 0)
            {
                Console.WriteLine("Вагонов Люкс в поезде - " + _train.CountLuxWagons);
                Console.WriteLine("Вагонов Бизнес в поезде - " + _train.CountBusinessWagons);
                Console.WriteLine("Вагонов Эконом в поезде - " + _train.CountEconomWagons);
                Console.WriteLine("Всего мест в поезде - " + _train.CountPlaceOfTrain);
                Console.WriteLine();
            }
        }
    }

    class Direction
    {
        public string StartingPoint { get; private set; }
        public string DestinationPoint { get; private set; }

        public void CreateDirection(string startingPoint, string destinationPoint)
        {
            StartingPoint = startingPoint;
            DestinationPoint = destinationPoint;
        }
    }

    class Pasangers
    {
        private Random random = new Random();
        public int countPasangers { get; private set; }

        public int SellingTicket()
        {
            countPasangers = random.Next(1, 500);

            return countPasangers;
        }
    }

    class Train
    {
        private List<Wagons> _train = new List<Wagons>();
        public int CountLuxWagons { get; private set; }
        public int CountBusinessWagons { get; private set; }
        public int CountEconomWagons { get; private set; }
        public int CountPlaceOfTrain { get; private set; }

        public void CreateTrain(List<Wagons> train)
        {
            _train = train;

            foreach (Wagons i in train)
            {
                if (i._typeWagon == "Люкс")
                    CountLuxWagons += 1;


                if (i._typeWagon == "Бизнес")
                    CountBusinessWagons += 1;

                if (i._typeWagon == "Эконом")
                    CountEconomWagons += 1;

                CountPlaceOfTrain += i._capacity;
            }
        }
    }

    class ConstructorTrain
    {
        private List<Wagons> train = new List<Wagons>();
        private Random random = new Random();

        public List<Wagons> CreateTrain(int countPasengers)
        {
            int countPlaces = 0;
            bool reductorTrain = true;

            while (reductorTrain)
            {
                Console.Clear();

                Console.WriteLine("Колчиество проданных билетов - " + countPasengers);
                Console.WriteLine("Количество мест в поезде " + countPlaces);

                if (countPlaces < countPasengers)
                    Console.WriteLine("Необходимо добавить - " + (countPasengers - countPlaces));
                else if (countPlaces > countPasengers)
                    Console.WriteLine("Количество свободных мест - " + (countPlaces - countPasengers));
                Console.WriteLine();

                Console.WriteLine("1. Добавить вагон Люкс.");
                Console.WriteLine("2. Убрать вагон Люкс");
                Console.WriteLine("3. Добавить вагон Бизнес");
                Console.WriteLine("4. Убрать вагон Бизнес");
                Console.WriteLine("5. Добавить вагон Эконом");
                Console.WriteLine("6. Убрать вагон Эконом");
                Console.WriteLine("7. Выйти");

                if (countPasengers <= countPlaces)
                    Console.WriteLine("0. Закончить формирование поезда");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        Lux lux = new Lux();
                        train.Add(lux);
                        countPlaces += lux._capacity;
                        break;
                    case "2":
                        for (int i = train.Count - 1; i >= 0; i--)
                        {
                            if (train[i]._typeWagon == "Люкс")
                            {
                                countPlaces -= train[i]._capacity;
                                train.Remove(train[i]);
                                break;
                            }
                        }
                        break;
                    case "3":
                        Business business = new Business();
                        train.Add(business);
                        countPlaces += business._capacity;
                        break;
                    case "4":
                        for (int i = train.Count - 1; i >= 0; i--)
                        {
                            if (train[i]._typeWagon == "Бизнес")
                            {
                                countPlaces -= train[i]._capacity;
                                train.Remove(train[i]);
                                break;
                            }
                        }
                        break;
                    case "5":
                        Econom econom = new Econom();
                        train.Add(econom);
                        countPlaces += econom._capacity;
                        break;
                    case "6":
                        for (int i = train.Count - 1; i >= 0; i--)
                        {
                            if (train[i]._typeWagon == "Эконом")
                            {
                                countPlaces -= train[i]._capacity;
                                train.Remove(train[i]);
                                break;
                            }
                        }
                        break;
                    case "7":
                        reductorTrain = false;
                        train = null;
                        break;
                    case "0":
                        reductorTrain = false;
                        break;
                }
            }

            return train;
        }
    }

    class Wagons
    {
        public string _typeWagon { get; protected set; }
        public int _capacity { get; protected set; }
    }

    class Lux : Wagons
    {
        public Lux()
        {
            _typeWagon = "Люкс";
            _capacity = 16;
        }
    }

    class Business : Wagons
    {
        public Business()
        {
            _typeWagon = "Бизнес";
            _capacity = 36;
        }
    }

    class Econom : Wagons
    {
        public Econom()
        {
            _typeWagon = "Эконом";
            _capacity = 54;
        }
    }
}
