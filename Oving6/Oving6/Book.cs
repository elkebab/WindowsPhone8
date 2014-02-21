using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Microsoft.Phone.Data.Linq.Mapping;

namespace Oving6
{
    [Table]
    public class Book
    {
        [Column(IsPrimaryKey = true)]
        public string BookID 
        { 
            get;
            set; 
        }

        [Column]
        public string Title 
        { 
            get; 
            set; 
        }

        [Column]
        public string Author 
        { 
            get; 
            set; 
        }

        [Column]
        public string Publisher { 
            get; 
            set; 
        }

        [Column]
        public DateTime PublicationDate 
        { 
            get; 
            set; 
        }
    } 
}
