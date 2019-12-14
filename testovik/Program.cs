using System;
using System.IO;
using System.Threading;

namespace testovik
{
    class Program
    {
        static int kol = 0, kol_kadrs = 0;
        static string[] logins;
        static string[] passwds;
        static byte[] rols;

        static void login()
        {
            string name, passwd = "";
            ConsoleKeyInfo key;
            int count;

            if(kol == 0)
            {
                Console.Clear();
                Console.WriteLine("Нет зарегистрированных учеток.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.Clear();

            Console.WriteLine("Введите логин...");
            name = Console.ReadLine();
            Console.Clear();

            while (true)
            {

                Console.Clear();
                Console.WriteLine("Введите пароль...");

                for (count = 0; count < passwd.Length; count++)
                {
                    Console.Write("*");
                }

                key = Console.ReadKey();

                if ((((int)key.KeyChar) >= 97) && (((int)key.KeyChar) <= 122) || (((int)key.KeyChar) >= 65) && (((int)key.KeyChar) <= 90) || (((int)key.KeyChar) >= 1072) && (((int)key.KeyChar) <= 1103) || (((int)key.KeyChar) >= 1040) && (((int)key.KeyChar) <= 1071) || (((int)key.KeyChar) >= 48) && (((int)key.KeyChar) <= 57))
                {
                    passwd += key.KeyChar;
                }

                if ((key.Key == ConsoleKey.Backspace) && (passwd.Length != 0))
                {
                    passwd = passwd.Remove(passwd.Length - 1);
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }

                Console.Clear();
            }

            Console.Clear();
            //Console.WriteLine("Пароль: " + passwd + " " + "Логин " + name);
            //Console.ReadKey();
            for (count = 0; count < logins.Length; count++)
            {
                if ((name == logins[count]) && (passwd == passwds[count]))
                {
                    Console.WriteLine("Доступ разрешен.");
                    Console.ReadKey();

                    Console.Clear();

                    if (rols[count] == 1)
                    {
                        //Вызов отдела кадров
                    }

                    if (rols[count] == 2)
                    {
                        //Вызов отдела бугалтерии
                    }

                    if (rols[count] == 3)
                    {
                        //Вызов отдела склада
                    }


                    if (rols[count] == 4)
                    {
                        //Вызов отдела кассы
                    }

                    return;
                }
            }

            Console.WriteLine("Нет такой учетной записи.");
            Console.ReadKey();

        }

        static void reg()
        {
            BinaryWriter bw;
            string name = "", passwd = "";
            ConsoleKeyInfo key;
            int count = 0, pos = 0;
            byte rol;
            bool ok = false;
            string[] menu = { "Отдел кадров", "Бухгалтерия", "Склад", "Касса" };

            Console.Clear();

            while (!ok)
            {
                Console.Clear();
                Console.WriteLine("Введите логин...");
                name = Console.ReadLine();
                Console.Clear();

                for (count = 0; count < logins.Length; count++)
                {
                    if (logins[count] != name)
                    {
                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }
                }

                if (logins.Length == 0)
                {
                    ok = true;
                }

                if (ok)
                {
                    Console.WriteLine("Success!");
                }
                else
                {
                    Console.WriteLine("Данный пользователь уже зарегистрирован.");
                    Console.ReadKey();
                }


            }

            while (true)
            {

                Console.Clear();
                Console.WriteLine("Введите пароль...");

                for (count = 0; count < passwd.Length; count++)
                {
                    Console.Write("*");
                }

                key = Console.ReadKey();

                if ((((int)key.KeyChar) >= 97) && (((int)key.KeyChar) <= 122) || (((int)key.KeyChar) >= 65) && (((int)key.KeyChar) <= 90) || (((int)key.KeyChar) >= 1072) && (((int)key.KeyChar) <= 1103) || (((int)key.KeyChar) >= 1040) && (((int)key.KeyChar) <= 1071) || (((int)key.KeyChar) >= 48) && (((int)key.KeyChar) <= 57))
                {
                    passwd += key.KeyChar;
                }

                if ((key.Key == ConsoleKey.Backspace) && (passwd.Length != 0))
                {
                    passwd = passwd.Remove(passwd.Length - 1);
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }

                Console.Clear();
            }

            Console.Clear();
            //Console.WriteLine("Пароль: " + passwd + " " + "Логин " + name);
            //Console.ReadKey();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите роль...");

                for (count = 0; count < menu.Length; count++)
                {
                    if (count == pos)
                    {
                        Console.Write(menu[count] + " <~" + "\n");
                    }
                    else
                    {
                        Console.Write(menu[count] + "\n");
                    }
                }

                key = Console.ReadKey();

                if ((key.Key == ConsoleKey.UpArrow) && (pos != 0))
                {
                    pos--;
                }

                if ((key.Key == ConsoleKey.DownArrow) && (pos != menu.Length - 1))
                {
                    pos++;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    if (pos == 0)
                    {
                        rol = 1;
                        break;
                    }

                    if (pos == 1)
                    {
                        rol = 2;
                        break;
                    }

                    if (pos == 2)
                    {
                        rol = 3;
                        break;
                    }

                    if (pos == 3)
                    {
                        rol = 4;
                        break;
                    }

                }

            }

            bw = new BinaryWriter(new FileStream("C:/Ocherednaya/passwd", FileMode.Append));
            bw.Write(name);
            bw.Write(passwd);
            bw.Write(rol);
            bw.Close();
            Console.Clear();
            loop();



        }


        static void loop()
        {
            BinaryReader br;
            string[] menu = { "Войти.", "Регистрация." };

            ConsoleKeyInfo key;

            int count, pos = 0;

            kol = 0;

            br = new BinaryReader(new FileStream("C:/Ocherednaya/passwd", FileMode.Open));

            while (br.PeekChar() != -1)
            {
                br.ReadString();
                br.ReadString();
                br.ReadByte();
                kol++;
            }

            br.Close();

            logins = new string[kol];
            passwds = new string[kol];
            rols = new byte[kol];

            br = new BinaryReader(new FileStream("C:/Ocherednaya/passwd", FileMode.Open));

            for (count = 0; count < kol; count++)
            {
                logins[count] = br.ReadString();
                passwds[count] = br.ReadString();
                rols[count] = br.ReadByte();
            }

            br.Close();

            while (true)
            {
                for (count = 0; count < menu.Length; count++)
                {
                    if (count == pos)
                    {
                        Console.Write(menu[count] + " <~" + "\n");
                    }
                    else
                    {
                        Console.Write(menu[count] + "\n");
                    }
                }

                key = Console.ReadKey();

                if ((key.Key == ConsoleKey.UpArrow) && (pos != 0))
                {
                    pos--;
                }

                if ((key.Key == ConsoleKey.DownArrow) && (pos != menu.Length - 1))
                {
                    pos++;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    if (pos == 0)
                    {
                        login();
                    }

                    if (pos == 1)
                    {
                        reg();
                    }
                }

                Console.Clear();

            }


        }

        static void Main(string[] args)
        {

            if (!Directory.Exists("C:/Ocherednaya"))
            {
                Directory.CreateDirectory("C:/Ocherednaya");
            }

            if (!File.Exists("C:/Ocherednaya/passwd"))
            {
                File.Create("C:/Ocherednaya/passwd").Close();
            }

            if (!File.Exists("C:/Ocherednaya/kadrs"))
            {
                File.Create("C:/Ocherednaya/kadrs").Close();
            }

            if (!File.Exists("C:/Ocherednaya/bugalteria"))
            {
                File.Create("C:/Ocherednaya/bugalteria").Close();
            }

            if (!File.Exists("C:/Ocherednaya/sklad"))
            {
                File.Create("C:/Ocherednaya/sklad").Close();
            }

            if (!File.Exists("C:/Ocherednaya/kassa"))
            {
                File.Create("C:/Ocherednaya/kassa").Close();
            }

            loop();
        }
    }
}
