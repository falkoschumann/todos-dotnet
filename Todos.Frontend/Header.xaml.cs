﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Todos.Frontend
{
    /// <summary>
    /// Interaktionslogik für Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public event Action<string> OnAddTodo;

        public Header()
        {
            InitializeComponent();
        }

        public void FocusNewTodo()
        {
            newTodo.Focus();
        }

        private void HandleNewTodoKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                return;
            }

            var textBox = (TextBox)sender;
            var title = textBox.Text.Trim();
            if (title.Length == 0)
            {
                return;
            }

            OnAddTodo(title);
            textBox.Text = "";
        }
    }
}
