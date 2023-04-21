﻿using BFZ.AtividadeExtensionistaII.Domain.Models;
using BFZ.AtividadeExtensionistaII.Viewmodels.Abstractions.Auth;

namespace BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;

public class LoginViewModel : BaseViewModel, ILoginViewModel
{
    public AutenticationRequest AutenticationRequest { get; set; } = new AutenticationRequest();

}