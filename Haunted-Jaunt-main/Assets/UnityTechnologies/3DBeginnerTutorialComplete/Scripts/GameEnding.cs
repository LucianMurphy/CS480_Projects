using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;
    //Particle system for the caught effect
    public ParticleSystem caughtEffect;
    public Text objectiveDistanceText;
    public Font objectiveDistanceFont;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    void Start ()
    {
        EnsureObjectiveDistanceTextExists ();
        UpdateObjectiveDistanceText ();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer ()
    {
        m_IsPlayerCaught = true;
        //Trigger for the particles
        caughtEffect.Play();
    }

    void Update ()
    {
        if (m_IsPlayerAtExit)
        {
            SetObjectiveDistanceTextVisible (false);
            EndLevel (exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            SetObjectiveDistanceTextVisible (false);
            EndLevel (caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
        else
        {
            UpdateObjectiveDistanceText ();
        }
    }

    void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
            
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene (0);
            }
            else
            {
                Application.Quit ();
            }
        }
    }

    void UpdateObjectiveDistanceText ()
    {
        if (player == null)
        {
            return;
        }

        EnsureObjectiveDistanceTextExists ();

        Vector3 toExit = transform.position - player.transform.position;
        toExit.y = 0f;

        // Dot product gives the squared length of the vector, which we use for planar distance.
        float squaredDistanceToExit = Vector3.Dot (toExit, toExit);
        float distanceToExit = Mathf.Sqrt (squaredDistanceToExit);
        objectiveDistanceText.text = $"Exit: {distanceToExit:0.0} m";
        SetObjectiveDistanceTextVisible (true);
    }

    void EnsureObjectiveDistanceTextExists ()
    {
        if (objectiveDistanceText != null)
        {
            return;
        }

        Canvas targetCanvas = FindObjectOfType<Canvas> ();

        if (targetCanvas == null)
        {
            GameObject canvasObject = new GameObject ("ObjectiveDistanceCanvas");
            targetCanvas = canvasObject.AddComponent<Canvas> ();
            targetCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler> ();
            canvasObject.AddComponent<GraphicRaycaster> ();
        }

        GameObject textObject = new GameObject ("ObjectiveDistanceText");
        textObject.transform.SetParent (targetCanvas.transform, false);

        RectTransform rectTransform = textObject.AddComponent<RectTransform> ();
        rectTransform.anchorMin = new Vector2 (0f, 1f);
        rectTransform.anchorMax = new Vector2 (0f, 1f);
        rectTransform.pivot = new Vector2 (0f, 1f);
        rectTransform.anchoredPosition = new Vector2 (24f, -24f);
        rectTransform.sizeDelta = new Vector2 (360f, 60f);

        objectiveDistanceText = textObject.AddComponent<Text> ();
        objectiveDistanceText.font = objectiveDistanceFont != null
            ? objectiveDistanceFont
            : Resources.GetBuiltinResource<Font> ("LegacyRuntime.ttf");
        objectiveDistanceText.fontSize = 24;
        objectiveDistanceText.alignment = TextAnchor.UpperLeft;
        objectiveDistanceText.horizontalOverflow = HorizontalWrapMode.Overflow;
        objectiveDistanceText.verticalOverflow = VerticalWrapMode.Overflow;
        objectiveDistanceText.color = Color.white;
        objectiveDistanceText.raycastTarget = false;

        Outline outline = textObject.AddComponent<Outline> ();
        outline.effectColor = new Color (0f, 0f, 0f, 0.85f);
        outline.effectDistance = new Vector2 (1f, -1f);
    }

    void SetObjectiveDistanceTextVisible (bool isVisible)
    {
        if (objectiveDistanceText != null)
        {
            objectiveDistanceText.enabled = isVisible;
        }
    }
}
