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
using System.Windows.Shapes;

namespace PersonalScheduler
{
    /// <summary>
    /// Interaction logic for VisualNotificationWindow.xaml
    /// </summary>
    public partial class VisualNotificationWindow : Window
    {
        public VisualNotificationWindow(ScheduledEvent ev)
        {
            InitializeComponent();

            visualNotificationName.Text = ev.Name;
            visualNotificationDescription.Text = ev.Description;
            visualNotificationPlace.Text = ev.Place;
        }
    }
}
