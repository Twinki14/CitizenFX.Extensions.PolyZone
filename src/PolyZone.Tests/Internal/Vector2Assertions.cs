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
    private readonly Vector2 _instance = instance;
    protected override string Identifier => "directory";

    public AndConstraint<Vector2Assertions> BeInside(ISpatial2dShape shape, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(shape.Contains(_instance))
            .FailWith($"Expected point to be inside polygon, point was { _instance.X }, { _instance.Y }");

        return new AndConstraint<Vector2Assertions>(this);
    }
    
    public AndConstraint<Vector2Assertions> BeOutside(ISpatial2dShape shape, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(shape.Contains(_instance) is false)
            .FailWith($"Expected point to be outside polygon, point was { _instance.X }, { _instance.Y }");

        return new AndConstraint<Vector2Assertions>(this);
    }
}
