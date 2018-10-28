using UnityEngine;
using UnityEngine.UI;

namespace Components.Gui {

    public class WorldSpaceLabel : MonoBehaviour {
        public Text LabelElement;

        public string LabelName { get; }

        public string Text {
            get { return _text; }
            set {
                _text = value;
                if (_label != null) {
                    _label.text = value;
                }
            }
        }

        private Transform _transform;
        private Transform _labelTransform;
        private Text      _label;
        private string    _text;

        private static GameObject _canvas;

        private void Awake() {
            _transform = transform;

            if (_canvas == null) {
                _canvas = GameObject.FindWithTag("Canvas");
            }

            if (_label == null) {
                _label      = Instantiate(LabelElement, _canvas.transform);
                _label.name = $"Game Label - {LabelName}";
            }

            _labelTransform = _label.transform;
        }

        private void OnDestroy() {
            if (_label != null) {
                Destroy(_label.gameObject);
            }
        }

        private void Update() {
            Vector2 position = Camera.main.WorldToScreenPoint(_transform.position);
            _labelTransform.position = position;
        }
    }

}