using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Core.Application.Interfaces.Helpers
{
    public interface IJsonHelper
    {
        string Serialize<T>(T value);
        T? Deserialize<T>(string json);
    }
}
