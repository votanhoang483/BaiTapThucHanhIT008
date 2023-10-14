using System;


namespace hinhhoc
{
    public class Diem
    {
        public float a, b;
        public void nhapdiem()
        {
            Console.Write("Nhap toa do cua x: ");
            a = Convert.ToSingle(Console.ReadLine());
            Console.Write("Nhap toa do cua y: ");
            b = Convert.ToSingle(Console.ReadLine());
        }
        public void xuatdiem()
        {
            Console.WriteLine("({0),{1}", a, b);
        }
    }
    public abstract class Shape
    {
        public float dai, rong, bankinh;
        public string ten;
        public virtual void nhap()
        {

        }
        public virtual void tinhS()
        {
            
        }
        public virtual void ve()
        {

        }
    }
    public class hinhchunhat : Shape 
    {
        public Diem d = new Diem();
        public override void nhap()
        {
            ten = "Hinh Chu Nhat";
            d.nhapdiem();
            Console.Write("Nhap chieu dai: ");
            dai= Convert.ToSingle(Console.ReadLine());
            Console.Write("Nhap chieu rong: ");
            rong=Convert.ToSingle(Console.ReadLine());
        }
        public override void ve()
        {
            Console.WriteLine(ten);
            Console.WriteLine("Toa do diem thu nhat la ({0},{1}) ", d.a+rong, d.b+dai);
            Console.WriteLine("Toa do diem thu hai la ({0},{1}) ", d.a+rong, d.b);
            Console.WriteLine("Toa do diem thu ba la ({0},{1}) ", d.a, d.b+dai);
            Console.WriteLine("Toa do diem thu tu la ({0},{1}) ", d.a, d.b);
        }
        public override void tinhS()
        {
            Console.WriteLine("Dien tich cua hinh chu nhat la: {0}", dai * rong);
        }
    }
    public class hinhvuong: hinhchunhat
    {
        public override void nhap()
        {
            ten = "Hinh Vuong";
            d.nhapdiem();
            Console.Write("Nhap chieu dai: ");
            dai = Convert.ToSingle(Console.ReadLine());
        }
        public override void ve()
        {
            Console.WriteLine(ten);
            Console.WriteLine("Toa do diem thu nhat la ({0},{1}) ", d.a + dai, d.b + dai);
            Console.WriteLine("Toa do diem thu hai la ({0},{1}) ", d.a + dai, d.b);
            Console.WriteLine("Toa do diem thu ba la ({0},{1}) ", d.a, d.b + dai);
            Console.WriteLine("Toa do diem thu tu la ({0},{1}) ", d.a, d.b);
        }
        public override void tinhS()
        {
            Console.WriteLine("Dien tich cua hinh vuong la: {0}", dai * dai);
        }
    }
    public class hinhtamgiac : Shape
    {
        readonly public Diem[] d = new Diem[3];
        public override void nhap()
        {
            ten = "Hinh Tam Giac";
            for(int i=0;i<3;i++)
            {
                d[i] = new Diem();
                Console.WriteLine("Nhap toa do diem thu {0}", i+1);
                d[i].nhapdiem();
            }
        }
        public override void ve()
        {
            Console.WriteLine(ten);
            Console.WriteLine("Toa do diem thu nhat la ({0},{1}) ", d[0].a , d[0].b );
            Console.WriteLine("Toa do diem thu hai la ({0},{1}) ", d[1].a , d[1].b);
            Console.WriteLine("Toa do diem thu ba la ({0},{1}) ", d[2].a, d[2].b );
        }
        public override void tinhS()
        {
            float canh1 = (float)Math.Sqrt((d[0].a - d[1].a) * (d[0].a - d[1].a) + (d[0].b - d[1].b) * (d[0].b - d[1].b));
            float canh2 = (float)Math.Sqrt((d[0].a - d[2].a) * (d[0].a - d[2].a) + (d[0].b - d[2].b) * (d[0].b - d[2].b));
            float canh3 = (float)Math.Sqrt((d[2].a - d[1].a) * (d[2].a - d[1].a) + (d[2].b - d[1].b) * (d[2].b - d[1].b));
            float s = (canh1 + canh2 + canh3) / 3;
            float kq = (float)Math.Sqrt(s * Math.Abs(s - canh1) * Math.Abs(s - canh2) * Math.Abs(s - canh3));
            Console.WriteLine("Dien tich cua hinh tam giac la: {0}", kq);
        }
    }
    public class hinhtron : Shape
    {
        Diem d=new Diem();
        public override void nhap()
        {
            ten = "Hinh Tron";
            Console.WriteLine("Nhap toa do tam duong tron");
            d.nhapdiem();
            Console.Write("Nhap ban kinh: ");
            bankinh = Convert.ToSingle(Console.ReadLine());
        }
        public override void ve()
        {
            Console.WriteLine(ten);
            Console.WriteLine("Toa do tam duong tron la ({0},{1}) ", d.a, d.b);;
        }
        public override void tinhS()
        {
            float kq = bankinh * bankinh * (float)3.14;
            Console.WriteLine("Dien tich cua hinh tron la: {0}", kq);
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap so hinh can tao: ");
            int so=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Shape[] sh = new Shape[so];
            for(int i=0;i<so;i++)
            {
                Console.WriteLine("Nhap thong tin hinh thu {0}", i + 1);
                Random r=new Random();
                int ra = r.Next(1, 4);
                if(ra == 1)
                {
                    sh[i] = new hinhchunhat();
                }
                if (ra == 2)
                {
                    sh[i] = new hinhvuong();
                }
                if(ra==3)
                {
                    sh[i]=new hinhtamgiac();
                }
                if(ra==4)
                {
                    sh[i] = new hinhtron();
                }
                sh[i].nhap();
                Console.WriteLine();
            }
            for(int i=0;i<so;i++)
            {
                Console.WriteLine();
                Console.WriteLine("Hinh thu {0}\n", i+1);
                sh[i].ve();
                sh[i].tinhS();
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
