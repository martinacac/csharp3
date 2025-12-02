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

    public interface IRepositoryAsync<T>
    where T : class
    {
        public Task CreateAsync(T item);
        // Read
        public Task<T?> ReadByIdAsync(int id); //může vrátit nulovou hodnotu (nullable)
        public Task<IEnumerable<T>> ReadAllAsync();

        // Update
        public Task UpdateAsync(T item);

        public Task DeleteByIdAsync(int id);
    }
}
