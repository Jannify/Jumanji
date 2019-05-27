using BrunoMikoski.TextJuicer.Effects;
using System.Linq;
using TMPro;
using UnityEngine;

namespace BrunoMikoski.TextJuicer
{
    [ExecuteAlways()]
    [RequireComponent(typeof(TextMeshProUGUI))]
    [AddComponentMenu("UI/Text Juicer/Juiced TextMeshPro")]
    public class JuicedTextMeshPro : MonoBehaviour
    {
        public const string VERSION = "0.0.2";

        [SerializeField]
        private float duration = 1.0f;
        [SerializeField]
        private float delay = 0.01f;
        [SerializeField, Range(0.0f, 1.0f)]
        private float progress;
        [SerializeField]
        private bool playOnEnable = true;
        [SerializeField]
        private bool loop = false;
        [SerializeField]
        private bool playForever = false;

        private CharacterData[] charactersData;
        private UIVertex[] textVertex;
        private VertexModifier[] vertexModifiers;
        private bool isPlaying;
        private float internalTime;
        private bool isDirty;
        private float lastInternalTime;
        private float realTotalAnimationTime;

        private TextMeshProUGUI TMP_textComponent;
        public TextMeshProUGUI TMP_TextComponent
        {
            get {
                if (TMP_textComponent == null)
                {
                    SetDirty();
                    UpdateComponents();
                }
                return TMP_textComponent;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            SetDirty();
        }
#endif

        private void OnEnable()
        {
            SetDirty();
            if (Application.isPlaying && playOnEnable) Play();
        }

        private UIVertex ModifyTextMeshProVertex(CharacterData characterData, UIVertex uiVertex)
        {
            for (int i = 0; i < vertexModifiers.Length; i++)
            {
                VertexModifier vertexModifier = vertexModifiers[i];
                vertexModifier.Apply(characterData, ref uiVertex);
            }
            return uiVertex;
        }

        public void ModifyTextMeshProMesh()
        {
            if (TMP_textComponent.text.Length != charactersData.Length || textVertex.Length == 0)
            {
                SetDirty();
                return;
            }

            if (isDirty) return;

            UIVertex[] tmpUIVertex = new UIVertex[textVertex.Length];
            for (int i = 0; i < charactersData.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    tmpUIVertex[i * 4 + j] = ModifyTextMeshProVertex(charactersData[i], textVertex[i * 4 + j]);
                }
            }

            try
            {
                for (int i = 0; i < TMP_textComponent.textInfo.meshInfo?[0].mesh?.vertices?.Length; i++)
                {
                    TMP_textComponent.textInfo.meshInfo[0].vertices[i] = tmpUIVertex[i].position;
                    TMP_textComponent.textInfo.meshInfo[0].uvs0[i] = tmpUIVertex[i].uv0;
                    TMP_textComponent.textInfo.meshInfo[0].uvs2[i] = tmpUIVertex[i].uv1;
                    TMP_textComponent.textInfo.meshInfo[0].normals[i] = tmpUIVertex[i].normal;
                    TMP_textComponent.textInfo.meshInfo[0].colors32[i] = tmpUIVertex[i].color;
                    TMP_textComponent.textInfo.meshInfo[0].tangents[i] = tmpUIVertex[i].tangent;

                    TMP_textComponent.textInfo.meshInfo[0].mesh.vertices[i] = tmpUIVertex[i].position;
                    TMP_textComponent.textInfo.meshInfo[0].mesh.uv[i] = tmpUIVertex[i].uv0;
                    TMP_textComponent.textInfo.meshInfo[0].mesh.uv2[i] = tmpUIVertex[i].uv1;
                    TMP_textComponent.textInfo.meshInfo[0].mesh.normals[i] = tmpUIVertex[i].normal;
                    TMP_textComponent.textInfo.meshInfo[0].mesh.tangents[i] = tmpUIVertex[i].tangent;
                }
                TMP_textComponent.textInfo.meshInfo[0].mesh.SetVertices(tmpUIVertex.Select(vertex => vertex.position).ToList());
                TMP_textComponent.textInfo.meshInfo[0].mesh.SetUVs(0, tmpUIVertex.Select(vertex => vertex.uv0).ToList());
                TMP_textComponent.textInfo.meshInfo[0].mesh.SetUVs(1, tmpUIVertex.Select(vertex => vertex.uv1).ToList());
                TMP_textComponent.textInfo.meshInfo[0].mesh.SetNormals(tmpUIVertex.Select(vertex => vertex.normal).ToList());
                TMP_textComponent.textInfo.meshInfo[0].mesh.SetColors(tmpUIVertex.Select(vertex => vertex.color).ToList());
                TMP_textComponent.textInfo.meshInfo[0].mesh.SetTangents(tmpUIVertex.Select(vertex => vertex.tangent).ToList());
                TMP_textComponent.UpdateGeometry(TMP_textComponent.textInfo.meshInfo[0].mesh, 0);

            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private void FixedUpdate()
        {
            UpdateComponents();
            ModifyTextMeshProMesh();
            UpdateTime();
            CheckAnimation();
        }

        public void Complete()
        {
            if (isPlaying)
                progress = 1.0f;
        }

        public void Restart()
        {
            internalTime = 0;
            SetDirty();
        }

        public void Play(bool fromBeginning = true)
        {
            if (fromBeginning)
                Restart();

            isPlaying = true;
        }

        public void Stop()
        {
            isPlaying = false;
        }

        private void CheckAnimation()
        {
            if (isPlaying)
            {
                if (internalTime + Time.deltaTime <= realTotalAnimationTime || playForever)
                {
                    internalTime += Time.deltaTime;
                }
                else
                {
                    if (loop)
                    {
                        internalTime = 0;
                    }
                    else
                    {
                        internalTime = realTotalAnimationTime;
                        progress = 1.0f;
                        Stop();
                    }
                }
            }
        }

        private void UpdateTime()
        {
            if (!isPlaying)
            {
                internalTime = progress * realTotalAnimationTime;
            }
            else
            {
                progress = internalTime / realTotalAnimationTime;
            }

            for (int i = 0; i < charactersData.Length; i++)
                charactersData[i].UpdateTime(internalTime);

            if (internalTime != lastInternalTime)
            {
                lastInternalTime = internalTime;
                TMP_textComponent.SetAllDirty();
            }
        }

        private void UpdateComponents()
        {
            if (isDirty)
            {
                if (TMP_textComponent == null) TMP_textComponent = GetComponent<TextMeshProUGUI>();
                vertexModifiers = GetComponents<VertexModifier>();

                if (TMP_textComponent?.text.Length != charactersData?.Length)
                {
                    TMP_textComponent.ForceMeshUpdate();
                    Mesh tmpMesh = TMP_textComponent.textInfo.meshInfo[0].mesh;
                    textVertex = new UIVertex[tmpMesh.vertices.Length];
                    for (int i = 0; i < tmpMesh.vertices.Length; i++)
                    {
                        textVertex[i].position = tmpMesh.vertices[i];
                        textVertex[i].uv0 = tmpMesh.uv[i];
                        textVertex[i].uv1 = tmpMesh.uv2[i];
                        textVertex[i].normal = tmpMesh.normals[i];
                        textVertex[i].color = tmpMesh.colors[i];
                        textVertex[i].tangent = tmpMesh.tangents[i];
                    }
                    Debug.Log("Updated Mesh");
                }

                int charCount = TMP_textComponent.text.Length;
                charactersData = new CharacterData[charCount];

                realTotalAnimationTime = duration +
                                         (charCount * delay);

                for (int i = 0; i < charCount; i++)
                {
                    charactersData[i] = new CharacterData(delay * i,
                        duration, i, charCount);
                }

                isDirty = false;
            }
        }

        public void SetDirty()
        {
            isDirty = true;
        }

        public void SetProgress(float targetProgress)
        {
            SetDirty();
            UpdateComponents();
            progress = targetProgress;
            internalTime = progress * realTotalAnimationTime;
        }

        public void SetPlayForever(bool shouldPlayForever)
        {
            playForever = shouldPlayForever;
        }
    }
}
