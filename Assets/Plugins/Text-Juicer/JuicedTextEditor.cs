using UnityEditor;
using UnityEngine;

namespace BrunoMikoski.TextJuicer
{
    [CustomEditor(typeof(JuicedText))]
    class JuicedTextEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            JuicedText juicedText = (JuicedText)target;
            if (GUILayout.Button("Play")) juicedText.Play();
        }
    }

    [CustomEditor(typeof(JuicedTextMeshPro))]
    class JuicedTextMeshProEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            JuicedTextMeshPro juicedText = (JuicedTextMeshPro)target;
            if (GUILayout.Button("Play")) juicedText.Play();
        }
    }
}
