using UnityEngine;
using Extensions;

namespace Components.Helpers {

    public class CircleRenderer : MonoBehaviour {
        public LineRenderer LineRenderer { get; private set; }
        public int          Smoothness = 72;
        public float        Radius     = 1.0f;
        public Color        Color      = Color.red;

        private Transform _transform;

        private void Start() {
            _transform                   = transform;
            LineRenderer                 = gameObject.GetOrAddComponent<LineRenderer>();
            LineRenderer.widthMultiplier = 0.04f;
            LineRenderer.material.color  = Color;
            LineRenderer.useWorldSpace   = false;
            LineRenderer.loop            = true;
        }

        private void GenerateCircle() {
            LineRenderer.positionCount = Smoothness;

            for (int i = 0; i < Smoothness; i++) {
                var angleRad = 360.0f / Smoothness * i * Mathf.Deg2Rad;
                LineRenderer.SetPosition(
                    Smoothness - i - 1,
                    new Vector3(
                        Mathf.Cos(angleRad) * Radius,
                        0.05f,
                        Mathf.Sin(angleRad) * Radius
                    )
                );
            }
        }

        private void Update() {
            GenerateCircle();
        }
    }

}