using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace UserAuthorizer;

public class Function
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<APIGatewayCustomAuthorizerResponse> FunctionHandler(APIGatewayCustomAuthorizerRequest request, ILambdaContext context)
    {
        context.Logger.Log("Start");
        try
        {
            return await Task.Run(() => CreateAuthorizerResponse(true));
        }
        catch (Exception ex)
        {
            context.Logger.LogLine($"Invalid access token: {ex}");
            throw new UnauthorizedAccessException("Unauthorized");
        }
    }

    private static APIGatewayCustomAuthorizerResponse CreateAuthorizerResponse(bool allow)
    {
        var effect = allow ? "Allow" : "Deny";
        var context = new APIGatewayCustomAuthorizerContextOutput
        {
            [UserIdName] = GetUserId()
        };

        var response = new APIGatewayCustomAuthorizerResponse
        {
            PrincipalID = GetUserId(),
            PolicyDocument = new APIGatewayCustomAuthorizerPolicy
            {
                Version = "2012-10-17",
                Statement = new List<APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement>
                {
                    new()
                    {
                        Action = new HashSet<string> { "execute-api:Invoke" },
                        Effect = effect,
                        Resource = new HashSet<string>() { "*" }  // this allow access to ALL endpoints of the api
                    }
                }
            },
            Context = context
        };

        return response;
    }

    private static string UserIdName = "UserId";

    private static string GetUserId()
    {
        return "myUserId";
    }
}
