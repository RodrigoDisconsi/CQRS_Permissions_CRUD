using CRUDCleanArchitecture.Application.Common.Interfaces.Services;
using CRUDCleanArchitecture.Application.Common.Wrapper;
using CRUDCleanArchitecture.Application.Permisos.Commands.ModifyPermission;
using CRUDCleanArchitecture.Domain.Entities;
using CRUDCleanArchitecture.Infrastructure.Persistence;
using CRUDCleanArchitecture.Infrastructure.Services.Permission;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace ApplicationTest.Permisos.Command.ModifyPermission
{
    public class ModifyPermissionTest
    {
        private Mock<IPermissionService> _permissionServiceMock;
        private ModifyPermissionCommandHandler _handler;
        private DbContextOptions<ApplicationDbContext> _options;

        [SetUp]
        public void Setup()
        {
            _permissionServiceMock = new Mock<IPermissionService>();
            _handler = new ModifyPermissionCommandHandler(_permissionServiceMock.Object);

            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Test]
        public async Task Handle_ShouldReturnSuccessResponse_WhenPermissionModified()
        {
            // Arrange
            var request = new ModifyPermissionCommand
            {
                PermissionId = 1,
                NombreEmpleado = "John",
                ApellidoEmpleado = "Doe",
                TipoPermisoId = 1,
                FechaPermiso = DateTime.Now
            };

            var cancellationToken = new CancellationToken();

            _permissionServiceMock.Setup(x => x.UpdatePermission(request, cancellationToken)).ReturnsAsync(Response<ModifyPermissionResponse>.Success());

            // Act
            var response = await _handler.Handle(request, cancellationToken);

            // Assert
            Assert.True(response.Succeeded);
            _permissionServiceMock.Verify(x => x.UpdatePermission(request, cancellationToken), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnFailResponse_WhenPermissionNotFound()
        {
            // Arrange
            var request = new ModifyPermissionCommand
            {
                PermissionId = 1,
                NombreEmpleado = "John",
                ApellidoEmpleado = "Doe",
                TipoPermisoId = 1,
                FechaPermiso = DateTime.Now
            };

            var cancellationToken = new CancellationToken();

            _permissionServiceMock.Setup(x => x.UpdatePermission(request, cancellationToken)).ReturnsAsync(Response<ModifyPermissionResponse>.Fail());

            // Act
            var response = await _handler.Handle(request, cancellationToken);

            // Assert
            Assert.False(response.Succeeded);
            _permissionServiceMock.Verify(x => x.UpdatePermission(request, cancellationToken), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldUpdatePermissionInDatabaseAndIndexDocument_WhenPermissionModified()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var mockMediator = new Mock<IMediator>();

            var request = new ModifyPermissionCommand
            {
                PermissionId = 1,
                NombreEmpleado = "John",
                ApellidoEmpleado = "Doe",
                TipoPermisoId = 1,
                FechaPermiso = DateTime.Now
            };

            using (var context = new ApplicationDbContext(_options, mockMediator.Object))
            {
                var permiso = new Permiso
                {
                    Id = request.PermissionId,
                    NombreEmpleado = "OldName",
                    ApellidoEmpleado = "OldLastName",
                    TipoPermisoId = 1,
                    FechaPermiso = DateTime.Now.AddDays(-1)
                };

                context.Permisos.Add(permiso);
                await context.SaveChangesAsync(cancellationToken);
            }

            var mockLogger = new Mock<ILogger<PermissionService>>();
            var mockElastic = new Mock<IElasticService>();

            // Act
            using (var context = new ApplicationDbContext(_options, mockMediator.Object))
            {
                var permissionService = new PermissionService(context, mockLogger.Object);
                var response = await permissionService.UpdatePermission(request, cancellationToken);

                // Assert
                Assert.True(response.Succeeded);

                var updatedPermiso = await context.Permisos.FindAsync(request.PermissionId);
                Assert.NotNull(updatedPermiso);
                Assert.AreEqual(request.NombreEmpleado, updatedPermiso.NombreEmpleado);
                Assert.AreEqual(request.ApellidoEmpleado, updatedPermiso.ApellidoEmpleado);
                Assert.AreEqual(request.TipoPermisoId, updatedPermiso.TipoPermisoId);
                Assert.AreEqual(request.FechaPermiso, updatedPermiso.FechaPermiso);
            }

            mockElastic.Verify(e => e.IndexDocument(It.IsAny<Permiso>()), Times.Once);
        }
    }
}