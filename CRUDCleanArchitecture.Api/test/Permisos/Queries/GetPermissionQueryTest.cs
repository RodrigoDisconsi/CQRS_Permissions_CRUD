namespace ApplicationTest.Permisos.Queries
{
    using System.Threading;
    using CRUDCleanArchitecture.Application.Permisos.Queries.GetPermissions;
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using CRUDCleanArchitecture.Application.Common.Interfaces.Repositories;
    using CRUDCleanArchitecture.Application.Common.Models;
    using CRUDCleanArchitecture.Application.Permisos.Queries.GetPermissions.Response;
    using CRUDCleanArchitecture.Domain.Queries.Permissions;
    using MediatR;
    using CRUDCleanArchitecture.Domain.Events;

    namespace ApplicationTest.Permisos.Query.GetPermissions
    {
        public class GetPermissionsQueryTest
        {
            private Mock<IPermissionRepository> _permissionRepositoryMock;
            private Mock<IMapper> _mapperMock;
            private Mock<IMediator> _mediatorMock;
            private GetPermissionsQueryHandler _handler;

            [SetUp]
            public void Setup()
            {
                _permissionRepositoryMock = new Mock<IPermissionRepository>();
                _mapperMock = new Mock<IMapper>();
                _mediatorMock = new Mock<IMediator>();
                _handler = new GetPermissionsQueryHandler(_permissionRepositoryMock.Object, _mapperMock.Object, _mediatorMock.Object);
            }

            [Test]
            public async Task Handle_ShouldReturnPermissionResponse()
            {
                // Arrange
                var request = new GetPermissionsQuery();
                var cancellationToken = new CancellationToken();

                var permissions = new PaginatedList<PermissionView>(new List<PermissionView>
                {
                    new PermissionView { PermisoId = 1, NombreEmpleado = "John", ApellidoEmpleado = "Doe" }
                }, 1, 1, 1);

                var permissionDtos = new List<PermissionDto>
                {
                    new PermissionDto { PermisoId = 1, NombreEmpleado = "John", ApellidoEmpleado = "Doe" }
                };

                _permissionRepositoryMock.Setup(x => x.GetPermissions(request)).ReturnsAsync(permissions);
                _mapperMock.Setup(x => x.ConfigurationProvider).Returns(new MapperConfiguration(cfg => cfg.CreateMap<PermissionView, PermissionDto>()));
                _mapperMock.Setup(x => x.Map<List<PermissionDto>>(permissions.Items)).Returns(permissionDtos);

                // Act
                var result = await _handler.Handle(request, cancellationToken);

                // Assert
                Assert.NotNull(result);
                Assert.AreEqual(permissionDtos.Count, result.Permisos.TotalCount);
                _mediatorMock.Verify(m => m.Publish(It.IsAny<GetPermissionsEvent>(), cancellationToken), Times.Once);
            }
        }
    }
}