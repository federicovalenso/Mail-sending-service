# Service for sending emails from other services
## Installation and deployment

1. install dotnet-sdk
2. dotnet restore
3. dotnet publish -r <platform> -c Release
   
For windows you need to register service:
1. `sc MailSender binPath= path\to\MailClientService.exe DisplayName= "Service for sending mails by other services" start= auto`
2. add rules to firewall
3. `sc start MailSender`