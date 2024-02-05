using Microsoft.EntityFrameworkCore;
using CRUDCleanArchitecture.Application.Common.Interfaces;
using CRUDCleanArchitecture.Domain.Entities;
using System.Reflection;
using MediatR;

namespace CRUDCleanArchitecture.Infrastructure.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;

    public ApplicationDbContext(
        DbContextOptions options,
        IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }
    public virtual DbSet<Permiso> Permisos { get; set; }
    public virtual DbSet<TipoPermiso> TiposPermisos { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        await _mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
