using System;

namespace testovik
{
    class Program
    {
        static void Main()
        {
            int[] mass;
            int[] mass2;
            int N, count, count2, rez = 0;
            Random rand = new Random();

            Console.WriteLine("Введите N...");
            N = Convert.ToInt32(Console.ReadLine());

            mass = new int[N];

            for(count = 0; count < mass.Length; count++)
            {
                mass[count] = rand.Next(1, 10);
                Console.WriteLine(mass[count]);
            }

            Console.WriteLine();                           

            for(count = 0; count < mass.Length; count++)
            {
                rez = 0;

                for (count2 = mass.Length - 1; count2 > -1; count2--)
                {
                    if(mass[count] == mass[count2])
                    {
                        rez++;
                    }

                    if(rez > 1)
                    {
                        mass[count] = -100;
                    }
                }
            }

            rez = 0;

            for(count = 0; count < mass.Length; count++)
            {
                if(mass[count] == -100)
                {
                    rez++;
                }
            }

            mass2 = new int[mass.Length - rez];
            Console.WriteLine(mass2.Length);
            //Console.ReadKey();

            count2 = 0;

            for(count = 0; count < mass.Length; count++)
            {
                if(mass[count] != -100)
                {
                    mass2[count2] = mass[count];
                    //Console.WriteLine(mass[count]);
                    count2++;
                }
            }

            for (count = 0; count < mass2.Length; count++)
            {
                Console.WriteLine(mass2[count]);
            }

        }
    }
}
