using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class UnityExtension
{
    public static Sequence DOJumpOne(this Transform target, Vector3 endValue, float jumpPower, float duration,
        Vector3 up, bool snapping = false)
    {
        Vector3 halfEndTarget = target.position + (endValue - target.position) / 2;
        float upX = up.normalized.x * jumpPower;
        float upY = up.normalized.y * jumpPower;
        float upZ = up.normalized.z * jumpPower;
        Sequence s = DOTween.Sequence();
        Tween yTween = DOTween.To(() => target.position, x => target.position = x, new Vector3(0, halfEndTarget.y + upY, 0), duration/2 )
                .SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.Linear);

        s.Append(DOTween.To(() => target.position, x => target.position = x, new Vector3(halfEndTarget.x + upX, 0, 0), duration/2)
                .SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)
            ).Join(DOTween.To(() => target.position, x => target.position = x, new Vector3(0, 0, halfEndTarget.z + upZ), duration/2)
                .SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear)
            ).Join(yTween).SetTarget(target).SetEase(DOTween.defaultEaseType);


        Tween yTween2 = DOTween.To(() => target.position, x => target.position = x, new Vector3(0, endValue.y, 0), duration/2)
            .SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.Linear);

        s.Append(DOTween.To(() => target.position, x => target.position = x, new Vector3(endValue.x, 0, 0), duration/2)
            .SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)
        ).Join(DOTween.To(() => target.position, x => target.position = x, new Vector3(0, 0, endValue.z), duration/2)
            .SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear)
        ).Join(yTween2).SetTarget(target).SetEase(DOTween.defaultEaseType);

        return s;
    }
}


