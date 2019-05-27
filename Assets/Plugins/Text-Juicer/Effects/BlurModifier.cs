using UnityEngine;

namespace BrunoMikoski.TextJuicer.Effects
{
    [AddComponentMenu("UI/Text Juicer/Effects/Blur")]
    public class BlurModifier : VertexModifier
    {
        [SerializeField]
        private AnimationCurve curve = new AnimationCurve(new Keyframe(0, 1));

        public override void Apply(CharacterData characterData, ref UIVertex uiVertex)
        {
            uiVertex.color *= new Color(1, 1, 1, curve.Evaluate(characterData.Progress));
        }
    }
}