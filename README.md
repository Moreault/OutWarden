![OutWarden](https://github.com/Moreault/OutWarden/blob/master/outwarden.png)

# OutWarden
Lightweight generic `Result<T>` type to avoid using `out` parameters in C#.

```c#
//Here's what classic "tryget" code looks like
public bool TryGetJeans(Pants value, out Jeans jeans)
{
    ...
}

//We've all done that (not really) but let's look a cleaner alternative...
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