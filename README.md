![OutWarden](https://github.com/Moreault/OutWarden/blob/master/outwarden.png)

# OutWarden
Simple and straightforward Result type that protects you, among other things, to use the `out` parameters.

```c#
//Here's what classic "tryget" code looks like
public bool TryGetJeans(Pants value, out Jeans jeans)
{
    ...
}

//We've all done that (not really) but let's look at a cleaner alternative...
public Result<Jeans> TryGetJeans(Pants value)
{
    ...
}
```

# Getting started

```cs
return Result<T>.Success(value);

return Result<T>.Failure();

return Result<T>.Failure("Something went wrong");
```