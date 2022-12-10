using UnityEngine;

public class ReviewsSystem : MonoBehaviour {
    public static ReviewsSystem Singletone;

    public float CafeRating { get; set; }

    [SerializeField] private float minRating = -2f;
    [SerializeField] private float maxRating = 2f;

    private void Awake() {
        Singletone = this;
        CafeRating = 1f;
    }

    public void ChangeRating(float value) {
        var tryChangeRating = CafeRating + value;
        if (tryChangeRating <= maxRating && tryChangeRating >= minRating) CafeRating = tryChangeRating;
    }
}