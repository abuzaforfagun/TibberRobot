using System.Collections.Generic;
using TibberRobot.Domain.Features.RobotMovement.CommandValidators;
using Xunit;

namespace TibberRobot.Domain.Tests
{
    public class CommandValidatorsTests
    {
        [Theory]
        [MemberData(nameof(CommandValidatorData))]
        public void CommandValidator_IsValidCommand_ShouldReturn_CorrectData(decimal position, decimal limit, bool expectedResult)
        {
            var validator = new CommandValidator();

            var result = validator.IsValidCommand(position, limit);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [MemberData(nameof(NegativeGraphCommandValidatorData))]
        public void NegativeGraphCommandValidator_IsValidCommand_ShouldReturn_CorrectData(decimal position, decimal limit, bool expectedResult)
        {
            var validator = new NegativeGraphCommandValidator();

            var result = validator.IsValidCommand(position, limit);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [MemberData(nameof(PositiveGraphCommandValidatorData))]
        public void PositiveGraphCommandValidator_IsValidCommand_ShouldReturn_CorrectData(decimal position, decimal limit, bool expectedResult)
        {
            var validator = new PositiveGraphCommandValidator();

            var result = validator.IsValidCommand(position, limit);

            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> CommandValidatorData() =>
            new List<object[]>
            {
                new object[] { 10, 10, false },
                new object[] { 9, 10, true },
                new object[] { 0, 5, true }
            };

        public static IEnumerable<object[]> NegativeGraphCommandValidatorData() =>
            new List<object[]>
            {
                new object[] { 10, 10, false },
                new object[] { 10, -10, true },
                new object[] { -9, -10, true },
                new object[] { 0, 5, false },
                new object[] { -1, 5, false },
            };

        public static IEnumerable<object[]> PositiveGraphCommandValidatorData() =>
            new List<object[]>
            {
                new object[] { 10, 10, false },
                new object[] { 10, -10, false },
                new object[] { 0, -5, false },
                new object[] { -9, -10, true },
                new object[] { -1, 5, true },
            };
    }
}
