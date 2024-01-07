namespace OutWarden.Tests;

public abstract class ResultTester<T> : Tester
{
    protected abstract T DefaultValue { get; }

    [TestMethod]
    public void Failure_WhenNoMessageIsSpecified_ReturnEmptyFailure() => Ensure.WhenIsNullOrWhiteSpace(message =>
    {
        //Arrange

        //Act
        var result = Result<T>.Failure(message);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().BeEmpty();
        result.Value.Should().Be(DefaultValue);
    });

    [TestMethod]
    public void Failure_WhenMessageIsSpecified_ReturnFailureWithMessage()
    {
        //Arrange
        var message = Fixture.Create<string>();

        //Act
        var result = Result<T>.Failure(message);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(message);
        result.Value.Should().Be(DefaultValue);
    }

    [TestMethod]
    public void Success_WhenValueIsNull_StillReturnSuccess()
    {
        //Arrange
        T value = default!;

        //Act
        var result = Result<T>.Success(value);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().BeEmpty();
        result.Value.Should().Be(DefaultValue);
    }

    [TestMethod]
    public void Success_WhenValueIsNotNull_ReturnValue()
    {
        //Arrange
        var value = Fixture.Create<T>();

        //Act
        var result = Result<T>.Success(value);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().BeEmpty();
        result.Value.Should().Be(value);
    }

    [TestMethod]
    public void Constructor_Always_InstantiateEmptyFailure()
    {
        //Arrange

        //Act
        var result = new Result<T>();

        //Assert
        result.Should().Be(Result<T>.Failure());
    }

    [TestMethod]
    public void ToString_WhenIsNullSuccess_ReturnEmpty()
    {
        //Arrange
        var instance = Result<T>.Success(default!);

        //Act
        var result = instance.ToString();

        //Assert
        if (typeof(T).IsClass)
            result.Should().BeEmpty();
        else
            result.Should().Be(DefaultValue!.ToString());
    }

    [TestMethod]
    public void ToString_WhenIsNonNullSuccess_ReturnValueAsString()
    {
        //Arrange
        var value = Fixture.Create<T>();
        var instance = Result<T>.Success(value);

        //Act
        var result = instance.ToString();

        //Assert
        result.Should().Be(value!.ToString());
    }

    [TestMethod]
    public void ToString_WhenIsEmptyFailure_ReturnEmpty()
    {
        //Arrange

        //Act
        var result = Result<T>.Failure().ToString();

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void ToString_WhenIsFailureWithMessage_ReturnMessage()
    {
        //Arrange
        var message = Fixture.Create<string>();

        //Act
        var result = Result<T>.Failure(message).ToString();

        //Assert
        result.Should().Be(message);
    }

    [TestMethod]
    public void Deconstruct_Always_Deconstruct()
    {
        //Arrange
        var instance = Fixture.Create<Result<T>>();

        //Act
        var (isSuccess, value, message) = instance;

        //Assert
        isSuccess.Should().Be(instance.IsSuccess);
        value.Should().Be(instance.Value);
        message.Should().Be(instance.Message);
    }

    [TestMethod]
    public void DeconstructWithoutMessage_Always_Deconstruct()
    {
        //Arrange
        var instance = Fixture.Create<Result<T>>();

        //Act
        var (isSuccess, value) = instance;

        //Assert
        isSuccess.Should().Be(instance.IsSuccess);
        value.Should().Be(instance.Value);
    }

    [TestMethod]
    public void TryGetResultToResultConversionOperator_Always_Convert()
    {
        //Arrange
        var instance = Fixture.Create<TryGetResult<T>>();

        //Act
        Result<T> result = instance;

        //Assert
        result.IsSuccess.Should().Be(instance.IsSuccess);
        result.Value.Should().Be(instance.Value);
        result.Message.Should().BeEmpty();
    }

    [TestMethod]
    public void ResultToTryGetResultConversionOperator_Always_Convert()
    {
        //Arrange
        var instance = Fixture.Create<Result<T>>();

        //Act
        TryGetResult<T> result = instance;

        //Assert
        result.IsSuccess.Should().Be(instance.IsSuccess);
        result.Value.Should().Be(instance.Value);
    }

    [TestMethod]
    public void Ensure_ValueEquality() => Ensure.ValueEquality<Result<T>>(Fixture);

    [TestMethod]
    public void Ensure_ConsistentHashCode() => Ensure.ConsistentHashCode<Result<T>>(Fixture);

    [TestMethod]
    public void Ensure_IsJsonSerializable() => Ensure.IsJsonSerializable<Result<T>>(Fixture);
}