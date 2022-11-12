using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    public static DialogueObject instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    public string[] Dialogue => dialogue;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;
}
