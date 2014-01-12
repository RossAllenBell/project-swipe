using UnityEngine;

public class Utils {
    
    public static void DrawOutline(Rect position, string text, GUIStyle style) {
            DrawOutline(position, text, style, 2);
    }
    
    public static void DrawOutline(Rect position, string text, GUIStyle style, int offset) {
            DrawOutline(position, text, style, offset, style.normal.textColor);
    }
            
    public static void DrawOutline(Rect position, string text, GUIStyle style, int offset, Color color) {
            DrawOutline(position, text, style, offset, color, InvertColor(color));
    }
    
    public static void DrawOutline(Rect position, string text, GUIStyle style, int offset, Color color, Color outColor){
        GUIStyle backupStyle = style;
        style.normal.textColor = outColor;
        position.x -= offset;
        GUI.Label(position, text, style);
        position.x += offset * 2;
        GUI.Label(position, text, style);
        position.x -= offset;
        position.y -= offset;
        GUI.Label(position, text, style);
        position.y += offset * 2;
        GUI.Label(position, text, style);
        position.y -= offset;
        style.normal.textColor = color;
        GUI.Label(position, text, style);
        style = backupStyle;
    }
    
    public static Color InvertColor (Color color) {
        return new Color (1.0f-color.r, 1.0f-color.g, 1.0f-color.b);
    }
        
}
