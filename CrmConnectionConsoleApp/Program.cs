// See https://aka.ms/new-console-template for more information
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.PowerPlatform.Cds.Client;
using Microsoft.Xrm.Sdk.Query;
using System.Net.Http.Headers;

Console.WriteLine("Hello, World!");

System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;


//CdsServiceClient cdsServiceClient = new CdsServiceClient("AuthType = ClientSecret; ClientId=c78cad2b-30fd-4d53-8318-edd76bb269ae; Url =https://orga3420e63.crm4.dynamics.com/; ClientSecret=nPn8Q~WRYMps6eRNi7KtZF7iPT1fvVDJDGrRwcV9;");

//if (cdsServiceClient.IsReady)
//{
//    var account = cdsServiceClient.Retrieve("account", new Guid("b4cea450-cb0c-ea11-a813-000d3a1b1223"), new ColumnSet(true));


//}


string resource = "https://bankfabdemo.crm.dynamics.com";

// client id and client secret of the application
ClientCredential clientCrendential = new ClientCredential("c78cad2b-30fd-4d53-8318-edd76bb269ae",
"nPn8Q~WRYMps6eRNi7KtZF7iPT1fvVDJDGrRwcV9");

// Authenticate the registered application with Azure Active Directory.
AuthenticationContext authContext =
new AuthenticationContext("https://login.microsoftonline.com/683be6e3-0602-455d-aff6-67af47bd26f5/oauth2/token");

AuthenticationResult authResult = await authContext.AcquireTokenAsync(resource, clientCrendential);
var accessToken = authResult.AccessToken;

// use HttpClient to call the Web API
HttpClient httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

httpClient.BaseAddress = new Uri("https://orga3420e63.api.crm4.dynamics.com/api/data/v9.2/");

var response = httpClient.GetAsync("WhoAmI").Result;
if (response.IsSuccessStatusCode)
{
    var userDetails = response.Content.ReadAsStringAsync().Result;
}
