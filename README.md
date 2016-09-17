# messagingtoolkit-messenger

A Windows service which uses MessagingToolkit to receive SMS and send out MMS based on the content of the SMS.

Once you downloaded the source code,

1. From Visual Studio, open Package Manager Console, and run "update-package" to install all the dependant assemblies.
2. The sample uses a "code first" approach, meaning the SQL Server database will be created when the application is run for the first time. Open "MessengerInitializer.cs" 
- By default sample employee record will be created with sample photo.
- CHANGE *defaultGatewayConfig* value to reflect your device configuration
- CHANGE *gatewayPhoneNumber* to reflect your device phone number
3. In App.config you can see the database connection string. By default the database is created in the application folder.
4. The sample can be run in Console mode or install as Windows service.
