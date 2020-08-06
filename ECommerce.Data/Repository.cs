using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data
{
    public class Repository <T> where T : class, IRepogitory<T> 
    {
    }
}
