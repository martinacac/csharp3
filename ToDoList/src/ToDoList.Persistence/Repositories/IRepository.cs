namespace ToDoList.Persistence.Repositories
{


    public interface IRepository<T>
    where T : class
    {
        public void Create(T item);
        // Read
        public T? ReadById(int id); //může vrátit nulovou hodnotu (nullable)
        public IEnumerable<T> ReadAll();

        // Update
        public void Update(T item);

        public void DeleteById(int id);
    }
}
