# Windows-Authentication-with-Users-and-Roles-table

ASP.NET Core 2.1 EF

ntroduction
You have a self implemented Authentication Service (User-Permission Management) and want use simple Attributes to control Access about your Controllers or Action-Methods?

Then you're exactly right here.

With this example you can use your existing Permission-Management in a ASP.NET Core Application.

Attention this is not for Authentication, you still need something like Windowsauthentication.
This is only a Way to authorize a User with some Permissions.

My Example is a ASP.NET Core WebApi Project with WindowsAuthentication.

Background
I have a own Permission-Management in a Database. There i manage the Users with their Permissions, like an Active Directory, but with some more delegate Options. 
So i want use a simple Authorize Attribute to set required Permissions for Controllers or Action-Methods.
Example which i used in ASP.NET 4.5:  [Authorize(Roles=”Administrator”)]

Preparations 
We need first the Permissions and Users from the Database or whatever. 

In my example we see only the IUserCache Interface which implements a Cached Repository of Users. And this Users have a Property as a List of Permissions. 

The UserCache reloads the User if the Cachetime of this is expired or this User do not exists in Cache. 

Nuget Packages (.NET Core Extensions)
Microsoft.AspNetCore.Authentication 

Microsoft.NETCore.App 

Microsoft.AspNetCore.Mvc.Core

 

5 Steps to Authorize 
Own Principal-Implementation 

Permission-Provider (Get the Permissions) 

Custom Authorize Attribute 

Authorization-Requirement 

Set Dependencies 

 

1. Own Principal Implementation 
We create a Principal-Class deriven from ClaimsPrincipal.

This Class override the IsInRole with our PermissionProvider to check the Role. 
Our Principal needs our PermissionProvider, which will check the Roles from this User. 
