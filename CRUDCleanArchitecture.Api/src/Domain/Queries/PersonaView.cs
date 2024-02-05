using CRUDCleanArchitecture.Domain.Attributes;

namespace CRUDCleanArchitecture.Domain.Queries;
public class PersonaView
{
    public int PersonaId { get; set; }

    public int DNI { get; set; }
    [Buscador]
    public string Nombre { get; set; }
    [Buscador]
    public string Apellido { get; set; }
}
