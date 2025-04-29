using UnityEngine; 
using TListPlugin; 
[System.Serializable]
public class IdentifierAndData_KeyUIGamePanelInfo : AbsIdentifierAndData<IndifNameSO_KeyUIGamePanelInfo, string, KeyUIGamePanelInfo>
{

 [SerializeField] 
 private KeyUIGamePanelInfo _dataKey;


 public override KeyUIGamePanelInfo GetKey()
 {
  return _dataKey;
 }
}
