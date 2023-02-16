using Exercise2;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Prism.Commands;
using Prism.Mvvm;

namespace Exercise2
{
    public class MainWindowViewModel : BindableBase, INotifyPropertyChanged
    {
    public MainWindowViewModel()
    {
        Family = new ObservableCollection<Agent>();
        Family.Add(new Agent("22", "Tine", "C#", "WPF"));
        Family.Add(new Agent("007", "James Bond", "Espionage", "MI6"));
        Family.Add(new Agent("42", "The Answer", "Everything", "Life, the Universe, and Everything"));
    }

        private ObservableCollection<Agent> family;
        public ObservableCollection<Agent> Family
        {
            get { return family; }
            set { SetProperty(ref family, value); }
        }

        //ObservableCollection<Agent> family;

        //public ObservableCollection<Agent> Family
        //{
        //    get { return family; }
        //    set
        //    {
        //        if (family == value)
        //        {
        //            return;
        //        }

        //        family = value;
        //        Notify();
        //    }
        //}

        private Agent currentPerson;
        public Agent CurrentPerson
        {
            get { return currentPerson; }
            set { SetProperty(ref currentPerson, value); }
        }

        //Agent currentPerson;

    //public Agent CurrentPerson
    //{
    //    get { return currentPerson; }
    //    set
    //    {
    //        if (currentPerson == value)
    //        {
    //            return;
    //        }

    //        currentPerson = value;
    //        Notify();
    //    }
    //}

    // INotifyPropertyChanged Members
    public event PropertyChangedEventHandler PropertyChanged;

    protected void Notify([CallerMemberName] string propName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

    private int currentIndex = 0;

    public int CurrentIndex
    {
        get { return currentIndex; }
        set { SetProperty(ref currentIndex, value); }
    }


        private DelegateCommand? _previusCommand;
    public DelegateCommand PreviusCommand =>
        _previusCommand ?? (_previusCommand = new DelegateCommand(ExecutePreviusCommand, CanExecutePreviusCommand).ObservesProperty(() => CurrentIndex));
    void ExecutePreviusCommand()
    {
        if (CurrentIndex > 0)
            --CurrentIndex;
    }
    bool CanExecutePreviusCommand()
    {
        if (CurrentIndex > 0)
            return true;
        else
            return false;
    }

    }
}
