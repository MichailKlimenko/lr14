using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace лр14
{
    // Класс для хранения информации о книге
    public class Book
    {
        public string Format { get; set; }
        public int FileSize { get; set; }
        public string Title { get; set; }
        public string UDK { get; set; }
        public int PageCount { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
        public List<Author> Authors { get; set; }
        public DateTime UploadDate { get; set; }
    }

    // Класс для хранения информации об авторе
    public class Author
    {
        public string FullName { get; set; }
        public string Country { get; set; }
        public int ID { get; set; }
    }
}
