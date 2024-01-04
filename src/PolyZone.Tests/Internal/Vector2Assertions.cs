using CitizenFX.Core;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using PolyZone.Shapes;

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

    public AndConstraint<Vector2Assertions> BeInside(Polygon polygon, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(polygon.Contains(_instance))
            .FailWith($"Expected point to be inside polygon, point was { _instance.X }, { _instance.Y }");

        return new AndConstraint<Vector2Assertions>(this);
    }
}
