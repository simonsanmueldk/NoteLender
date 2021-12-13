using Xunit;

namespace GrpcTest
{
    [CollectionDefinition(TestCollections.ApiIntegration)]
    public class TestServerCollection : ICollectionFixture<TestServerFixture>
    {
    }
}