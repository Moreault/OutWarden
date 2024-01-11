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

    [JsonInclude]
    public T Value { get; private init; } = default!;

    [JsonInclude]
    public bool IsSuccess { get; private init; } = false;

    [JsonInclude] 
    public string Message { get; private init; } = string.Empty;

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

    public static implicit operator Result<T>(TryGetResult<T> result) => new()
    {
        IsSuccess = result.IsSuccess,
        Value = result.Value!
    };

    public static implicit operator TryGetResult<T>(Result<T> result) => new()
    {
        IsSuccess = result.IsSuccess,
        Value = result.Value!
    };
}