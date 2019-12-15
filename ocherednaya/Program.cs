using System;
using System.IO;
using System.Threading;

namespace ocherednaya
{
    class Program
    {
        static int kol = 0, kol_kadrs = 0, kol_bugalteria = 0, kol_sklad = 0, kol_kassa = 0;
        static string[] logins;
        static string[] passwds;
        static byte[] rols;
        static string[,] kadrs_data;
        static string[,] bugalteria_data;
        static string[,] sklad_data;
        static string[,] kassa_data;

        static void login()
        {
            string name, passwd = "";
            ConsoleKeyInfo key;
            int count;
            /*
            if(kol == 0)
            {
                Console.Clear();
                Console.WriteLine("Нет зарегистрированных учеток.");
                Console.ReadKey();
                Console.Clear();
                loop();
            }
            */

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
                        kadrs();
                    }

                    if (rols[count] == 2)
                    {
                        bugalteria();
                    }

                    if (rols[count] == 3)
                    {
                        sklad();
                    }

                    if (rols[count] == 4)
                    {
                        kassa();
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

        static void kadrs()
        {
            string[] menu = { "Добавить данные сотрудника.", "Просмотр данных сотрудника.", "Выход." };
            int count, count2, pos = 0;
            ConsoleKeyInfo key;

            BinaryReader br = new BinaryReader(new FileStream("C:/Ocherednaya/kadrs", FileMode.Open));

            kol_kadrs = 0;

            while (br.PeekChar() != -1)
            {
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                kol_kadrs++;
            }

            br.Close();

            if (kol_kadrs != 0)
            {
                kadrs_data = new string[kol_kadrs, 14];

                br = new BinaryReader(new FileStream("C:/Ocherednaya/kadrs", FileMode.Open));

                for (count = 0; count < kol_kadrs; count++)
                {
                    for (count2 = 0; count2 < 14; count2++)
                    {
                        if (count2 == 0)
                        {
                            kadrs_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 1)
                        {
                            kadrs_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 2)
                        {
                            kadrs_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 3)
                        {
                            kadrs_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 4)
                        {
                            kadrs_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 5)
                        {
                            kadrs_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 6)
                        {
                            kadrs_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 7)
                        {
                            kadrs_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 8)
                        {
                            kadrs_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 9)
                        {
                            kadrs_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 10)
                        {
                            kadrs_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 11)
                        {
                            kadrs_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 12)
                        {
                            kadrs_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 13)
                        {
                            kadrs_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }
                    }
                }

                br.Close();

                //Console.ReadKey();
            }

            Console.Clear();

            /*for(count = 0; count < kol_kadrs; count++)
            {
                for(count2 = 0; count2 < 14; count2++)
                {
                    Console.WriteLine(kadrs_data[count, count2]);
                }

                Console.WriteLine();
            }

            //Console.WriteLine(kadrs_data.Length);
            //Console.WriteLine(kadrs_data[0,0].Length);

            //Console.ReadKey();
            */

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
                        //login();
                        Console.Clear();
                        kadrs_add();
                    }

                    if (pos == 1)
                    {
                        Console.Clear();
                        kadrs_show();
                        //reg();
                    }

                    if (pos == 2)
                    {
                        return;
                    }
                }

