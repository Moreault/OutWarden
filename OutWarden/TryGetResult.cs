namespace ToolBX.OutWarden;

public readonly record struct TryGetResult<T>
{
    public static readonly TryGetResult<T> Failure = new(false);

    public bool IsSuccess { get; init; }
    public T? Value { get; init; }

    public TryGetResult(bool isSuccess, T? value)
    {
        IsSuccess = isSuccess;
        Value = value;
    }

    public TryGetResult(T? value)
    {
        IsSuccess = true;
        Value = value;
    }

    public TryGetResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
        Value = default;
    }

    public override string ToString() => IsSuccess ? Value?.ToString() ?? string.Empty : string.Empty;
}