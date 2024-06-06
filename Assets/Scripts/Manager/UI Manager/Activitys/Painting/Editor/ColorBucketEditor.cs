using UnityEditor;
using UnityEditor.EventSystems;

[CustomEditor(typeof(ColorBucket))]
public class DragBrushEditor : EventTriggerEditor 
{
	//This method is called every time Unity needs to show or update the 
	//inspector when you select a gameobject with an XXX component.
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		//This will show the default inspector for the XXX component.
		base.OnInspectorGUI();
	}
}