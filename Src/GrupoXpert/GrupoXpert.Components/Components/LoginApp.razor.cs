using GrupoXpert.Components.Models;
using Microsoft.AspNetCore.Components;

namespace GrupoXpert.Components.Components
{
    public partial class LoginApp : ComponentBase
    {
        /// <summary>
        /// Define el icono del botón
        /// </summary>
        [Parameter]
        public EventCallback<Credential> OnLogin { get; set; }

        private string user = "usuario";

        private string pass = string.Empty;

        /// <summary>
        /// Evento click del login
        /// </summary>
        private async Task OnClickHandler()
        {
            try
            {
                if (OnLogin.HasDelegate)
                {
                    Credential credential = new Credential()
                    {
                        Password = pass,
                        User = user
                    };
                    await OnLogin.InvokeAsync(credential);
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
