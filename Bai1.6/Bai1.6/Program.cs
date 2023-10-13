using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai1._6
{
    internal class Program
    {
        interface IThinking
        {
            void thinking_behavior();
        }
        interface IIntellingent
        {
            void intelligent_behavior();
        }
        interface IAbility: IThinking, IIntellingent
        {

        }
        public class Mamal
        {
            public string characteristics;
        }
        public class Whale: Mamal
        {
            public Whale()
            {

            }
        }
        public class Human: Mamal, IAbility
        {
            public Human()
            {

            }
            public void intelligent_behavior()
            {

            }
            public void thinking_behavior()
            {

            }
        }
        static void Main(string[] args)
        {
        }
    }
}
