$baseUrl = "https://localhost:7039"

$apiAuthorEndpoint = "$baseUrl/api/Users"

$author = @(
    @{
        Username = "Alexander"
        Password = "Testing123!"
    }
)

 $jsonBody = $author | ConvertTo-Json
 $response = Invoke-RestMethod -Uri $apiAuthorEndpoint -Method Post -Body $jsonBody -ContentType "application/json"
 Write-Host "Response: " $response


	
