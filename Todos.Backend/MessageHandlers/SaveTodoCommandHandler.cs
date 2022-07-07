﻿using Todos.Contract;
using Todos.Contract.Data;
using Todos.Contract.Messages;

namespace Todos.Backend.MessageHandlers
{
    public class SaveTodoCommandHandler : ISaveTodoCommandHandling
    {
        private readonly ITodosRepository repo;

        public SaveTodoCommandHandler(ITodosRepository repo)
        {
            this.repo = repo;
        }

        public ICommandStatus Handle(SaveTodoCommand command)
        {
            var todos = repo.LoadTodos().ToList();
            todos = Save(todos, command.Id, command.Title);
            repo.StoreTodos(todos.ToArray());
            return new Success();
        }

        private static List<Todo> Save(List<Todo> todos, int id, string title)
        {
            if (title.Length == 0)
            {
                return todos.FindAll(t => t.ID != id).ToList();
            }

            return todos.Select(t => t.ID == id ? new Todo(t.ID, title, t.IsCompleted) : t).ToList();
        }
    }
}