using Components;
using Helpers;
using UnityEngine;

public class BuildManager : Singleton<BuildManager> {

    [SerializeField] private BuildingComponent _buildable;

    public BuildingComponent CurrentTowerToBuild { get; private set; } = null;

    public bool IsInBuildState() {
        return Instance.CurrentTowerToBuild != null;
    }

    private void Update() {
        if (Input.GetKeyDown(Controls.ToggleBuild.Code)) {
            CurrentTowerToBuild = CurrentTowerToBuild == null ? _buildable : null;
            Events.FireBuildingStateToggledEvent(CurrentTowerToBuild != null);
        }
    }

}