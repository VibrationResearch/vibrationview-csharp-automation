using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VibrationVIEWControlPanel
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private VibrationVIEWAPI _vibrationVIEW;
		public MainWindow()
		{
			InitializeComponent();
			//Initialize API
			_vibrationVIEW = new VibrationVIEWAPI();
			//Set DataContext so that Bindings from the XAML work
			DataContext = this;
			//Initialize commands
			OpenTestCommand = new RelayCommand(OpenTest);
			StartTestCommand = new RelayCommand(StartTest);
			StopTestCommand = new RelayCommand(StopTest);
			SaveDataCommand = new RelayCommand(SaveData);
			GetStatusCommand = new RelayCommand(GetStatus);
			ReadChannelsCommand = new RelayCommand(ReadChannels);
		}

		public ICommand OpenTestCommand { get; }
		public ICommand StartTestCommand { get; }
		public ICommand StopTestCommand { get; }
		public ICommand SaveDataCommand { get; }
		public ICommand GetStatusCommand { get; }
		public ICommand ReadChannelsCommand { get; }

		private void OpenTest()
		{
			_vibrationVIEW.OpenTest();
		}

		private void StartTest()
		{
			_vibrationVIEW.StartTest();
		}

		private void StopTest()
		{
			_vibrationVIEW.StopTest();
		}


		private void SaveData()
		{
			_vibrationVIEW.SaveData();
		}

		private void ReadChannels()
		{
			List<string> channelLabels = _vibrationVIEW.GetAllChannelInfo();
			channelReadingsList.Items.Clear();
			foreach (string channel in channelLabels)
			{
				channelReadingsList.Items.Add(channel);
			}
		}

		private void GetStatus()
		{
			var (status, code) = _vibrationVIEW.GetStatus();
			stopCodeLabel.Text = status;
		}
	}
}