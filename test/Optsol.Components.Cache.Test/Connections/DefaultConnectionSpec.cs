using FluentAssertions;
using Moq;
using Optsol.Components.Cache.Connections;
using Optsol.Components.Cache.Exceptions;
using Optsol.Components.Cache.Settings;
using Xunit;

namespace Optsol.Components.Cache.Test.Connections;

public class DefaultConnectionSpec
{
#if DEBUG
    [Fact(DisplayName = "Deve se connectar ao redis")]
#elif RELEASE
    [Fact(DisplayName = "Deve se connectar ao redis", Skip = "redis local docker test")]
#endif
    public void Deve_Connectar_Redis()
    {
        //given
        var cacheSettingsMock = new Mock<CacheSettings>();
        cacheSettingsMock
            .SetupGet(settings => settings.ConnectionString)
            .Returns("localhost,password=OPTSOL@dev");

        //when 
        var defaultConnection = new DefaultConnection(cacheSettingsMock.Object);

        //then
        defaultConnection.Should().NotBeNull();
        defaultConnection.IsConnected().Should().BeTrue();
    }

#if DEBUG
    [Fact(DisplayName = "Deve obter uma instância do database")]
#elif RELEASE
    [Fact(DisplayName = "Deve obter uma instância do database", Skip = "redis local docker test")]
#endif
    public void Deve_Obter_Instancia_Database()
    {
        //given
        var cacheSettingsMock = new Mock<CacheSettings>();
        cacheSettingsMock
            .SetupGet(settings => settings.ConnectionString)
            .Returns("localhost,password=OPTSOL@dev");

        var defaultConnection = new DefaultConnection(cacheSettingsMock.Object);

        //when 
        var database = defaultConnection.GetDatabase();

        //then
        database.Should().NotBeNull();
    }

#if DEBUG
    [Fact(DisplayName = "Deve exibir exceção se não houver string de conexão")]
#elif RELEASE
    [Fact(DisplayName = "Deve exibir exceção se não houver string de conexão", Skip = "redis local docker test")]
#endif
    public void Deve_Exibir_Excecao_Sem_ConnectionString()
    {
        //given
        var cacheSettingsMock = new Mock<CacheSettings>();

        //when 
        var defaultConnectionAction = () => new DefaultConnection(cacheSettingsMock.Object);

        //then
        defaultConnectionAction.Should().ThrowExactly<CacheConnectionException>();
    }
}