using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace tinhphanso
{
    class phanso
    {
        public int tu {  get; set; }
        public int mau {  get; set; }
        public void nhap()
        {
            Console.Write("Nhap tu so: ");
            tu=Convert.ToInt32(Console.ReadLine());
            Console.Write("Nhap mau so: ");
            do
            {
                mau = Convert.ToInt32(Console.ReadLine());
                if(mau==0)
                {
                    {
                        Console.Write("Mau so bat buoc phai khac 0, vui long nhap lai mau so: ");
                    }
                }
            }
            while (mau == 0);
            

        }
        public void xuat()
        {
            Console.WriteLine("Phan So: " + tu + "/" + mau);
        }
        public static int GCD(int x, int y)
        {
            int a = Math.Abs(x);
            int b = Math.Abs(y);
            if (a == 0 || b == 0)
                return a + b;
            else
            {
                while (a != b)
                {
                    if (a > b)
                        a = a - b;
                    else
                        b = b - a;
                }
                return a;
            }
        }
        public static phanso operator +(phanso p1, phanso p2)
        {
            phanso tmp = new phanso();
            tmp.tu = p1.tu * p2.mau + p1.mau * p2.tu;
            tmp.mau = p1.mau * p2.mau;
            int gcd = GCD(tmp.tu, tmp.mau) ;
            tmp.tu = tmp.tu / gcd;
            tmp.mau = tmp.mau / gcd;
            return tmp;
        }
        public static phanso operator -(phanso p1, phanso p2)
        {
            phanso tmp = new phanso();
            tmp.tu = p1.tu * p2.mau - p1.mau * p2.tu;
            tmp.mau = p1.mau * p2.mau;
            int gcd = GCD(tmp.tu, tmp.mau);
            tmp.tu = tmp.tu / gcd;
            tmp.mau = tmp.mau / gcd;
            return tmp;
        }
        public static phanso operator *(phanso p1, phanso p2)
        {
            phanso tmp = new phanso();
            tmp.tu = p1.tu * p2.tu;
            tmp.mau = p1.mau * p2.mau;
            int gcd = GCD(tmp.tu, tmp.mau);
            tmp.tu = tmp.tu / gcd;
            tmp.mau = tmp.mau / gcd;
            return tmp;
        }
        public static phanso operator /(phanso p1, phanso p2)
        {
            phanso tmp = new phanso();
            tmp.tu = p1.tu * p2.mau;
            tmp.mau = p1.mau * p2.tu;
            int gcd = GCD(tmp.tu, tmp.mau);
            tmp.tu = tmp.tu / gcd;
            tmp.mau = tmp.mau / gcd;
            return tmp;
        }
        public static bool operator ==(phanso p1, phanso p2)
        {
            phanso tmp = new phanso();
            tmp = p1 - p2;
            if (tmp.tu == 0)
                return true;
            else
                return false;

        }
        public static bool operator !=(phanso p1, phanso p2)
        {
            phanso tmp = new phanso();
            tmp = p1 - p2;
            if (tmp.tu == 0)
                return false;
            else
                return true;
        }
        public static bool operator >(phanso p1, phanso p2)
        {
            phanso tmp = new phanso();
            tmp = p1 - p2;
            if ((tmp.tu>0)&&(tmp.mau>0))
                return true;
            else
                return false;
        }
        public static bool operator <(phanso p1, phanso p2)
        {
            phanso tmp = new phanso();
            tmp = p1 - p2;
            if (((tmp.tu < 0) && (tmp.mau > 0))|| ((tmp.tu > 0) && (tmp.mau < 0)))
                return true;
            else
                return false;
        }
        public static bool operator >=(phanso p1, phanso p2)
        {
            phanso tmp = new phanso();
            tmp = p1 - p2;
            if ((tmp.tu >= 0) && (tmp.mau > 0))
                return true;
            else
                return false;
        }
        public static bool operator <=(phanso p1, phanso p2)
        {
            phanso tmp = new phanso();
            tmp = p1 - p2;
            if (((tmp.tu <= 0) && (tmp.mau > 0)) || ((tmp.tu >= 0) && (tmp.mau < 0)))
                return true;
            else
                return false;
        }
        public static phanso ToPhanSo(int x)
        {
            phanso tmp=new phanso();
            tmp.tu = x;
            tmp.mau = 1;
            return tmp;
        }
        public static float ToSingleByPS(phanso p1)
        {
            float c = (float)p1.tu / (float)p1.mau;
            return c;
        }
    }
    public class PhanSo : IComparable
    {
        private phanso p = new phanso();
        public void nhappso()
        {
            p.nhap();
        }
        public void xuatpso()
        {
            p.xuat();
        }
        public int CompareTo(Object obj)
        {
            PhanSo PhSoToCompare = (PhanSo)obj;
            if (this.p == PhSoToCompare.p)
                return 0;
            if (this.p > PhSoToCompare.p)
                return 1;
            else
                return -1;
        }
    }
    class Program
    {
        static void Main()
        {
            phanso p1 = new phanso();
            phanso tmp = new phanso();
            phanso p2 = new phanso();
            Console.WriteLine("Nhap phan so thu nhat");
            p1.nhap();
            Console.WriteLine("Nhap phan so thu hai");
            p2.nhap();
            tmp = p1 + p2;
            Console.WriteLine("Ket qua la ");
            tmp.xuat();
            if (p1 > p2)
                Console.WriteLine("Phan so thu nhat lon hon");
            if (p1 == p2)
                Console.WriteLine("Hai phan so bang nhau");
            if (p1 < p2)
                Console.WriteLine("Phan so thu nhat nho hon");
            PhanSo phanso1 = new PhanSo();
            Console.WriteLine("Nhap phan so thu nhat cua day");
            phanso1.nhappso();
            PhanSo phanso2 = new PhanSo();
            Console.WriteLine("Nhap phan so thu hai cua day");
            phanso2.nhappso();
            PhanSo phanso3 = new PhanSo();
            Console.WriteLine("Nhap phan so thu ba cua day");
            phanso3.nhappso();
            PhanSo phanso4 = new PhanSo();
            Console.WriteLine("Nhap phan so thu tu cua day");
            phanso4.nhappso();
            PhanSo phanso5 = new PhanSo();
            Console.WriteLine("Nhap phan so thu nam cua day");
            phanso5.nhappso();
            List<PhanSo> list = new List<PhanSo>();
            list.Add(phanso1);
            list.Add(phanso2);
            list.Add(phanso3);
            list.Add(phanso4);
            list.Add(phanso5);
            list.Sort();
            Console.WriteLine();
            Console.WriteLine("Thu tu cac phan so tu nho den lon sau khi duoc sap xep");
            foreach(PhanSo phanso in list)
            {
                phanso.xuatpso();
            }
        }
    }
}

