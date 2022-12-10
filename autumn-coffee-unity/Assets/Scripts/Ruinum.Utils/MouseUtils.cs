using UnityEngine;


namespace Ruinum.Utils
{
    public static class MouseUtils
    {
        public static Vector3 GetMouseWorld2DPosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        public static bool TryRaycast2DToMousePosition(out RaycastHit2D raycastHit2D)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            raycastHit2D = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (raycastHit2D.collider != null) return true;

            return false;
        }
    }
}
