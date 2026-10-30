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

On the frontend side for things to look "nice" I have copied code straight over, this includes.
Even though the front end could be much better I spent more time on the logic.

Code that has been pasted is:\
`NewProfile.Razor`, \
`Chats.razor`, \
`ChatDetails.razor`, \
and the `NotifyProfileChanged` idea.

AI has taught me alot along the way about DI lifetimes and events and how they operate.\
I also learned why `IReadOnlyList` instead of `IEnumerable`\
as lists are shared between users in singletons. 
It could also be very confusing as we went in circles a lot of times. 
