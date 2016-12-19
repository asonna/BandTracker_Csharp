# _MY BAND TRACKER_ (C#, HTML, Nancy and Razor project, many-to-many)

_*Epicodus C# week-4 Code Review Project, 12-16-16*_

by Annie Sonna.


##Description

This webpage is an app for band tracking by venue. Each band can be associated with many venues as one venue can also be associated with many bands. This webpage demonstrates database usage with many-to-many relationships.


###Objective from Epicodus page

Practice the concept of many to many relationship and how to code it in C# in association with razor, Nancy, Mono and SQL.


##Specifications:

I1. Input 1
 - See the specDoc.txt file for all the specifications related to this website.

##Setup/Installation requirements

1. Clone this repository to desktop.
2. Use powershell under window machine to navigate to the cloned project folder.
3. Run the following command "dnu restore"
4. You will need a database called "band_tracker" with the "bands" and "venues" tables.
5. Connect to your server and use the following command to create the database:
     - CREATE DATABASE band_tracker;
     - GO
     - USE band_tracker;
     - GO
     - CREATE TABLE venues (id INT IDENTITY(1,1)), name VARCHAR(255), address VARCHAR(255));
     - GO
     - CREATE TABLE bands (id INT IDENTITY(1,1)), name VARCHAR(255), type VARCHAR(255));
     - GO
     - CREATE TABLE bands_venues (id INT IDENTITY(1,1), band_id INT, venue_id INT);
     - GO
6. Create a backup of above database called "band_tracker_test" and restore it.
7. When writing your test, you can use the following command line on PowerShell for testing: "dnx test".  
8. Run "dnx kestel" command to run this app
9. In your browser, navigate to http://localhost:5004/
10. Then you are ready to start using this webpage!

## Known Bugs
TBD.


## Technologies Used

1. html
2. github
3. Atom
4. Nancy Web Application
5. SQL Server Management
6. C#
7. Xunit
8. Kestrel Server
9. DNX
10. Mono


## Link to the project on GitHub Pages

https://github.com/asonna/BandTracker_Csharp


## Copyright and license information

Copyright (c) 2016 Annie Nguimzong Sonna
