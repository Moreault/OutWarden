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

[TestClass]
public class RandomTests : ResultTester<string>
{
    protected override string DefaultValue => null!;

    [TestMethod]
    public void METHOD_WhenWHEN_THEN()
    {
        //Arrange
        var anus1 = Dummy.Create<Result<string>>();
        var anus2 = Dummy.Create<Result<string>>();

        //Act
        var result = anus1 == anus2;

        //Assert
        result.Should().BeFalse();
    }



}
