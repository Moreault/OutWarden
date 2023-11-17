namespace OutWarden.Tests;

//TODO Test with both ref and value types as well as nullable value types
[TestClass]
public class ResultOfReferenceTypeTest : ResultTester<string>
{
    protected override string DefaultValue => null!;
}

[TestClass]
public class ResultOfValueTypeTest : ResultTester<int>
{
    protected override int DefaultValue => 0;

}

[TestClass]
public class ResultOfNullableValueTypeTest : ResultTester<int?>
{
    protected override int? DefaultValue => null!;
}