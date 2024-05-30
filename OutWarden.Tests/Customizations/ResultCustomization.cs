namespace OutWarden.Tests.Customizations;

[AutoCustomization]
public sealed class ResultCustomization : CustomizationBase
{
    protected override IEnumerable<Type> Types { get; } = [typeof(Result<>)];

    protected override IDummyBuilder BuildMe(IDummy dummy, Type type)
    {
        return dummy.Build<object>().FromFactory(() =>
        {
            var genericType = type.GetGenericArguments().Single();
            var resultOf = typeof(Result<>).MakeGenericType(genericType);

            var flip = Coin.Flip(nameof(Result<int>.Success), nameof(Result<int>.Failure))!;
            var method = resultOf.GetSingleMethod(flip);

            if (flip == nameof(Result<int>.Success))
            {
                var parameters = method.GetParameters().Select(x => dummy.Create(genericType)).ToArray();
                return method.Invoke(null, parameters)!;
            }

            return method.Invoke(null, [dummy.Create<string>()])!;
        });
    }
}