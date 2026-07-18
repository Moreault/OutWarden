![OutWarden](https://github.com/Moreault/OutWarden/blob/master/outwarden.png)

# OutWarden
Simple and straightforward Result type that protects you, among other things, from using `out` parameters.

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

## Creating results

```cs
//With a value
return Result<T>.Success(value);

//Or simply return the value directly (implicit conversion)
return value;

//Failure without message
return Result<T>.Failure();

//Failure with message
return Result<T>.Failure("Something went wrong");
```

## Consuming results

```cs
//Check success or failure
if (result.IsSuccess) { ... }
if (result.IsFailure) { ... }

//Get value with a fallback
var value = result.ValueOr(fallback);

//Get value or throw
var value = result.ValueOrThrow();
var value = result.ValueOrThrow("Custom error message");

//Deconstruct
var (isSuccess, value, message) = result;
var (isSuccess, value) = result;
```

## Non-generic Result

For operations that succeed or fail without producing a value:

```cs
return Result.Success();
return Result.Failure();
return Result.Failure("Something went wrong");
```
