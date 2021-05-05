﻿using UnityEngine;
using ViveSR.anipal.Eye;
using VRCFaceTracking.QuickMenu.EyeTracking;
using Object = UnityEngine.Object;

namespace VRCFaceTracking.QuickMenu
{
    public class MainMenu
    {
        private readonly EyeTrackingMenu _eyeTrackingMenu;
        //private readonly LipTrackingMenu _lipTrackingMenu;

        public MainMenu(Transform parentMenuTransform, AssetBundle bundle)
        {
            var menuPrefab = bundle.LoadAsset<GameObject>("VRCSRanipal");
            var menuObject = Object.Instantiate(menuPrefab);
            menuObject.transform.parent = parentMenuTransform;
            menuObject.transform.localPosition = Vector3.zero;
            menuObject.transform.localScale = Vector3.oneVector;
            menuObject.transform.localRotation = new Quaternion(0, 0, 0, 1);

            _eyeTrackingMenu = new EyeTrackingMenu(menuObject.transform.Find("Pages/Eye Tracking"), menuObject.transform.Find("Tabs/Buttons/Eye Tracking"));
            //_lipTrackingMenu = new LipTrackingMenu(menuObject.transform.Find("Pages/Lip Tracking"), menuObject.transform.Find("Tabs/Buttons/Lip Tracking"));
            
            foreach (var sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                switch (sprite.name)
                {
                    case "UI_ButtonToggleBottom_Bifrost":
                        ToggleButton.ToggleDown = sprite;
                        break;
                    case "UI_ButtonToggleTop_Bifrost":
                        ToggleButton.ToggleUp = sprite;
                        break;
                }
            
            UpdateEnabledTabs(SRanipalTrack.EyeEnabled, SRanipalTrack.LipEnabled);
        }

        public void UpdateEnabledTabs(bool eye = false, bool lip = false)
        {
            _eyeTrackingMenu.TabObject.SetActive(eye);
            //_lipTrackingMenu.TabObject.SetActive(lip);
            
            if (eye)
                _eyeTrackingMenu.Root.SetActive(true);
            //else if (lip)
                //_lipTrackingMenu.Root.SetActive(true);
        }

        public void UpdateParams(EyeData_v2? eyeData)
        {
            if (_eyeTrackingMenu.Root.active && eyeData.HasValue) _eyeTrackingMenu.UpdateEyeTrack(eyeData.Value);
            //if (_lipTrackingMenu.Root.active && lipImage != null) _lipTrackingMenu.UpdateImage(lipImage);
        }
    }
}