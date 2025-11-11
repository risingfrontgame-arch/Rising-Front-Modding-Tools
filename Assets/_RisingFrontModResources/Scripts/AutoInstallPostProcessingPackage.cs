//using UnityEngine;
//using UnityEditor;
//using UnityEditor.PackageManager;
//using UnityEditor.PackageManager.Requests;

//[InitializeOnLoad]
//public class AutoInstallPostProcessingPackage
//{
//    static AddRequest addRequest;
//    static ListRequest listRequest;

//    static AutoInstallPostProcessingPackage()
//    {
//        // Start the installation process when Unity loads
//      InstallPackage();
//    }

//    public static void InstallPackage()
//    {
//        // Check if the package is already installed
//        listRequest = Client.List();  // List packages installed in the project
//        EditorApplication.update += ProgressCheck;
//    }

//    private static void ProgressCheck()
//    {
//        if (listRequest.IsCompleted)
//        {
//            if (listRequest.Status == StatusCode.Success)
//            {
//                bool isInstalled = false;
//                foreach (var package in listRequest.Result)
//                {
//                    if (package.name == "com.unity.postprocessing")
//                    {
//                        Debug.Log("Post Processing package is already installed.");
//                        isInstalled = true;
//                        break;
//                    }
//                }

//                if (!isInstalled)
//                {
//                    Debug.Log("Post Processing package not found. Installing now...");
//                    // If the package is not found, install it
//                    addRequest = Client.Add("com.unity.postprocessing");
//                    EditorApplication.update += InstallCheck;
//                }
//            }
//            else if (listRequest.Status >= StatusCode.Failure)
//            {
//                Debug.LogError("Failed to list packages: " + listRequest.Error.message);
//            }

//            EditorApplication.update -= ProgressCheck;
//        }
//    }

//    private static void InstallCheck()
//    {
//        if (addRequest.IsCompleted)
//        {
//            if (addRequest.Status == StatusCode.Success)
//            {
//                Debug.Log("Post Processing package installed successfully.");
//            }
//            else if (addRequest.Status >= StatusCode.Failure)
//            {
//                Debug.LogError("Failed to add Post Processing package: " + addRequest.Error.message);
//            }

//            EditorApplication.update -= InstallCheck;
//        }
//    }
//}