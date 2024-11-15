using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Semaforo
{
    class Program
    {
        private static SemaphoreSlim SemaphoreUnoVerde = new SemaphoreSlim(0, 3);
        private static SemaphoreSlim SemaphoreUnoRojo = new SemaphoreSlim(0, 2);

        private static SemaphoreSlim SemaphoreDosVerde = new SemaphoreSlim(0, 1);
        private static SemaphoreSlim SemaphoreDosRojo = new SemaphoreSlim(0, 4);

        private static SemaphoreSlim SemaphoreTresVerde = new SemaphoreSlim(0, 1);
        private static SemaphoreSlim SemaphoreTresRojo = new SemaphoreSlim(0, 4);

        static void Main(string[] args)
        {
            Thread TC = new Thread(Controller);

            Thread T1 = new Thread(S1);
            Thread T2 = new Thread(S2);
            Thread T3 = new Thread(S3);
            Thread T4 = new Thread(S4);
            Thread T5 = new Thread(S5);

            T1.Start();
            T2.Start();
            T3.Start();
            T4.Start();
            T5.Start();

            Console.WriteLine("Esperando que el controller empiece a secuenciar la programación");
            Thread.Sleep(1000);            

            TC.Start();
        }

        public static void S1()
        {
            while (true)
            {
                Console.WriteLine("S1 esperando verde");
                SemaphoreUnoVerde.Wait();

                Console.WriteLine("S1 esperando rojo");
                SemaphoreDosRojo.Wait();

                Console.WriteLine("S1 esperando rojo");
                SemaphoreTresRojo.Wait();
            }
        }

        public static void S2()
        {
            while (true)
            {
                Console.WriteLine("S2 esperando verde");
                SemaphoreUnoVerde.Wait();

                Console.WriteLine("S2 esperando rojo");
                SemaphoreDosRojo.Wait();

                Console.WriteLine("S2 esperando rojo");
                SemaphoreTresRojo.Wait();
            }
        }

        public static void S3()
        {
            while (true)
            {
                Console.WriteLine("S3 esperando verde");
                SemaphoreUnoVerde.Wait();

                Console.WriteLine("S3 esperando rojo");
                SemaphoreDosRojo.Wait();

                Console.WriteLine("S3 esperando rojo");
                SemaphoreTresRojo.Wait();
            }
        }

        public static void S4()
        {
            while (true)
            {
                Console.WriteLine("S4 esperando rojo");
                SemaphoreUnoRojo.Wait();

                Console.WriteLine("S4 esperando verde");
                SemaphoreDosVerde.Wait();

                Console.WriteLine("S4 esperando rojo");
                SemaphoreTresRojo.Wait();
            }
        }

        public static void S5()
        {
            while (true)
            {
                Console.WriteLine("S5 esperando rojo");
                SemaphoreUnoRojo.Wait();

                Console.WriteLine("S5 esperando rojo");
                SemaphoreDosRojo.Wait();

                Console.WriteLine("S5 esperando verde");
                SemaphoreTresVerde.Wait();
            }
        }


        public static void Controller()
        {
            while (true)
            {
                Console.WriteLine("Se liberan 3 verdes y 2 rojos.");

                SemaphoreUnoRojo.Release(2);
                SemaphoreUnoVerde.Release(3);

                Thread.Sleep(2000);

                Console.WriteLine("Se liberan 1 verde y 4 rojos.");
                SemaphoreDosVerde.Release(1);

                //if(SemaphoreDosVerde.CurrentCount > 0)? //Tomar alguna decisión de por qué hay hilos libres cuando no debería
                SemaphoreDosRojo.Release(4);

                Thread.Sleep(2000);

                Console.WriteLine("Se liberan 1 verde y 4 rojos.");
                SemaphoreTresVerde.Release(1);
                SemaphoreTresRojo.Release(4);

                Thread.Sleep(2000);
            }
        }
    }
}
