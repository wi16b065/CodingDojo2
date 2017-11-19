using GalaSoft.MvvmLight;
using Shared.BaseModels;
using Shared.Interfaces;
using Simulation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace CodingDojo3.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        


        public List<ItemViewModel> Items { get; set; }

        //kennen sich zur Laufzeit nicht
        //private Simulator sim = new Simulator(items);

        private string currentTime;

        public string CurrentTime
        {
            get { return currentTime; }
            set { currentTime = value; RaisePropertyChanged("CurrentTime"); }
        }

        private string currentDate;

        public string CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; RaisePropertyChanged("CurrentDate"); }
        }

        private DispatcherTimer _dateTimer;

        public ObservableCollection<ItemViewModel> SensorList { get; set; }
        public ObservableCollection<ItemViewModel> ActorList { get; set; }
        public ObservableCollection<string> ModeSelectionList { get; }

        public MainViewModel()
        {
            this.Items = new List<ItemViewModel>();
            SensorList = new ObservableCollection<ItemViewModel>();
            ActorList = new ObservableCollection<ItemViewModel>();
            ModeSelectionList = new ObservableCollection<string>();

            //Fill ModeSelectionList
            foreach (var item in Enum.GetNames(typeof(SensorModeType)))
            {
                ModeSelectionList.Add(item);
            }
            foreach (var item in Enum.GetNames(typeof(ModeType)))
            {
                ModeSelectionList.Add(item);

            }

            this._dateTimer = new DispatcherTimer();
            this._dateTimer.Interval = TimeSpan.FromSeconds(1);
            //füge eine Methode hinzu die nach Interval ausgeführt wird
            this._dateTimer.Tick += _dateTimer_Tick;
            this._dateTimer.Start();
            LoadData();

        }

        private void _dateTimer_Tick(object sender, EventArgs e)
        {
            var timestamp = DateTime.Now;

            this.CurrentDate = timestamp.ToString("dd.MM.yyyy");
            this.CurrentTime = timestamp.ToString("HH:mm:ss");
        }

        private void LoadData()
        {
            Simulator sim = new Simulator(Items);
            foreach (var item in sim.Items)
            {
                if (item.ItemType.Equals(typeof(ISensor)))
                    SensorList.Add(item);
                else if (item.ItemType.Equals(typeof(IActuator)))
                    ActorList.Add(item);
            }
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
            CurrentDate = DateTime.Now.ToLocalTime().ToShortDateString();
        }
    }
}