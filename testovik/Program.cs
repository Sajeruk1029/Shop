using System;
using System.IO;

namespace testovik
{
    class Program
    {

        static string[] logins;
        static string[] passwds;
        static byte[] rols;
        static int kol = 0;

        static void Main(string[] args)
        {

            BinaryReader br;

            string name, passwd, rol_rez;
            byte rol= 0;
            ConsoleKeyInfo key;
            bool login_ok = false;
            byte rez = 0;

            if(!Directory.Exists("C:/Jopa"))
            {
                Directory.CreateDirectory("C:/Jopa");
            }

            if(!File.Exists("C:/Jopa/users"))
            {
                File.Create("C:/Jopa/users").Close();
            }

            br = new BinaryReader(new FileStream("C:/Jopa/users", FileMode.Open));

            while(br.PeekChar() != -1)
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

            br = new BinaryReader(new FileStream("C:/Jopa/users", FileMode.Open));

            
            for(int count = 0; count < kol; count++)
            {
                logins[count] = br.ReadString();
                passwds[count] = br.ReadString();
                rols[count] = br.ReadByte();

            }
            
            br.Close();

            Console.WriteLine("Введите логин...");
            name = Console.ReadLine();

            Console.WriteLine("Введите пароль...");
            passwd = Console.ReadLine();

            for(int count = 0; count < kol; count++)
            {
                if((name == logins[count]) && (passwd == passwds[count]))
                {
                    login_ok = true;
                    rez = rols[count];
                }
            }

            if(login_ok)
            {
                Console.WriteLine("Success!");
                Console.ReadKey();
                Console.WriteLine("Rol: " + rez);
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("Failed!");
                Console.ReadKey();
            }


        }
    }
}
