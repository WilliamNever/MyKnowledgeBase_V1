1. How to setup User Secrets?
* - add secrets.json file to the following folder - 
%APPDATA%\Microsoft\UserSecrets\$"{Uer Secret id - a guid}"\
Ps: -
* - $"{Uer Secret id - a guid}" is at <UserSecretsId>d5e251f6-dfeb-411f-b654-154b1334face</UserSecretsId> in Net6Test.csproj file
* - Right click the project name, check the 'Manage User Secrets' to add/edit the existing secrets file.
