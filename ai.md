Ai has been a mentor/tutor for most of this project.\
I built all the models and services by myself with what I had planned out.\
At a certain point when I felt I had the models and services that was needed.\
I asked Ai for a review in accordance to how far I'd gotten\
and to what the requirements were. Ai then gave me information on what methods I was missing\
in the service or other unknown bugs in my models that I had overseen.

I have myself changed names of classes and methods to make them easier for me to understand\
as I noticed it grew a confusion when a lot of names were very similar. I've learned that\
XML comments can be useful to add after "finishing" a method as it will give an explanation of what it does.
This is useful as the codebase grows.

On the frontend side, AI has given me all the markup for every page.
For the logic within a page it has guided me for most of the time except for the files provided bellow.

Code that has been pasted is:\
`NewProfile.Razor`, \
`Chats.razor`, \
`ChatDetails.razor`, \
and the `NotifyProfileChanged` idea.

It has also provided suggestions for some of my services:
The implementation of:
`SetProfileAsync, RestoreProfileAsync + the IsLoaded pattern`
AI also gave smaller fix suggestions for `UpdateProfile` and the hardcoded `_nextId` that I decided to go with.
Ai has also written the XML comments for Public methods. 

AI has taught me alot along the way about DI lifetimes and events and how they operate.\
I also learned why `IReadOnlyList` instead of `IEnumerable`\
as lists are shared between users in singletons.
It could also be very confusing as we went in circles a lot of times.\
Ai also implemented methods along the way we didn't even discuss adding.