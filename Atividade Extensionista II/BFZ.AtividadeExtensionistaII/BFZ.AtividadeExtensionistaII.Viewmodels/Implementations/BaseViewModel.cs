using BFZ.AtividadeExtensionistaII.Viewmodels.Abstractions;
using Microsoft.Maui.Controls;

namespace BFZ.AtividadeExtensionistaII.Viewmodels.Implementations;

public class BaseViewModel : BindableObject, IBaseViewModel
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
}