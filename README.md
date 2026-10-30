# Campus Chat
This app is a Blazor web app for students. Students can write in threads, open direct chats with each other,\
select a created profile, update that profile or create a new profile from scratch.
## How to run:
Have .NET 10 SDK installed.\
Run this from a clean clone.
```
dotnet run
```
Opens on http://localhost:5062\
All data is in memory and resets when the app restarts

## How to use
sign in on `/login` by picking a seeded user or creating your own (No password required).\
The `Audit User` is the auditor if you want to test wether all threads are readable\

## What doesn't work yet
* Unread counts per joined thread (requirement 7)
* Auditor reading other people's direct chats. The service allows it in `GetMessages()`,\
and auditors can read every thread. (requirement 12, chat part)
There is just no page to find the chats. 
* Profile deletion exists in the service but has no page. 
