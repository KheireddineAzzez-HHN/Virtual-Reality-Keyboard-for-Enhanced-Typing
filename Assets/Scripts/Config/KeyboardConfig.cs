using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardConfig : MonoBehaviour
{
     
    public static int columns = 6;
    public static int rows = 10;
    public static string keyAudioPath = "audio/";
    public static float basicWeightKeypart = 0.1f;
    public static Color startColor = Color.blue;
    public static Color endColor = new Color(0.5f, 0, 0.5f);
    public static float keyPatNormalWeightColorMultplicator = 1f;
    public static float keyPartBaseWeightColorMultiplicator = 7f;
    public enum KeyboardType
    {
        Controllers_With_Keyboard,
        Gloves_With_keyboard,
    }

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }


    public enum keyStatus
    {

        PRESSED,
        REALESED

    }

    public enum env_data_collection
    {

        Test,
        Prod,
        USERCONFIG,
            WAITING
    }
    public enum KeyNames
    {
        KEY_0,
        KEY_1,
        KEY_2,
        KEY_3,
        KEY_4,
        KEY_5,
        KEY_6,
        KEY_7,
        KEY_8,
        KEY_9,
        key_a,
        key_b,
        key_c,
        key_comma,
        key_d,
        key_delete,
        key_e,
        key_f,
        key_g,
        key_h,
        key_i,
        key_j,
        key_k,
        key_l,
        key_m,
        key_n,
        key_o,
        key_p,
        key_point,
        key_q,
        key_r,
        key_return,
        key_s,
        key_shift,
        key_space,
        key_switch,
        key_t,
        key_u,
        key_v,
        key_w,
        key_x,
        key_y,
        key_z,
        left_arrow,
        right_arrow,
        up_arrow,
        down_arrow
    }

    public enum KeyPartNames
    {
        bottom_edge_l1,
        bottom_edge_l2,
        bottom_edge_l3,
        bottom_edge_l4,
        bottom_edge_left,
        bottom_edge_r1,
        bottom_edge_r2,
        bottom_edge_r3,
        bottom_edge_r4,
        bottom_edge_right,
        bottom_l1,
        bottom_l2,
        bottom_l3,
        bottom_l4,
        bottom_l5,
        bottom_l6,
        bottom_l7,
        bottom_l8,
        bottom_l9,
        bottom_l10,
        bottom_l11,
        bottom_l12,
        bottom_l13,
        bottom_l14,
        bottom_l15,
        bottom_r1,
        bottom_r2,
        bottom_r3,
        bottom_r4,
        bottom_r5,
        bottom_r6,
        bottom_r7,
        bottom_r8,
        bottom_r9,
        bottom_r10,
        bottom_r11,
        bottom_r12,
        bottom_r13,
        bottom_r14,
        bottom_r15,
        Frame,
        top_edge_l1,
        top_edge_l2,
        top_edge_l3,
        top_edge_l4,
        top_edge_left,
        top_edege_r1,
        top_edge_r2,
        top_edge_r3,
        top_edge_r4,
        top_edge_right,
        top_l1,
        top_l2,
        top_l3,
        top_l4,
        top_l5,
        top_l6,
        top_l7,
        top_l8,
        top_l9,
        top_l10,
        top_l11,
        top_l12,
        top_l13,
        top_l14,
        top_l15,
        top_r1,
        top_r2,
        top_r3,
        top_r4,
        top_r5,
        top_r6,
        top_r7,
        top_r8,
        top_r9,
        top_r10,
        top_r11,
        top_r12,
        top_r13,
        top_r14,
        top_r15
    }

 

    public enum KeyboardInteractiveTag
    {

        Thumb_cap_right,
        Thumb_cap_left
    }

    public enum KeySoundFiles
    {
        _click_key
    }

    public enum RayCast {

        RAYCASTENTER,
        RAYCASTEXIT,


    }

    public enum MaskLayers
    {

        KEYPARTLAYER
    }
    public static HashSet<KeyPartNames> KeypartsBaseWeight = new HashSet<KeyPartNames>
        {
            KeyPartNames.bottom_edge_l1,
            KeyPartNames.bottom_edge_l2,
            KeyPartNames.bottom_edge_l3,
            KeyPartNames.bottom_edge_l4,
            KeyPartNames.bottom_edge_left,
            KeyPartNames.bottom_edge_r1,
            KeyPartNames.bottom_edge_r2,
            KeyPartNames.bottom_edge_r3,
            KeyPartNames.bottom_edge_r4,
            KeyPartNames.bottom_edge_right,
            KeyPartNames.top_edge_l1,
            KeyPartNames.top_edge_l2,
            KeyPartNames.top_edge_l3,
            KeyPartNames.top_edge_l4,
            KeyPartNames.top_edge_left,
            KeyPartNames.top_edege_r1,
            KeyPartNames.top_edge_r2,
            KeyPartNames.top_edge_r3,
            KeyPartNames.top_edge_r4,
            KeyPartNames.top_edge_right,
            KeyPartNames.Frame
        };

    
}
