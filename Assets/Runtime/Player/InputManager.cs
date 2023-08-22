// made by Oliver Beebe 2023
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace OTStudios.DDJ.Runtime {

    public class InputManager : PlayerComponent {

        [Header("Movement")]
        public Axis Movement;

        [Header("Game")]
        public Button Pause;

        [Header("Debug")]
        public Button Debug1;
        public Button Debug2;
        public Button Debug3;
        public Button Debug4;

        private List<InputClass> inputs;

        private void Awake() => inputs = new() {

            Movement,

            Pause,

            Debug1,
            Debug2,
            Debug3,
            Debug4,
        };

        public void EnableInputs(bool enable) => inputs.ForEach(input => input.Disabled = !enable);

        private void Update() => inputs.ForEach(input => input.Update());

        public interface InputClass {
            public bool Disabled { get; set; }
            public void Update();
        }

        private const float LabelFieldPercent = 2f / 3f;

        [System.Serializable] public class Axis : InputClass {

            public bool Disabled { get; set; } = false;
            public Vector2 Vector => Disabled ? Vector2.zero : _vector;
            public bool Pressed  => !Disabled && _pressed;
            public bool Down     => !Disabled && _down;
            public bool Released => !Disabled && _released;

            public Axis(string horizontalAxis, string verticalAxis, bool raw, bool normalize) {
                this.horizontalAxis = horizontalAxis;
                this.verticalAxis = verticalAxis;
                this.raw = raw;
                this.normalize = normalize;
            }

            #region Internals

            [SerializeField] private string horizontalAxis, verticalAxis;
            [SerializeField] private bool raw, normalize;

            private Vector2 _vector;
            private bool pressedLast = false;
            private bool _pressed    = false;
            private bool _down       = false;
            private bool _released   = false;

            public void Update() {

                _vector = raw
                    ? new(UnityEngine.Input.GetAxisRaw(horizontalAxis), UnityEngine.Input.GetAxisRaw(verticalAxis))
                    : new(UnityEngine.Input.GetAxis(horizontalAxis),    UnityEngine.Input.GetAxis(verticalAxis));

                if (normalize) _vector = _vector.normalized;

                _pressed    = _vector != Vector2.zero;
                _down       = _pressed && !pressedLast;
                _released   = !_pressed && pressedLast;
                pressedLast = _pressed;
            }

            #endregion

            #region Editor
            #if UNITY_EDITOR

            [CustomPropertyDrawer(typeof(Axis))]
            private class AxisPropertyDrawer : PropertyDrawer {

                public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

                    EditorGUI.BeginProperty(position, label, property);

                    float xyLabelWidth = 15,
                            preLabelDist = 5,
                            rawLabelWidth = 33,
                            rawWidth = 48,
                            normalizeLabelWidth = 65,
                            normalizeWidth = 80,
                            stringWidth = (position.width - EditorGUIUtility.labelWidth * LabelFieldPercent - EditorGUIUtility.standardVerticalSpacing - preLabelDist * 3 - rawWidth - normalizeWidth) / 2f;
                    position.width = EditorGUIUtility.labelWidth * LabelFieldPercent;

                    EditorGUI.LabelField(position, label);

                    position.x += position.width + EditorGUIUtility.standardVerticalSpacing;
                    position.width = stringWidth;
                    EditorGUI.PrefixLabel(position, new GUIContent("X", "Horizontal axis"));

                    position.x += xyLabelWidth;
                    position.width -= xyLabelWidth;
                    EditorGUI.PropertyField(position, property.FindPropertyRelative("horizontalAxis"), GUIContent.none);

                    position.x += position.width + preLabelDist;
                    position.width += xyLabelWidth;
                    EditorGUI.PrefixLabel(position, new GUIContent("Y", "Vertical axis"));

                    position.x += xyLabelWidth;
                    position.width -= xyLabelWidth;
                    EditorGUI.PropertyField(position, property.FindPropertyRelative("verticalAxis"), GUIContent.none);

                    position.x += position.width + preLabelDist;
                    position.width = rawWidth;
                    EditorGUI.PrefixLabel(position, new GUIContent("Raw", "Should the axis use GetAxisRaw?"));

                    position.x += rawLabelWidth;
                    position.width -= rawLabelWidth;
                    EditorGUI.PropertyField(position, property.FindPropertyRelative("raw"), GUIContent.none);

                    position.x += position.width + preLabelDist;
                    position.width = normalizeWidth;
                    EditorGUI.PrefixLabel(position, new GUIContent("Normalize", "Should the axis be normalized?"));

                    position.x += normalizeLabelWidth;
                    position.width -= normalizeLabelWidth;
                    EditorGUI.PropertyField(position, property.FindPropertyRelative("normalize"), GUIContent.none);

                    EditorGUI.EndProperty();
                }
            }

            #endif
            #endregion
        }

        [System.Serializable] public class Button : InputClass {

            public bool Disabled { get; set; } = false;
            public bool Pressed  => !Disabled && _pressed;
            public bool Down     => !Disabled && _down;
            public bool Released => !Disabled && _released;

            public Button(params KeyCode[] keycodes) {
                this.keycodes = keycodes;
                type = Type.key;
            }

            public Button(int mouseButton) {
                this.mouseButton = mouseButton;
                type = Type.mouse;
            }

            #region Internals

            [SerializeField] private Type type;
            [SerializeField] private KeyCode[] keycodes;
            [SerializeField] private int mouseButton;

            private enum Type { key, mouse }
            private bool _pressed  = false;
            private bool _down     = false;
            private bool _released = false;

            public void Update() {

                switch (type) {

                    case Type.key:
                        _pressed = _down = _released = false;
                        foreach (var keyCode in keycodes) {
                            _pressed  |= UnityEngine.Input.GetKey(keyCode);
                            _down     |= UnityEngine.Input.GetKeyDown(keyCode);
                            _released |= UnityEngine.Input.GetKeyUp(keyCode);
                        }
                        break;

                    case Type.mouse:
                        _pressed  = UnityEngine.Input.GetMouseButton(mouseButton);
                        _down     = UnityEngine.Input.GetMouseButtonDown(mouseButton);
                        _released = UnityEngine.Input.GetMouseButtonUp(mouseButton);
                        break;
                }
            }

            #endregion

            #region Editor
            #if UNITY_EDITOR

            [CustomPropertyDrawer(typeof(Button))]
            private class ButtonPropertyDrawer : PropertyDrawer {

                private (string relativePath, string name) GetInputProperty(SerializedProperty property) => (Type)property.FindPropertyRelative("type").enumValueIndex switch {
                    Type.key    => ("keycodes",     "KeyCodes"),
                    _           => ("mouseButton",  "Mouse Button")
                };

                private List<KeyCode> keys;

                public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

                    EditorGUI.BeginProperty(position, label, property);

                    float propertySpacing = 15,
                            propertyWidth = position.width - EditorGUIUtility.labelWidth * LabelFieldPercent - propertySpacing,
                            typeWidthPercent = 0.2f,
                            codeWidthPercent = 0.8f;

                    position.height = EditorGUIUtility.singleLineHeight;
                    position.width = EditorGUIUtility.labelWidth * LabelFieldPercent;
                    EditorGUI.LabelField(position, label);

                    position.x += position.width;
                    position.width = propertyWidth * typeWidthPercent;
                    EditorGUI.PropertyField(position, property.FindPropertyRelative("type"), GUIContent.none);

                    (string relative, string name) = GetInputProperty(property);
                    var codesProperty = property.FindPropertyRelative(relative);

                    position.x += position.width + propertySpacing;
                    position.width = propertyWidth * codeWidthPercent;
                    position.height = EditorGUI.GetPropertyHeight(codesProperty);
                    EditorGUI.PropertyField(position, codesProperty, new GUIContent(name));

                    EditorGUI.EndProperty();
                }

                public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
                    => EditorGUI.GetPropertyHeight(property.FindPropertyRelative(GetInputProperty(property).relativePath));
            }

            #endif
            #endregion
        }
    }
}