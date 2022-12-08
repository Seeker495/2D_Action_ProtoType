using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CanBreakInfo", menuName = "Create_CanBreakInfo")]
public class CanBreakInfo : ScriptableObject
{
    [SerializeField]
    private int m_hp;
    [SerializeField]
    private List<CanBreakObjectInfo> m_dropItems;
    [SerializeField]
    private Sprite m_sprite;

    public int HP => m_hp;
    public List<CanBreakObjectInfo> DropItems => m_dropItems;
    public Sprite Sprite => m_sprite;

}
