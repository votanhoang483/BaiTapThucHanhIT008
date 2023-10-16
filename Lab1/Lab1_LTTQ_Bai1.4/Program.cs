using System;
using System.Threading;
namespace Event
{
    class Test
    {
        static void Main(string[] args)
        {
            NhietKe nhietke = new NhietKe(28);
            ManHinh manhinh = new ManHinh(nhietke);
            for (; ; )
            {
                Thread.Sleep(1000);
                Random random = new Random();
                int nhietdo = random.Next(10) - 5;
                nhietke.NhietDo += nhietdo;
            }
            Console.ReadLine();
        }
    }
}