# Orchardcore Test Project

## OrchardCore Sandbox

Goal: Create the CRUD we might see in a full-stack solution for calendar events

My approach is very simple: "How can I make every component simple and also potentially re-usable".


## Installation

run `reinstall_dotnet.sh` if you're running Linux debian.


## Running the application


I use these to run the applications.

To run my CMS attempt run the `run_cms.sh` bash file.
To run my vanilla Razor project, run `run_sandbox.sh` file.

Of course, you can always `cd` into either the `CMS.Web` and `sandbox` folders respectively and run `dotnet run watch` if you want.


No instructions for Visual Studio.  I assume you already know how.


## Services

#### `CalendarEventsService` - WIP

I'm unsure of the events I'll need for this and what authentication will be required, so that's why I've left this incomplete for now.  Will be easy to update, however, because it will have the same abilities as `GunService`.


## Stack (so far)

* [Hydro](usehydro.dev) - AlpineJS + RazorPages = HYDRO!!! 

   I'm likely going to make the EventCalander component a HydroComponent for now, because a) the events system is super-easy and b) grafting it back into OrchardCore *should* be easy, assuming we can figure out the events and map them to my CRUD.

   Hydro events are similar enough to the CMS Module events that I should be able to transition no problem, and you'll still be able to see that I'm more than capable of picking up OrchardCore Modules with the right instructions within a week or two.

* Slite3 - Scaffolded it myself, not sure if that's how OC does it, but it works!
* DaisyUI - For pretty visuals similar to bootstrap and for ease of copy/pasted UI components that work out of the box.  Can do Bootstrap 4, but this was a faster way to iterate and test.
* Alpinejs - supports hydro
* HTMX - in place of CMS events (for now).

## Todos

- [ ] Integrate the calendar into a CMS Module
  - [ ] Invesigate how the calendar library wants you to use it.
  - [ ] Figure out how a CMS Module event lifecycle works.
- [ ] Crud to finish and test:
  - [ ] Delete
  - [ ] ...?
- [ ] copy/paste the event calendar into a Hydro Component
  - [ ] Create a Hydro viewelement `hydro-event` with `hx-get` support
  - [ ] Wite the basic CRUD events described [here](https://docs.google.com/document/d/1JIHROJFNqIXdeoulxHi9pbp4Nphp2BuzLGBeGz-kyMo/edit)
  - [ ] Wire the eventcalender service into the calendar
  - [ ] Ensure the calendar easily loads onto the page from script with no `id` issues.