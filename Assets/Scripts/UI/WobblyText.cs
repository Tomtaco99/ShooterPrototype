using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WobblyText : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI textComponent;
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.ForceMeshUpdate();
        TMP_TextInfo textinfo = textComponent.textInfo;
        for (int i = 0; i < textinfo.characterCount; i++)
        {
            TMP_CharacterInfo charinfo = textinfo.characterInfo[i];

            Vector3[] verts = textinfo.meshInfo[charinfo.materialReferenceIndex].vertices;
            for (int j = 0; j < 4; j++)
            {
                var orig = verts[charinfo.vertexIndex + j];
                verts[charinfo.vertexIndex + j] =
                    orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * 10f, 0);
            }
        }

        for (int i = 0; i < textinfo.meshInfo.Length; i++)
        {
            var meshInfo = textinfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
