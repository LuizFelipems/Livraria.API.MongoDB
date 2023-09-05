using Flunt.Notifications;
using Livraria.Domain.Interfaces.Commands;
using System;
using System.Text.Json.Serialization;

namespace Livraria.Domain.Commands.Livro.Input
{
    public class ApagarLivroCommand : Notifiable, ICommandPadrao
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                if (Id == Guid.Empty)
                    AddNotification("Id", "Id é um campo obrigatório");

                return Valid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}