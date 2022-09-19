![OutWarden](https://github.com/Moreault/OutWarden/blob/master/outwarden.png)

# OutWarden
Protects your code from pesky 'out' keywords by providing a TryGetResult type.

```c#
//Here's what classic "tryget" code looks like
public bool TryGetJeans(Pants value, out Jeans jeans)
{
    ...
}

//We've all done that (not really) but let's look a cleaner alternative...
public TryGetResult<Jeans> TryGetJeans(Pants value)
{
    ...
}
```

"What sorcery is this", I hear you say? Indeed! Who would have thought about returning values in a return statement rather than entry parameters. That is just blasphemous.

# Is that it?
Oh yes. It only adds a generic struct with a bool and value properties. That's it.

It's mostly meant as a common type library rather than a full library. Feel free to use it (or not.)

# Wow, you must really hate the 'out' keyword!
I find it weird that there's not more people annoyed by it to be honest.