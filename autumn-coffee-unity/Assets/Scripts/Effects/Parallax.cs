using UnityEngine;
using Ruinum.Core;

public class Parallax : Executable
{
    [Header("Parameters")]
    [SerializeField] private float offset;
    [SerializeField] private float offsetY = -1;
    [SerializeField] private float offsetX = -1;
    [SerializeField] private float speed;

    private Vector3 _startPos;

    public override void Start()
    {
        base.Start();
        _startPos = transform.position;
    }

    public override void Execute()
    {
        Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float posX = Mathf.Lerp(transform.position.x, _startPos.x + (mousePos.x * offset * offsetX), speed * Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, _startPos.y + (mousePos.y * offset * offsetY), speed * Time.deltaTime);

        transform.position = new Vector3(posX, posY, _startPos.z);
    }
}
