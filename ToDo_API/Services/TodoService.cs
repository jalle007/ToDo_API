using Microsoft.EntityFrameworkCore;
using ToDo_API.Models;

namespace ToDo_API.Services
{
    public interface ITodoService
    {
        IEnumerable<TodoItem> GetAllItems();
        TodoItem AddItem(TodoItem item, string username);
    }

    public class TodoService : ITodoService
    {
        private readonly ToDoDbContext _context;

        public TodoService(ToDoDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TodoItem> GetAllItems()
        {
            return _context.TodoItems.Include(i => i.User).ToList();
        }

        public TodoItem AddItem(TodoItem item, string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return null;

            item.User = user;

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return item;
        }
    }

}
