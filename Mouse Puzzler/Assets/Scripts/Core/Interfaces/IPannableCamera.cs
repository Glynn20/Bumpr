using UnityEngine;

public interface IPannableCamera
{
    public void SnapToPoint(Transform point);
    public void SnapToPointInstant(Transform point);
}
