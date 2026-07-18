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
public class ResultTests : Tester
{
    [TestMethod]
    public void Failure_WhenNoMessageIsSpecified_ReturnEmptyFailure() => Ensure.WhenIsNullOrWhiteSpace(message =>
    {
        //Arrange

        //Act
        var result = Result.Failure(message);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Message.Should().BeEmpty();
    });

    [TestMethod]
    public void Failure_WhenMessageIsSpecified_ReturnFailureWithMessage()
    {
        //Arrange
        var message = Dummy.Create<string>();

        //Act
        var result = Result.Failure(message);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(message);
    }

    [TestMethod]
    public void Success_Always_ReturnSuccess()
    {
        //Arrange

        //Act
        var result = Result.Success();

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Message.Should().BeEmpty();
    }

    [TestMethod]
    public void Constructor_Always_InstantiateEmptyFailure()
    {
        //Arrange

        //Act
        var result = new Result();

        //Assert
        result.Should().Be(Result.Failure());
    }

    [TestMethod]
    public void ToString_WhenIsEmptyFailure_ReturnEmpty()
    {
        //Arrange

        //Act
        var result = Result.Failure().ToString();

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void ToString_WhenIsFailureWithMessage_ReturnMessage()
    {
        //Arrange
        var message = Dummy.Create<string>();

        //Act
        var result = Result.Failure(message).ToString();

        //Assert
        result.Should().Be(message);
    }

    [TestMethod]
    public void ToString_WhenIsSuccess_ReturnEmpty()
    {
        //Arrange

        //Act
        var result = Result.Success().ToString();

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void Deconstruct_Always_Deconstruct()
    {
        //Arrange
        var instance = Dummy.Create<Result>();

        //Act
        var (isSuccess, message) = instance;

        //Assert
        isSuccess.Should().Be(instance.IsSuccess);
        message.Should().Be(instance.Message);
    }

    [TestMethod]
    public void Ensure_ValueEquality() => Ensure.ValueEquality<Result>(Dummy);

    [TestMethod]
    public void Ensure_ValueHashCode() => Ensure.ValueHashCode<Result>(Dummy, JsonSerializerOptions.Default);

    [TestMethod]
    public void Ensure_IsJsonSerializable() => Ensure.IsJsonSerializable<Result>(Dummy);

    [TestMethod]
    public void Ensure_IsFailureIsNotSerialized()
    {
        //Arrange
        var instance = Dummy.Create<Result>();

        //Act
        var json = JsonSerializer.Serialize(instance);

        //Assert
        json.Should().NotContain("IsFailure");
        json.Should().NotContain("isFailure");
    }
}