using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wspol
{
    public class NumberGenerator
    {
        public int GenerateRandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 101);
        }
    }
}
