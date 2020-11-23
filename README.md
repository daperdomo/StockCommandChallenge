# StockCommandChallenge

Run these commands to set up the databse from EF code first:

1) dotnet ef database update -p StockCommandChallenge --context ApplicationDbContext

2) dotnet ef database update -p StockCommandChallenge --context StockCommandChallengeContext


To set up RabbitMQ Server configuration go to appsettings.json and configure ServiceBrokerHostName, ServiceBrokerUsername, ServiceBrokerPassword in AppSetting Key.
