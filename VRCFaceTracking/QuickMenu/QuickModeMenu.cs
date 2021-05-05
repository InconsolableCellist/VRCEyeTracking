﻿using System;
using System.Linq;
using System.Reflection;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace VRCFaceTracking.QuickMenu
{
    public static class QuickModeMenu
    {
        private static bool _hasInitMenu;
        public static MainMenu MainMenu;

        public static void CheckIfShouldInit()
        {
            if (!_hasInitMenu)
                InitializeMenu();
        }

        private static void InitializeMenu()
        {
            CreateNotificationTab("VRCSRanipal", "Text", Color.green); // Organization 100
            _hasInitMenu = true;
        }

        private static void CreateNotificationTab(string name, string text, Color color)
        {
            var bundle = AssetBundle.LoadFromMemory(ExtractAb());
            
            var existingTabs = Resources.FindObjectsOfTypeAll<MonoBehaviourPublicObCoGaCoObCoObCoUnique>()[0].field_Public_ArrayOf_GameObject_0.ToList();

            var quickMenu = Resources.FindObjectsOfTypeAll<global::QuickMenu>()[0];

            // Tab

            var quickModeTabs = quickMenu.transform.Find("QuickModeTabs").GetComponent<MonoBehaviourPublicObCoGaCoObCoObCoUnique>();
            var newTab = Object.Instantiate(quickModeTabs.transform.Find("NotificationsTab"), quickModeTabs.transform);
            newTab.name = name;
            Object.DestroyImmediate(newTab.GetComponent<MonoBehaviourPublicGaTeSiSiUnique>());
            SetTabIndex(newTab, (MonoBehaviourPublicObCoGaCoObCoObCoUnique.EnumNPublicSealedvaHoNoPl4vUnique)existingTabs.Count);
            newTab.Find("Badge").GetComponent<RawImage>().color = color;
            newTab.Find("Badge/NotificationsText").GetComponent<Text>().text = text;

            existingTabs.Add(newTab.gameObject);

            Resources.FindObjectsOfTypeAll<MonoBehaviourPublicObCoGaCoObCoObCoUnique>()[0].field_Public_ArrayOf_GameObject_0 = existingTabs.ToArray();

            newTab.Find("Icon").GetComponent<Image>().sprite = LoadQmSprite(bundle);

            // Menu

            var quickModeMenus = quickMenu.transform.Find("QuickModeMenus");
            var newMenu = new GameObject(name + "Menu", new Il2CppSystem.Type[] { Il2CppType.Of<RectTransform>() }).GetComponent<RectTransform>();
            newMenu.SetParent(quickModeMenus, false);
            newMenu.anchorMin = new Vector2(0, 1);
            newMenu.anchorMax = new Vector2(0, 1);
            newMenu.sizeDelta = new Vector2(1680f, 1200f);
            newMenu.pivot = new Vector2(0.5f, 0.5f);
            newMenu.anchoredPosition = new Vector2(0, 200f);
            newMenu.gameObject.SetActive(false);

            MainMenu = new MainMenu(newMenu, bundle);

            // Tab interaction
            var tabButton = newTab.GetComponent<Button>();
            tabButton.onClick.RemoveAllListeners();
            tabButton.onClick.AddListener((Action)(() =>
            {
                global::QuickMenu.prop_QuickMenu_0.field_Private_GameObject_6.SetActive(false);
                global::QuickMenu.prop_QuickMenu_0.field_Private_GameObject_6 = newMenu.gameObject;
                newMenu.gameObject.SetActive(true);
            }));
            
            newTab.transform.FindChild("Badge").gameObject.SetActive(false);

            // Allow invite menu to instantiate
            quickModeMenus.Find("QuickModeNotificationsMenu").gameObject.SetActive(true);
            quickModeMenus.Find("QuickModeNotificationsMenu").gameObject.SetActive(false);
        }

        private static void SetTabIndex(Transform tab, MonoBehaviourPublicObCoGaCoObCoObCoUnique.EnumNPublicSealedvaHoNoPl4vUnique value)
        {
            var tabDescriptor = tab.GetComponents<MonoBehaviour>().First(c => c.GetIl2CppType().GetMethod("ShowTabContent") != null);

            tabDescriptor.GetIl2CppType().GetFields().First(f => f.FieldType.IsEnum).SetValue(tabDescriptor, new Il2CppSystem.Int32 { m_value = (int)value }.BoxIl2CppObject());
        }


        private static Sprite LoadQmSprite(AssetBundle bundle)
        {
            var t = bundle.LoadAsset<Texture2D>("sranipal");
            var rect = new Rect(0.0f, 0.0f, t.width, t.height);
            var pivot = new Vector2(0.5f, 0.5f);
            var border = Vector4.zero;

            return Sprite.CreateSprite_Injected(t, ref rect, ref pivot, 100.0f, 0, SpriteMeshType.Tight, ref border, false);
        }
        
        private static byte[] ExtractAb()
        {
            var a = Assembly.GetExecutingAssembly();
            using (var resFilestream = a.GetManifestResourceStream("VRCFaceTracking.VRCFaceTracking"))
            {
                if (resFilestream == null) return null;
                var ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);
                return ba;
            }
        }
    }
}
