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
using System.Runtime.CompilerServices;
using System.IO;
using System.Numerics;


namespace Vinter22_Eksamen_boardgames.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
        
        private IDialogService _dialogService;
        private readonly string _appTitle = "GameShop";
        private string _filePath = "";
        private Game? _currentGame;
        private ObservableCollection<Game>? _allgames = new ObservableCollection<Game>();


        public MainWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            //_games = FileTranslator.ReadFile("games.xml");
            _allgames.Add(new Game("Chess", "A game of chess", "1990", 2, 2, 0, false));
            _allgames.Add(new Game("Monopoly", "A game of monopoly", "1990", 2, 4, 0, true));
            _allgames.Add(new Game("Risk", "A game of risk", "1990", 2, 6, 0, true));
            _allgames.Add(new Game("Catan", "A game of catan", "1990", 2, 4, 0, true));
            _currentGame = _allgames[0];

        }

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

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        private DelegateCommand _searchCommand;
        public DelegateCommand SearchCommand =>
            _searchCommand ?? (_searchCommand = new DelegateCommand(ExecuteSearchCommand, CanExecuteSearch));

        private void ExecuteSearchCommand()
        {
            string searchText = SearchText;
            ICollectionView view = CollectionViewSource.GetDefaultView(AllGames);

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                //OnPropertyChanged(nameof(SearchText));
                //RaisePropertyChanged("SearchText");
                //RaisePropertyChanged("AllGames");
                view.Filter = item => ((Game)item).Name.ToLower().Contains(searchText.ToLower());
                _currentGame = _allgames[0];
                //Dirty = true;
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

            //return true;

        }

        //dialogshandlers
        private DelegateCommand _addGCommand;
        public DelegateCommand AddGCommand =>
            _addGCommand ?? (_addGCommand = new DelegateCommand(AddGExecute));

        private void AddGExecute()
        {
            var tempGame = new Game("Chess", "A game of chess", "1990", 2, 2, 0, false);
            ((App)Application.Current).Games = tempGame;

            _dialogService.ShowDialog("AddGameDialog", null, r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    _allgames.Add(tempGame);
                    CurrentGame = tempGame;
                    Dirty = true;
                }
            });
        }



        //save stuff

        private DelegateCommand _newFileCommand;

        public DelegateCommand NewFileCommand =>
            _newFileCommand ?? (_newFileCommand = new DelegateCommand(NewFileCommandExecute));

        private void NewFileCommandExecute()
        {
            MessageBoxResult res = MessageBox.Show("Any unsaved data will be lost. Are you sure you want to initiate a new file?", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                AllGames.Clear();
                Filename = "";
                Dirty = false;
            }
        }

        DelegateCommand _saveFileAsCommand;

        public DelegateCommand SaveFileAsCommand =>
            _saveFileAsCommand ?? (_saveFileAsCommand = new DelegateCommand(SaveFileAsCommandExecute));

        private void SaveFileAsCommandExecute()
        {
            var dlg = new SaveFileDialog
            {
                Filter = "The Debt Book documents|*.debt|All Files|*.*",
                DefaultExt = "debt"
            };
            if (_filePath == "")
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dlg.InitialDirectory = Path.GetDirectoryName(_filePath);

            if (dlg.ShowDialog(App.Current.MainWindow) == true)
            {
                _filePath = dlg.FileName;
                Filename = Path.GetFileName(_filePath);
                SaveFile();
            }
        }

        DelegateCommand _saveFileCommand;

        public DelegateCommand SaveFileCommand => _saveFileCommand ?? (_saveFileCommand =
            new DelegateCommand(SaveFileCommandExecute, CanExecuteSaveFileCommand)
                .ObservesProperty(() => AllGames.Count));

        private void SaveFileCommandExecute()
        {
            SaveFile();
        }

        private bool CanExecuteSaveFileCommand()
        {
            return (_filename != "") && (AllGames.Count > 0);
        }

        private void SaveFile()
        {
            try
            {
                FileTranslator.SaveFile(_filePath, AllGames);
                Dirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to save file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private DelegateCommand _openFileCommand;

        public DelegateCommand OpenFileCommand =>
            _openFileCommand ?? (_openFileCommand = new DelegateCommand(OpenFileCommandExecute));

        private void OpenFileCommandExecute()
        {
            var dlg = new OpenFileDialog()
            {
                Filter = "The Debt Book documents|*.debt|All Files|*.*",
                DefaultExt = "debt"
            };
            if (_filePath == "")
            {
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                dlg.InitialDirectory = Path.GetDirectoryName(_filePath);
            }

            if (dlg.ShowDialog() == true)
            {
                _filePath = dlg.FileName;
                Filename = Path.GetFileName(_filePath);
                try
                {
                    AllGames = FileTranslator.ReadFile(_filePath);
                    Dirty = false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
