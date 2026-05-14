using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using UnityEngine;

public class MainMenuInjector : MonoBehaviour
{
    MainMenuViewModel viewModel;

    private void Start()
    {
        var noesis = GetComponent<NoesisView>();

        viewModel = new MainMenuViewModel();

        noesis.Content.DataContext = viewModel;
    }

}

public class MainMenuViewModel : BaseViewModel
{
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

    private float number;

    public MainMenuViewModel()
    {
        IncrementNumber = new DelegateCommand(() => Number++);
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
    private Action onExecute;

    public DelegateCommand(Action onExecute)
    {
        this.onExecute = onExecute;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        onExecute?.Invoke();
    }
}
