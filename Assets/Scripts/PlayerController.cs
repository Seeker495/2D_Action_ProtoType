using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.VersionControl;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/*******************************************************************
 *  <概要>
 *  ユーザーのコントローラークラス。
 *  <仕組み>
 *  ボタンに応じて行われるアクションが異なるため,
 *  インスペクタで対応するアクションを対応させたりする。
 *******************************************************************/
public class PlayerController
{
    public static ControlActions Controller = new ControlActions();
}
