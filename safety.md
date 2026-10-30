## Personal Data and safety

### Questions and Answers:

1. Name every piece of personal data that the product stores.
* The data about a user is found in `Profile.cs` which is,
`id, username, bio, avatarcolor, schoolyear`. In `ThreadPost.cs` is stores what someone wrote and when.
`DirectMessage.cs` stores who talks to who, what they said and when. `ThreadMembership.cs` shows which threads a user has joined.
Private threads will then appear on the users profile, so another user browsing that profile can see they are a part of it, but wont have acces to its posts.
In addition, when users browse threads they can still see the private threads, but they can't read posts within it.
2. State how long the product keeps a direct chat. Justify the number.
* Today it's until restart. If I were to pick a date it would be until the student leaves/by the time he/she is done studying.
I wouldn't keep the data forever as its personal data, which can be misused. 
3. A student leaves the college. State what happens to their posts. State what happens to
their profile.
* Currently, there is no page for deleting a user. `DeleteProfile()` is implemented in the service,
but it only removes the profile. Post will still be shown. I believe that is okay, because if the post is helpful why not keep it?
The lookup of a profile would fail, and would show "Former Student" instead. For direct messages at this point won't be removed as
`_directMessages` aren't being affected in the `DeleteProfile()` method. Even if its not implemented, these chats should be accessable until
the user either leaves or is done studying at this college.
4. A user posts abuse in a thread. Name who removes the post. Name who decides.
* The author of a post is only available to delete posts, which is a gap. 
The Auditor should be able to remove abuse in threads. There could also be a rule one could set on a thread,
for instance, adding censorship to posts being written inside the thread, the creator of a thread could be responsible for this. 
5. A direct chat is private. State what stops another user from reading it. The audit role
reads it anyway. State who holds that role. State whether the product tells the two people.
* `CanReadConversation` makes sure the `callerId`is either `userA` or `userB`. If not, the method returns false. 
The role `IsAudit` from `seedData` is the one holding this role thats `id:6`. Currently there is no notice if someone else is reading these messages. 
This should be told right when Users create their profile, so they are aware that their messages are being read from the beginning. 
Users might discuss things that aren't school related, and should be aware that those messages should probably take place elsewhere.
6. Name one thing that the product must never log.
* The accessCode. It's supposed to keep a thread unreadable from users who are not invited.
7. A private thread has a code. State where the product keeps that code.
* In `DiscussionThread.AccessCode`. As of now its private and stored in memory in plain text. 
Its not optimal, hashing the password would be better. The browser also reveals the code when you type it in. 