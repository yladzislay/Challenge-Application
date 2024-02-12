using Application.Domain.Services;
using Xunit;

namespace Tests;

public class NotificationServiceTests
{
    [Fact]
    public void NotifyApproachingDepositLimit_ShouldGenerateCorrectMessage()
    {
        // Arrange
        var notificationService = new NotificationService();
        const string emailAddress = "test@example.com";
        const decimal currentDepositAmount = 3000m;
        const decimal limit = 4000m;
        var expectedMessage = "[Notification]: Approaching deposit limit!"
                              + Environment.NewLine
                              + $"-[Current deposit amount]: {currentDepositAmount}"
                              + Environment.NewLine
                              + $"-[Limit]: {limit} "
                              + Environment.NewLine
                              + $"-[Notifying user]: {emailAddress}"
                              + Environment.NewLine;

        // Act
        string? actualMessage;
        using (var consoleOutput = new ConsoleOutputRedirector())
        {
            notificationService.NotifyApproachingDepositLimit(emailAddress, currentDepositAmount, limit);
            actualMessage = consoleOutput.GetOutput();
        }

        // Assert
        Assert.Equal(expectedMessage, actualMessage);
    }
}

// Helper class to redirect console output for testing
public class ConsoleOutputRedirector : IDisposable
{
    private readonly StringWriter _consoleOutput;
    private readonly TextWriter _originalOutput;

    public ConsoleOutputRedirector()
    {
        _consoleOutput = new StringWriter();
        _originalOutput = Console.Out;
        Console.SetOut(_consoleOutput);
    }

    public string GetOutput()
    {
        return _consoleOutput.ToString();
    }

    public void Dispose()
    {
        Console.SetOut(_originalOutput);
        _consoleOutput.Dispose();
    }
}