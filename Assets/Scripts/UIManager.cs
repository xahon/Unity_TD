using Helpers;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    public  Text ControlInfoText;
    private bool _buildingEnabled;

    private void Start() {
        _buildingEnabled              =  BuildManager.Instance.IsInBuildState();
        Events.OnBuildingStateToggled += OnBuildingStateToggled;

        OnChange();
    }

    private void OnBuildingStateToggled(bool isEnabled) {
        _buildingEnabled = isEnabled;
        OnChange();
    }

    private void OnChange() {
        ControlInfoText.text =
            $"To toggle building mode press {Emphasis.Emphasize(Controls.ToggleBuild)} key\n" +
            $"Build enabled: {(_buildingEnabled ? Emphasis.Emphasize("YES", EmphasizeKind.Positive) : Emphasis.Emphasize("NO", EmphasizeKind.Negative))}\n" +
            $"Tower shooting? It's not implemented yet {Emphasis.Emphasize(":)", EmphasizeKind.Positive)}";
    }

}

public struct Emphasis {
    public string Color { get; }

    public Emphasis(string color) {
        Color = color;
    }

    public static string Emphasize(string text, Emphasis emphasis) {
        return $"<color={emphasis.Color}>{text}</color>";
    }

    public static string Emphasize(ControlInfo control) {
        return Emphasize(control.Name, EmphasizeKind.Control);
    }
}

public static class EmphasizeKind {
    public static readonly Emphasis Control  = new Emphasis("yellow");
    public static readonly Emphasis Positive = new Emphasis("lime");
    public static readonly Emphasis Negative = new Emphasis("red");
}