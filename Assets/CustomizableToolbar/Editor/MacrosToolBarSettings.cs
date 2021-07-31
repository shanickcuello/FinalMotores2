using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class MacrosToolSettingsData
{
    [SerializeField] private string m_commandName = null;
    [SerializeField] private string m_buttonName = null;
    [SerializeField] private Texture m_image = null;
    [SerializeField] private int m_width = 0;

    public string CommandName { get { return m_commandName; } }
    public string ButtonName { get { return m_buttonName; } }
    public Texture Image { get { return m_image; } }
    public int Width { get { return m_width; } }

    public bool IsValid { get { return !string.IsNullOrEmpty(m_commandName); } }
}

public sealed class MacrosToolBarSettings : ScriptableObject
{
    [SerializeField] private MacrosToolSettingsData[] m_list = null;

    public IList<MacrosToolSettingsData> List { get { return m_list; } }
}
