using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace RandomDiscount.Ui {
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window {
        public SettingsWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.Let(props => {
                SliderMax.Value = props.MaxDiscount;
                SliderMin.Value = props.MinDiscount;
                SliderFactor.Value = props.DiscountFactor;
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if(SliderMax.Value > SliderMin.Value) {
                Properties.Settings.Default.Let(props => {
                    props.MaxDiscount = (int)SliderMax.Value;
                    props.MinDiscount = (int)SliderMin.Value;
                    props.DiscountFactor = (int)SliderFactor.Value;
                    props.Save();
                });
            } else {
                e.Cancel = true;
                MessageBox.Show("Максимум меньше минимума", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
