using System.Collections.Generic;
using UnityEngine;

public class CableVerlet : MonoBehaviour
{
    [Header("Rope")]
    [SerializeField] private int _numOfRopeSegments = 50;
    [SerializeField] private float _ropeSegmentLength = 0.225f;

    [Header("Physics")]
    [SerializeField] private Vector2 _gravityForce = new(0f, -2f);
    [SerializeField] private float _dampingFactor = 0.90f;

    [Header("Constraints")]
    [SerializeField] private int _numOfConstraintRuns = 50;
    private LineRenderer _lineRenderer;
    private List<RopeSegment> _ropeSegments = new();
    private Vector3 _ropeStartPoint;

    public GameObject SocketCableHolder;
    public Socket socket;
    public GameObject hole;

    void Awake()
    {
        _lineRenderer = socket.GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _numOfRopeSegments;

        _ropeStartPoint = hole.transform.position;

        for (int i = 0; i < _numOfRopeSegments; i++)
        {
            _ropeSegments.Add(new RopeSegment(_ropeStartPoint));
            _ropeStartPoint.y -= _ropeSegmentLength;
        }

        Debug.Log($"SocketCableHolder.transform.position {SocketCableHolder.transform.position}");

    }

    private void Update()
    {
        DrawRope();
    }

    public void DrawRope()
    {
        Vector3[] ropePositions = new Vector3[_numOfRopeSegments];

        for (int i = 0; i < _ropeSegments.Count; i++)
        {
            ropePositions[i] = _ropeSegments[i].CurrentPosition;
        }

        _lineRenderer.SetPositions(ropePositions);
    }

    private void FixedUpdate()
    {
        Simulate();

        for (int i = 0; i < _numOfConstraintRuns; i++)
        {
            ApplyConstraints();
        }
    }

    private void Simulate()
    {
        for (int i = 0; i < _ropeSegments.Count; i++)
        {
            RopeSegment segment = _ropeSegments[i];
            Vector2 velocity = (segment.CurrentPosition - segment.OldPosition) * _dampingFactor;
            segment.OldPosition = segment.CurrentPosition;
            segment.CurrentPosition += velocity;
            segment.CurrentPosition += _gravityForce * Time.fixedDeltaTime;

            _ropeSegments[i] = segment;
        }
    }

    private void ApplyConstraints()
    {
        RopeSegment firstSegment = _ropeSegments[0];
        RopeSegment lastSegment = _ropeSegments[_ropeSegments.Count - 1];
        firstSegment.CurrentPosition = hole.transform.position;
        lastSegment.CurrentPosition = SocketCableHolder.transform.position;
        _ropeSegments[0] = firstSegment;
        _ropeSegments[_ropeSegments.Count - 1] = lastSegment;

        for (int i = 0; i < _numOfRopeSegments - 1; i++)
        {
            RopeSegment currentSeg = _ropeSegments[i];
            RopeSegment nextSeg = _ropeSegments[i + 1];
            float dist = (currentSeg.CurrentPosition - nextSeg.CurrentPosition).magnitude;
            float difference = dist - _ropeSegmentLength;

            Vector2 changeDir = (currentSeg.CurrentPosition - nextSeg.CurrentPosition).normalized;
            Vector2 changeVector = changeDir * difference;

            if (i != 0)
            {
                currentSeg.CurrentPosition = changeVector * 0.5f;
                nextSeg.CurrentPosition += changeVector * 0.5f;
            }
            else
            {
                nextSeg.CurrentPosition += changeVector;
            }

            _ropeSegments[i] = currentSeg;
            _ropeSegments[i + 1] = nextSeg;
        }
    }

    public struct RopeSegment
    {
        public Vector2 CurrentPosition;
        public Vector2 OldPosition;

        public RopeSegment(Vector2 pos)
        {
            CurrentPosition = pos;
            OldPosition = pos;
        }
    }


}
