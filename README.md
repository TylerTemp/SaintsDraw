# SaintsDraw #

[![openupm](https://img.shields.io/npm/v/today.comes.saintsdraw?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/today.comes.saintsdraw/)
[![openupm](https://img.shields.io/badge/dynamic/json?color=brightgreen&label=downloads&query=%24.downloads&suffix=%2Fmonth&url=https%3A%2F%2Fpackage.openupm.com%2Fdownloads%2Fpoint%2Flast-month%2Ftoday.comes.saintsdraw)](https://openupm.com/packages/today.comes.saintsdraw/)

`SaintsDraw` allow you to draw arrow, circle, and arc in Unity, using Gizmos or LineRenderer

Developed by: [TylerTemp](https://github.com/TylerTemp), [墨瞳](https://github.com/xc13308)

Unity: 2019.1 or above

## Installation ##

*   Using [OpenUPM](https://openupm.com/packages/today.comes.saintsdraw/)

    ```bash
    openupm add today.comes.saintsdraw
    ```

*   Using git upm:

    add this line to manifest.json in your project

    ```javascript
    {
        "dependencies": {
            "today.comes.saintsdraw": "https://github.com/TylerTemp/SaintsDraw.git",
            // your other dependencies...
        }
    }
    ```

*   Using a unitypackage:

    Go to the [Release Page](https://github.com/TylerTemp/SaintsDraw/releases) to download a desired version of unitypackage and import it to your project

*   Using a git submodule:

    ```bash
    git submodule add https://github.com/TylerTemp/SaintsDraw.git Assets/SaintsDraw
    ```
    
## Change Log ##

**1.0.2**

Add `camera` & `distance` for `DrawWireRectTransform` to support camera mode of a canvas.

See [the full change log](https://github.com/TylerTemp/SaintsDraw/blob/master/CHANGELOG.md).

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

```csharp
using SaintsDraw;
Arrow.Draw(Vector3.zero, Vector3.one);
```

### Circle (Disk) ###

![circle](https://github.com/TylerTemp/SaintsDraw/assets/6391063/6eaef5f9-2b00-433c-86a9-368c04061ebe)

Using `Circle.Draw` to draw an circle (disk), which has parameters:

*   `Vector3 center` center of the circle
*   `float radius` radius of the circle
*   `Vector3 upward` up direction of the circle. The circle is always perpendicular to this value. Usually `Vector3.up` is used
*   `int numSegments` how many segments to draw for the arc. The bigger it is, the smoother the arc is

Using `Circle.DrawBySegCount` to draw an circle with fixed segment steps, which means each segment will have the same angle. It has the same parameters as `Circle.Draw` except `int numSegments` is replaced by `float segAngle`.

Append a `LineRenderer` as the first parameter to draw the arc using `LineRenderer`

```csharp
using SaintsDraw;
Circle.Draw(Vector3.zero, 5f, Vector3.up, 40);
```

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

Using `Arc.DrawBySegCount` to draw an with fixed segment steps, which means each segment will have the same angle. It has the same parameters as `Arc.Draw` except `int numSegments` is replaced by `float segAngle`.


Append a `LineRenderer` as the first parameter to draw the arc using `LineRenderer`

```csharp
using SaintsDraw;
Arc.Draw(Vector3.zero, 5f, 60f, 120f, Vector3.up, Vector3.left, 40);
```

### `DrawWireRectTransform` ###

```csharp
UIGizmos.DrawWireRectTransform(RectTransform rectTransform, Camera camera=null, float distance=5f)
```

Draw a wireframe of a RectTransform in the scene view. This works even the `RectTransform` has rotation and scale.

Parameters:
*   `RectTransform rectTransform` the RectTransform to draw
*   `Camera camera` the camera of the canvas. `null` for `Overlay` type.
*   `float distance` the distance from the camera to the wireframe. This only works if you pass the camera parameter which is the target camera of the canvas, and ensure your `rectTransform` is under this canvas too.

```csharp
UIGizmos.DrawWireRectTransform(GetComponent<RectTransform>());
```

![DrawWireRectTransform](https://github.com/TylerTemp/SaintsDraw/assets/6391063/bf4c2d67-0731-4cd2-aad3-1653bc9420ac)

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

### Arc Tools ###

this will normalized your angle, which allow over 360 but will has no overlap

```csharp
(float normFromArc, float normToArc) = Arc.NormalAngleRange(_fromArc, _toArc);
```

this will display an arrow from arc center to the angle you want to check, helpful when testing `upward` and `plate`

```csharp
Vector3 startPos = Arc.GetDirection(_upward, _plate, angle).normalized * _arcRadis;
Arrow.Draw(Vector3.zero, startPos);
```
