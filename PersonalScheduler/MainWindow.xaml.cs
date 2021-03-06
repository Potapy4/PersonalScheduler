﻿using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Windows;
using System.Windows.Threading;

namespace PersonalScheduler
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		EventManager _eventManager;       

		public MainWindow()
		{
			InitializeComponent();

			_eventManager = new EventManager();

            // Assign event handlers here, so that event listbox is kept in sync
            // with the list inside _eventManager

            _eventManager.onAdd += UpdateUI;
            _eventManager.onDelete += UpdateUI;
			
			// The following function initializes a one-second regular timer, the handler will
			// call _eventManager.ProcessEvents - check template code below
			SetupTimer();
		}

        private void UpdateUI()
        {
            listBoxEvents.Items.Clear();
            _eventManager.ScheduledEvents.ForEach(x => listBoxEvents.Items.Add(x));            
        }

        #region TemplateCode

        DispatcherTimer _timer;

		private void SetupTimer()
		{
			_timer = new DispatcherTimer();
			_timer.Interval = new TimeSpan(0, 0, 1);
			_timer.Tick += Timer_Tick;
			_timer.Start();
		}

		private void buttonAdd_Click(object sender, RoutedEventArgs e)
		{
			EventInfoWindow infoWindow = new EventInfoWindow(_eventManager);
			infoWindow.ShowDialog();
		}

		private void buttonRemove_Click(object sender, RoutedEventArgs e)
		{
            if (listBoxEvents.SelectedItem != null)
            {               
               _eventManager.RemoveEvent(listBoxEvents.SelectedItem as ScheduledEvent);
            }
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
            try
            {
                _eventManager.ProcessEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		bool _finalShutdown = false;
		bool _firstTimeHint = true;
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!_finalShutdown)
			{
				e.Cancel = true;
				Hide();
				taskBarIcon.Visibility = Visibility.Visible;
				if (_firstTimeHint)
				{
					taskBarIcon.ShowBalloonTip("Personal Scheduler", "The application is still running",
						BalloonIcon.Info);
					_firstTimeHint = false;
				}
			}
		}

		private void MenuItemExit_Click(object sender, RoutedEventArgs e)
		{
			_finalShutdown = true;
			taskBarIcon.Dispose();
			Application.Current.Shutdown();
		}

		private void MenuItemRestore_Click(object sender, RoutedEventArgs e)
		{
			Show();
			taskBarIcon.Visibility = Visibility.Hidden;
		}
        #endregion
    }
}
