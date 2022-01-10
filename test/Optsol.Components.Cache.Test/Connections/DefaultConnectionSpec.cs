using FluentAssertions;
using Moq;
using Optsol.Components.Cache.Connections;
using Optsol.Components.Cache.Exceptions;
using Optsol.Components.Cache.Settings;
using Xunit;

namespace Optsol.Components.Cache.Test.Connections;

public class DefaultConnectionSpec
{
    [Fact(DisplayName = "Deve se connectar ao redis")]
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

    [Fact(DisplayName = "Deve obter uma instância do database")]
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

    [Fact(DisplayName = "Deve exibir exceção se não houver string de conexão")]
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