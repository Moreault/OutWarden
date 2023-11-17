namespace OutWarden.Tests.Customizations;

[AutoCustomization]
public sealed class ResultSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        var type = request as Type ?? request.GetType();
        if (type.IsGenericType)
        {
            var genericType = type.GenericTypeArguments.First();
            var resultOf = typeof(Result<>).MakeGenericType(genericType);
            if (type == resultOf)
            {
                var random = new Random().Next(2);
                var method = resultOf.GetSingleMethod(random == 0 ? "Success" : "Failure");

                var parameters = method.GetParameters().Select(x => ResolveParameter(x, context)).ToArray();

                return method.Invoke(null, parameters)!;
            }
        }

        return new NoSpecimen();
    }

    private static object ResolveParameter(ParameterInfo parameter, ISpecimenContext context)
    {
        if (parameter.ParameterType.IsGenericType && parameter.ParameterType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            var underlyingType = Nullable.GetUnderlyingType(parameter.ParameterType);
            return context.Resolve(underlyingType);
        }

        return context.Resolve(parameter.ParameterType);
    }

}