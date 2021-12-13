using System;
using Grpc.Net.Client;
using GrpcService;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GrpcTest
{
    [Collection(TestCollections.ApiIntegration)]
    public class UnitTest1
    {
        private readonly  BusinessServer.BusinessServerClient greeterClient;
        
        public UnitTest1(TestServerFixture testServerFixture)
        {
            var channel = testServerFixture.GrpcChannel;
            greeterClient = new BusinessServer.BusinessServerClient(channel);
        }
        
        [Fact]
        public async void success_GetGroupList()
        {
            var reply = await greeterClient.GetGroupListAsync(
                new Request {Name = "76"});
            Assert.Equal("[]", reply.Message);
        }
    }
}