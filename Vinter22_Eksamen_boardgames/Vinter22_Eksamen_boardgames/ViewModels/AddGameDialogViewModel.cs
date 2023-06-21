using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Vinter22_Eksamen_boardgames.Models;

namespace Vinter22_Eksamen_boardgames.ViewModels
{
    public class AddGameDialogViewModel : BindableBase, IDialogAware
    {

        private string _title = "Add new game";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private Game _newGame;

        public Game NewGame
        {
            get { return _newGame; }
            set { SetProperty(ref _newGame, value); }
        }

        

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            NewGame = ((App)Application.Current).Games;
        }

        public event Action<IDialogResult> RequestClose;

        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand => _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));

        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter.ToLower() == "true")
            {
                result = ButtonResult.OK;
                ((App)Application.Current).Games = NewGame;
            }
            else if (parameter.ToLower() == "false")
            {
                result = ButtonResult.Cancel;
            }

            RaiseRequestClose(new DialogResult(result));
        }
    }
}
