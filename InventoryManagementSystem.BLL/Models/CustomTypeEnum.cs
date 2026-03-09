using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models
{
    public enum CustomTypeEnum
    {
        FixedText = 1,
        Bit20RandomNumber = 2,
        Bit32RandomNumber = 3,
        SixDigitRandomNumber = 4,
        NineDigitRandomNumber = 5,
        Guid = 6,
        DateTime = 7,
        Sequence = 8
    }
}
