namespace BFZ.AtividadeExtensionistaII.Viewmodels.Abstractions;

public interface IBaseViewModel
{
    bool IsLoading { get; set; }

    bool IsBusy { get; set; }

    Task SetBusyAsync(
        Func<Task> function
        , string? messageBusy = null
        , bool updateIsBusyAfterFunction = true);

    Task<T> SetBusyAsync<T>(
        Func<Task<T>> function
        , string? messageBusy = null
        , bool updateIsBusyAfterFunction = true);

    Task SetLoadingAsync(
        Func<Task> function);

    Task<T> SetLoadingAsync<T>(
        Func<Task<T>> function);
}