using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Microsoft.Phone.Data.Linq.Mapping;

namespace Oving6
{
    class BooksDataContext : DataContext
    {
        public Table<Book> Books;
        public Table<Author> Authors;
        public Table<Publisher> Publishers;
        
        public BooksDataContext(string connection)
            : base(connection) { }
    } 
}
