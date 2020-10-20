using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IndentityServerCenter
{
    public class Config
    {
        public static IEnumerable<ApiResource> GEtApiResources =>
            new List<ApiResource>
            {
                new ApiResource("api", "MyAPI")
            };

        public static IEnumerable<Client> GetClients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = new []{new Secret("Secret".Sha256()) },//客户端用来获取的token
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,//允许授权的模式，这里采用的是用户的木马和客户端模式
                    AllowedScopes = {"api"}   //允许访问的范围
                }
            };
    }
    //public class Config
    //{
    //    public static IEnumerable<IdentityResource> Ids =>
    //        new IdentityResource[]
    //        {
    //            new IdentityResources.OpenId()
    //        };
    //    public static IEnumerable<ApiScope> Apis =>
    //         new List<ApiScope>()
    //         {
    //             new ApiScope("api","API")
    //         };

    //    public static IEnumerable<IdentityResource> IdentityResources =>
    //        new IdentityResource[]
    //        {
    //            new IdentityResources.OpenId()
    //        };

    //    public static IEnumerable<ApiScope> ApiScopes =>
    //        new ApiScope[]
    //            { };

    //    public static IEnumerable<Client> Clients =>
    //        new []
    //        {
    //            new Client
    //            {
    //                ClientId = "1",//客户端的ID
    //                ClientSecrets = new []{new Secret("Secret".Sha256()) },//客户端用来获取的token
    //                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,//允许授权的模式，这里采用的是用户的木马和客户端模式
    //                AllowedScopes = new []{"api"}   //允许访问的范围
    //            }
    //        };

    //    public static List<TestUser> Users=>
    //    new List<TestUser>
    //    {
    //        new TestUser
    //        {
    //            SubjectId = "1",
    //            Username = "Chen",
    //            Password = "Chen"
    //        }
    //    };
    //}
}
