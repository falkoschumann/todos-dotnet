﻿using Todos.Contract;
using Todos.Contract.Messages;

namespace Todos.Backend.MessageHandlers
{
    public class SelectTodosQueryHandler : ISelectTodosQueryHandling
    {
        private ITodosRepository repo;

        public SelectTodosQueryHandler(ITodosRepository repo)
        {
            this.repo = repo;
        }

        public SelectTodosQueryResult Handle(SelectTodosQuery query)
        {
            var todos = repo.LoadTodos();
            return new SelectTodosQueryResult(todos);
        }
    }
}