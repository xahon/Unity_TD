using Components.Helpers;
using UnityEngine;

namespace Components {

    public class BuildPlace : MonoBehaviour {
        private BuildingComponent _building; // TODO: GameObject?
        private BuildingComponent _buildingTransparent;

        private bool      _alreadyHovered;
        private Transform _transform;
        private Vector3   _buildPosition;

        private void Start() {
            _transform     = transform;
            _buildPosition = _transform.position + new Vector3(0, 0.5f, 0); // 0.5f?
        }

        private void OnMouseOver() {
            if (!_alreadyHovered) {
                OnEnterOnce();
                _alreadyHovered = true;
            }

            if (AlreadyBuilt() || !Input.GetKey(Controls.Build.Code) || !BuildManager.Instance.IsInBuildState()) {
                return;
            }

            Debug.Log("Building tower");
            _building = Instantiate(BuildManager.Instance.CurrentTowerToBuild, _buildPosition, Quaternion.identity);

            DestroyTransparentBuilding();
        }

        private void OnEnterOnce() {
            if (AlreadyBuilt() || BuildManager.Instance.CurrentTowerToBuild == null) return;

            _buildingTransparent = Instantiate(BuildManager.Instance.CurrentTowerToBuild, _buildPosition, Quaternion.identity);
            TransparentRenderering transparentRenderering = _buildingTransparent.gameObject.AddComponent<TransparentRenderering>();
            transparentRenderering.Alpha = 0.3f;

            CircleRenderer circleRenderer = _buildingTransparent.gameObject.AddComponent<CircleRenderer>();
            circleRenderer.Radius = _buildingTransparent.gameObject.GetComponent<IAttackComponent>().Range;
            circleRenderer.Color  = Color.blue;
        }

        private void OnMouseExit() {
            DestroyTransparentBuilding();
            _alreadyHovered = false;
        }

        private void DestroyTransparentBuilding() {
            Destroy(_buildingTransparent != null ? _buildingTransparent.gameObject : null);
        }

        private bool AlreadyBuilt() {
            return _building != null;
        }
    }

}