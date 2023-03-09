
namespace Libs.extend
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class EnumLabelAttribute : HeaderAttribute
    {
        public EnumLabelAttribute(string header) : base(header) { }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(EnumLabelAttribute))]
    public class EnumLabelDrawer : PropertyDrawer
    {
        private readonly List<string> m_displayNames = new List<string>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            m_displayNames.Clear();
            var att = (EnumLabelAttribute)attribute;
            var type = property.serializedObject.targetObject.GetType();
            var enumtype = type.GetField(property.name).FieldType;
            foreach (var enumName in property.enumNames)
            {
                var hds = enumtype.GetField(enumName).GetCustomAttributes(typeof(HeaderAttribute), false);
                m_displayNames.Add(hds.Length <= 0 ? enumName : ((HeaderAttribute)hds[0]).header);
            }
            EditorGUI.BeginChangeCheck();
            var value = EditorGUI.Popup(position, att.header, property.enumValueIndex, m_displayNames.ToArray());
            if (EditorGUI.EndChangeCheck()) property.enumValueIndex = value;
        }
    }
#endif
}