#if VRC_SDK_VRCSDK3 && UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using AnimatorController = UnityEditor.Animations.AnimatorController;
using VRCAvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
using VRC.SDK3.Avatars.ScriptableObjects;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace I5UCC.VRCImmersiveImmobilize
{
    public class VRCASLGestures : EditorWindow
    {
        private VRCAvatarDescriptor targetAvatar = null;

        private GUIStyle titleStyle = null;
        private GUIStyle titleStyle2 = null;
        private GUIStyle errorStyle = null;
        private GUIStyle highlightedStyle = null;

        private AnimatorController animatorToAdd = null;
        private VRCExpressionParameters parametersToAdd = null;
        private VRCExpressionsMenu menuToAdd = null;

        private readonly string controllerPath = @"Packages\com.i5ucc.vrcimmersiveimmobilize\VRCII_FX.controller";

        private readonly string parameterPath = @"Packages\com.i5ucc.vrcimmersiveimmobilize\VRCII_Parameters.asset";

        private readonly string menuPath = @"Packages\com.i5ucc.vrcimmersiveimmobilize\VRCII_Menu.asset";

        private readonly int cost = 3;

        private readonly int layerindex = 4; //FX Controller

        [MenuItem("Tools/I5UCC/VRCImmersiveImmobilize")]
        public static void Open()
        {
            GetWindow<VRCASLGestures>("VRCImmersiveImmobilize");
        }

        private void OnGUI()
        {
            titleStyle = new GUIStyle()
            {
                fontSize = 14,
                fixedHeight = 28,
                fontStyle = FontStyle.Bold,
                normal = {
                    textColor = Color.white
                }
            };


            titleStyle2 = new GUIStyle()
            {
                fontSize = 23,
                fixedHeight = 28,
                fontStyle = FontStyle.Bold,
                normal = {
                    textColor = Color.white
                }
            };

            highlightedStyle = new GUIStyle()
            {
                fontStyle = FontStyle.Bold,
                normal = {
                    textColor = Color.white
                }
            };

            errorStyle = new GUIStyle()
            {
                fontStyle = FontStyle.Bold,
                normal = {
                    textColor = Color.red
                }
            };

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            DrawSimple();
        }

        private void DrawSimple()
        {
            EditorGUILayout.Space(12);
            EditorGUILayout.LabelField("VRCImmersiveImmobilize", titleStyle2);
            EditorGUILayout.Space(35);
            EditorGUILayout.LabelField("Avatar (Drag and Drop avatar here)", titleStyle);
            EditorGUILayout.Space();
            targetAvatar = EditorGUILayout.ObjectField(targetAvatar, typeof(VRCAvatarDescriptor), true, GUILayout.Height(30)) as VRCAvatarDescriptor;
            EditorGUILayout.Space();

            if (targetAvatar)
            {
                int TotalCost;
                if (targetAvatar.expressionParameters != null)
                    TotalCost = targetAvatar.expressionParameters.CalcTotalCost() + cost;
                else
                    TotalCost = cost;

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Memory needed: " + "3", highlightedStyle);
                EditorGUILayout.LabelField("Total Memory: " + TotalCost.ToString() + "/256", highlightedStyle);

                if (TotalCost <= 256 && (targetAvatar.expressionsMenu == null || targetAvatar.expressionsMenu.controls.Count != 8))
                {
                    animatorToAdd = AssetDatabase.LoadAssetAtPath(controllerPath, typeof(AnimatorController)) as AnimatorController;
                    parametersToAdd = AssetDatabase.LoadAssetAtPath(parameterPath, typeof(VRCExpressionParameters)) as VRCExpressionParameters;
                    menuToAdd = AssetDatabase.LoadAssetAtPath(menuPath, typeof(VRCExpressionsMenu)) as VRCExpressionsMenu;

                    Debug.LogWarning(animatorToAdd);

                    if (targetAvatar.baseAnimationLayers[layerindex].animatorController == animatorToAdd)
                        GUI.enabled = false;
                    else
                        GUI.enabled = true;

                    if (GUILayout.Button("install"))
                    {
                        targetAvatar.customizeAnimationLayers = true;
                        if (targetAvatar.baseAnimationLayers[layerindex].isDefault || targetAvatar.baseAnimationLayers[layerindex].animatorController == null || AnimatorAlreadySet())
                           targetAvatar.baseAnimationLayers[layerindex].animatorController = animatorToAdd;
                        else
                           MergeController(targetAvatar, animatorToAdd);
                        targetAvatar.baseAnimationLayers[layerindex].isDefault = false;

                        targetAvatar.customExpressions = true;
                        if (targetAvatar.expressionParameters == null)
                           targetAvatar.expressionParameters = parametersToAdd;
                        else
                           MergeParameters(targetAvatar, parametersToAdd);

                           
                        if (targetAvatar.expressionsMenu == null)
                           targetAvatar.expressionsMenu = menuToAdd;
                        else
                           MergeMenus(targetAvatar, menuToAdd);

                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                    
                }
                else if (targetAvatar.expressionsMenu.controls.Count == 8)
                {
                    GUI.enabled = false;
                    GUILayout.Button("Install");
                    GUI.enabled = true;

                    EditorGUILayout.LabelField("Not enough space in Menu!", errorStyle);
                }
                else
                {
                    GUI.enabled = false;
                    GUILayout.Button("Install");
                    GUI.enabled = true;

                    EditorGUILayout.LabelField("Not enough Parameter space!", errorStyle);
                }
            }
        }

        private void MergeParameters(VRCAvatarDescriptor descriptor, VRCExpressionParameters parametersToAdd)
        {
            VRCExpressionParameters parametersOriginal = (VRCExpressionParameters)descriptor.expressionParameters;
            List<VRCExpressionParameters.Parameter> addparams = new List<VRCExpressionParameters.Parameter>();
            
            for (int i = 0; i < parametersToAdd.parameters.Length; i++)
            {
                VRCExpressionParameters.Parameter p = parametersToAdd.parameters[i];
                if (!ParametersContainsParameter(parametersOriginal, p.name))
                {
                    VRCExpressionParameters.Parameter temp = new VRCExpressionParameters.Parameter
                    {
                        name = p.name,
                        valueType = p.valueType,
                        defaultValue = p.defaultValue,
                        saved = p.saved
                    };
                    addparams.Add(temp);
                }
                else
                    Debug.LogWarning(p.name + " parameter does already exist!");
            }

            EditorUtility.SetDirty(descriptor.expressionParameters);
            descriptor.expressionParameters.parameters = parametersOriginal.parameters.Concat(addparams.ToArray()).ToArray();
        }

        private void MergeMenus(VRCAvatarDescriptor descriptor, VRCExpressionsMenu menuToAdd)
        {
            VRCExpressionsMenu menuOriginal = (VRCExpressionsMenu)descriptor.expressionsMenu;
            foreach (VRCExpressionsMenu.Control c in menuToAdd.controls)
            {
                if (!MenuContainsControl(menuOriginal, c.name))
                {
                    VRCExpressionsMenu.Control temp = new VRCExpressionsMenu.Control
                    {
                        name = c.name,
                        icon = c.icon,
                        labels = c.labels,
                        parameter = c.parameter,
                        style = c.style,
                        subMenu = c.subMenu,
                        subParameters = c.subParameters,
                        type = c.type,
                        value = c.value
                    };

                    menuOriginal.controls.Add(temp);
                }
            }
            EditorUtility.SetDirty(descriptor.expressionsMenu);
        }

        private void MergeController(VRCAvatarDescriptor descriptor, AnimatorController controllerToAdd)
        {
            AnimatorController controllerOriginal = (AnimatorController)descriptor.baseAnimationLayers[layerindex].animatorController;

            for (int i = 1; i < controllerOriginal.layers.Length; i++)
                if (controllerOriginal.layers[i].name.Equals("ImmersiveImmobilize"))
                {
                    controllerOriginal.RemoveLayer(i);
                    i--;
                }   

            AnimatorCloner.MergeControllers(controllerOriginal, controllerToAdd);
        }
        private bool ParametersContainsParameter(VRCExpressionParameters c, string name)
        {
            foreach (VRCExpressionParameters.Parameter p in c.parameters)
                if (p.name == name)
                    return true;

            return false;
        }

        private bool MenuContainsControl(VRCExpressionsMenu c, string name)
        {
            foreach (VRCExpressionsMenu.Control p in c.controls)
                if (p.name == name)
                    return true;

            return false;
        }

        private bool AnimatorAlreadySet()
        {
            for (int i = 0; i < controllerPath.Length; i++)
                if (targetAvatar.baseAnimationLayers[layerindex].animatorController.name + ".controller" == controllerPath.Substring(controllerPath.LastIndexOf("/") + 1))
                    return true;
            return false;
        }
    }
}

#endif