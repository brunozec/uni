using System.Resources;
using BFZ.AtividadeExtensionistaII.Common.Resources;
using Syncfusion.Blazor;

namespace BFZ.AtividadeExtensionistaII.Common;

public class SyncfusionLocalizer : ISyncfusionStringLocalizer
{
    public string GetText(
        string key)
    {
        return ResourceManager.GetString(key);
    }

    public ResourceManager ResourceManager
    {
        get
        {
            // Replace the ApplicationNamespace with your application name.
            return SfResources.ResourceManager;
        }
    }
}