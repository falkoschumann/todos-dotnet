using System;
using System.Windows;
using System.Windows.Controls;

namespace Todos.Frontend
{
    /// <summary>
    /// Interaktionslogik für Footer.xaml
    /// </summary>
    public partial class Footer : UserControl
    {
        public event Action<Filter> OnFilterChanged;
        public event Action OnClearCompleted;

        public Footer()
        {
            InitializeComponent();
        }

        public Filter Filter { get; set; } = Filter.All;

        public int ActiveCount
        {
            set { activeCount.Text = $"{value} {Utils.Pluralize(value, "item")} left"; }
        }

        public int CompletedCount
        {
            set { clearCompleted.Visibility = value > 0 ? Visibility.Visible : Visibility.Hidden; }
        }

        private void HandleFilterAll(object sender, RoutedEventArgs e)
        {
            Filter = Filter.All;
            OnFilterChanged(Filter);
        }

        private void HandleFilterActive(object sender, RoutedEventArgs e)
        {
            Filter = Filter.Active;
            OnFilterChanged(Filter);
        }

        private void HandleFilterCompleted(object sender, RoutedEventArgs e)
        {
            Filter = Filter.Completed;
            OnFilterChanged(Filter);
        }

        private void HandleClearCompleted(object sender, RoutedEventArgs e)
        {
            OnClearCompleted();
        }
    }
}
