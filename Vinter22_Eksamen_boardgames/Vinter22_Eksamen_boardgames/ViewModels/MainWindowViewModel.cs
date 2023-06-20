using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using Vinter22_Eksamen_boardgames.Models;
using Vinter22_Eksamen_boardgames.Data;
using System.Windows;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Controls;


namespace Vinter22_Eksamen_boardgames.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
        
        private IDialogService _dialogService;
        private readonly string _appTitle = "GameShop";
        private string _filePath = "";
        private Game? _currentGame;


        public MainWindowViewModel()
        {
            //_dialogService = dialogService;
            //_games = FileTranslator.ReadFile("games.xml");
            _allgames.Add(new Game("Chess", "A game of chess", "1990", 2, 2, 0, false));
            _allgames.Add(new Game("Monopoly", "A game of monopoly", "1990", 2, 4, 0, true));
            _allgames.Add(new Game("Risk", "A game of risk", "1990", 2, 6, 0, true));
            _allgames.Add(new Game("Catan", "A game of catan", "1990", 2, 4, 0, true));
            _currentGame = _allgames[0];

        }

        private ObservableCollection<Game>? _allgames = new ObservableCollection<Game>();

        public ObservableCollection<Game>? AllGames
        {
            get { return _allgames; }
            set { SetProperty(ref _allgames, value); }
        }


        public Game? CurrentGame
        {
            get { return _currentGame; }
            set { SetProperty(ref _currentGame, value); }
        }

        private string _filename = "";
        public string Filename
        {
            get { return _filename; }
            set
            {
                SetProperty(ref _filename, value);
                RaisePropertyChanged("Title");
            }
        }

        private bool _dirty = false;
        public bool Dirty
        {
            get { return _dirty; }
            set
            {
                SetProperty(ref _dirty, value);
                RaisePropertyChanged("Title");
            }
        }

        public string Title
        {
            get
            {
                var s = "";
                if (Dirty)
                {
                    s = "*";
                }
                return Filename + s + " - " + _appTitle;
            }
        }


        private DelegateCommand _availChange;
        public DelegateCommand AvailChange =>
            _availChange ?? (_availChange = new DelegateCommand(_availChangeExecute, CanExecuteAvailChange).ObservesProperty(() => CurrentGame));

        private void _availChangeExecute()
        {
            if (CurrentGame != null)
            {
                CurrentGame.Availability = !CurrentGame.Availability;
            }
        }

        private bool CanExecuteAvailChange()
        {
            return CurrentGame != null;
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }


        private DelegateCommand _searchCommand;
        public DelegateCommand SearchCommand =>
            _searchCommand ?? (_searchCommand = new DelegateCommand(ExecuteSearchCommand, CanExecuteSearch));

        private void ExecuteSearchCommand()
        {
            string searchText = SearchText;
            ICollectionView view = CollectionViewSource.GetDefaultView(AllGames);

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                view.Filter = item => ((Game)item).Name.ToLower().Contains(searchText.ToLower());
            }
            else
            {
                view.Filter = null;
            }

            view.Refresh(); // Refresh the view to apply the filter
        }

        private bool CanExecuteSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                return true;

            string searchTextLower = SearchText.ToLower();
            foreach (Game game in AllGames)
            {
                if (game.Name != null && game.Name.ToLower().Contains(searchTextLower))
                {
                    return true;
                }
            }

            return false;
        }




    }
}
