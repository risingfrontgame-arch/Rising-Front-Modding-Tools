using UnityEngine;
using System.Collections.Generic;
using Steamworks;
using UnityEditor;
using System.IO;
using System;
/// <summary>
/// This class was created by Vitor Pêgas on 01/01/2017
/// From http://answers.unity3d.com/questions/1282772/steamworksnet-and-steam-workshop.html
/// Added here so it doesn't disappear 
/// Requires https://steamworks.github.io/
/// </summary>
public class SteamWorkshop
{
    public static SteamWorkshop singleton;
    void Awake() { singleton = this; subscribedItemList = new List<PublishedFileId_t>(); }
    private CallResult<RemoteStoragePublishFileResult_t> RemoteStoragePublishFileResult;
    private CallResult<RemoteStorageEnumerateUserSubscribedFilesResult_t> RemoteStorageEnumerateUserSubscribedFilesResult;
    private CallResult<RemoteStorageGetPublishedFileDetailsResult_t> RemoteStorageGetPublishedFileDetailsResult;
    private CallResult<RemoteStorageDownloadUGCResult_t> RemoteStorageDownloadUGCResult;
    private CallResult<RemoteStorageUnsubscribePublishedFileResult_t> RemoteStorageUnsubscribePublishedFileResult;

    private PublishedFileId_t publishedFileID;
    private UGCHandle_t UGCHandle;

    public List<PublishedFileId_t> subscribedItemList;

    public bool fetchedContent;
    private string itemContent;


    private string lastFileName;
    void OnEnable()
    {
        RemoteStoragePublishFileResult = CallResult<RemoteStoragePublishFileResult_t>.Create(OnRemoteStoragePublishFileResult);
        RemoteStorageEnumerateUserSubscribedFilesResult = CallResult<RemoteStorageEnumerateUserSubscribedFilesResult_t>.Create(OnRemoteStorageEnumerateUserSubscribedFilesResult);
        RemoteStorageGetPublishedFileDetailsResult = CallResult<RemoteStorageGetPublishedFileDetailsResult_t>.Create(OnRemoteStorageGetPublishedFileDetailsResult);
        RemoteStorageDownloadUGCResult = CallResult<RemoteStorageDownloadUGCResult_t>.Create(OnRemoteStorageDownloadUGCResult);
        RemoteStorageUnsubscribePublishedFileResult = CallResult<RemoteStorageUnsubscribePublishedFileResult_t>.Create(OnRemoteStorageUnsubscribePublishedFileResult);
    }
   
    public string getContent()
    {
        return itemContent;
    }

    public void GetSubscribedItems()
    {
        SteamAPICall_t handle = SteamRemoteStorage.EnumerateUserSubscribedFiles(0);
        RemoteStorageEnumerateUserSubscribedFilesResult.Set(handle);
    }

    /// <summary>
    /// Gets the Item content (subscribed) to variable itemContent
    /// When done downloading, fetchedContent will be TRUE.
    /// </summary>
    /// <param name="ItemID"></param>
    public void GetItemContent(int ItemID)
    {
        fetchedContent = false;
        publishedFileID = subscribedItemList[ItemID];

        SteamAPICall_t handle = SteamRemoteStorage.GetPublishedFileDetails(publishedFileID, 0);
        RemoteStorageGetPublishedFileDetailsResult.Set(handle);
    }

    public void DeleteFile(string filename)
    {
        bool ret = SteamRemoteStorage.FileDelete(filename);
    }

