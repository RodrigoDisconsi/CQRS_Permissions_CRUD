using CRUDCleanArchitecture.Application.Common.Interfaces;
using CRUDCleanArchitecture.Application.Common.Interfaces.Services;
using CRUDCleanArchitecture.Application.Common.Wrapper;
using CRUDCleanArchitecture.Application.Permisos.Commands.RequestPermission;
using CRUDCleanArchitecture.Domain.Entities;
using CRUDCleanArchitecture.Infrastructure.Persistence;
using CRUDCleanArchitecture.Infrastructure.Services.Permission;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace ApplicationTest.Permisos.Command.RequestPermission
{
    public class RequestPermissionTest
    {
        private readonly Mock<IApplicationDbContext> _context;
        private Mock<IPermissionService> _permissionServiceMock;
        private RequestPermissionCommandHandler _requestHandler;

        public RequestPermissionTest()
        {
            _context = new Mock<IApplicationDbContext>();
            _permissionServiceMock = new Mock<IPermissionService>();
            _requestHandler = new(_permissionServiceMock.Object);
        }

        [SetUp]
        public void Init()
        {
            _context.Setup(x => x.Permisos);
        }


        [Test]
        public async Task Handle_ShouldReturnSuccessResponse_WhenPermissionCreated()
        {
            // Arrange
            var request = new RequestPermissionCommand
            {
                NombreEmpleado = "John",
                ApellidoEmpleado = "Doe",
                TipoPermisoId = 1,
                FechaPermiso = DateTime.Now
            };

            var cancellationToken = new CancellationToken();

            var resp = new Response<RequestPermissionResponse>
            {
                Succeeded = true,
                Data = new RequestPermissionResponse
                {
                    PermisoId = 1
                }
            };

            _permissionServiceMock.Setup(x => x.RequestPermission(request, cancellationToken))
                .ReturnsAsync(resp);

            // Act
            var response = await _requestHandler.Handle(request, cancellationToken);

            // Assert
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            Assert.NotNull(response.Data.PermisoId);
        }

        [Test]
        public async Task Handle_ShouldReturnFailResponse_WhenExceptionThrown()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var resp = new Response<RequestPermissionResponse>
            {
                Succeeded = false
            };

            _permissionServiceMock.Setup(x => x.RequestPermission(It.IsAny<RequestPermissionCommand>(), cancellationToken))
                .ReturnsAsync(Response<RequestPermissionResponse>.Fail());

            // Act
            var response = await _requestHandler.Handle(It.IsAny<RequestPermissionCommand>(), cancellationToken);

            // Assert
            Assert.False(response.Succeeded);
            Assert.Null(response.Data);
        }

        [Test]
        public async Task RequestPermission_ShouldReturnSuccessResponse_WhenPermissionCreated()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var mockMediator = new Mock<IMediator>();

            using (var context = new ApplicationDbContext(options, mockMediator.Object))
            {
                var request = new RequestPermissionCommand
                {
                    NombreEmpleado = "John",
                    ApellidoEmpleado = "Doe",
                    TipoPermisoId = 1,
                    FechaPermiso = DateTime.Now
                };
                var cancellationToken = new CancellationToken();

                var mockLogger = new Mock<ILogger<PermissionService>>();
                var mockElastic = new Mock<IElasticService>();

                var permissionService = new PermissionService(context, mockLogger.Object);

                // Act
                var response = await permissionService.RequestPermission(request, cancellationToken);

                // Assert
                Assert.True(response.Succeeded);
                Assert.NotNull(response.Data);
                Assert.NotNull(response.Data.PermisoId);
                mockElastic.Verify(e => e.IndexDocument(It.IsAny<Permiso>()), Times.Once);
            }
        }


    }

}