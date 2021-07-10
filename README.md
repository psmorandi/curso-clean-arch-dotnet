# Clean Architecture Project Example
This is an implementation example of a Clean Architecture project. Implemenation is going to be done along with the course presented by [Rodrigo Branas](https://github.com/rodrigobranas). Don't use as is in production, but feel free to base your implementation on this example.

## Implementation
Requirements to run this example:
- Visual Studio 2019 Preview (>=2.0)
- dotnet 6 preview (>=6.0.100)
- postgresql (docker compose available - `database.yaml`)

Technologies used in this implementation:
- dotnet 6 (preview)
- AspNet Core 6 (preview)
- Entity Framework Core 6 (preview)
- Blazor WebAssembly (preview)

## TODOs
- [ ] Better exception handling
     - [ ] Better domain exception handling
     - [ ] Better infrastructure exeption handling
- [ ] Add authetication to Web API
- [ ] Use the Web API authetnciation in Blazor
- [ ] Better HttpClient injection in Blazor (we can use the factory to inject a "typed" http client, would make easier than ref with a string)
- [ ] Refreshing the page after paying an invoice is not working (to disable the button)
- [ ] Load Level, Module and Classrooms as a combobox
- [ ] A web page to show all invoice events (details of paid invoice maybe)
- [ ] CRUD for Level, Module and Classrooms

## About Clean Architecture
It's a collection of design principles to avoid coupling between each component, enabling the separation of concerns through an "onion" like design. Check out more [here](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).

## About The Course
If you're interested in take the course (pt-BR only) checkout [this link]( https://app.agilecode.com.br/public/products/cd4aea57-4b43-41e9-bda5-f487c3817b29).