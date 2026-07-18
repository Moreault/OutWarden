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
        var message = Dummy.Create<string>();

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
        var value = Dummy.Create<T>();

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
            result.Should()!.Be(DefaultValue!.ToString());
    }

    [TestMethod]
    public void ToString_WhenIsNonNullSuccess_ReturnValueAsString()
    {
        //Arrange
        var value = Dummy.Create<T>();
        var instance = Result<T>.Success(value);

        //Act
        var result = instance.ToString();

        //Assert
        result.Should()!.Be(value!.ToString());
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
        var message = Dummy.Create<string>();

        //Act
        var result = Result<T>.Failure(message).ToString();

        //Assert
        result.Should().Be(message);
    }

    [TestMethod]
    public void Deconstruct_Always_Deconstruct()
    {
        //Arrange
        var instance = Dummy.Create<Result<T>>();

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
        var instance = Dummy.Create<Result<T>>();

        //Act
        var (isSuccess, value) = instance;

        //Assert
        isSuccess.Should().Be(instance.IsSuccess);
        value.Should().Be(instance.Value);
    }

    [TestMethod]
    public void ImplicitConversion_Always_ReturnSuccess()
    {
        //Arrange
        var value = Dummy.Create<T>();

        //Act
        Result<T> result = value;

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(value);
        result.Message.Should().BeEmpty();
    }

    [TestMethod]
    public void IsFailure_WhenIsSuccess_ReturnFalse()
    {
        //Arrange
        var result = Result<T>.Success(Dummy.Create<T>());

        //Act
        //Assert
        result.IsFailure.Should().BeFalse();
    }

    [TestMethod]
    public void IsFailure_WhenIsNotSuccess_ReturnTrue()
    {
        //Arrange
        var result = Result<T>.Failure(Dummy.Create<string>());

        //Act
        //Assert
        result.IsFailure.Should().BeTrue();
    }

    [TestMethod]
    public void ValueOr_WhenIsSuccess_ReturnValue()
    {
        //Arrange
        var value = Dummy.Create<T>();
        var result = Result<T>.Success(value);
        var fallback = Dummy.Create<T>();

        //Act
        var output = result.ValueOr(fallback);

        //Assert
        output.Should().Be(value);
    }

    [TestMethod]
    public void ValueOr_WhenIsFailure_ReturnFallback()
    {
        //Arrange
        var result = Result<T>.Failure(Dummy.Create<string>());
        var fallback = Dummy.Create<T>();

        //Act
        var output = result.ValueOr(fallback);

        //Assert
        output.Should().Be(fallback);
    }

    [TestMethod]
    public void ValueOrThrow_WhenIsSuccess_ReturnValue()
    {
        //Arrange
        var value = Dummy.Create<T>();
        var result = Result<T>.Success(value);

        //Act
        var output = result.ValueOrThrow();

        //Assert
        output.Should().Be(value);
    }

    [TestMethod]
    public void ValueOrThrow_WhenIsFailureWithNoCustomMessage_ThrowWithResultMessage()
    {
        //Arrange
        var message = Dummy.Create<string>();
        var result = Result<T>.Failure(message);

        //Act
        var action = () => result.ValueOrThrow();

        //Assert
        action.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [TestMethod]
    public void ValueOrThrow_WhenIsFailureWithCustomMessage_ThrowWithCustomMessage()
    {
        //Arrange
        var result = Result<T>.Failure(Dummy.Create<string>());
        var customMessage = Dummy.Create<string>();

        //Act
        var action = () => result.ValueOrThrow(customMessage);

        //Assert
        action.Should().Throw<InvalidOperationException>().WithMessage(customMessage);
    }

    [TestMethod]
    public void Ensure_ValueEquality() => Ensure.ValueEquality<Result<T>>(Dummy);

    [TestMethod]
    public void Ensure_ValueHashCode() => Ensure.ValueHashCode<Result<T>>(Dummy, JsonSerializerOptions.Default);

    [TestMethod]
    public void Ensure_IsJsonSerializable() => Ensure.IsJsonSerializable<Result<T>>(Dummy);

    [TestMethod]
    public void Ensure_IsFailureIsNotSerialized()
    {
        //Arrange
        var instance = Dummy.Create<Result<T>>();

        //Act
        var json = JsonSerializer.Serialize(instance);

        //Assert
        json.Should().NotContain("IsFailure");
        json.Should().NotContain("isFailure");
    }
}