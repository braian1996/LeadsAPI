using FluentValidation;
using LeadsAPI.DTOs;
using LeadsAPI.Entidades;

namespace LeadsAPI.Helpers
{
    public class LeadValidator : AbstractValidator<LeadDTO>
    {
        public LeadValidator()
        {
            RuleFor(x => x.PlaceId).NotEmpty();
            RuleFor(x => x.AppointmentAt)
                .NotEmpty().WithMessage("La fecha de turno es requerida")
                .Must(date => date > DateTime.UtcNow)
                .WithMessage("La fecha de turno debe ser en el mayor a la actual");
            RuleFor(x => x.ServiceType)
                .Must(s => new[] { "cambio_aceite", "rotacion_neumaticos", "otro" }.Contains(s));

            RuleFor(x => x.Contact)
                .NotNull().WithMessage("El contacto es requerido")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Contact!.Name).NotEmpty().WithMessage("Nombre es requerido.");
                    RuleFor(x => x.Contact!.Email).EmailAddress().WithMessage("El Email es invalido."); ;
                    RuleFor(x => x.Contact!.Phone).NotEmpty().WithMessage("El telefono es requerido."); ;
                });

            When(x => x.Vehicle != null, () =>
            {
                RuleFor(x => x.Vehicle!.LicensePlate).NotEmpty().WithMessage("La licencia es requerida."); ;
            });
        }
    }
}
