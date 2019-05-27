using UnityEngine;

namespace BrunoMikoski.TextJuicer.Effects
{
    public abstract class VertexModifier : MonoBehaviour
    {
        private JuicedText juicedText;
        private JuicedTextMeshPro juicedTextMeshPo;

        private void OnValidate()
        {
            if (GetComponent<JuicedText>() != null)
                GetComponent<JuicedText>().SetDirty();
            else if (GetComponent<JuicedTextMeshPro>() != null)
                GetComponent<JuicedTextMeshPro>().SetDirty();
        }
        public abstract void Apply(CharacterData characterData, ref UIVertex uiVertex);
    }
}