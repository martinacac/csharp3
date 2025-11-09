namespace ToDoList.Persistence.Repositories
{


    public interface IRepository<T>
    where T : class
    {
        public void Create(T item); //doplnit
        // Read
        T? GetById(int id);
        IEnumerable<T> GetAll();

        // Update
        void Update(T item);

        void Add(T entity);

        void DeleteById(int id);
    }
}
