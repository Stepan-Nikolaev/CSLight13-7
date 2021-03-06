﻿using System;
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
            RailwayStation railwayStation = new RailwayStation();
            Train train = new Train();

            bool createDiractionCompleted = false;
            bool sellingTicketCompleted = false;
            bool createTrainCompleted = false;
            bool sendTrainCompleted = false;
            int userInput = -1;

            Console.WriteLine("Добро пожаловать в планировщик поездов. Нажмите любую клавищу, чтобы перейти к созданию плана поезда.");
            Console.ReadKey();

            while (userInput != 0)
            {
                Console.Clear();
                railwayStation.DisplayInformationTable(direction, train);

                if (!createDiractionCompleted)
                {
                    Console.WriteLine("1. Перейти к созданию направления поезда.");
                }

                if (createDiractionCompleted && !sellingTicketCompleted)
                {
                    Console.WriteLine("2. Начать продажу билетов.");
                }

                if (sellingTicketCompleted && !createTrainCompleted)
                {
                    Console.WriteLine("3. Приступить к формированию поезда.");
                }

                if (createTrainCompleted && !sendTrainCompleted)
                {
                    Console.WriteLine("4. Поезд готов к отправлению. Отправить поезд.");
                }

                Console.WriteLine("0. Выход.");

                userInput = Convert.ToInt32(Console.ReadLine());

                switch (userInput)
                {
                    case 1:
                        if (!createDiractionCompleted)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите, пожалуйста, откуда будет поезд?");
                            string startingPoint = Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("Введите, пожалуйста, куда направдяется поезд?");
                            string destinationPoint = Console.ReadLine();
                            direction.CreateDirection(startingPoint, destinationPoint);
                            createDiractionCompleted = true;
                        }
                        break;
                    case 2:
                        if (createDiractionCompleted && !sellingTicketCompleted)
                        {
                            Console.Clear();
                            Console.WriteLine("Количество проданных билетов - " + railwayStation.SellTicket());
                            Console.ReadKey();
                            sellingTicketCompleted = true;
                        }
                        break;
                    case 3:
                        if (sellingTicketCompleted && !createTrainCompleted)
                        {
                            train.CreateTrain(railwayStation.CreateTrain(railwayStation.CountPasangers));
                            createTrainCompleted = true;
                        }
                        break;
                    case 4:
                        if (createTrainCompleted && !sendTrainCompleted)
                        {
                            Console.Clear();
                            Console.WriteLine("Поезд отправлен. Хотите спланировать новую поездку?");
                            Console.WriteLine("1. Да, хочу!");
                            Console.WriteLine("2. Нет, выход.");
                            string userChoice = Console.ReadLine();

                            if (userChoice == "1")
                            {
                                createDiractionCompleted = false;
                                sellingTicketCompleted = false;
                                createTrainCompleted = false;
                                sendTrainCompleted = false;
                                direction = new Direction();
                                railwayStation = new RailwayStation();
                                train = new Train();
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

    class RailwayStation
    {
        private List<Wagons> _wagons = new List<Wagons>();
        private Random _random = new Random();
        public int CountPasangers { get; private set; }

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
                        _wagons.Add(lux);
                        countPlaces += lux._capacity;
                        break;
                    case "2":
                        for (int i = _wagons.Count - 1; i >= 0; i--)
                        {
                            if (_wagons[i]._typeWagon == "Люкс")
                            {
                                countPlaces -= _wagons[i]._capacity;
                                _wagons.Remove(_wagons[i]);
                                break;
                            }
                        }
                        break;
                    case "3":
                        Business business = new Business();
                        _wagons.Add(business);
                        countPlaces += business._capacity;
                        break;
                    case "4":
                        for (int i = _wagons.Count - 1; i >= 0; i--)
                        {
                            if (_wagons[i]._typeWagon == "Бизнес")
                            {
                                countPlaces -= _wagons[i]._capacity;
                                _wagons.Remove(_wagons[i]);
                                break;
                            }
                        }
                        break;
                    case "5":
                        Econom econom = new Econom();
                        _wagons.Add(econom);
                        countPlaces += econom._capacity;
                        break;
                    case "6":
                        for (int i = _wagons.Count - 1; i >= 0; i--)
                        {
                            if (_wagons[i]._typeWagon == "Эконом")
                            {
                                countPlaces -= _wagons[i]._capacity;
                                _wagons.Remove(_wagons[i]);
                                break;
                            }
                        }
                        break;
                    case "7":
                        reductorTrain = false;
                        _wagons = null;
                        break;
                    case "0":
                        reductorTrain = false;
                        break;
                }
            }

            return _wagons;
        }

        public int SellTicket()
        {
            CountPasangers = _random.Next(1, 500);

            return CountPasangers;
        }

        public void DisplayInformationTable(Direction direction, Train train)
        {
            if (direction.StartingPoint != null && direction.DestinationPoint != null)
            {
                Console.WriteLine($"Направление: {direction.StartingPoint} - {direction.DestinationPoint}");
                Console.WriteLine();
            }

            if (CountPasangers != 0)
            {
                Console.WriteLine("Количество проданных билетов - " + CountPasangers);
                Console.WriteLine();
            }

            if (train.CountPlaceOfTrain != 0)
            {
                Console.WriteLine("Вагонов Люкс в поезде - " + train.CountLuxWagons);
                Console.WriteLine("Вагонов Бизнес в поезде - " + train.CountBusinessWagons);
                Console.WriteLine("Вагонов Эконом в поезде - " + train.CountEconomWagons);
                Console.WriteLine("Всего мест в поезде - " + train.CountPlaceOfTrain);
                Console.WriteLine();
            }
        }
    }

    class Train
    {
        private List<Wagons> _wagons = new List<Wagons>();
        public int CountLuxWagons { get; private set; }
        public int CountBusinessWagons { get; private set; }
        public int CountEconomWagons { get; private set; }
        public int CountPlaceOfTrain { get; private set; }

        public void CreateTrain(List<Wagons> wagons)
        {
            _wagons = wagons;

            foreach (Wagons wagon in wagons)
            {
                if (wagon._typeWagon == "Люкс")
                    CountLuxWagons += 1;


                if (wagon._typeWagon == "Бизнес")
                    CountBusinessWagons += 1;

                if (wagon._typeWagon == "Эконом")
                    CountEconomWagons += 1;

                CountPlaceOfTrain += wagon._capacity;
            }
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
