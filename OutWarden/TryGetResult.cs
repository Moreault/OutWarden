namespace ToolBX.OutWarden;

[Obsolete("Use Result<T> instead")]
public readonly record struct TryGetResult<T>(bool IsSuccess, T? Value)
{
    public static readonly TryGetResult<T> Failure = new(false);

    public static TryGetResult<T> Success(T value) => new(true, value);

    public TryGetResult(T? value) : this(true, value)
    {
    }

    public TryGetResult(bool isSuccess) : this(isSuccess, default)
    {
    }

    public override string ToString() => IsSuccess ? Value?.ToString() ?? string.Empty : string.Empty;

    public void Deconstruct(out bool isSuccess, out T? value)
    {
        isSuccess = IsSuccess;
        value = Value;
    }
}