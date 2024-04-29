using MyApi.DataAccess;
using MyApi.Models;


namespace MyApi.DataAccess
{
    public interface IDataAccess
    {
        int CreateUser(User user);
        bool IsEmailAvailable(string email);
        bool AuthenticateUser(string email, string password, out User? user);
        IList<Book> GetAllBooks();
        IList<User> GetUsers();
        void BlockUser(int userId);
        void UnblockUser(int userId);
        void DeactivateUser(int userId);
        void ActivateUser(int userId);
        IList<BookCategory> GetAllCategories();
        void InsertNewBook(Book book);
        bool DeleteBook(int bookId);
    
    }
}