using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.Utils
{
    public delegate void onFailure<T, U, V>(T item, U error, V value);
    public delegate void onPreActionCallback();
}
