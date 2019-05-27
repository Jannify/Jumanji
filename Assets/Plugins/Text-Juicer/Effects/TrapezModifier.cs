using System;
using UnityEngine;

namespace BrunoMikoski.TextJuicer.Effects
{
    [AddComponentMenu("UI/Text Juicer/Effects/TrapezModifier")]
    public class TrapezModifier : VertexModifier
    {
        [SerializeField]
        private AnimationCurve xCurve = new AnimationCurve(new Keyframe(0, 1));
        [SerializeField]
        private AnimationCurve yCurve = new AnimationCurve(new Keyframe(0, 1));
        [SerializeField]
        private float xRange = 5;
        [SerializeField]
        private float yRange = 5;

        public override void Apply(CharacterData characterData, ref UIVertex uiVertex)
        {
            double f = (Math.PI * (characterData.Order + 1));
            string h = f.ToString() + "00000";
            long g = long.Parse(h.Substring(4, 9));
            float xRandom = g / 5000000f;
            int neg = int.Parse(h.Substring(4, 5));
            if (characterData.Order % 2 == 1) xRandom *= -1f;

            Vector3 posRaw = new Vector3((1f - xCurve.Evaluate(characterData.Progress)) * (xRange + xRandom), (1f - yCurve.Evaluate(characterData.Progress)) * -yRange, 0);

            uiVertex.position += posRaw;

        }
    }
}