# Design

## Structure: models, services, pages
Models store the data, services handles the data and the pages displays the data.
I would only need to change the service layer if I later on would want to move to a database.
That is because the pages know the Service layer, but not where the data comes from.
## Validation lives in the models
Since validation lives in the models, we do not have to repeat code by validating in the pages. It saves time,\
and catches the error way earlier.
It also prevents someone from instantiating an object with invalid data.
## Access checks live in the services (caller as parameter)
CanAccess would be a huge issue if it lived in a page,
it would let users change values in the url that could eventually route them to someone else's chat,
or access to a thread they would normally not be allowed to enter. Keeping it in the service is safer,\
as everything moves through the same gate.
## Services return IReadOnlyList snapshots
At first I used IEnumerable which runs and loops over a list. This is an issue because if a user posts something in a thread the shared list will be modified since Ienumarble loops over the list.
by using `ToList()` instead it shows a snapshot or copy of the list. 
## Lifetimes: singleton data services, scoped CurrentUserService
Singleton is used when we want to share data among users.\
However if we would use Singletons on CurrentUserService,\
the users data could leak or overwrite another users session data.
Scoped fixes this.
## Who am I: ProtectedSessionStorage, restore after first render
In order to store a userprofile I used ProtectedSessionStorage. This way a user is stored in the browser tab.\
We can then open the app in two seperate tabs with different users in order to test the app.
## Exceptions between layers
Exceptions throws a useful message both to developers and users.\
The type of the exception also tells us what went wrong (not found, not allowed, bad input etc.)
## Live updates with events
When a user Updates their profile, this happens live instead of needing a browser refresh. The same goes for posts, replies and Dm's.
Pages subscribes and call InvokeAsync and to unsubscribe we use Dispose. Without Dispose the Singletons would keep a reference to a closed page.
