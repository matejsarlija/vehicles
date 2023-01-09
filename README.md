## Vehicles App

This is an asp.net app made for a job application, consisting almost solely
from tips out of official Microsoft tutorial docs for asp.net, one for MVC/Razor part and one for "Data Access" part.

1.**[MVC/Razor](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-7.0&tabs=visual-studio)**

2.**[Data access](https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-7.0)**

In the first part you'll find the needed information (that you will have to fit for your use case, of course) to build out
the search and paging for your app, while data acccess related tutorial can help you with a filter style search.

To make paging work with a filter on your database / entity model you will "have to" fit it to an adequate view model, depending on your use case, this will provide an example.

One thing I would also like to do futher down the line is provide a refactor to Repository pattern, or maybe even CQRS, but I am new to the enterprise architecture patterns and for the moment I am having slight trouble with it :).

Older tutorials had a sample for that as well, but it's warned against, my guess is it was harder to pull off with newer EF syntax, which is a comment you'll also find in other articles on the internet. And also not to clutter beginner resources.

I am using .net on Linux (Fedora) with Rider with SQlite as the db so the search on VehicleMakes is case-sensitive since it is db dependent.
