﻿using BFZ.AtividadeExtensionistaII.Common.Stores;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BFZ.AtividadeExtensionistaII.Pages.Auth;

public partial class Login
{
    private bool _displayValidationErrorMessages;

    [Inject] private AuthenticationViewModel AuthenticationViewModel { get; set; }

    [Inject] private AuthStore AuthStore { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; }

    private string Error { get; set; }

    private Task HandleValidSubmit(
        EditContext arg)
    {
        _displayValidationErrorMessages = false;
        
        return Task.CompletedTask;
    }

    private Task OnInvalidSubmit(
        EditContext arg)
    {
        _displayValidationErrorMessages = true;

        return Task.CompletedTask;
    }
}