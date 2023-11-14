using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_LTTQ_Bai_1._5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Nhap duong dan thu muc:");
            string duongdan = Console.ReadLine();
            string duongdan1 = @duongdan;
            string[] danhsach = Directory.GetFiles(duongdan1);
            if (Directory.Exists(duongdan1))

            {
                Console.WriteLine("Cac tap tin la:");
                foreach (string s in danhsach)
                {
                    Console.WriteLine(s);
                }
            }
            else
            {
                Console.WriteLine("duong dan khong ton tai");
            }
        }
    }
}