    /// <summary>
    /// This functions saves a file to the workshop.
    /// Make sure file size doesn't pass the steamworks limit on your app settings.
    /// </summary>
    /// <param name="filePath">Path to the file (actual physical file) example: C:\\example_folder\\map.txt</param>
    /// <param name="workshopTitle">Workshop Item Title</param>
    /// <param name="workshopDescription">Workshop Item Description</param>
    /// <param name="tags">Tags</param>
    public void SaveToWorkshop(string filePath, string workshopTitle, string workshopDescription, string[] tags)
    {
        string fileName = Path.GetFileName(filePath);
        lastFileName = fileName;
        bool fileExists = SteamRemoteStorage.FileExists(fileName);

        if (fileExists)
        {
            Debug.Log("A file already exists with that name in the Steam Cloud!");
        }
        else
        {
            //Try to upload to Steam Cloud
            bool upload = UploadFile(filePath, fileName);

            if (!upload)
            {
                Debug.Log("Upload failed!");
            }
            else
            {
                UploadToWorkshop(fileName, workshopTitle, workshopDescription, tags);
            }
        }
    }
    private bool UploadFile(string filePath, string fileName)
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("File does not exist at the given filepath: " + filePath);
            return false;
        }

        byte[] data;
        try
        {
            data = File.ReadAllBytes(filePath);
        }
        catch (Exception e)
        {
            Debug.Log("Failed to read file at the given filepath: " + filePath);
            Debug.Log("Exception message: " + e.Message);
            return false;
        }

        if (data == null || data.Length == 0)
        {
            Debug.Log("No data read from file at the given filepath: " + filePath);
            return false;
        }

        bool ret = SteamRemoteStorage.FileWrite(fileName, data, data.Length);

        if (!ret)
        {
            Debug.Log("SteamRemoteStorage.FileWrite failed for file: " + fileName);
        }
        else
        {
            Debug.Log("File uploaded successfully to Steam Cloud: " + fileName);
        }

        return ret;
    }


    private void UploadToWorkshop(string fileName, string workshopTitle, string workshopDescription, string[] tags)
    {
        SteamAPICall_t handle = SteamRemoteStorage.PublishWorkshopFile(fileName, "Assets/StreamingAssets/Example.png", SteamUtils.GetAppID(), workshopTitle, workshopDescription, ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic, tags, EWorkshopFileType.k_EWorkshopFileTypeCommunity);
        RemoteStoragePublishFileResult.Set(handle);
    }

    public void Unsubscribe(PublishedFileId_t file)
    {
        SteamAPICall_t handle = SteamRemoteStorage.UnsubscribePublishedFile(file);
        RemoteStorageUnsubscribePublishedFileResult.Set(handle);
    }


    ///CallBacks
    ///


    void OnRemoteStorageUnsubscribePublishedFileResult(RemoteStorageUnsubscribePublishedFileResult_t pCallback, bool bIOFailure)
    {
        Debug.Log("[" + RemoteStorageUnsubscribePublishedFileResult_t.k_iCallback + " - RemoteStorageUnsubscribePublishedFileResult] - " + pCallback.m_eResult + " -- " + pCallback.m_nPublishedFileId);
    }

    void OnRemoteStoragePublishFileResult(RemoteStoragePublishFileResult_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_eResult == EResult.k_EResultOK)
        {
            publishedFileID = pCallback.m_nPublishedFileId;
            DeleteFile(lastFileName);
        }
    }

    void OnRemoteStorageEnumerateUserSubscribedFilesResult(RemoteStorageEnumerateUserSubscribedFilesResult_t pCallback, bool bIOFailure)
    {
        subscribedItemList = new List<PublishedFileId_t>();

        for (int i = 0; i < pCallback.m_nTotalResultCount; i++)
        {
            PublishedFileId_t f = pCallback.m_rgPublishedFileId[i];
            Debug.Log(f);
            subscribedItemList.Add(f);
        }
    }

    void OnRemoteStorageGetPublishedFileDetailsResult(RemoteStorageGetPublishedFileDetailsResult_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_eResult == EResult.k_EResultOK)
        {
            UGCHandle = pCallback.m_hFile;
            SteamAPICall_t handle = SteamRemoteStorage.UGCDownload(UGCHandle, 0);
            RemoteStorageDownloadUGCResult.Set(handle);
        }
    }

    void OnRemoteStorageDownloadUGCResult(RemoteStorageDownloadUGCResult_t pCallback, bool bIOFailure)
    {
        byte[] Data = new byte[pCallback.m_nSizeInBytes];
        int ret = SteamRemoteStorage.UGCRead(UGCHandle, Data, pCallback.m_nSizeInBytes, 0, EUGCReadAction.k_EUGCRead_Close);

        itemContent = System.Text.Encoding.UTF8.GetString(Data, 0, ret);

        fetchedContent = true;
        Debug.Log("content:" + itemContent);
    }
}