using UnityEngine;
using System.Collections;
using TMPro;
using UnityEditor.SearchService;

public class GameLoopHandler : MonoBehaviour
{
    // Player Ship handle
    [SerializeField] private PlayerStats pss;
    [SerializeField] private GameObject pickup;

    TMP_Text calloutText;
    int levelTimer;

    // Use this for initialization
    void Start()
    {
        Debug.Log("GameLoopHandler activated.");
        levelTimer = 0;
        pss.shipSpeed = 1;
        pss.health = 100;
        pss.armor = 2;
        pss.currTimeLvl = 1;
        pss.maxTimeLvl = 10;
        pss.maxTime = (25 + (pss.currTimeLvl * 5));
        pss.wpnSpeed = 1;
        pss.projSpeed = 1;

        calloutText = MakeCallout("<mspace=2.75em>LEVEL ONE\nGETTING STARTED...");
        
        pss.mslSpeed = 1;
    }

    private TMP_Text MakeCallout(string _str)
    {
        Debug.LogWarning("Entering MakeCallout Function");
        TMP_Text tmpTxt = gameObject.AddComponent<TextMeshPro>();
        // Load the Font Asset to be used.
        // TMP_FontAsset m_FontAsset = Resources.Load("Fonts & Materials/LiberationSans SDF", typeof(TMP_FontAsset)) as TMP_FontAsset;
        // tmpTxt.font = m_FontAsset;

        // Assign Material to TextMesh Pro Component
        //m_textMeshPro.fontSharedMaterial = Resources.Load("Fonts & Materials/LiberationSans SDF - Bevel", typeof(Material)) as Material;
        //m_textMeshPro.fontSharedMaterial.EnableKeyword("BEVEL_ON");

        // Set various font settings.
        tmpTxt.fontSize = 48;

        tmpTxt.alignment = TextAlignmentOptions.Center;

        //m_textMeshPro.anchorDampening = true; // Has been deprecated but under consideration for re-implementation.
        //m_textMeshPro.enableAutoSizing = true;

        //m_textMeshPro.characterSpacing = 0.2f;
        //m_textMeshPro.wordSpacing = 0.1f;

        //m_textMeshPro.enableCulling = true;
        tmpTxt.enableWordWrapping = false;
        tmpTxt.SetText(_str);
        tmpTxt.autoSizeTextContainer = true;

        return tmpTxt;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        levelTimer++;
        if (levelTimer == 1) {
            calloutText.SetText("");
        }
        if (levelTimer == 50)
        {
            calloutText.SetText("<mspace=1.05em>Level One\nGetting Started...");
        }
        if (levelTimer == 300)
        {
            Destroy(calloutText);
        }
        if (levelTimer == 500) {
            calloutText = MakeCallout("HURRY UP...");
            StartCoroutine(Movement(2f, "HURRY UP..."));
        }

    }

    IEnumerator Movement(float delay, string callout)
    {
        int localTimer = 0;
        int finalTimer = (int)(delay * 60);
        while (localTimer < (finalTimer - 32))
        {
            localTimer++;
            string alphaValue = "<alpha=#FF>";
            string txtOut = alphaValue + callout;
            calloutText.SetText(txtOut);
            yield return null;
        }

        while (localTimer < finalTimer)
        {
            localTimer++;
            int fadeTimer = (finalTimer - localTimer) * 4;
            string alphaValue = "<alpha=#"+fadeTimer.ToString("X2")+">";
            string txtOut = alphaValue+callout;
            calloutText.SetText(txtOut);
            yield return null;
        }

        Destroy(calloutText);
        yield return new WaitForSeconds(1f);
    } 

    public void GeneratePickup() {
        float v = Random.Range(-40f, 40f);
        float newXPos = v;
        Vector3 respawnVector = new Vector3(newXPos, 0f, 150f);
        Instantiate(pickup, respawnVector, Quaternion.identity);
    }

    public void SpawnItem(GameObject obj) {
        Instantiate(obj);
    }

    public void DestroyPickup(GameObject pickup)
    {
        Destroy(pickup);
    }
}
