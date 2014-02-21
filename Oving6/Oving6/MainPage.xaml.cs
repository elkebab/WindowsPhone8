using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Microsoft.Phone.Data.Linq.Mapping;
using Oving6.Resources;

namespace Oving6
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            DataContext db = new BooksDataContext("isostore:/bookDB.sdf");
            if (!db.DatabaseExists())
                db.CreateDatabase();

            lagre.Click += new RoutedEventHandler(lagre_Click);
            vis.Click += new RoutedEventHandler(vis_Click);
        }

        private void lagre_Click(object sender, RoutedEventArgs e)
        {
            BooksDataContext db = new BooksDataContext("isostore:/bookDB.sdf");
            Publisher pub = new Publisher()
            {
                PublisherID = "1",
                Name = "Suppeforlaget",
                City = "Skien",
                Url = "http://suppe.com"
            };
            db.Publishers.InsertOnSubmit(pub);

            Author auth = new Author()
            {
                AuthorID = "1",
                FirstName = "Lars",
                LastName = "Jensen"
            };

            db.Authors.InsertOnSubmit(auth);

            auth = new Author()
            {
                AuthorID = "2",
                FirstName = "Bob",
                LastName = "McPeenus"
            };

            db.Authors.InsertOnSubmit(auth);

            auth = new Author()
            {
                AuthorID = "3",
                FirstName = "Arne",
                LastName = "Brimi"
            };

            db.Authors.InsertOnSubmit(auth);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine("Ups... " + error);
            }     

            db = new BooksDataContext("isostore:/bookDB.sdf");

            Book theBook = new Book()
            {
                BookID = "1",
                Author = "1",
                Publisher = "1",
                PublicationDate = DateTime.Now,
                Title = "Suppeboka"
            };
            db.Books.InsertOnSubmit(theBook);

            theBook = new Book()
            {
                BookID = "2",
                Author = "2",
                Publisher = "1",
                PublicationDate = DateTime.Now,
                Title = "Internation Soup"
            };
            db.Books.InsertOnSubmit(theBook);

            theBook = new Book()
            {
                BookID = "3",
                Author = "3",
                Publisher = "1",
                PublicationDate = DateTime.Now,
                Title = "Mitt liv med sjalottlauk"
            };
            db.Books.InsertOnSubmit(theBook);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine("Ups2... " + error);
            }
        }

        private void vis_Click(object sender, RoutedEventArgs e)
        {
            BooksDataContext db = new BooksDataContext("isostore:/bookDB.sdf");
            var q = from b in db.Books
                    orderby b.Title
                    select b;
            List<Book> books = q.ToList();
            BooksLB.ItemsSource = books;
        }
    }
}