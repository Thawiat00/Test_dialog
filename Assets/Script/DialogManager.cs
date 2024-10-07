using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogContent
{
    public string speaker;
    public string text;
}

[System.Serializable]
public class Chapter
{
    public int chapter;
    public string title;
    public List<DialogContent> content;
}

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    private List<Chapter> chapters = new List<Chapter>();
    private int currentChapterIndex = 0;
    private int currentDialogIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadChapters();
        StartDialogue();
    }

    // ????????????????? JSON
    private void LoadChapters()
    {
        TextAsset chapter1 = Resources.Load<TextAsset>("chapter1");
        TextAsset chapter2 = Resources.Load<TextAsset>("chapter2");
        TextAsset chapter3 = Resources.Load<TextAsset>("chapter3");

        chapters.Add(JsonUtility.FromJson<Chapter>(chapter1.text));
        chapters.Add(JsonUtility.FromJson<Chapter>(chapter2.text));
        chapters.Add(JsonUtility.FromJson<Chapter>(chapter3.text));
    }

    // ????????????
    private void StartDialogue()
    {
        if (currentChapterIndex < chapters.Count)
        {
            Chapter currentChapter = chapters[currentChapterIndex];

            // ??????????
            Debug.Log("Title: " + currentChapter.title);

            // ?????????????
            currentDialogIndex = 0;
            ShowNextDialog();
        }
        else
        {
            Debug.Log("END Conversation");
        }
    }

    // ????????????????
    public void ShowNextDialog()
    {
        if (currentChapterIndex < chapters.Count)
        {
            Chapter currentChapter = chapters[currentChapterIndex];

            if (currentDialogIndex < currentChapter.content.Count)
            {
                DialogContent dialog = currentChapter.content[currentDialogIndex];
                Debug.Log(dialog.speaker + ": " + dialog.text);
                currentDialogIndex++;
            }
            else
            {
                // ??????????????????????????
                currentChapterIndex++;
                StartDialogue(); // ????????????
            }
        }
    }

    private void Update()
    {
        // ??????????????????????????????? Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextDialog();
        }
    }
}
