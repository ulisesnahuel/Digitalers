using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalers.Shared.Dtos
{
    public record GeneralResponse(bool Flag = false, string Message = null!);
    
}
