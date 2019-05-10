using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

public class WebEngagePostProcessBuild
{
    [PostProcessBuild]
    public static void EditXcodePlist(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            string WEBENGAGE_LICENSE_CODE = "YOUR-WEBENGAGE-LICENSE-CODE";
            string logLevel = "VERBOSE";
            bool apnsAutoRegister = true;
            bool trackLocation = false;

            // Update plist
            string plistPath = pathToBuiltProject + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
            PlistElementDict rootDict = plist.root;

            rootDict.SetString("WEGLicenseCode", WEBENGAGE_LICENSE_CODE);
            rootDict.SetString("WEGLogLevel", logLevel);
            rootDict.SetBoolean("WEGApnsAutoRegister", apnsAutoRegister);

            if (trackLocation)
            {
                PlistElementArray bgModes = rootDict.CreateArray("UIBackgroundModes");
                bgModes.AddString("location");

                // TODO: Background location useage key (new in iOS 8)
                //rootDict.SetString("NSLocationAlwaysUsageDescription", "Uses background location");
            }

            // Write to Info.plist file
            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }
}

