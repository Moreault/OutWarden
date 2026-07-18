namespace ToolBX.OutWarden;

/// <summary>
/// Generic result type that can be used to return a value or a message.
/// </summary>
public readonly record struct Result<T>()
{
    private static readonly Result<T> Empty = new();

    public static Result<T> Failure(string? message = null)
    {
        if (string.IsNullOrWhiteSpace(message)) return Empty;
        return new Result<T>
        {
            IsSuccess = false,
            Message = message
        };
    }

    public static Result<T> Success(T value) => new()
    {
        Value = value,
        IsSuccess = true
    };

    public static implicit operator Result<T>(T value) => Success(value);

    [JsonInclude]
    public T Value { get; private init; } = default!;

    [JsonInclude]
    public bool IsSuccess { get; private init; } = false;

    [JsonIgnore]
    public bool IsFailure => !IsSuccess;

    [JsonInclude]
    public string Message { get; private init; } = string.Empty;

    public T ValueOr(T fallback) => IsSuccess ? Value : fallback;

    public T ValueOrThrow(string? errorMessage = null)
    {
        if (IsSuccess) return Value;
        throw new InvalidOperationException(errorMessage ?? Message);
    }

    public override string ToString()
    {
        if (IsSuccess)
            return Value?.ToString() ?? string.Empty;

        return string.IsNullOrWhiteSpace(Message) ? string.Empty : Message;
    }

    public void Deconstruct(out bool isSuccess, out T? value, out string message)
    {
        Deconstruct(out isSuccess, out value);
        message = Message;
    }

    public void Deconstruct(out bool isSuccess, out T? value)
    {
        isSuccess = IsSuccess;
        value = Value;
    }
}

/// <summary>
/// Non-generic result type for operations that succeed or fail without producing a value.
/// </summary>
public readonly record struct Result()
{
    private static readonly Result Empty = new();
    private static readonly Result Ok = new() { IsSuccess = true };

    public static Result Failure(string? message = null)
    {
        if (string.IsNullOrWhiteSpace(message)) return Empty;
        return new Result
        {
            IsSuccess = false,
            Message = message
        };
    }

    public static Result Success() => Ok;

    [JsonInclude]
    public bool IsSuccess { get; private init; } = false;

    [JsonIgnore]
    public bool IsFailure => !IsSuccess;

    [JsonInclude]
    public string Message { get; private init; } = string.Empty;

    public override string ToString() => string.IsNullOrWhiteSpace(Message) ? string.Empty : Message;

    public void Deconstruct(out bool isSuccess, out string message)
    {
        isSuccess = IsSuccess;
        message = Message;
    }
}
