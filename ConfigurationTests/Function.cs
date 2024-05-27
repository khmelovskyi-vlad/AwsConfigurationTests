using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using ConfigurationTests.Model;
using Newtonsoft.Json;
using System.Net;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ConfigurationTests
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var userId = request.RequestContext.Authorizer["principalId"].ToString();

            context.Logger.Log($"principalId: {userId}");

            if (request.PathParameters != null && request.PathParameters.Any() &&
                request.PathParameters.TryGetValue("input", out var input))
            {
                var testVariable = Environment.GetEnvironmentVariable("testVariable");

                return GetResponse(HttpStatusCode.OK, 
                    new ConfigurationTestsResponseModel($"Input in upper case: {input.ToUpper()}, testVariable: {testVariable}, principalId: {userId}"));
            }

            return GetResponse(HttpStatusCode.BadRequest, "Bad request");
        }

        public static APIGatewayProxyResponse GetResponse<T>(HttpStatusCode statusCode, T body)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)statusCode,
                Body = JsonConvert.SerializeObject(body)
            };
        }
    }
}
