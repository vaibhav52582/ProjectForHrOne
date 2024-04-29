﻿using MyApi.DataAccess;
using Dapper;
using MyApi.Models;
using System.Data.SqlClient;



namespace MyApi.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration configuration;
        private readonly string DbConnection;

        public DataAccess(IConfiguration _configuration)
        {
            configuration = _configuration;
            DbConnection = configuration["connectionStrings:DBConnect"] ?? "";
        }

        public int CreateUser(User user)
        {
            var result = 0;
            using (var connection = new SqlConnection(DbConnection))
            {
                var parameters = new
                {
                    fn = user.FirstName,
                    ln = user.LastName,
                    em = user.Email,
                    mb = user.Mobile,
                    pwd = user.Password,
                    blk = user.Blocked,
                    act = user.Active,
                    con = user.CreatedOn,
                    type = user.UserType.ToString()
                };
                var sql = "insert into Users (FirstName, LastName, Email, Mobile, Password, Blocked, Active, CreatedOn, UserType) values (@fn, @ln, @em, @mb, @pwd, @blk, @act, @con, @type);";
                result = connection.Execute(sql, parameters);
            }
            return result;
        }

        public bool IsEmailAvailable(string email)
        {
            var result = false;

            using (var connection = new SqlConnection(DbConnection))
            {
                result = connection.ExecuteScalar<bool>("select count(*) from Users where Email=@email;", new { email });
            }

            return !result;
        }

        public bool AuthenticateUser(string email, string password, out User? user)
        {
            var result = false;
            using (var connection = new SqlConnection(DbConnection))
            {
                result = connection.ExecuteScalar<bool>("select count(1) from Users where email=@email and password=@password;", new { email, password });
                if (result)
                {
                    user = connection.QueryFirst<User>("select * from Users where email=@email;", new { email });
                }
                else
                {
                    user = null;
                }
            }
            return result;
        }

        public IList<Book> GetAllBooks()
        {
            IEnumerable<Book> books = null;
            using (var connection = new SqlConnection(DbConnection))
            {
                var sql = "select * from Books;";
                books = connection.Query<Book>(sql);

                foreach (var book in books)
                {
                    sql = "select * from BookCategories where Id=" + book.CategoryId;
                    book.Category = connection.QuerySingle<BookCategory>(sql);
                }
            }
            return books.ToList();
        }

        public bool OrderBook(int userId, int bookId)
        {
            var ordered = false;

            using (var connection = new SqlConnection(DbConnection))
            {
                var sql = $"insert into Orders (UserId, BookId, OrderedOn, Returned) values ({userId}, {bookId}, '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', 0);";
                var inserted = connection.Execute(sql) == 1;
                if (inserted)
                {
                    sql = $"update Books set Ordered=1 where Id={bookId}";
                    var updated = connection.Execute(sql) == 1;
                    ordered = updated;
                }
            }

            return ordered;
        }

        public IList<User> GetUsers()
        {
            IEnumerable<User> users;
            using (var connection = new SqlConnection(DbConnection))
            {
                users = connection.Query<User>("select * from Users;");

                var listOfOrders =
                    connection.Query("select u.Id as UserId, o.BookId as BookId, o.OrderedOn as OrderDate, o.Returned as Returned from Users u LEFT JOIN Orders o ON u.Id=o.UserId;");

            }
            return users.ToList();
        }

        public void BlockUser(int userId)
        {
            using var connection = new SqlConnection(DbConnection);
            connection.Execute("update Users set Blocked=1 where Id=@Id", new { Id = userId });
        }

        public void UnblockUser(int userId)
        {
            using var connection = new SqlConnection(DbConnection);
            connection.Execute("update Users set Blocked=0 where Id=@Id", new { Id = userId });
        }

        public void ActivateUser(int userId)
        {
            using var connection = new SqlConnection(DbConnection);
            connection.Execute("update Users set Active=1 where Id=@Id", new { Id = userId });
        }

        public void DeactivateUser(int userId)
        {
            using var connection = new SqlConnection(DbConnection);
            connection.Execute("update Users set Active=0 where Id=@Id", new { Id = userId });
        }

        public IList<BookCategory> GetAllCategories()
        {
            IEnumerable<BookCategory> categories;

            using (var connection = new SqlConnection(DbConnection))
            {
                categories = connection.Query<BookCategory>("select * from BookCategories;");
            }

            return categories.ToList();
        }

        public void InsertNewBook(Book book)
        {
            using var conn = new SqlConnection(DbConnection);
            var sql = "select Id from BookCategories where Category=@cat and SubCategory=@subcat";
            var parameter1 = new
            {
                cat = book.Category.Category,
                subcat = book.Category.SubCategory
            };
            var categoryId = conn.ExecuteScalar<int>(sql, parameter1);

            sql = "insert into Books (Title, Author, Price, Ordered, CategoryId) values (@title, @author, @price, @ordered, @catid);";
            var parameter2 = new
            {
                title = book.Title,
                author = book.Author,
                price = book.Price,
                ordered = false,
                catid = categoryId
            };
            conn.Execute(sql, parameter2);
        }

        public bool DeleteBook(int bookId)
        {
            var deleted = false;
            using (var connection = new SqlConnection(DbConnection))
            {
                var sql = $"delete Books where Id={bookId}";
                deleted = connection.Execute(sql) == 1;
            }
            return deleted;
        }

      
    }
}