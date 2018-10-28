using UnityEngine;

namespace Components.Helpers {

    public class TransparentRenderering : MonoBehaviour {
        private Renderer[] _renderers;
        public  float      Alpha = 1.0f;

        private void Start() {
            _renderers = GetComponentsInChildren<Renderer>();

            foreach (var curRenderer in _renderers) {
                foreach (var curMaterial in curRenderer.materials) {
                    Color color = curMaterial.color;
                    color.a           = Alpha;
                    curMaterial.color = color;
                }
            }
        }
    }

}