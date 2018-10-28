using System;
using System.Collections;
using UnityEngine;

public static class Actions {

    public static IEnumerator MoveToPosition(MovableComponent self, Vector3 position, Action onFinish) {
        if (Mathf.Abs(self.Speed) <= 10e-5) {
            Debug.LogError("Speed can't be less than 0");
            yield return null;
        }

        float   timeStarted   = Time.time;
        Vector3 startPosition = self.Transform.position;
        Vector3 endPosition   = position;
        float   distance      = Vector3.Distance(startPosition, endPosition);
        float   timeNeeded    = distance / self.Speed;

        while (true) {
            if (Mathf.Abs(timeNeeded) <= 10e-5) {
                break;
            }

            var step = Mathf.Clamp01((Time.time - timeStarted) / timeNeeded);

            self.Transform.position = Vector3.Lerp(startPosition, endPosition, step);

            if (self.Transform.position == endPosition) {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        onFinish();
    }

    public static IEnumerator MoveToTarget(MovableComponent self, MovableComponent target, Action onFinish) {
        if (Mathf.Abs(self.Speed) <= 10e-5) {
            Debug.LogError("Speed can't be less than 0");
            yield return null;
        }

        float   timeStarted   = Time.time;
        Vector3 startPosition = self.Transform.position;
        Vector3 endPosition   = new Vector3();

        while (true) {
            endPosition.Set(target.Transform.position.x, target.Transform.position.y, target.Transform.position.z);
            float distance   = Vector3.Distance(startPosition, endPosition);
            float timeNeeded = distance / self.Speed;

            if (Mathf.Abs(timeNeeded) <= 10e-5) {
                break;
            }

            var step = Mathf.Clamp01((Time.time - timeStarted) / timeNeeded);

            self.Transform.position = Vector3.Lerp(startPosition, endPosition, step);

            if (self.Transform.position == endPosition) {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        onFinish();
    }
}