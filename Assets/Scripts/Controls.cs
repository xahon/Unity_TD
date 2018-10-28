using UnityEngine;

public struct ControlInfo {
    public KeyCode Code { get; }
    public string  Name { get; }

    public ControlInfo(KeyCode code, string name) {
        Code = code;
        Name = name;
    }
}

public static class Controls {
    public static readonly ControlInfo ToggleBuild = new ControlInfo(KeyCode.Space, "Space");
    public static readonly ControlInfo Build       = new ControlInfo(KeyCode.Mouse0, "LMB");
}