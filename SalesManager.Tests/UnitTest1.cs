using Xunit;
using SalesManager.Domain.Entities;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
namespace SalesManager.Tests;

public class UserEndPointTest
{
    [Fact(DisplayName = "Register New User")]
    [Trait("User", "Register")]
    public async void UserEndPoint_ShouldCallRegisterSuccess()
    {
        //Arrange
        UserInput user = new UserInput();
        user.UserName = "saleAdmin";
        user.Password = "password#";
        user.Name = "Sales manager";
        user.Email = "salesm@salesm.com";
        
        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8);

        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        var apiClient = new HttpClient(clientHandler);
        
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        // Act
        var apiResponse = await apiClient.PostAsync("http://localhost:5029/signup", httpContent);

        // Assert
        Assert.True(apiResponse.IsSuccessStatusCode);
    }

    [Fact(DisplayName = "Authenticator")]
    [Trait("User", "Authenticator")]
    public async void UserEndPoint_ShouldCallAutheticateSuccess()
    {
        //Arrange
        UserAuthenticator user = new UserAuthenticator();
        user.UserName = "saleAdmin";
        user.Password = "password#";
        
        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8);

        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        var apiClient = new HttpClient(clientHandler);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        // Act
        var apiResponse = await apiClient.PostAsync("http://localhost:5029/signin", httpContent);

        // Assert
        Assert.True(apiResponse.IsSuccessStatusCode);
    }

}