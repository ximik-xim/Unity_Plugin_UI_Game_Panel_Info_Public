using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Просто панель с ифнормацией(заложенной через UI)
/// (В теории можно унаследоваться или просто использовать как отдельную базовую функцию любой панели)
/// </summary>
public class UIGamePanelInfo : MonoBehaviour
{
    /// <summary>
    /// Нужен что бы менять родителей у панели
    /// И удалять потом панель при переходе на другую сцену
    /// </summary>
    [SerializeField] 
    private GameObject _parent;
    
    [SerializeField] 
    private GameObject _panelInfo;

    [SerializeField]
    private bool _isOpen = false;
    public bool IsOpen => _isOpen;

    public event Action OnUpdateStatus;
 
    [SerializeField]
    private GetDKOPatch _patchTaskData;
    private TSG_StorageKeyTaskDataMono _taskData;

    [SerializeField] 
    private GetDataSO_TSG_KeyStorageTask _keyTaskDataOpen;
    [SerializeField] 
    private GetDataSO_TSG_KeyStorageTask _keyTaskDataClose;

    
    private void Awake()
    {
        if (_patchTaskData.Init == false)
        {
            _patchTaskData.OnInit += OnInit;
            return;
        }

        GetDataDKO();
    }

    private void OnInit()
    {
        _patchTaskData.OnInit -= OnInit;
        GetDataDKO();
    }

    private void GetDataDKO()
    {
        var DKOData = (DKODataInfoT<TSG_StorageKeyTaskDataMono>)_patchTaskData.GetDKO();
        _taskData = DKOData.Data;
        
        _taskData.AddTaskData(_keyTaskDataOpen.GetData(), new TSG_StorageTaskDefaultData());
        _taskData.AddTaskData(_keyTaskDataClose.GetData(), new TSG_StorageTaskDefaultData());
    }

    public void Open()
    {
        if (_taskData.GetTaskData(_keyTaskDataOpen.GetData()).IsThereTask == false) 
        {
            OpenPanel();    
        }
    }

    public void OpenIgnoreTask()
    {
        OpenPanel();
    }

    private void OpenPanel()
    {
        _isOpen = true;
        _panelInfo.SetActive(true);
        OnUpdateStatus?.Invoke(); 
    }
    
    public void Close()
    {
        if (_taskData.GetTaskData(_keyTaskDataClose.GetData()).IsThereTask == false) 
        {
            ClosePanel();    
        }
    }
    
    public void CloseIgnoreTask()
    {
        ClosePanel();
    }
    
    private void ClosePanel()
    {
        _isOpen = false;
        _panelInfo.SetActive(false);
        OnUpdateStatus?.Invoke();
    }

    public GameObject GetParent()
    {
        return _parent;
    }

    public TSG_StorageTaskDefaultData GetTaskDataOpen()
    {
        return _taskData.GetTaskData(_keyTaskDataOpen.GetData());
    }
    
    public TSG_StorageTaskDefaultData GetTaskDataClose()
    {
        return _taskData.GetTaskData(_keyTaskDataClose.GetData());
    }

    private void OnDestroy()
    {
        _taskData.RemoveTaskData(_keyTaskDataOpen.GetData());
        _taskData.RemoveTaskData(_keyTaskDataClose.GetData());
    }
}
