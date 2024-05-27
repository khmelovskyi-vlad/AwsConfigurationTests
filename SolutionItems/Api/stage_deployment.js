const { exec } = require("child_process");

function handler(data, serverless, options) {
    let stage_name = data["StageName"]
    let rest_api_id = data["RestApiId"]
    let region = data["Region"]
    let statement_with_profile = "aws apigateway create-deployment --region " + region + " --rest-api-id " + rest_api_id +
        " --stage-name " + stage_name +  " --profile " + stage_name + " --description \"Automatic Deployment after Serverless Framework Deployment\""
    exec("aws apigateway create-deployment --region " + region + " --rest-api-id " + rest_api_id +
        " --stage-name " + stage_name + " --description \"Automatic Deployment after Serverless Framework Deployment\"",
        (error, stdout, stderr) => {
            if (error) {
                console.log(`error: ${error.message}`);
                return;
            }
            if (stderr) {
                console.log(`stderr: ${stderr}`);
                return;
            }
            console.log(`stdout: ${stdout}`);
        })
}

module.exports = { handler }