using Noesis;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using UnityEngine;

public class MainMenuInjector : MonoBehaviour
{
    MainMenuViewModel viewModel;

    public PopupData continueMessage;
    public PopupData newGameMessage;

    private void Start()
    {
        var noesis = GetComponent<NoesisView>();

        viewModel = new MainMenuViewModel(continueMessage, newGameMessage);

        noesis.Content.DataContext = viewModel;
    }

}

public class MainMenuViewModel : BaseViewModel
{
    public PopupViewModel Popup { get; private set; }
        = new PopupViewModel();

    public float Number
    {
        get => number;
        private set
        {
            number = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand IncrementNumber { get; private set; }
    public DelegateCommand Continue { get; private set; }
    public DelegateCommand NewGame { get; private set; }

    private float number;
    private readonly PopupData continueMessage;
    private readonly PopupData newGameMessage;

    public MainMenuViewModel(PopupData continueMessage, PopupData newGameMessage)
    {
        IncrementNumber = new DelegateCommand(() => Number++);
        Continue = new DelegateCommand(OnContinue);
        NewGame = new DelegateCommand(OnNewGame);
        this.continueMessage = continueMessage;
        this.newGameMessage = newGameMessage;
    }

    private void OnNewGame()
    {
        Popup.Show(newGameMessage);
    }

    private void OnContinue()
    {
        Popup.Show(continueMessage);
    }
}

public class PopupViewModel : BaseViewModel
{
    private Visibility visibility;
    public Visibility Visibility
    {
        get { return visibility; }
        set 
        { 
            visibility = value;
            OnPropertyChanged();
        }
    }

    private string message;

    public string Message
    {
        get { return message; }
        set 
        { 
            message = value; 
            OnPropertyChanged();
        }
    }

    public DelegateCommand Dismiss { get; private set; }

    public PopupViewModel()
    {
        Dismiss = new DelegateCommand(OnDismiss);
    }

    private void OnDismiss()
    {
        Visibility = Visibility.Collapsed;
    }

    public void Show(PopupData data)
    {
        Message = data.message;
        Visibility = Visibility.Visible;
    }
}

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    // Create the OnPropertyChanged method to raise the event
    // The calling member's name will be used as the parameter.
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

public class DelegateCommand : ICommand
{
    public event System.EventHandler CanExecuteChanged;
    private Action onExecute;

    public DelegateCommand(Action onExecute)
    {
        this.onExecute = onExecute;
    }


    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        onExecute?.Invoke();
    }
}
