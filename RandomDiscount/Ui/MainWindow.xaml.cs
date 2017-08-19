using RandomDiscount.Services;
using RandomDiscount.Ui;
using System;
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

namespace RandomDiscount {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        enum State {
            Before,
            After
        }

        private State currentState;
        private RandomizerService randomizerService;

        public MainWindow() {
            InitializeComponent();
        }

        private void ButtonGame_Click(object sender, RoutedEventArgs e) {
            EnterState(
                currentState == State.Before ?
                State.After :
                State.Before
            );
        }

        private void Settings_Click(object sender, RoutedEventArgs e) {
            new SettingsWindow().Let(settings => {
                settings.Owner = this;
                settings.ShowDialog();
            });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            randomizerService = new RandomizerService();
            EnterState(State.Before);
        }

        void EnterState(State newState) {
            switch (newState) {
                case State.Before:
                    MediaBlock.Visibility = Visibility.Visible;
                    Discount.Visibility = Visibility.Collapsed;
                    ButtonGame.Content = Properties.Resources.ButtonGame;
                    break;
                case State.After:
                    CalculateDiscount();
                    MediaBlock.Visibility = Visibility.Collapsed;
                    Discount.Visibility = Visibility.Visible;
                    ButtonGame.Content = Properties.Resources.ButtonAgain;
                    break;
            }
            currentState = newState;
        }

        void CalculateDiscount() {
            //TODO - calculate
        }
    }
}
