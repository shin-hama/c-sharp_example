using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DynamicMenu
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel { Header = "Alpha" },
                new MenuItemViewModel { Header = "Beta",
                    SubMenuItems = new ObservableCollection<MenuItemViewModel>
                        {
                            new MenuItemViewModel { Header = "Beta1" },
                            new MenuItemViewModel { Header = "Beta2",
                                SubMenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "Beta1a" },
                                    new MenuItemViewModel { Header = "Beta1b" },
                                    new MenuItemViewModel { Header = "Beta1c" }
                                }
                            },
                            new MenuItemViewModel { Header = "Beta3" }
                        }
                },
                new MenuItemViewModel { Header = "Gamma" }
            };

            DataContext = this;
        }
    }

    public class MenuItemViewModel
    {
        private readonly ICommand _command;

        public MenuItemViewModel()
        {
            _command = new CommandViewModel(Execute);
        }
        public string test { get; } = "test";
        public string Header { get; set; }

        public ObservableCollection<MenuItemViewModel> SubMenuItems { get; set; }

        public ICommand Command
        {
            get
            {
                return _command;
            }
        }

        private void Execute()
        {
            // (NOTE: In a view model, you normally should not use MessageBox.Show()).
            MessageBox.Show("Clicked at " + Header);
        }
    }

    public class CommandViewModel : ICommand
    {
        private readonly Action _action;

        public CommandViewModel(Action action)
        {
            _action = action;
        }

        public void Execute(object o)
        {
            _action();
        }

        public bool CanExecute(object o)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
    }
}
