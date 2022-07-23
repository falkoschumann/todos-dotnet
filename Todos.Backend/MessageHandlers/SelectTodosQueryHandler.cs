using Todos.Contract;
using Todos.Contract.Messages;

namespace Todos.Backend.MessageHandlers
{
    public class SelectTodosQueryHandler : ISelectTodosQueryHandling
    {
        private readonly ITodosRepository _repo;

        public SelectTodosQueryHandler(ITodosRepository repo)
        {
            _repo = repo;
        }

        public SelectTodosQueryResult Handle(SelectTodosQuery query)
        {
            var todos = _repo.LoadTodos();
            return new SelectTodosQueryResult(todos);
        }
    }
}
