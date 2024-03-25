# Orchardcore Test Project

## OrchardCore Sandbox

Goal: Create the CRUD we might see in a full-stack solution for calendar events

My approach is very simple: "How can I make every component simple and also potentially re-usable".


## Installation

run `reinstall_dotnet.sh` if you're running Linux debian.


## Running the application


I use these to run the applications.

To run my CMS attempt run the `run_cms_app.sh` bash file.
To run my vanilla Razor project, run `run_sandbox.sh` file.

Of course, you can always `cd` into either the `CMS.Web` and `sandbox` folders respectively and run `dotnet run watch` if you want.


No instructions for Visual Studio.  I assume you already know how.


## Services

#### `CalendarEventsService` - WIP

I'm unsure of the events I'll need for this and what authentication will be required, so that's why I've left this incomplete for now.  Will be easy to update, however, because it will have the same abilities as `GunService`.


## Stack (so far)

* Slite3 - Scaffolded it myself, not sure if that's how OC does it, but it works!
* DaisyUI - For pretty visuals similar to bootstrap and for ease of copy/pasted UI components that work out of the box.  Can do Bootstrap 4, but this was a faster way to iterate and test.
* Alpinejs - Basically does jQuery's job, but with Reactivity we'd see in Blazor.
* HTMX -  Basically does the job of Ajax + DOM swaps, all in a few simple commands.  I'm using it in place of CMS events (for now) because I don't know much about ItemTypes, etc. and how they work.
  * Also, the calendar has some premium events for their boxes, so to get around that, I can give those boxes super-powers and still have events in vanilla Razor Pages.  🎉
* CodeMechanic  - my personal set of Nuget packages I used to iterate quickly.  A `nuget.config` is provided, so all you have to do is a `dotnet restore` or `dotnet add package <packagename>` for any missing `CodeMechanic.*` dependencies.

## Todos

- [x] Integrate the calendar into a CMS Module
  - [x] Invesigate how the calendar library wants you to use it.
    - [ ] https://fullcalendar.io/docs/date-clicking-selecting
  - [ ] Figure out how a CMS Module event lifecycle works.
- [x] Crud to finish and test:
  - [x] Get all events w/ 'published' status; Requirement: "I can visit a page /calendar and see a calendar with all published events"
  - [x] Delete
  - [x] Bulk create (or single)
- [x] copy/paste the event calendar into a Hydro Component
  - [x] Create a Hydro viewelement `hydro-event` with `hx-get` support
  - [x] Wite the basic CRUD events described [here](https://docs.google.com/document/d/1JIHROJFNqIXdeoulxHi9pbp4Nphp2BuzLGBeGz-kyMo/edit)
  - [x] Wire the eventcalender service into the calendar
  - [x] Ensure the calendar easily loads onto the page from script with no `id` issues.
  - [ ] If you have time, wrap each Hydro event in a custom Authorization context from Hydro, demonstrating you know how to secure the `/admin` path.
  - [x] Extra:
    - [x] Try https://fullcalendar.io/docs/month-view making this a set of toggles in an optionspanel hydro component.
    - [x] When user selects an event, toggle it and show the form.
      - [x] Add
      - [x] Danger Zone
- [ ] Published Events:
  - [x] Update your Add form with a select
  - [x] Update your edit form with a select
  - [ ] *optional* add a chip filter or toggle to the calendar?
  
## User Stories 
o As an Admin User 
    I want a calendar module where I can add Events to show up on my Front-End Calendar So that end-users can see what activities are coming up and have passed 
    And I will know that I am done when 
    - [x] I have a calendar content type on the /Admin that I can enter a new Event 
    - [x] I can Edit an Event 
      - [x] Todo: Show the Edit panel
      - [x] then allow edits.
    - [x] I can Add New Events 
    - [x] I can Delete an event 
    - [x] I can see a list of all events 
o As a Front-End User 
    I want a calendar module where I can view all Events 
    So that I can see what activities are coming up and have passed 
    And I will know that I am done when 
    - [x] I can visit a page /calendar and see a calendar with all published events
    - [x] When I can view the calendar in Month, Week, Day, and [Agenda Views](https://fullcalendar.io/docs/v3/agenda-view) 
    - [x] When Navigate Forward and Backward on the Calendar to see Past Events and  upcoming events 


# Features

* Full CRUD functionality for `/Admin` page.
* Calendar view and list view may be toggled in the `Admin` page!
* List, month, day and year views for `/Calendar` page.
* Self-seeding database (requires a `LocalDatabase.db`).  
  * If no table exists, one may be created by running `SeedCalendar(int limit)`, where the limit is the max rows you wish to seed.  I used the Bogus library to create fake seeded events.


# Helpful Resources

[HTMX + MVC](https://khalidabuhakmeh.com/htmx-with-aspnet-core)


## Upgrades (nice to haves)

1. [Single Quotes in serialized JSON objects](https://alexanderzeitler.com/articles/serializing-dotnet-csharp-object-for-use-with-alpine-x-data-attribute/)
2. [Hydro](usehydro.dev) - AlpineJS + RazorPages = HYDRO!!!
