IIS Hosting:
Publish to 
	C:\VS\CoFloPeopleAPI\bin\Release\net8.0\publish
Copy Files to IIS Directory
	C:\inetpub\wwwroot\Hosting\CoFloPeopleApi


Docker Hosting:
docker build -t coflopeopleapiimage .
docker run -d --name coflopeopleapi -p 5545:8080 coflopeopleapiimage