using UnityEngine.UIElements;
using UnityEditor;
using System.Collections.Generic;

[UxmlElement]
public partial class SegmentedProgressBar : VisualElement
{
    private const string UxmlPath = "Assets/UI Toolkit/Menu/Components/SegmentedProgressBar/SegmentedProgressBar.uxml";
    private VisualTreeAsset m_VisualTreeAsset;
    private VisualElement m_ProgressBarRoot;
    private List<VisualElement> m_ProgressSegments = new List<VisualElement>();

    public SegmentedProgressBar()
    {
        m_VisualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
        m_VisualTreeAsset.CloneTree(this);
        m_ProgressBarRoot = this.Q<VisualElement>("ProgressbarRootContainer");
    }

    public void SetMaxLevel(int maxLevel)
    {
        // 1. Clear any existing segments
        m_ProgressBarRoot.Clear();
        m_ProgressSegments.Clear();

        // 2. Generate new segments based on maxLevel
        for (int i = 0; i < maxLevel; i++)
        {
            // Create a new VisualElement (which will be a segment)
            VisualElement segment = new VisualElement();

            // Add the USS classes for styling
            segment.AddToClassList("segment");

            // Add to the container and the reference list
            m_ProgressBarRoot.Add(segment);
            m_ProgressSegments.Add(segment);
        }
    }


    /// <summary>
    /// Updates the visual state of the segmented progress bar.
    /// </summary>
    public void UpdateProgress(int currentLevel)
    {
        // Safety check to ensure segments have been created
        if (m_ProgressSegments.Count == 0)
            return;

        for (int i = 0; i < m_ProgressSegments.Count; i++)
        {
            if (i < currentLevel)
            {
                // Add the style class for the filled state
                m_ProgressSegments[i].AddToClassList("segment-fill");
            }
            else
            {
                // Remove the style class for the empty state
                m_ProgressSegments[i].RemoveFromClassList("segment-fill");
            }
        }
    }
}