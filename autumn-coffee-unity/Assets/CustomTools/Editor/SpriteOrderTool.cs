using System;
using UnityEngine;
using UnityEditor;

namespace CustomTools.Editor
{
    [CanEditMultipleObjects]
    public class SpriteOrderTool : EditorWindow
    {
        [MenuItem("Tools/Sprite Order")]
        static void Init()
        {
            SpriteOrderTool window = (SpriteOrderTool)EditorWindow.GetWindow(typeof(SpriteOrderTool), false, "Sprite Order Tool");
            window.Show();
        }

        private void OnGUI()
        {
            var selection = Selection.gameObjects;
            
            if (GUILayout.Button("Move Up +1"))
                Move(1, selection);
            if (GUILayout.Button("Move Up +5"))
                Move(5, selection);
            GUILayout.Space(15);
            GUI.Box(new Rect(0,45, position.width, 25), GetMiddleOrderValue(selection));
            GUILayout.Space(15);
            if (GUILayout.Button("Move Down -1"))
                Move(-1, selection);
            if (GUILayout.Button("Move Down -5"))
                Move(-5, selection);
            
        }

        private string GetMiddleOrderValue(GameObject[] selection)
        {
            int countOfSprites = 0;
            int sumOfOrderLayers = 0;
            for (int i = 0; i < selection.Length; i++)
            {
                var rend = selection[i].GetComponent<SpriteRenderer>();
                if(rend == null)
                    continue;

                countOfSprites++;
                sumOfOrderLayers += rend.sortingOrder;
            }

            return ((float) sumOfOrderLayers / (float) countOfSprites).ToString("0.0");
        }

        private void Move(int amount, GameObject[] selection)
        {
            for (int i = 0; i < selection.Length; i++)
            {
                var rend = selection[i].GetComponent<SpriteRenderer>();
                if(rend == null)
                    continue;

                rend.sortingOrder += amount;
            }
        }
    }
}
