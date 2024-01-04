using CitizenFX.Core;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Tests.Internal;

public static class Vector2AssertionExtensions
{
    public static Vector2Assertions Should(this Vector2 instance)
    {
        return new Vector2Assertions(instance); 
    } 
}

public class Vector2Assertions(Vector2 instance) : ReferenceTypeAssertions<Vector2, Vector2Assertions>(instance)
{
    protected override string Identifier => "directory";

    public AndConstraint<Vector2Assertions> BeInside(ISpatial2dShape shape, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(shape.Contains(instance))
            .FailWith($"Expected point to be inside polygon, point was { instance.X }, { instance.Y }");

        return new AndConstraint<Vector2Assertions>(this);
    }
}
