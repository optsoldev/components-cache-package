using FluentAssertions;
using Moq;
using Optsol.Components.Cache.Connections;
using Optsol.Components.Cache.Services;
using Optsol.Components.Cache.Settings;
using System.Collections.Generic;
using Xunit;

namespace Optsol.Components.Cache.Test.Services
{
    public class CacheServiceSpec
    {
#if DEBUG
        [Fact(DisplayName = "Deve criar o serviço de cache")]
#elif RELEASE
        [Fact(DisplayName = "Deve criar o serviço de cache", Skip = "redis local docker test")]
#endif
        public void Deve_Criar_Servico()
        {
            //given
            var cacheSettingsMock = new Mock<CacheSettings>();
            cacheSettingsMock
                .SetupGet(settings => settings.ConnectionString)
                .Returns("localhost,password=OPTSOL@dev");

            var cacheConnection = new Mock<DefaultConnection>(cacheSettingsMock.Object);

            //when
            var cacheService = new CacheService(cacheConnection.Object);

            //then
            cacheService.Should().NotBeNull();
        }


#if DEBUG
        [Fact(DisplayName = "Deve criar um cache persistente no redis")]
#elif RELEASE
        [Fact(DisplayName = "Deve criar um cache persistente no redis", Skip = "redis local docker test")]
#endif
        public void Deve_Criar_Cache_Persistente()
        {
            //given
            var cacheSettingsMock = new Mock<CacheSettings>();
            cacheSettingsMock
                .SetupGet(settings => settings.ConnectionString)
                .Returns("localhost,password=OPTSOL@dev");

            var cacheConnection = new Mock<DefaultConnection>(cacheSettingsMock.Object);
            var cacheService = new CacheService(cacheConnection.Object);

            var valor = new { Id = 1, Valor = "Algum valor 1" };
            var keyValue = new KeyValuePair<string, object>("Persistente", valor);

            //when
            cacheService.Save(keyValue);

            //then
            cacheService.Should().NotBeNull();
        }

#if DEBUG
        [Fact(DisplayName = "Deve criar um cache temporário no redis")]
#elif RELEASE
        [Fact(DisplayName = "Deve criar um cache temporário no redis", Skip = "redis local docker test")]
#endif
        public void Deve_Criar_Cache_Temporario()
        {
            //given
            var cacheSettingsMock = new Mock<CacheSettings>();
            cacheSettingsMock
                .SetupGet(settings => settings.ConnectionString)
                .Returns("localhost,password=OPTSOL@dev");

            var cacheConnection = new Mock<DefaultConnection>(cacheSettingsMock.Object);
            var cacheService = new CacheService(cacheConnection.Object);

            var valor = new { Id = 2, Valor = "Algum valor 2" };
            var keyValue = new KeyValuePair<string, object>("Temporario", valor);

            //when
            cacheService.Save(keyValue, 1);

            //then
            cacheService.Should().NotBeNull();
        }

#if DEBUG
        [Fact(DisplayName = "Deve excluir um cache persistente")]
#elif RELEASE
        [Fact(DisplayName = "Deve excluir um cache persistente", Skip = "redis local docker test")]
#endif
        public void Deve_Excluir_Cache_Persistente()
        {
            //given
            Deve_Criar_Cache_Persistente();

            var cacheSettingsMock = new Mock<CacheSettings>();
            cacheSettingsMock
                .SetupGet(settings => settings.ConnectionString)
                .Returns("localhost,password=OPTSOL@dev");

            var cacheConnection = new Mock<DefaultConnection>(cacheSettingsMock.Object);
            var cacheService = new CacheService(cacheConnection.Object);

            //when
            var deleteAction = () => cacheService.Delete("Persistente");

            //then
            deleteAction.Should().NotThrow();
        }
    }
}
