namespace OutWarden.Tests;

[TestClass]
public class ResultOfReferenceTypeTests : ResultTester<string>
{
    protected override string DefaultValue => null!;
}

[TestClass]
public class ResultOfValueTypeTests : ResultTester<int>
{
    protected override int DefaultValue => 0;
}

[TestClass]
public class ResultOfNullableValueTypeTests : ResultTester<int?>
{
    protected override int? DefaultValue => null!;
}