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
using System.Windows.Threading;

namespace RandomDiscount {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        /// <summary>
        /// Describes the UI state
        /// </summary>
        enum State {
            Game, // the screen with a cat
            Progress, // progress screen
            Results // the screen with a results
        }

        // contains current state of screen
        private State currentState;
        // this is where our randomizing logic is resting
        private RandomizerService randomizerService;

        // CTOR
        public MainWindow() {
            InitializeComponent();
        }

        #region Event handlers
        // when window is loaded
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            // we initialize a randomizer
            randomizerService = new RandomizerService(Properties.Settings.Default);
            // and initiate the screen ith a state with cat
            EnterState(State.Game);
        }

        // when Game gutton clicked
        private void ButtonGame_Click(object sender, RoutedEventArgs e) {
            EnterState( // we select to which state we have to move the screen
                currentState == State.Game ?
                State.Progress :
                State.Game
            );
        }

        // after click on Settings button
        private void Settings_Click(object sender, RoutedEventArgs e) {
            // we construct and open Settings screen
            new SettingsWindow().Let(settings => { // check that Let stuff, I stole it in Kotlin
                settings.Owner = this;
                settings.ShowDialog();
            });
        }
        #endregion

        void EnterState(State newState) {
            switch (newState) {
                case State.Game: // going to state with cat
                    MediaBlock.Visibility = Visibility.Visible;
                    Discount.Visibility = Visibility.Collapsed;
                    ButtonText.Text = Properties.Resources.ButtonGame;
                    progress.Visibility = Visibility.Hidden;
                    break;
                case State.Progress:
                    progress.Visibility = Visibility.Visible;
                    new DispatcherTimer().Let(timer => {
                        timer.Interval = new TimeSpan(0, 0, 2);
                        timer.Tick += (sender, e) => {
                            timer.Stop();
                            EnterState(State.Results);
                        };
                        timer.Start();
                    });

                    break;
                case State.Results: // calculater results and show result items
                    Discount.Text = $"{randomizerService.Result}%"; // our discount
                    MediaBlock.Visibility = Visibility.Collapsed;
                    Discount.Visibility = Visibility.Visible;
                    ButtonText.Text = Properties.Resources.ButtonAgain;
                    progress.Visibility = Visibility.Hidden;
                    break;
            }
            currentState = newState;
        }
     
    }
}
