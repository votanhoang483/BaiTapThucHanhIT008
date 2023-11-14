using System;
namespace Event
{
    public class NhietKe
    {
        private int nhietdo;
        public delegate void NhietKeHandler(object obj, ThongTinNhietDo nhietdo);
        public event NhietKeHandler ThayDoiNhietDo;
        public NhietKe(int nhietdo)
        {
            this.nhietdo = nhietdo;
        }
        public int NhietDo
        {
            get { return nhietdo; }
            set
            {
                if (value != NhietDo)
                {
                    nhietdo = value;
                    if (ThayDoiNhietDo != null)
                    {
                        ThayDoiNhietDo(this, new ThongTinNhietDo(value));
                    }
                }
            }
        }
    }
    public class ThongTinNhietDo : System.EventArgs
    {
        private int nhietdo;
        public ThongTinNhietDo(int nhietdo)
        {
            this.nhietdo = nhietdo;
        }
        public int NhietDo
        {
            get { return nhietdo; }
            set { nhietdo = value; }
        }
    }
}
