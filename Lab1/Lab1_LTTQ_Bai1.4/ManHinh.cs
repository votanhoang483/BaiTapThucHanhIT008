using System;
namespace Event
{
    public class ManHinh
    {
        public ManHinh(NhietKe nhietke)
        {
            nhietke.ThayDoiNhietDo += new NhietKe.NhietKeHandler(HienThi);
        }
        public void HienThi(object obj, ThongTinNhietDo ttnd)
        {
            Console.WriteLine("Nhiet do hien tai la {0}", ttnd.NhietDo);
        }
    }
}