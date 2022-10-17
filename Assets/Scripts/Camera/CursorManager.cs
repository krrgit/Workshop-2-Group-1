using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {
    [SerializeField] Texture2D aimCursor;

    Vector2 cursorHotspot;

    void Start()
    {
        cursorHotspot = new Vector2(aimCursor.width / 2, aimCursor.height / 2);
        Cursor.SetCursor(aimCursor, cursorHotspot, CursorMode.Auto);
    }
}
