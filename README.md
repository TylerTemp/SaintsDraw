# SaintsDraw #

`SaintsDraw` allow you to draw arrow, circle, and arc in Unity, using Gizmos or LineRenderer

## Draw ##

### Arrow ###

![arrow](https://github.com/TylerTemp/SaintsDraw/assets/6391063/603ec523-98de-45b0-87a6-50761a3d5a8c)

Using `Arrow.Draw` to draw an arrow, which has parameters:

*   `Vector3 from` point where the arrow starts (tail)
*   `Vector3 to` points where the arrow ends (head)
*   `float arrowHeadLength  = 0.5f`
*   `float arrowHeadAngle = 20.0f`
*   `Vector3? up = null` up direction of the arrow, default is `Vector3.up`. This is useful when you have some rotation on the arrow. The arrow is always perpendicular to this `up` direction.

Append a `LineRenderer` as the first parameter to draw the arrow using `LineRenderer`

### Arc ###

![circle_arc](https://github.com/TylerTemp/SaintsDraw/assets/6391063/bb6ca2e8-cb52-405c-954a-c31773c0a629)


Using `Arc.Draw` to draw an arc, which has parameters:

*   `Vector3 center` center of the arc
*   `float radius` radius of the arc
*   `float fromArc` angle to start
*   `float toArc` angle to end
*   `Vector3 upward` up direction of the arc. The arc is always perpendicular to this value. Usually `Vector3.up` is used
*   `Vector3 plate` as the arc no has a plate which is perpendicular to the arc, this parameter is used to determine the plate's start point. It'll be automatically put on the plate defined by the `upward` direction.

    Usually `Vector3.left` or `Vector3.forward` is used

*   `int numSegments` how many segments to draw for the arc. The bigger it is, the smoother the arc is

Using `Arc.DrawBySegCount` to draw an with fixed segment step, which means each segment will have the same angle. It has the same parameters as `Arc.Draw` except `int numSegments` is replaced by `float segAngle`.


Append a `LineRenderer` as the first parameter to draw the arc using `LineRenderer`

### Circle ###

`Circle` is just a shortcut of Arc with `fromArc = 0` and `toArc = 360`. Just omit these two parameters with the same function name as `Arc` to draw a circle.


## Some Tools ##

### Gizmos Color ###

```csharp
using (new ColorScoop(Color.green))
{
    Arrow.Draw(Vector2.zero, Vector2.up);
}
```

### Gizmos Matrix ###

Useful if you want to draw gizmos in local space inheriting parent's scale and rotation

```csharp
using (new MatrixScoop(transform.localToWorldMatrix))
{
    Arrow.Draw(Vector2.zero, Vector2.up);
}
```

### Circle/Arc Tools ###


this will normalized your angle, which allow over 360 but will has no overlap

```csharp
(float normFromArc, float normToArc) = Arc.NormalAngleRange(_fromArc, _toArc);
```

this will display an arrow from arc center to the angle you want to check, helpful when testing `upward` and `plate`

```csharp
Vector3 startPos = Arc.GetDirection(_upward, _plate, angle).normalized * _arcRadis;
Arrow.Draw(Vector3.zero, startPos);
```
