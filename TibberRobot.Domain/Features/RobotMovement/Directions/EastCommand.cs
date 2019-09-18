﻿using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public class EastCommand : CommandValidtor, ICommand
    {
        public decimal Limit { get; set; }

        public PositionResource GetNewPoint(decimal x, decimal y)
        {
            if (IsNextStepValidInPositiveGraph(x, Limit))
            {
                return new PositionResource(x + 1, y);
            }
            return null;
        }
    }
}
