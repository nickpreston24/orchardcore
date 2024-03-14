# Summary

There are a few libraries and bits of custom code I'm using for this project, that I think perfectly demonstrate not only how I code, but how flexible and fast I can solve a problem with what I'm given.

![Maximum Effort](https://media.giphy.com/media/FxUFE9TPXIKFG/giphy.gif?cid=790b76111viwuo4kvb7rz5wc24iylam0wgg0i6sdccjqvz96&ep=v1_gifs_search&rid=giphy.gif&ct=g)

### Why is HTMX here?

[Htmx](htmx.org) is a popular frontend library that lets us be independent of heavy, popular JS Frameworks and allow us to use our ASP.NET MVC like it's an SSR.  

This fits withing OrchardCore because we can update any HTML element by calling our backend API, like this:

```html
  <!-- have a button POST a click via AJAX -->
  <button hx-post="/clicked" hx-swap="outerHTML">
    Click Me
  </button>
```

### What happens:
1.  Our endpoint gets called
2.  Whatever we send back gets rendered in place of the button.
3.  Yeah, that's about it.
4. ... why are you still reading this list?

#### TLDR Explanation 
    Basically, I'm just having the Schedule buttons perform GET requests and get updated by the backend.  (and so I can finish quickly and knock out that extra credit!)

* [Examples](https://htmx.org/examples/)
* [Rationale](https://htmx.org/essays/)
* What I did to make the calendar work
  * https://htmx.org/examples/click-to-edit/
  * 

## What is CodeMechanic?

`CodeMechanic` is just my private repo of utility libraries for C# that I keep on GitHub.

It has helper methods that make C# a bit more fluid to work with, with without forcing dependencies on a project. E.g.,
making something like this incredibly simple and fast to write:

```cshtml
    var all_markdown_headers = new Grepper() { ... FileSearchMask = "*.md", FileSearchLinePattern = "^#*" }
        .GetMatchingFiles() // IEnumerable<GrepResult>
        ...
```

#### CodeMechanic.Types

For example, the `ToInt()` method handles null checking ternary logic on a string and
tries to parse it, and without throwing an error, sets a default or a fallback provided by the developer.

`"42".ToInt(); //42`, `"NaN".ToInt(); // 0`, and `"fubar".ToInt(fallback: 42); // 42`.

These help with `cshtml` files in Razor MVC, when we need to convert a string on the fly to an Int, but haven't
necessarily updated our entire stack to support said int just yet.

* Rationale - Basically, I started CodeMechanic repo last year to handle weird quirks and inconveniences in C# I'd find in my contract work (like Enums, amirite?).  CodeMechanic evolved over time into tiny .net7+ libraries that can help devs build complex services very quickly, without doing much boilerplate at all, and doing more LINQ-like chained methods for quickly getting their ideas coded.
