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

namespace Vinter22_Eksamen_boardgames.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
        private ObservableCollection<Game> _games = new ObservableCollection<Game>();
        private IDialogService _dialogService;
        private readonly string _appTitle = "GameShop";
        private string _filePath = "";
        private Game _currentGame;

        public MainWindowViewModel( IDialogService dialogService )
        {
            _dialogService = dialogService;
            //_games = FileTranslator.ReadFile("games.xml");
            _games.Add(new Game("Chess", "A game of chess", "1990", 2, 2, 0, false));
            _games.Add(new Game("Monopoly", "A game of monopoly", "1990", 2, 4, 0, true));
            _games.Add(new Game("Risk", "A game of risk", "1990", 2, 6, 0, true));
            _games.Add(new Game("Catan", "A game of catan", "1990", 2, 4, 0, true));
            _currentGame = _games[0];

        }

        public ObservableCollection<Game> Games
        {
            get { return _games; }
            set { SetProperty(ref _games, value); }
        }

        public Game CurrentGame
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

        private DelegateCommand _changeAvail;
        public DelegateCommand ChangeAvail =>
            _changeAvail ?? (_changeAvail = new DelegateCommand(_changeAvailExecute, CanExecuteChangeAvail).ObservesProperty(() => CurrentGame));

        private void _changeAvailExecute()
        {
            _currentGame.Availability = false;
            RaisePropertyChanged(nameof(Games));
        }
        private bool CanExecuteChangeAvail()
        {
            return CurrentGame != null;
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



    }
}
