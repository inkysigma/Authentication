using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Frame.Stores.Results
{
    public class QueryResult<T>
    {
        public bool Succeeded { get; set; }

        public int RowsModified { get; set; }

        public T Result { get; set; }
    }
}
