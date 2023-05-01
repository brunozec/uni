using System.ComponentModel;
using System.Runtime.CompilerServices;
using BFZ.AtividadeExtensionistaII.Viewmodels.Abstractions;
using Microsoft.Maui.Controls;

namespace BFZ.AtividadeExtensionistaII.Viewmodels.Implementations;

public class BaseViewModel : IBaseViewModel, INotifyPropertyChanged
{
    private bool _isLoading;
    private bool _isBusy;

    public bool IsLoading
    {
        get => _isLoading;
        set => _isLoading = value;
    }

    public bool IsBusy
    {
        get => _isBusy;
        set => _isBusy = value;
    }

    public async Task SetBusyAsync(
        Func<Task> function
        , string? messageBusy = null
        , bool updateIsBusyAfterFunction = true)
    {
        IsBusy = true;

        await function.Invoke();

        if (updateIsBusyAfterFunction)
            IsBusy = false;
    }

    public async Task<T> SetBusyAsync<T>(
        Func<Task<T>> function
        , string? messageBusy = null
        , bool updateIsBusyAfterFunction = true)
    {
        IsBusy = true;

        var result = await function.Invoke();

        if (updateIsBusyAfterFunction)
            IsBusy = false;

        return result;
    }

    public async Task SetLoadingAsync(
        Func<Task> function)
    {
        IsLoading = true;

        await function.Invoke();

        IsLoading = false;
    }

    public async Task<T> SetLoadingAsync<T>(
        Func<Task<T>> function)
    {
        IsLoading = true;

        var result = await function.Invoke();

        IsLoading = false;

        return result;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(
        ref T field
        , T value
        , [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}