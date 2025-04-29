using System;
using UnityEngine;

public class SetAwakeUIGamePanelInfoInStorage : MonoBehaviour
{
    private bool _isInit = false;
    public bool IsInit => _isInit;
    public event Action OnInit;
    
    [SerializeField]
    private GetDKOPatch _patchStorageGamePanelInfo;

    [SerializeField] 
    private GetDataSO_KeyUIGamePanelInfo _keyPanel;
    [SerializeField]
    private UIGamePanelInfo _panelInfo;
    
    private void Awake()
    {
        if (_patchStorageGamePanelInfo.Init == false)
        {
            _patchStorageGamePanelInfo.OnInit += OnInitStoragePanel;
            return;
        }

        GetDataDKO();
    }

    private void OnInitStoragePanel()
    {
        _patchStorageGamePanelInfo.OnInit -= OnInitStoragePanel;
        GetDataDKO();
    }

    private void GetDataDKO()
    {
        var DKOData = (DKODataInfoT<UIGamePanelInfoStorage>)_patchStorageGamePanelInfo.GetDKO();
        UIGamePanelInfoStorage taskInfo = DKOData.Data;
        taskInfo.AddPanel(_keyPanel.GetData(), _panelInfo);

        _isInit = true;
        OnInit?.Invoke();
    }
}
