using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Jump = nameof(Jump);
    private const string Fire1 = nameof(Fire1);

    public bool IsJump { get; private set; } = false;
    public bool IsFire { get; private set; } = false;

    private void Update()
    {
        if (Input.GetButtonDown(Jump))
            IsJump = true;
        else
            IsJump = false;

        if (Input.GetButtonUp(Fire1))
            IsFire = true;
        else
            IsFire = false;
    }
}
