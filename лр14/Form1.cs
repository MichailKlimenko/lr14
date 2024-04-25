using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace лр14
{
    public partial class Form1 : Form
    {
        private List<Book> library = new List<Book>(); // Хранение списка книг
        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadSampleBooks();
        }

        // Инициализация DataGridView
        private void InitializeDataGridView()
        {
            // Установка столбцов
            dataGridView1.ColumnCount = 9;
            dataGridView1.Columns[0].Name = "Формат";
            dataGridView1.Columns[1].Name = "Размер файла";
            dataGridView1.Columns[2].Name = "Название";
            dataGridView1.Columns[3].Name = "УДК";
            dataGridView1.Columns[4].Name = "Кол-во страниц";
            dataGridView1.Columns[5].Name = "Издательство";
            dataGridView1.Columns[6].Name = "Год";
            dataGridView1.Columns[7].Name = "Автор";
            dataGridView1.Columns[8].Name = "Дата загрузки";
        }

        // Метод для загрузки книг в DataGridView
        private void LoadBooksIntoDataGridView(List<Book> books)
        {
            dataGridView1.Rows.Clear();
            foreach (var book in books)
            {
                dataGridView1.Rows.Add(
                    book.Format,
                    book.FileSize,
                    book.Title,
                    book.UDK,
                    book.PageCount,
                    book.Publisher,
                    book.Year,
                    string.Join(", ", book.Authors.Select(a => a.FullName)),
                    book.UploadDate.ToShortDateString()
                );
            }
        }

        // Метод для добавления примеров книг в библиотеку
        private void LoadSampleBooks()
        {
            // Создание авторов
            Author author1 = new Author { FullName = "Иван Петров", Country = "Россия", ID = 1 };
            Author author2 = new Author { FullName = "Анна Иванова", Country = "США", ID = 2 };
            Author author3 = new Author { FullName = "Александр Сидоров", Country = "Франция", ID = 3 };
            Author author4 = new Author { FullName = "Елена Козлова", Country = "Германия", ID = 4 };

            // Создание книг
            Book book1 = new Book
            {
                Format = "PDF",
                FileSize = 1024,
                Title = "Путешествие по времени",
                UDK = "123.456",
                PageCount = 300,
                Publisher = "Фантастическое издательство",
                Year = 2022,
                Authors = new List<Author> { author1, author2 },
                UploadDate = DateTime.Now
            };

            Book book2 = new Book
            {
                Format = "EPUB",
                FileSize = 2048,
                Title = "Тайны древних племен",
                UDK = "789.012",
                PageCount = 250,
                Publisher = "Научное издательство",
                Year = 2019,
                Authors = new List<Author> { author2, author3 },
                UploadDate = DateTime.Now.AddDays(-30)
            };

            Book book3 = new Book
            {
                Format = "MOBI",
                FileSize = 1536,
                Title = "Секреты космоса",
                UDK = "345.678",
                PageCount = 400,
                Publisher = "Космическое издательство",
                Year = 2020,
                Authors = new List<Author> { author1, author4 },
                UploadDate = DateTime.Now.AddDays(-60)
            };

            Book book4 = new Book
            {
                Format = "PDF",
                FileSize = 512,
                Title = "Загадочный остров",
                UDK = "987.654",
                PageCount = 200,
                Publisher = "Приключенческое издательство",
                Year = 2017,
                Authors = new List<Author> { author3 },
                UploadDate = DateTime.Now.AddDays(-90)
            };

            Book book5 = new Book
            {
                Format = "EPUB",
                FileSize = 1024,
                Title = "Таинственные твари",
                UDK = "321.654",
                PageCount = 350,
                Publisher = "Фэнтезийное издательство",
                Year = 2023,
                Authors = new List<Author> { author4 },
                UploadDate = DateTime.Now.AddDays(-120)
            };

            // Добавление книг в список библиотеки
            library.Add(book1);
            library.Add(book2);
            library.Add(book3);
            library.Add(book4);
            library.Add(book5);

            // Обновление DataGridView
            LoadBooksIntoDataGridView(library);
        }

        // Метод для поиска книги по заданным критериям
        private List<Book> SearchBooks(string publisher, int year, int pageCountFrom, int pageCountTo)
        {
            var results = library.Where(book =>
                (string.IsNullOrEmpty(publisher) || book.Publisher.ToLower().Contains(publisher.ToLower())) &&
                (year == 0 || book.Year == year) &&
                (pageCountFrom == 0 || book.PageCount >= pageCountFrom) &&
                (pageCountTo == 0 || book.PageCount <= pageCountTo)
            ).ToList();

            return results;
        }

        // Метод для сортировки книг по автору и дате загрузки
        private void SortBooks(string authorName, DateTime uploadDate)
        {
            if (!string.IsNullOrEmpty(authorName))
                library = library.OrderBy(book => book.Authors.Any(author => author.FullName.ToLower().Contains(authorName.ToLower()))).ToList();

            if (uploadDate != DateTime.MinValue)
                library = library.OrderBy(book => book.UploadDate).ToList();

            LoadBooksIntoDataGridView(library);
        }

        // Обработчик события нажатия кнопки для поиска книг
        private void button1_Click(object sender, EventArgs e)
        {
            string publisher = textBox1.Text;
            int year = string.IsNullOrEmpty(textBox2.Text) ? 0 : int.Parse(textBox2.Text);
            int pageCountFrom = string.IsNullOrEmpty(textBox3.Text) ? 0 : int.Parse(textBox3.Text);
            int pageCountTo = string.IsNullOrEmpty(textBox4.Text) ? 0 : int.Parse(textBox4.Text);

            var results = SearchBooks(publisher, year, pageCountFrom, pageCountTo);
            LoadBooksIntoDataGridView(results);
        }

        // Обработчик события нажатия кнопки для сортировки книг
        private void button2_Click(object sender, EventArgs e)
        {
            string authorName = textBox5.Text;
            DateTime uploadDate = dateTimePicker1.Value;

            SortBooks(authorName, uploadDate);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Очистка текстовых полей и элемента DateTimePicker
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            dateTimePicker1.Value = DateTime.Now;

            // Очистка DataGridView и загрузка начального списка книг
            library.Clear();
            LoadSampleBooks();
        }
    }
}
