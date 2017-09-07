# Monkey Chat

This is the completed sample project for the August 2017 Monkey Chat challenge using the Azure Mobile Client for online/offline sync. Before running this be sure to create the file `src/MonkeyChat/secrets.json` as follows:

```json
{
    "AppServiceEndpoint": "https://{YOUR_AZURE_APP_SERVICE}.azurewebsites.net/",
    "AlternateLoginHost": "",
    "LoginUriPrefix": ""
}
```

This should then generate the Secrets.cs on build.