                Console.Clear();

            }


        }

        static void kadrs_add()
        {
            bool ok = false;
            BinaryWriter bw = new BinaryWriter(new FileStream("C:/Ocherednaya/kadrs", FileMode.Append));
            string name, familiya, otchestvo, number_dogovora, day_rojdeniya, mesac_rojdeniya, year_rojdeniya, pasport_seriya, pasport_number, snils, inn, doljnost, oklad, obrazovanie;
            int rez, count, count2;

            Console.WriteLine("Введите имя...");
            name = Console.ReadLine();
            Console.WriteLine("Введите фамилию...");
            familiya = Console.ReadLine();
            Console.WriteLine("Введите отчество...");
            otchestvo = Console.ReadLine();

            Console.WriteLine("Введите номер договора...");
            number_dogovora = Console.ReadLine();
            while (!int.TryParse(number_dogovora, out rez))
            {
                Console.WriteLine("Введен некорректный номер договора.");
                number_dogovora = Console.ReadLine();
            }

            Console.WriteLine("Введите день рождения...");
            day_rojdeniya = Console.ReadLine();
            while ((!int.TryParse(day_rojdeniya, out rez)) || (rez < 1) || (rez > 31))
            {
                Console.WriteLine("Введен некорректный день рождения.");
                day_rojdeniya = Console.ReadLine();
            }

            Console.WriteLine("Введите месяц рождения...");
            mesac_rojdeniya = Console.ReadLine();
            while ((!int.TryParse(mesac_rojdeniya, out rez)) || (rez < 1) || (rez > 12))
            {
                Console.WriteLine("Введен некорректный месяц.");
                mesac_rojdeniya = Console.ReadLine();
            }

            Console.WriteLine("Введите год рождения...");
            year_rojdeniya = Console.ReadLine();
            while (!int.TryParse(year_rojdeniya, out rez))
            {
                Console.WriteLine("Введен некорректный год.");
                year_rojdeniya = Console.ReadLine();
            }

            while (true)
            {

                Console.WriteLine("Введите серию паспорта...");
                pasport_seriya = Console.ReadLine();
                while ((!int.TryParse(pasport_seriya, out rez)) || (pasport_seriya.Length != 4))
                {
                    Console.WriteLine("Введена некорректная серия паспорта.");
                    pasport_seriya = Console.ReadLine();
                }


                for (count = 0; count < kol_kadrs; count++)
                {
                    if (pasport_seriya == kadrs_data[count, 7])
                    {
                        Console.WriteLine("Данная серия паспорта уже используется.");
                        Console.ReadKey();
                        ok = false;
                        break;
                    }
                    else
                    {
                        ok = true;
                    }
                }

                if (kol_kadrs == 0)
                {
                    ok = true;
                }

                if (ok)
                {
                    break;
                }

            }

            ok = false;

            while (true)
            {

                Console.WriteLine("Введите номер паспорта...");
                pasport_number = Console.ReadLine();
                while ((!int.TryParse(pasport_number, out rez)) || (pasport_number.Length != 6))
                {
                    Console.WriteLine("Введена некорректный номер паспорта.");
                    pasport_number = Console.ReadLine();
                }

                for (count = 0; count < kol_kadrs; count++)
                {
                    if (pasport_number == kadrs_data[count, 8])
                    {
                        Console.WriteLine("Данный номер паспорта уже используется.");
                        Console.ReadKey();
                        ok = false;
                        break;
                    }
                    else
                    {
                        ok = true;
                    }
                }

                if (kol_kadrs == 0)
                {
                    ok = true;
                }

                if (ok)
                {
                    break;
                }

            }

            ok = false;

            while (true)
            {

                Console.WriteLine("Введите снилс...");
                snils = Console.ReadLine();
                while ((!long.TryParse(snils, out long rezl)) || (snils.Length != 11))
                {
                    Console.WriteLine("Введен некорректный снилс. " + snils.Length);
                    snils = Console.ReadLine();
                }

                for (count = 0; count < kol_kadrs; count++)
                {
                    if (snils == kadrs_data[count, 9])
                    {
                        Console.WriteLine("Данный снилс уже используется.");
                        Console.ReadKey();
                        ok = false;
                        break;
                    }
                    else
                    {
                        ok = true;
                    }
                }

                if (kol_kadrs == 0)
                {
                    ok = true;
                }

                if (ok)
                {
                    break;
                }

            }

            ok = false;

            while (true)
            {

                Console.WriteLine("Введите инн...");
                inn = Console.ReadLine();
                while ((!long.TryParse(inn, out long rezl)) || (inn.Length != 12))
                {
                    Console.WriteLine("Введена некорректный инн.");
                    inn = Console.ReadLine();
                }

                for (count = 0; count < kol_kadrs; count++)
                {
                    if (inn == kadrs_data[count, 10])
                    {
                        Console.WriteLine("Данный инн уже используется.");
                        Console.ReadKey();
                        ok = false;
                        break;
                    }
                    else
                    {
                        ok = true;
                    }
                }

                if (kol_kadrs == 0)
                {
                    ok = true;
                }

                if (ok)
                {
                    break;
                }

            }

            Console.WriteLine("Введите первую должность...");
            doljnost = Console.ReadLine() + ";";

            Console.WriteLine("Введите вторую должность...");
            doljnost += Console.ReadLine() + ";";

            Console.WriteLine("Введите оклад...");
            oklad = Console.ReadLine();
            while (!int.TryParse(oklad, out rez))
            {
                Console.WriteLine("Введена некорректный оклад.");
                oklad = Console.ReadLine();
            }

            Console.WriteLine("Введите образование...");
            obrazovanie = Console.ReadLine();

            bw.Write(name);
            bw.Write(familiya);
            bw.Write(otchestvo);
            bw.Write(number_dogovora);
            bw.Write(day_rojdeniya);
            bw.Write(mesac_rojdeniya);
            bw.Write(year_rojdeniya);
            bw.Write(pasport_seriya);
            bw.Write(pasport_number);
            bw.Write(snils);
            bw.Write(inn);
            bw.Write(doljnost);
            bw.Write(oklad);
            bw.Write(obrazovanie);

            bw.Close();

            kol_kadrs = 0;
            BinaryReader br = new BinaryReader(new FileStream("C:/Ocherednaya/kadrs", FileMode.Open));

            while (br.PeekChar() != -1)
            {
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                kol_kadrs++;
            }

            br.Close();

            kadrs_data = new string[kol_kadrs, 14];

            br = new BinaryReader(new FileStream("C:/Ocherednaya/kadrs", FileMode.Open));

            for (count = 0; count < kol_kadrs; count++)
            {
                for (count2 = 0; count2 < 14; count2++)
                {
                    if (count2 == 0)
                    {
                        kadrs_data[count, count2] = br.ReadString();
                        //Console.WriteLine(br.ReadString());
                    }

                    if (count2 == 1)
                    {
                        kadrs_data[count, count2] = br.ReadString();
                        //Console.WriteLine(br.ReadString());
                    }

                    if (count2 == 2)
                    {
                        kadrs_data[count, count2] = br.ReadString();
                        //Console.WriteLine(br.ReadString());
                    }

                    if (count2 == 3)
                    {
                        kadrs_data[count, count2] = br.ReadString();
                        //Console.WriteLine(br.ReadString());
                    }

                    if (count2 == 4)
                    {
                        kadrs_data[count, count2] = br.ReadString();
                        //Console.WriteLine(br.ReadString());
                    }

                    if (count2 == 5)
                    {
                        kadrs_data[count, count2] = br.ReadString();
                        //Console.WriteLine(br.ReadString());
                    }

                    if (count2 == 6)
                    {
                        kadrs_data[count, count2] = br.ReadString();
                        //Console.WriteLine(br.ReadString());
                    }

                    if (count2 == 7)
                    {
                        kadrs_data[count, count2] = br.ReadString();
                        //Console.WriteLine(br.ReadString());
                    }

                    if (count2 == 8)
                    {
                        kadrs_data[count, count2] = br.ReadString();
                        //Console.WriteLine(br.ReadString());
                    }

                    if (count2 == 9)
                    {
                        kadrs_data[count, count2] = br.ReadString();
                        //Console.WriteLine(br.ReadString());
                    }

                    if (count2 == 10)
                    {
                        kadrs_data[count, count2] = br.ReadString();
                        //Console.WriteLine(br.ReadString());
                    }

                    if (count2 == 11)
                    {
                        kadrs_data[count, count2] = br.ReadString();
                        //Console.WriteLine(br.ReadString());
                    }

                    if (count2 == 12)
                    {
                        kadrs_data[count, count2] = br.ReadString();
                        //Console.WriteLine(br.ReadString());
                    }

                    if (count2 == 13)
                    {
                        kadrs_data[count, count2] = br.ReadString();
                        //Console.WriteLine(br.ReadString());
                    }
                }
            }

            br.Close();

            return;

        }

        static void kadrs_show()
        {
            int count, count2;

            if (kol_kadrs == 0)
            {
                Console.Clear();
                Console.WriteLine("Нет данных о работниках.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            for (count = 0; count < kol_kadrs; count++)
            {
                for (count2 = 0; count2 < 14; count2++)
                {
                    Console.WriteLine(kadrs_data[count, count2]);
                }

                Console.WriteLine();

            }

            Console.ReadKey();
            Console.Clear();
            return;

        }

        static void bugalteria()
        {
            string[] menu = { "Добавить операцию.", "Просмотр операций.", "Выход." };
            int count, count2, pos = 0;
            ConsoleKeyInfo key;

            BinaryReader br = new BinaryReader(new FileStream("C:/Ocherednaya/bugalteria", FileMode.Open));

            kol_bugalteria = 0;

            while (br.PeekChar() != -1)
            {
                br.ReadString();
                br.ReadString();
                br.ReadString();
                kol_bugalteria++;
            }

            br.Close();

            if (kol_bugalteria != 0)
            {
                bugalteria_data = new string[kol_bugalteria, 3];

                br = new BinaryReader(new FileStream("C:/Ocherednaya/bugalteria", FileMode.Open));

                for (count = 0; count < kol_bugalteria; count++)
                {
                    for (count2 = 0; count2 < 3; count2++)
                    {
                        if (count2 == 0)
                        {
                            bugalteria_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 1)
                        {
                            bugalteria_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 2)
                        {
                            bugalteria_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }
                    }
                }

                br.Close();

                //Console.ReadKey();
            }

            Console.Clear();

            /*for(count = 0; count < kol_kadrs; count++)
            {
                for(count2 = 0; count2 < 14; count2++)
                {
                    Console.WriteLine(kadrs_data[count, count2]);
                }

                Console.WriteLine();
            }

            //Console.WriteLine(kadrs_data.Length);
            //Console.WriteLine(kadrs_data[0,0].Length);

            //Console.ReadKey();
            */

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
                        //login();
                        Console.Clear();
                        bugateria_add();
                        //kadrs_add();
                    }

                    if (pos == 1)
                    {
                        Console.Clear();
                        bugalteria_show();
                        //kadrs_show();
                        //reg();
                    }

                    if (pos == 2)
                    {
                        return;
                    }
                }

                Console.Clear();

            }
        }

        static void bugateria_add()
        {
            BinaryWriter bw = new BinaryWriter(new FileStream("C:/Ocherednaya/bugalteria", FileMode.Append));
            string name, data, summa;
            long rez;
            int count, count2;


            Console.WriteLine("Введите имя операции...");
            name = Console.ReadLine();
            data = DateTime.Now.ToString();

            Console.WriteLine("Введите сумму операции...");
            summa = Console.ReadLine();
            while(!long.TryParse(summa, out rez))
            {
                Console.WriteLine("Введено некорректное значение!");
                summa = Console.ReadLine();
            }

            Console.Clear();

            Console.WriteLine();

            Console.WriteLine(name);
            Console.WriteLine(data);
            Console.WriteLine(summa);
            Console.ReadKey();

            bw.Write(name);
            bw.Write(data);
            bw.Write(summa);

            bw.Close();

            Console.Clear();

            BinaryReader br = new BinaryReader(new FileStream("C:/Ocherednaya/bugalteria", FileMode.Open));

            kol_bugalteria = 0;

            while (br.PeekChar() != -1)
            {
                br.ReadString();
                br.ReadString();
                br.ReadString();
                kol_bugalteria++;
            }

            br.Close();

            if (kol_bugalteria != 0)
            {
                bugalteria_data = new string[kol_bugalteria, 3];

                Console.WriteLine(kol_bugalteria);
                Console.ReadKey();

                br = new BinaryReader(new FileStream("C:/Ocherednaya/bugalteria", FileMode.Open));

                for (count = 0; count < kol_bugalteria; count++)
                {
                    for (count2 = 0; count2 < 3; count2++)
                    {
                        if (count2 == 0)
                        {
                            bugalteria_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 1)
                        {
                            bugalteria_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 2)
                        {
                            bugalteria_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }
                    }
                }

                br.Close();

                //Console.ReadKey();
            }

            return;

        }

        static void bugalteria_show()
        {
            int count, count2;

            if (kol_bugalteria == 0)
            {
                Console.Clear();
                Console.WriteLine("Нет данных об операциях.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            for (count = 0; count < kol_bugalteria; count++)
            {
                for (count2 = 0; count2 < 3; count2++)
                {
                    Console.WriteLine(bugalteria_data[count, count2]);
                }

                Console.WriteLine();

            }

            Console.ReadKey();
            Console.Clear();
            return;
        }

        static void sklad()
        {
            string[] menu = { "Добавить товар.", "Просмотреть список товаров.", "Выход." };
            int count, count2, pos = 0;
            ConsoleKeyInfo key;

            BinaryReader br = new BinaryReader(new FileStream("C:/Ocherednaya/sklad", FileMode.Open));

            kol_sklad = 0;

            while (br.PeekChar() != -1)
            {
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                kol_sklad++;
            }

            br.Close();

            if (kol_sklad != 0)
            {
                sklad_data = new string[kol_sklad, 4];

                br = new BinaryReader(new FileStream("C:/Ocherednaya/sklad", FileMode.Open));

                for (count = 0; count < kol_sklad; count++)
                {
                    for (count2 = 0; count2 < 4; count2++)
                    {
                        if (count2 == 0)
                        {
                            sklad_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 1)
                        {
                            sklad_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 2)
                        {
                            sklad_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 3)
                        {
                            sklad_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }
                    }
                }

                br.Close();

                //Console.ReadKey();
            }

            Console.Clear();

            /*for(count = 0; count < kol_kadrs; count++)
            {
                for(count2 = 0; count2 < 14; count2++)
                {
                    Console.WriteLine(kadrs_data[count, count2]);
                }

                Console.WriteLine();
            }

            //Console.WriteLine(kadrs_data.Length);
            //Console.WriteLine(kadrs_data[0,0].Length);

            //Console.ReadKey();
            */

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
                        //login();
                        Console.Clear();
                        sklad_add();
                        //bugateria_add();
                        //kadrs_add();
                    }

                    if (pos == 1)
                    {
                        Console.Clear();
                        sklad_show();
                        //bugalteria_show();
                        //kadrs_show();
                        //reg();
                    }

                    if (pos == 2)
                    {
                        return;
                    }
                }

                Console.Clear();

            }
        }

        static void sklad_add()
        {
            BinaryWriter bw = new BinaryWriter(new FileStream("C:/Ocherednaya/sklad", FileMode.Append));
            string kod, name, kol, stoimost;
            long rez;
            int count, count2, rezerv;
            bool ok = false;

            while (true)
            {

                Console.WriteLine("Введите код продукта...");
                kod = Console.ReadLine();
                while (!int.TryParse(kod, out rezerv))
                {
                    Console.WriteLine("Введен некорректный код.");
                    kod = Console.ReadLine();
                }


                for (count = 0; count < kol_sklad; count++)
                {
                    if (kod == sklad_data[count, 0])
                    {
                        Console.WriteLine("Данный код уже используется.");
                        Console.ReadKey(true);
                        ok = false;
                        break;
                    }
                    else
                    {
                        ok = true;
                    }
                }

                if (kol_sklad == 0)
                {
                    ok = true;
                }

                if (ok)
                {
                    break;
                }

            }

            ok = false;

            while (true)
            {

                Console.WriteLine("Введите имя продукта...");
                name = Console.ReadLine();

                for (count = 0; count < kol_sklad; count++)
                {
                    if (name == sklad_data[count, 1])
                    {
                        Console.WriteLine("Данное имя уже используется.");
                        Console.ReadKey();
                        ok = false;
                        break;
                    }
                    else
                    {
                        ok = true;
                    }
                }

                if (kol_sklad == 0)
                {
                    ok = true;
                }

                if (ok)
                {
                    break;
                }

            }

            Console.WriteLine("Введите количество...");
            kol = Console.ReadLine();
            while (!long.TryParse(kol, out rez))
            {
                Console.WriteLine("Введено некорректное значение!");
                kol = Console.ReadLine();
            }

            Console.WriteLine("Введите стоймость...");
            stoimost = Console.ReadLine();
            while (!long.TryParse(stoimost, out rez))
            {
                Console.WriteLine("Введено некорректное значение!");
                stoimost = Console.ReadLine();
            }

            Console.Clear();

            Console.WriteLine();

            Console.WriteLine(kod);
            Console.WriteLine(name);
            Console.WriteLine(kol);
            Console.WriteLine(stoimost);
            Console.ReadKey();

            bw.Write(kod);
            bw.Write(name);
            bw.Write(kol);
            bw.Write(stoimost);

            bw.Close();

            Console.Clear();

            BinaryReader br = new BinaryReader(new FileStream("C:/Ocherednaya/sklad", FileMode.Open));

            kol_sklad = 0;

            while (br.PeekChar() != -1)
            {
                br.ReadString();
                br.ReadString();
                br.ReadString();
                br.ReadString();
                kol_sklad++;
            }

            br.Close();

            if (kol_sklad != 0)
            {
                sklad_data = new string[kol_sklad, 4];

                Console.WriteLine(kol_sklad);
                Console.ReadKey();

                br = new BinaryReader(new FileStream("C:/Ocherednaya/sklad", FileMode.Open));

                for (count = 0; count < kol_sklad; count++)
                {
                    for (count2 = 0; count2 < 4; count2++)
                    {
                        if (count2 == 0)
                        {
                            sklad_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 1)
                        {
                            sklad_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 2)
                        {
                            sklad_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 3)
                        {
                            sklad_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }
                    }
                }

                br.Close();

                //Console.ReadKey();
            }

            return;

        }

        static void sklad_show()
        {
            int count, count2;

            if (kol_sklad == 0)
            {
                Console.Clear();
                Console.WriteLine("Нет данных о продуктах.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            for (count = 0; count < kol_sklad; count++)
            {
                for (count2 = 0; count2 < 4; count2++)
                {
                    Console.WriteLine(sklad_data[count, count2]);
                }

                Console.WriteLine();

            }

            Console.ReadKey();
            Console.Clear();
            return;
        }

        static void kassa()
        {
            string[] menu = { "Добавить кассовую операцию.", "Просмотр кассовых операций.", "Выход." };
            int count, count2, pos = 0;
            ConsoleKeyInfo key;

            BinaryReader br = new BinaryReader(new FileStream("C:/Ocherednaya/kassa", FileMode.Open));

            kol_kassa = 0;

            while (br.PeekChar() != -1)
            {
                br.ReadString();
                br.ReadString();
                br.ReadString();
                kol_kassa++;
            }

            br.Close();

            if (kol_kassa != 0)
            {
                kassa_data = new string[kol_kassa, 3];

                br = new BinaryReader(new FileStream("C:/Ocherednaya/kassa", FileMode.Open));

                for (count = 0; count < kol_kassa; count++)
                {
                    for (count2 = 0; count2 < 3; count2++)
                    {
                        if (count2 == 0)
                        {
                            kassa_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 1)
                        {
                            kassa_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 2)
                        {
                            kassa_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }
                    }
                }

                br.Close();

                //Console.ReadKey();
            }

            Console.Clear();

            /*for(count = 0; count < kol_kadrs; count++)
            {
                for(count2 = 0; count2 < 14; count2++)
                {
                    Console.WriteLine(kadrs_data[count, count2]);
                }

                Console.WriteLine();
            }

            //Console.WriteLine(kadrs_data.Length);
            //Console.WriteLine(kadrs_data[0,0].Length);

            //Console.ReadKey();
            */

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
                        //login();
                        Console.Clear();
                        kassa_add();
                        //bugateria_add();
                        //kadrs_add();
                    }

                    if (pos == 1)
                    {
                        Console.Clear();
                        kassa_show();
                        //bugalteria_show();
                        //kadrs_show();
                        //reg();
                    }

                    if (pos == 2)
                    {
                        return;
                    }
                }

                Console.Clear();

            }
        }

        static void kassa_add()
        {
            BinaryWriter bw = new BinaryWriter(new FileStream("C:/Ocherednaya/kassa", FileMode.Append));
            string number, data, tovari = "", kol_tovari;
            long rez;
            int count, count2;


            Console.WriteLine("Введите номер пользователя...");
            number = Console.ReadLine();
            data = DateTime.Now.ToString();

            Console.WriteLine("Введите количество товаров...");
            kol_tovari = Console.ReadLine();
            while (!long.TryParse(kol_tovari, out rez))
            {
                Console.WriteLine("Введено некорректное значение!");
                kol_tovari = Console.ReadLine();
            }

            for(count = 0; count < rez; count++)
            {
                Console.WriteLine("Введите товар " + count);
                tovari += Console.ReadLine() + ";";
            }

            Console.Clear();

            Console.WriteLine();

            Console.WriteLine(number);
            Console.WriteLine(data);
            Console.WriteLine(tovari);
            Console.ReadKey();

            bw.Write(number);
            bw.Write(data);
            bw.Write(tovari);

            bw.Close();

            Console.Clear();

            BinaryReader br = new BinaryReader(new FileStream("C:/Ocherednaya/kassa", FileMode.Open));

            kol_kassa = 0;

            while (br.PeekChar() != -1)
            {
                br.ReadString();
                br.ReadString();
                br.ReadString();
                kol_kassa++;
            }

            br.Close();

            if (kol_kassa != 0)
            {
                kassa_data = new string[kol_kassa, 3];

                Console.WriteLine(kol_kassa);
                Console.ReadKey();

                br = new BinaryReader(new FileStream("C:/Ocherednaya/kassa", FileMode.Open));

                for (count = 0; count < kol_kassa; count++)
                {
                    for (count2 = 0; count2 < 3; count2++)
                    {
                        if (count2 == 0)
                        {
                            kassa_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 1)
                        {
                            kassa_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }

                        if (count2 == 2)
                        {
                            kassa_data[count, count2] = br.ReadString();
                            //Console.WriteLine(br.ReadString());
                        }
                    }
                }

                br.Close();

                //Console.ReadKey();
            }

            return;

        }

        static void kassa_show()
        {
            int count, count2;

            if (kol_kassa == 0)
            {
                Console.Clear();
                Console.WriteLine("Нет данных об кассовых операциях.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            for (count = 0; count < kol_kassa; count++)
            {
                for (count2 = 0; count2 < 3; count2++)
                {
                    Console.WriteLine(kassa_data[count, count2]);
                }

                Console.WriteLine();

            }

            Console.ReadKey();
            Console.Clear();
            return;
        }

        static void loop()
        {
            BinaryReader br;
            string[] menu = {"Войти.", "Регистрация."};

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
                for(count = 0; count < menu.Length; count++)
                {
                    if(count == pos)
                    {
                        Console.Write(menu[count] + " <~" + "\n");
                    }
                    else
                    {
                        Console.Write(menu[count] + "\n");
                    }
                }

                key = Console.ReadKey();

                if((key.Key == ConsoleKey.UpArrow) && (pos != 0))
                {
                    pos--;
                }

                if ((key.Key == ConsoleKey.DownArrow) && (pos != menu.Length - 1))
                {
                    pos++;
                }

                if(key.Key == ConsoleKey.Enter)
                {
                    if(pos == 0)
                    {
                        login();
                    }

                    if(pos == 1)
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
