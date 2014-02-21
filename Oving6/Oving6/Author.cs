using System.Data.Linq;
using System.Data.Linq.Mapping;
using Microsoft.Phone.Data.Linq.Mapping;

namespace Oving6
{
    [Table]
    public class Author
    {
        [Column(IsPrimaryKey = true)]
        public string AuthorID { get; set; }

        [Column]
        public string FirstName { get; set; }

        [Column]
        public string LastName { get; set; }
    } 
}
