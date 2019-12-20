using System;
using System.Collections.Generic;
using System.IO;

namespace taynaya
{
    class Program
    {

        struct uchetki
        {
            public string login, passwd, rol;
        }

        struct kadri
        {
            public string name, familiya, otchestvo, number_dogovora, day_rojdeniya, mesac_rojdeniya, year_rojdeniya, pasport_seriya, pasport_number, snils, inn, doljnost, oklad, obrazovanie;
        }

        struct bugalteria
        {
            public string name, data, summa;
        }

        struct sklad
        {
            public string kod, name, kol, stoimost;
        }

        struct kassa
        {
            public string number, data, tovari, kol_tovari;
        }

        static List<uchetki> zapisi_users;
        static List<kadri> kadri_data;
        static List<bugalteria> Bugalteria_data;
        static List<sklad> sklad_data;
        static List<kassa> kassa_data;

        static void start()
        {
            BinaryReader br;
            string[] menu = { "Войти.", "Регистрация." };
            
            uchetki rez_zapisi = new uchetki();

            zapisi_users = new List<uchetki>();

            ConsoleKeyInfo key;

            int count, pos = 0;

            br = new BinaryReader(new FileStream("C:/Taynaya/passwd", FileMode.Open));

            while (br.PeekChar() != -1)
         
            {
                rez_zapisi.login = br.ReadString();
                rez_zapisi.passwd = br.ReadString();
                rez_zapisi.rol = br.ReadString();
                zapisi_users.Add(rez_zapisi);
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

        static void login()
        {
            string name, passwd = "";
            ConsoleKeyInfo key;
            int count;
            
            
            if(zapisi_users.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Нет зарегистрированных учеток.");
                Console.ReadKey();
                Console.Clear();
                start();
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
            
            for (count = 0; count < zapisi_users.Count; count++)
            {
                if ((name == zapisi_users[count].login) && (passwd == zapisi_users[count].passwd))
                {
                    Console.WriteLine("Доступ разрешен.");
                    Console.ReadKey();

                    Console.Clear();

                    if (zapisi_users[count].rol == "1")
                    {
                        kadrs();
                    }

                    if (zapisi_users[count].rol == "2")
                    {
                        bugalteriya();
                    }

                    if (zapisi_users[count].rol == "3")
                    {
                        skladm();
                    }

                    if (zapisi_users[count].rol == "4")
                    {
                        kassam();
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
            string name = "", passwd = "", rol;
            ConsoleKeyInfo key;
            int count = 0, pos = 0;
            bool ok = false;
            string[] menu = { "Отдел кадров", "Бухгалтерия", "Склад", "Касса" };

            Console.Clear();

            while (!ok)
            {
                Console.Clear();
                Console.WriteLine("Введите логин...");
                name = Console.ReadLine();
                while(name.Length < 8)
                {
                    Console.WriteLine("Введенный логин должен быть не менее 8 символов.");
                    name = Console.ReadLine();
                }
                Console.Clear();

                for (count = 0; count < zapisi_users.Count; count++)
                {
                    if (zapisi_users[count].login != name)
                    {
                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }
                }

                if (zapisi_users.Count == 0)
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

                if ((key.Key == ConsoleKey.Enter) && (passwd.Length >= 8))
                {
                    break;
                }

                Console.Clear();
            }

            Console.Clear();
            
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
                        rol = "1";
                        break;
                    }

                    if (pos == 1)
                    {
                        rol = "2";
                        break;
                    }

                    if (pos == 2)
                    {
                        rol = "3";
                        break;
                    }

                    if (pos == 3)
                    {
                        rol = "4";
                        break;
                    }

                }

            }

            bw = new BinaryWriter(new FileStream("C:/Taynaya/passwd", FileMode.Append));
            bw.Write(name);
            bw.Write(passwd);
            bw.Write(rol);
            bw.Close();
            Console.Clear();
            start();
        }

        static void kadrs()
        {
            string[] menu = { "Добавить данные сотрудника.", "Просмотр данных сотрудника.", "Удалить данные сотрудника.", "Выход." };
            int count, count2, pos = 0;
            ConsoleKeyInfo key;

            kadri rez_kadri = new kadri();

            kadri_data = new List<kadri>();

            BinaryReader br = new BinaryReader(new FileStream("C:/Taynaya/kadrs", FileMode.Open));

            while (br.PeekChar() != -1)
            {
                rez_kadri.name = br.ReadString();
                rez_kadri.familiya = br.ReadString();
                rez_kadri.otchestvo = br.ReadString();
                rez_kadri.familiya = br.ReadString();
                rez_kadri.number_dogovora = br.ReadString();
                rez_kadri.day_rojdeniya = br.ReadString();
                rez_kadri.mesac_rojdeniya = br.ReadString();
                rez_kadri.year_rojdeniya = br.ReadString();
                rez_kadri.pasport_seriya = br.ReadString();
                rez_kadri.pasport_number = br.ReadString();
                rez_kadri.snils = br.ReadString();
                rez_kadri.inn = br.ReadString();
                rez_kadri.doljnost = br.ReadString();
                rez_kadri.obrazovanie = br.ReadString();
                kadri_data.Add(rez_kadri);
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
                        Console.Clear();
                        kadrs_add();
                    }

                    if (pos == 1)
                    {
                        Console.Clear();
                        kadrs_show();
                    }

                    if (pos == 2)
                    {
                        Console.Clear();
                        kadri_del();
                    }

                    if(pos == 3)
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
            BinaryWriter bw = new BinaryWriter(new FileStream("C:/Taynaya/kadrs", FileMode.Append));
            
            kadri rez_kadri = new kadri();

            int rez, count, count2;

            Console.WriteLine("Введите имя...");
            rez_kadri.name = Console.ReadLine();
            Console.WriteLine("Введите фамилию...");
            rez_kadri.familiya = Console.ReadLine();
            Console.WriteLine("Введите отчество...");
            rez_kadri.otchestvo = Console.ReadLine();

            while (true)
            {
                Console.WriteLine("Введите номер договора...");
                rez_kadri.number_dogovora = Console.ReadLine();

                while (!int.TryParse(rez_kadri.number_dogovora, out rez))
                {
                    Console.WriteLine("Введен некорректный номер договора.");
                    rez_kadri.number_dogovora = Console.ReadLine();
                }

                if((rez_kadri.number_dogovora[0] == '+') || (rez_kadri.number_dogovora[0] == '-'))
                {
                    continue;
                }
                else
                {
                    break;
                }

            }

            while (true)
            {

                Console.WriteLine("Введите день рождения...");

                rez_kadri.day_rojdeniya = Console.ReadLine();
                while ((!int.TryParse(rez_kadri.day_rojdeniya, out rez)) || (rez < 1) || (rez > 31))
                {
                    Console.WriteLine("Введен некорректный день рождения.");
                    rez_kadri.day_rojdeniya = Console.ReadLine();
                }

                if ((rez_kadri.day_rojdeniya[0] == '+') || (rez_kadri.day_rojdeniya[0] == '-'))
                {
                    continue;
                }
                else
                {
                    break;
                }

            }

            while (true)
            {

                Console.WriteLine("Введите месяц рождения...");
                rez_kadri.mesac_rojdeniya = Console.ReadLine();
                
                while ((!int.TryParse(rez_kadri.mesac_rojdeniya, out rez)) || (rez < 1) || (rez > 12))
                {
                    Console.WriteLine("Введен некорректный месяц.");
                    rez_kadri.mesac_rojdeniya = Console.ReadLine();
                }

                if ((rez_kadri.mesac_rojdeniya[0] == '+') || (rez_kadri.mesac_rojdeniya[0] == '-'))
                {
                    continue;
                }
                else
                {
                    break;
                }

            }

            while (true)
            {

                Console.WriteLine("Введите год рождения...");
                rez_kadri.year_rojdeniya = Console.ReadLine();

                while (!int.TryParse(rez_kadri.year_rojdeniya, out rez))
                {
                    Console.WriteLine("Введен некорректный год.");
                    rez_kadri.year_rojdeniya = Console.ReadLine();
                }

                if ((rez_kadri.year_rojdeniya[0] == '+') || (rez_kadri.year_rojdeniya[0] == '-'))
                {
                    continue;
                }
                else
                {
                    break;
                }

            }

            while (true)
            {

                Console.WriteLine("Введите серию паспорта...");
                rez_kadri.pasport_seriya = Console.ReadLine();
                while ((!int.TryParse(rez_kadri.pasport_seriya, out rez)) || (rez_kadri.pasport_seriya.Length != 4))
                {
                    Console.WriteLine("Введена некорректная серия паспорта.");
                    rez_kadri.pasport_seriya = Console.ReadLine();
                }


                for (count = 0; count < kadri_data.Count; count++)
                {
                    if (rez_kadri.pasport_seriya == kadri_data[count].pasport_seriya)
                    {
                        Console.WriteLine("Данная серия паспорта уже используется.");
                        Console.ReadKey(true);
                        ok = false;
                        break;
                    }
                    else
                    {
                        ok = true;
                    }
                }

                if (kadri_data.Count == 0)
                {
                    ok = true;
                }

                if ((rez_kadri.pasport_seriya[0] == '+') || (rez_kadri.pasport_seriya[0] == '-'))
                {
                    continue;
                }
                else
                {
                    if(ok)
                    {
                        break;
                    }
                }

                //if (ok)
                //{
                    //break;
                //}

            }

            ok = false;

            while (true)
            {

                Console.WriteLine("Введите номер паспорта...");
                rez_kadri.pasport_number = Console.ReadLine();
                while ((!int.TryParse(rez_kadri.pasport_number, out rez)) || (rez_kadri.pasport_number.Length != 6))
                {
                    Console.WriteLine("Введена некорректный номер паспорта.");
                    rez_kadri.pasport_number = Console.ReadLine();
                }

                for (count = 0; count < kadri_data.Count; count++)
                {
                    if (rez_kadri.pasport_number == kadri_data[count].pasport_number)
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

                if (kadri_data.Count == 0)
                {
                    ok = true;
                }

                if ((rez_kadri.pasport_number[0] == '+') || (rez_kadri.pasport_number[0] == '-'))
                {
                    continue;
                }
                else
                {
                    if (ok)
                    {
                        break;
                    }
                }
            }

            ok = false;

            while (true)
            {

                Console.WriteLine("Введите снилс...");
                rez_kadri.snils = Console.ReadLine();
                while ((!long.TryParse(rez_kadri.snils, out long rezl)) || (rez_kadri.snils.Length != 11))
                {
                    Console.WriteLine("Введен некорректный снилс.");
                    rez_kadri.snils = Console.ReadLine();
                }

                for (count = 0; count < kadri_data.Count; count++)
                {
                    if (rez_kadri.snils == kadri_data[count].snils)
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

                if (kadri_data.Count == 0)
                {
                    ok = true;
                }

                if ((rez_kadri.snils[0] == '+') || (rez_kadri.snils[0] == '-'))
                {
                    continue;
                }
                else
                {
                    if (ok)
                    {
                        break;
                    }
                }

            }

            ok = false;

            while (true)
            {

                Console.WriteLine("Введите инн...");
                rez_kadri.inn = Console.ReadLine();
                while ((!long.TryParse(rez_kadri.inn, out long rezl)) || (rez_kadri.inn.Length != 12))
                {
                    Console.WriteLine("Введена некорректный инн.");
                    rez_kadri.inn = Console.ReadLine();
                }

                for (count = 0; count < kadri_data.Count; count++)
                {
                    if (rez_kadri.inn == kadri_data[count].inn)
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

                if (kadri_data.Count == 0)
                {
                    ok = true;
                }

                if ((rez_kadri.inn[0] == '+') || (rez_kadri.inn[0] == '-'))
                {
                    continue;
                }
                else
                {
                    if (ok)
                    {
                        break;
                    }
                }

            }

            Console.WriteLine("Введите первую должность...");
            rez_kadri.doljnost = Console.ReadLine() + ";";

            Console.WriteLine("Введите вторую должность...");
            rez_kadri.doljnost += Console.ReadLine() + ";";

            Console.WriteLine("Введите оклад...");
            rez_kadri.oklad = Console.ReadLine();
            while (!int.TryParse(rez_kadri.oklad, out rez))
            {
                Console.WriteLine("Введена некорректный оклад.");
                rez_kadri.oklad = Console.ReadLine();
            }

            Console.WriteLine("Введите образование...");
            rez_kadri.obrazovanie = Console.ReadLine();

            bw.Write(rez_kadri.name);
            bw.Write(rez_kadri.familiya);
            bw.Write(rez_kadri.otchestvo);
            bw.Write(rez_kadri.number_dogovora);
            bw.Write(rez_kadri.day_rojdeniya);
            bw.Write(rez_kadri.mesac_rojdeniya);
            bw.Write(rez_kadri.year_rojdeniya);
            bw.Write(rez_kadri.pasport_seriya);
            bw.Write(rez_kadri.pasport_number);
            bw.Write(rez_kadri.snils);
            bw.Write(rez_kadri.inn);
            bw.Write(rez_kadri.doljnost);
            bw.Write(rez_kadri.oklad);
            bw.Write(rez_kadri.obrazovanie);

            bw.Close();

            kadri_data = new List<kadri>();

            BinaryReader br = new BinaryReader(new FileStream("C:/Taynaya/kadrs", FileMode.Open));

            while (br.PeekChar() != -1)
            {
                rez_kadri.name = br.ReadString();
                rez_kadri.familiya = br.ReadString();
                rez_kadri.otchestvo = br.ReadString();
                rez_kadri.number_dogovora = br.ReadString();
                rez_kadri.day_rojdeniya = br.ReadString();
                rez_kadri.mesac_rojdeniya = br.ReadString();
                rez_kadri.year_rojdeniya = br.ReadString();
                rez_kadri.pasport_seriya = br.ReadString();
                rez_kadri.pasport_number = br.ReadString();
                rez_kadri.snils = br.ReadString();
                rez_kadri.inn = br.ReadString();
                rez_kadri.doljnost = br.ReadString();
                rez_kadri.oklad = br.ReadString();
                rez_kadri.obrazovanie = br.ReadString();
                kadri_data.Add(rez_kadri);
            }

            br.Close();

            return;

        }
        static void kadrs_show()
        {
            int count, count2;

            if (kadri_data.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Нет данных о работниках.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            for (count = 0; count < kadri_data.Count; count++)
            {
                
                Console.WriteLine(kadri_data[count].name);
                Console.WriteLine(kadri_data[count].familiya);
                Console.WriteLine(kadri_data[count].otchestvo);
                Console.WriteLine(kadri_data[count].number_dogovora);
                Console.WriteLine(kadri_data[count].day_rojdeniya + "." + kadri_data[count].mesac_rojdeniya + "." + kadri_data[count].year_rojdeniya) ;
                Console.WriteLine(kadri_data[count].pasport_seriya + " " + kadri_data[count].pasport_number);
                Console.WriteLine(kadri_data[count].snils);
                Console.WriteLine(kadri_data[count].inn);
                Console.WriteLine(kadri_data[count].doljnost);
                Console.WriteLine(kadri_data[count].oklad);
                Console.WriteLine(kadri_data[count].obrazovanie);

                Console.WriteLine();

            }

            Console.ReadKey();
            Console.Clear();
            return;

        }

        static void kadri_del()
        {
            string name, familiya;
            int count, count2;
            BinaryWriter bw;

            if (kadri_data.Count == 0)
            {
                Console.WriteLine("Отсутствуют данные для удаления.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.WriteLine("Введите имя рабочего...");
            name = Console.ReadLine();
            Console.WriteLine("Введите фамилию рабочего...");
            familiya = Console.ReadLine();


            for (count = 0; count < kadri_data.Count; count++)
            {
                if ((kadri_data[count].name == name) && (kadri_data[count].familiya == familiya))
                {
                    kadri_data.RemoveAt(count);
                    Console.WriteLine("Успех!");
                    Console.ReadKey();
                    Console.Clear();

                    bw = new BinaryWriter(new FileStream("C:/Taynaya/kadrs", FileMode.Create));

                    for(count2 = 0; count2 < kadri_data.Count; count2++)
                    {
                        bw.Write(kadri_data[count2].name);
                        bw.Write(kadri_data[count2].familiya);
                        bw.Write(kadri_data[count2].otchestvo);
                        bw.Write(kadri_data[count2].number_dogovora);
                        bw.Write(kadri_data[count2].day_rojdeniya);
                        bw.Write(kadri_data[count2].mesac_rojdeniya);
                        bw.Write(kadri_data[count2].year_rojdeniya);
                        bw.Write(kadri_data[count2].pasport_seriya);
                        bw.Write(kadri_data[count2].pasport_number);
                        bw.Write(kadri_data[count2].snils);
                        bw.Write(kadri_data[count2].inn);
                        bw.Write(kadri_data[count2].doljnost);
                        bw.Write(kadri_data[count2].oklad);
                        bw.Write(kadri_data[count2].obrazovanie);

                    }

                    bw.Close();

                    return;
                }
            }

            Console.WriteLine("Таких данных нет. Невозможно удалить то, чего нет.");
            Console.ReadKey();
            Console.Clear();
            return;

        }

        static void bugalteriya()
        {
            string[] menu = { "Добавить операцию.", "Просмотр операций.", "Удаление операции.", "Выход." };
            int count, count2, pos = 0;
            ConsoleKeyInfo key;

            bugalteria rez_bugalteria = new bugalteria();

            Bugalteria_data = new List<bugalteria>();

            BinaryReader br = new BinaryReader(new FileStream("C:/Taynaya/bugalteria", FileMode.Open));

            while (br.PeekChar() != -1)
            {
                rez_bugalteria.name = br.ReadString();
                rez_bugalteria.data = br.ReadString();
                rez_bugalteria.summa = br.ReadString();
                Bugalteria_data.Add(rez_bugalteria);
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
                        Console.Clear();
                        bugateria_add();
                    }

                    if (pos == 1)
                    {
                        Console.Clear();
                        bugalteria_show();
                    }

                    if (pos == 2)
                    {
                        Console.Clear();
                        bugalteria_del();
                    }

                    if(pos == 3)
                    {
                        return;
                    }

                }

                Console.Clear();

            }
        }

        static void bugateria_add()
        {
            BinaryWriter bw = new BinaryWriter(new FileStream("C:/Taynaya/bugalteria", FileMode.Append));

            bugalteria rez_bugalteria = new bugalteria();

            Bugalteria_data = new List<bugalteria>();

            long rez;
            int count, count2;


            Console.WriteLine("Введите имя операции...");
            rez_bugalteria.name = Console.ReadLine();
            rez_bugalteria.data = DateTime.Now.ToString();

            while (true)
            {

                Console.WriteLine("Введите сумму операции...");
                rez_bugalteria.summa = Console.ReadLine();
                
                while (!long.TryParse(rez_bugalteria.summa, out rez))
                {
                    Console.WriteLine("Введено некорректное значение!");
                    rez_bugalteria.summa = Console.ReadLine();
                }

                if ((rez_bugalteria.summa[0] == '+') || (rez_bugalteria.summa[0] == '-'))
                {
                    continue;
                }
                else
                {
                    break;
                }

            }

            Console.Clear();

            Console.WriteLine();

            Console.WriteLine(rez_bugalteria.name);
            Console.WriteLine(rez_bugalteria.data);
            Console.WriteLine(rez_bugalteria.summa);
            Console.ReadKey();

            bw.Write(rez_bugalteria.name);
            bw.Write(rez_bugalteria.data);
            bw.Write(rez_bugalteria.summa);

            bw.Close();

            Console.Clear();

            BinaryReader br = new BinaryReader(new FileStream("C:/Taynaya/bugalteria", FileMode.Open));

            while (br.PeekChar() != -1)
            {
                rez_bugalteria.name = br.ReadString();
                rez_bugalteria.data = br.ReadString();
                rez_bugalteria.summa = br.ReadString();
                Bugalteria_data.Add(rez_bugalteria);
            }

            br.Close();

            return;

        }

        static void bugalteria_show()
        {
            int count, count2;

            if (Bugalteria_data.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Нет данных об операциях.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            for (count = 0; count < Bugalteria_data.Count; count++)
            {

                Console.WriteLine(Bugalteria_data[count].name);
                Console.WriteLine(Bugalteria_data[count].data);
                Console.WriteLine(Bugalteria_data[count].summa);
                Console.WriteLine();

            }

            Console.ReadKey();
            Console.Clear();
            return;
        }

        static void bugalteria_del()
        {
            BinaryWriter bw;
            string name;
            int count, count2;

            if(Bugalteria_data.Count == 0)
            {
                Console.WriteLine("Отсутствуют данные для удаления.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.WriteLine("Введите имя операции...");
            name = Console.ReadLine();

            for(count = 0; count < Bugalteria_data.Count; count++)
            {
                if(Bugalteria_data[count].name == name)
                {
                    Bugalteria_data.RemoveAt(count);
                    Console.WriteLine("Успех!");
                    Console.ReadKey();
                    Console.Clear();

                    bw = new BinaryWriter(new FileStream("C:/Taynaya/bugalteria", FileMode.Create));

                    for (count2 = 0; count2 < kadri_data.Count; count2++)
                    {
                        bw.Write(Bugalteria_data[count2].name);
                        bw.Write(Bugalteria_data[count2].data);
                        bw.Write(Bugalteria_data[count2].summa);
                    }

                    bw.Close();

                    return;
                }
            }

            Console.WriteLine("Таких данных нет. Невозможно удалить то, чего нет.");
            Console.ReadKey();
            Console.Clear();
            return;

        }

        static void skladm()
        {
            string[] menu = { "Добавить товар.", "Просмотреть список товаров.", "Удалить товар.", "Выход." };
            int count, count2, pos = 0;
            ConsoleKeyInfo key;

            sklad rez_sklad = new sklad();

            sklad_data = new List<sklad>();

            BinaryReader br = new BinaryReader(new FileStream("C:/Taynaya/sklad", FileMode.Open));

            while (br.PeekChar() != -1)
            {
                rez_sklad.kod = br.ReadString();
                rez_sklad.name = br.ReadString();
                rez_sklad.kol = br.ReadString();
                rez_sklad.stoimost = br.ReadString();
                sklad_data.Add(rez_sklad);
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
                        Console.Clear();
                        sklad_add();
                    }

                    if (pos == 1)
                    {
                        Console.Clear();
                        sklad_show();
                    }

                    if (pos == 2)
                    {
                        Console.Clear();
                        sklad_del();
                    }

                    if(pos == 3)
                    {
                        return;
                    }

                }

                Console.Clear();

            }
        }

        static void sklad_add()
        {
            BinaryWriter bw = new BinaryWriter(new FileStream("C:/Taynaya/sklad", FileMode.Append));

            sklad rez_sklad = new sklad();

            long rez;
            int count, count2, rezerv;
            bool ok = false;

            while (true)
            {

                Console.WriteLine("Введите код продукта...");
                rez_sklad.kod = Console.ReadLine();
                
                while (!int.TryParse(rez_sklad.kod, out rezerv))
                {
                    Console.WriteLine("Введен некорректный код.");
                    rez_sklad.kod = Console.ReadLine();
                }


                for (count = 0; count < sklad_data.Count; count++)
                {
                    if (rez_sklad.kod == sklad_data[count].kod)
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

                if (sklad_data.Count == 0)
                {
                    ok = true;
                }

                if ((rez_sklad.kod[0] == '+') || (rez_sklad.kod[0] == '-'))
                {
                    continue;
                }
                else
                {
                    if (ok)
                    {
                        break;
                    }
                }

            }

            ok = false;

            while (true)
            {

                Console.WriteLine("Введите имя продукта...");
                rez_sklad.name = Console.ReadLine();

                for (count = 0; count < sklad_data.Count; count++)
                {
                    if (rez_sklad.name == sklad_data[count].name)
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

                if (sklad_data.Count == 0)
                {
                    ok = true;
                }

                if (ok)
                {
                    break;
                }

            }

            while (true)
            {

                Console.WriteLine("Введите количество...");
                rez_sklad.kol = Console.ReadLine();

                while (!long.TryParse(rez_sklad.kol, out rez))
                {
                    Console.WriteLine("Введено некорректное значение!");
                    rez_sklad.kol = Console.ReadLine();
                }

                if ((rez_sklad.kol[0] == '+') || (rez_sklad.kol[0] == '-'))
                {
                    continue;
                }
                else
                {
                    break;
                }

            }

            while (true)
            {

                Console.WriteLine("Введите стоймость...");
                rez_sklad.stoimost = Console.ReadLine();

                while (!long.TryParse(rez_sklad.stoimost, out rez))
                {
                    Console.WriteLine("Введено некорректное значение!");
                    rez_sklad.stoimost = Console.ReadLine();
                }

                if ((rez_sklad.stoimost[0] == '+') || (rez_sklad.stoimost[0] == '-'))
                {
                    continue;
                }
                else
                {
                    break;
                }

            }

            Console.Clear();

            Console.WriteLine();

            Console.WriteLine(rez_sklad.kod);
            Console.WriteLine(rez_sklad.name);
            Console.WriteLine(rez_sklad.kol);
            Console.WriteLine(rez_sklad.stoimost);
            Console.ReadKey();

            bw.Write(rez_sklad.kod);
            bw.Write(rez_sklad.name);
            bw.Write(rez_sklad.kol);
            bw.Write(rez_sklad.stoimost);

            bw.Close();

            Console.Clear();

            sklad_data = new List<sklad>();

            BinaryReader br = new BinaryReader(new FileStream("C:/Taynaya/sklad", FileMode.Open));

            while (br.PeekChar() != -1)
            {
                rez_sklad.kod = br.ReadString();
                rez_sklad.name = br.ReadString();
                rez_sklad.kol = br.ReadString();
                rez_sklad.stoimost = br.ReadString();
                sklad_data.Add(rez_sklad);
            }

            br.Close();

            return;

        }

        static void sklad_show()
        {
            int count, count2;

            if (sklad_data.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Нет данных о продуктах.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            for (count = 0; count < sklad_data.Count; count++)
            {
                Console.WriteLine(sklad_data[count].kod);
                Console.WriteLine(sklad_data[count].name);
                Console.WriteLine(sklad_data[count].kol);
                Console.WriteLine(sklad_data[count].stoimost);

                Console.WriteLine();

            }

            Console.ReadKey();
            Console.Clear();
            return;
        }

        static void sklad_del()
        {
            BinaryWriter bw;
            string name;
            int count, count2;

            if (sklad_data.Count == 0)
            {
                Console.WriteLine("Отсутствуют данные для удаления.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.WriteLine("Введите название товара...");
            name = Console.ReadLine();

            for (count = 0; count < sklad_data.Count; count++)
            {
                if (sklad_data[count].name == name)
                {
                    sklad_data.RemoveAt(count);
                    Console.WriteLine("Успех!");
                    Console.ReadKey();
                    Console.Clear();

                    bw = new BinaryWriter(new FileStream("C:/Taynaya/sklad", FileMode.Create));

                    for(count2 = 0; count2 < sklad_data.Count; count2++)
                    {
                        bw.Write(sklad_data[count2].kod);
                        bw.Write(sklad_data[count2].name);
                        bw.Write(sklad_data[count2].kol);
                        bw.Write(sklad_data[count2].stoimost);
                    }

                    bw.Close();

                    return;
                }
            }

            Console.WriteLine("Таких данных нет. Невозможно удалить то, чего нет.");
            Console.ReadKey();
            Console.Clear();
            return;

        }
        static void kassam()
        {
            string[] menu = { "Добавить кассовую операцию.", "Просмотр кассовых операций.", "Удалить кассовую операцию.", "Выход." };
            int count, count2, pos = 0;
            ConsoleKeyInfo key;

            kassa rez_kassa = new kassa();

            kassa_data = new List<kassa>();

            BinaryReader br = new BinaryReader(new FileStream("C:/Taynaya/kassa", FileMode.Open));

            while (br.PeekChar() != -1)
            {
                rez_kassa.number = br.ReadString();
                rez_kassa.data = br.ReadString();
                rez_kassa.tovari = br.ReadString();
                kassa_data.Add(rez_kassa);
            }

            br.Close();

            Console.Clear();

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
                        Console.Clear();
                        kassa_add();   
                    }

                    if (pos == 1)
                    {
                        Console.Clear();
                        kassa_show();
                    }

                    if (pos == 2)
                    {
                        Console.Clear();
                        kassa_del();
                    }

                    if(pos == 3)
                    {
                        return;
                    }

                }

                Console.Clear();

            }
        }

        static void kassa_add()
        {
            BinaryWriter bw = new BinaryWriter(new FileStream("C:/Taynaya/kassa", FileMode.Append));

            kassa rez_kassa = new kassa();

            kassa_data = new List<kassa>();

            long rez;
            int count, count2;


            Console.WriteLine("Введите номер пользователя...");
            rez_kassa.number = Console.ReadLine();
            
            rez_kassa.data = DateTime.Now.ToString();

            while (true)
            {

                Console.WriteLine("Введите количество товаров...");
                rez_kassa.kol_tovari = Console.ReadLine();

                while (!long.TryParse(rez_kassa.kol_tovari, out rez))
                {
                    Console.WriteLine("Введено некорректное значение!");
                    rez_kassa.kol_tovari = Console.ReadLine();
                }

                if ((rez_kassa.kol_tovari[0] == '+') || (rez_kassa.kol_tovari[0] == '-'))
                {
                    continue;
                }
                else
                {
                    break;
                      
                }

            }

            for (count = 0; count < rez; count++)
            {
                Console.WriteLine("Введите товар " + count);
                rez_kassa.tovari += Console.ReadLine() + ";";
            }

            Console.Clear();

            Console.WriteLine();

            Console.WriteLine(rez_kassa.number);
            Console.WriteLine(rez_kassa.data);
            Console.WriteLine(rez_kassa.tovari);
            Console.ReadKey();

            bw.Write(rez_kassa.number);
            bw.Write(rez_kassa.data);
            bw.Write(rez_kassa.tovari);

            bw.Close();

            Console.Clear();

            BinaryReader br = new BinaryReader(new FileStream("C:/Taynaya/kassa", FileMode.Open));

            while (br.PeekChar() != -1)
            {
                rez_kassa.number = br.ReadString();
                rez_kassa.data = br.ReadString();
                rez_kassa.tovari = br.ReadString();
                kassa_data.Add(rez_kassa);
            }

            br.Close();

            return;

        }

        static void kassa_show()
        {
            int count, count2;

            if (kassa_data.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Нет данных об кассовых операциях.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            for (count = 0; count < kassa_data.Count; count++)
            {
                Console.WriteLine(kassa_data[count].number);
                Console.WriteLine(kassa_data[count].data);
                Console.WriteLine(kassa_data[count].tovari);
                Console.WriteLine();

            }

            Console.ReadKey();
            Console.Clear();
            return;
        }

        static void kassa_del()
        {
            BinaryWriter bw;
            string number;
            int count, count2;

            if (kassa_data.Count == 0)
            {
                Console.WriteLine("Отсутствуют данные для удаления.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.WriteLine("Введите номер пользователя...");
            number = Console.ReadLine();

            for (count = 0; count < kassa_data.Count; count++)
            {
                if (kassa_data[count].number == number)
                {
                    kassa_data.RemoveAt(count);
                    Console.WriteLine("Успех!");
                    Console.ReadKey();
                    Console.Clear();

                    bw = new BinaryWriter(new FileStream("C:/Taynaya/kassa", FileMode.Create));

                    for(count2 = 0; count2 < kassa_data.Count; count2++)
                    {
                        bw.Write(kassa_data[count2].number);
                        bw.Write(kassa_data[count2].data);
                        bw.Write(kassa_data[count2].tovari);
                    }

                    bw.Close();

                    return;
                }
            }

            Console.WriteLine("Таких данных нет. Невозможно удалить то, чего нет.");
            Console.ReadKey();
            Console.Clear();
            return;

        }

        static void Main(string[] args)
        {

            if(!Directory.Exists("C:/Taynaya"))
            {
                Directory.CreateDirectory("C:/Taynaya");
            }

            if(!File.Exists("C:/Taynaya/passwd"))
            {
                File.Create("C:/Taynaya/passwd").Close();
            }

            if (!File.Exists("C:/Taynaya/kadrs"))
            {
                File.Create("C:/Taynaya/kadrs").Close();
            }

            if (!File.Exists("C:/Taynaya/bugalteria"))
            {
                File.Create("C:/Taynaya/bugalteria").Close();
            }

            if (!File.Exists("C:/Taynaya/sklad"))
            {
                File.Create("C:/Taynaya/sklad").Close();
            }

            if (!File.Exists("C:/Taynaya/kassa"))
            {
                File.Create("C:/Taynaya/kassa").Close();
            }

            start();

        }
    }
}

