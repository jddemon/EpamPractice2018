using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Rewards.Manager.LogicContracts;
using Rewards.Manager.NewConfig;
using Rewards.Manager.Entities;

namespace Rewards.Manager.ConsoleUI
{
    class Program
    {
        // сервера зависимости
        private static IRewardLogic rewardLogic;
        private static IPersonLogic personLogic;
        private static IMedalLogic medalLogic;

        // статический конструктор класса program
        static Program()
        {
            // запуск ninject
            IKernel ninjectKernel = new StandardKernel();
            // регистрация серверов
            Config.RegisterServices(ninjectKernel);
            // создание связанных элементов
            rewardLogic = ninjectKernel.Get<IRewardLogic>();
            personLogic = ninjectKernel.Get<IPersonLogic>();
            medalLogic = ninjectKernel.Get<IMedalLogic>();
        }

        static string[] menutext = { "1.Список людей\n"
                                    +"2.Добавить человека в список\n"
                                    +"3.Удалить человека из списка\n"
                                    +"4.Внести изменения по человеку\n"
                                    +"5.Список наград у человека\n"
                                    +"6.Добавить награду человеку\n"
                                    +"7.Список всех видов наград\n"
                                    +"8.Новый вид награды\n"
                                    +"9.Удалить вид награды\n"
                                    +"0.Выход\n"
        };

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine(menutext[0]);
                    ConsoleKeyInfo entry = Console.ReadKey(intercept: true);
                    switch (entry.Key)
                    {
                        case ConsoleKey.D1:
                            ListPerson();
                            break;

                        case ConsoleKey.D2:
                            AddPerson();
                            break;
                           
                        case ConsoleKey.D3:
                            DeletePerson();
                            break;
                        case ConsoleKey.D4:
                            UpdatePerson();
                            break;
                            
                        case ConsoleKey.D5:
                            ListPersonReward();
                            break;
                           
                        case ConsoleKey.D6:
                            RewardPerson();
                            break;
                            
                        case ConsoleKey.D7:
                            ListMedal();
                            break;
                        case ConsoleKey.D8:
                            AddMedal();
                            break;
                            
                        case ConsoleKey.D9:
                            DeleteMedal();
                            break;
                            
                        case ConsoleKey.D0:
                            return;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                    Console.ReadLine();
                }


            }
        }
        // функция меню вывода списка людей
        static void ListPerson() {
            // получаем от сервера список людей
            Person[] persons = personLogic.GetAll();
            // вывод списка по персонам
            foreach (var person in persons)
            {
                Console.WriteLine($"{person.INN}. {person.FirstName} {person.LastName}");
            }
            Prompt("Нажмите Enter");
        }

        // функция выводит текст и ожидает ввода строки 
        static string Prompt(string text)
        {
            Console.WriteLine(text);
            string rez = Console.ReadLine();
            return rez;
        }
        // функция меню добавления человека
        static void AddPerson()
        {
            Person person = new Person();
            person.INN = decimal.Parse(Prompt("ИНН ?"));
            person.LastName = Prompt("Фамилия ?");
            person.FirstName = Prompt("Имя ?");
            person.MiddleName = Prompt("Отчество ?");
            person.City = Prompt("Город ?");
            person.Street = Prompt("Улица ?");
            person.House = Prompt("Дом ?");
            personLogic.Save(person);
        }
        // функция меню удаление из списка
        static void DeletePerson()
        {
            Person[] persons = personLogic.GetAll();

            foreach (var person in persons)
            {
                Console.WriteLine($"{person.INN}. {person.FirstName} {person.LastName}");
            }
            decimal INN = decimal.Parse(Prompt("Введите ИНН удаляемого:"));
            foreach (var person in persons)
            {
                if(person.INN == INN)
                {
                    personLogic.Delete(person.Id);
                    Prompt(INN.ToString() + " удален.Нажмите Enter");
                    return;
                }
            }
            Prompt(INN.ToString() + " не найден.Нажмите Enter");
        }
        //  функция меню обновления человека 
        static void UpdatePerson()
        {
            Person[] persons = personLogic.GetAll();

            foreach (var person in persons)
            {
                Console.WriteLine($"{person.INN}. {person.FirstName} {person.LastName}");
            }
            decimal INN = decimal.Parse(Prompt("Введите ИНН человека для изменений:"));
            foreach (var person in persons)
            {
                if (person.INN == INN)
                {
                    Console.WriteLine($"{person.INN}. {person.FirstName} {person.LastName} {person.MiddleName} {person.City} {person.Street} {person.House}");
                    person.LastName = Prompt("Фамилия ?");
                    person.FirstName = Prompt("Имя ?");
                    person.MiddleName = Prompt("Отчество ?");
                    person.City = Prompt("Город ?");
                    person.Street = Prompt("Улица ?");
                    person.House = Prompt("Дом ?");

                    personLogic.Update(person);
                    Prompt(INN.ToString() + " изменен.Нажмите Enter");
                    return;
                }
            }
            Prompt(INN.ToString() + " не найден.Нажмите Enter");
        }
        //функция меню список наград у человека
        static void ListPersonReward()
        {
            Person[] persons = personLogic.GetAll();

            foreach (var person in persons)
            {
                Console.WriteLine($"{person.INN}. {person.FirstName} {person.LastName}");
            }
            decimal INN = decimal.Parse(Prompt("Введите ИНН для просмотра списка наград:"));
            foreach (var person in persons)
            {
                if (person.INN == INN)
                {

                    Reward[] rewards=rewardLogic.GetPersonAll(person.Id);
                    foreach (var reward in rewards)
                    {
                        Console.WriteLine($"{person.FirstName} {person.LastName} {reward.Medal} ");
                    }
                    Prompt("Нажмите Enter");
                    return;
                }
            }
            Prompt(INN.ToString() + " не найден.Нажмите Enter");

        }
        //функция меню добавление награды человеку
        static void RewardPerson()
        {
            Person[] persons = personLogic.GetAll();

            foreach (var person in persons)
            {
                Console.WriteLine($"{person.INN}. {person.FirstName} {person.LastName}");
            }
            decimal INN = decimal.Parse(Prompt("Введите ИНН для выбора лица:"));
            Medal[] medals = medalLogic.GetAll();
            foreach (var medal in medals)
            {
                Console.WriteLine($"{medal.Id}. {medal.Name} {medal.Material}");
            }
            int id = int.Parse(Prompt("Введите номер медали:"));
            foreach (var person in persons)
            {
                if (person.INN == INN)
                {
                    foreach (var medal in medals)
                    {
                        if (id == medal.Id)
                        {
                            Reward reward = new Reward();
                            reward.FirstName = person.FirstName;
                            reward.LastName = person.LastName;
                            reward.INN = person.INN;
                            rewardLogic.Save(reward);
                            Prompt("Нажмите Enter");
                            return;
                        }
                    }
                    Prompt("Не надена медаль.Нажмите Enter");
                    return;
                }
            }
            Prompt("Не наден ИНН .Нажмите Enter");
        }
        //функция меню список медалей
        static void ListMedal()
        {
            Medal[] medals = medalLogic.GetAll();
            foreach (var medal in medals)
            {
                Console.WriteLine($"{medal.Id}. {medal.Name} {medal.Material}");
            }
            Prompt("Нажмите Enter");
        }
        // функция меню добавление медали
        static void AddMedal()
        {
            Medal medal = new Medal();
            medal.Name = Prompt("Название медали ?");
            medal.Material = Prompt("Из какого материала ?");
            medalLogic.Save(medal);
            Prompt("Нажмите Enter");
        }
        // функция меню удаление медали
        static void DeleteMedal()
        {
            Medal[] medals = medalLogic.GetAll();
            foreach (var medal in medals)
            {
                Console.WriteLine($"{medal.Id}. {medal.Name} {medal.Material}");
            }
            int id = int.Parse(Prompt("Введите номер медали:"));
            foreach (var medal in medals)
            {
                if (id == medal.Id)
                {
                    medalLogic.Delete(id);
                    Prompt("Медаль "+medal.Name+"удфлена. Нажмите Enter");
                    return;
                }
            }
            Prompt("Не надена медаль.Нажмите Enter");
        }
    }
}
